using CbaSodiq.Core.Models;
using CbaSodiq.Data.Repositories;
using CbaSodiq.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trx.Messaging;
using Trx.Messaging.Channels;
using Trx.Messaging.FlowControl;
using Trx.Messaging.Iso8583; 

namespace CbaProcessor
{
    public class CbaListener
    {
        //List<AtmTerminal> ourTerminals;
        public enum MessageSource
        {
            OnUs, RemoteOnUs, NotOnUs
        }

        UtilityLogic utility = new UtilityLogic();
        static void Listener_Receive(object sender, ReceiveEventArgs e)
        {
            try
            {
                UtilityLogic.LogMessage("Message received!");
                var client = sender as ClientPeer;
                Iso8583Message msg = e.Message as Iso8583Message;
                switch (GetTransactionSource(msg))
                {
                    case MessageSource.OnUs:
                        msg = TransactionManager.ProcessMessage(msg, MessageSource.OnUs);
                        break;
                    case MessageSource.RemoteOnUs:
                        msg = TransactionManager.ProcessMessage(msg, MessageSource.RemoteOnUs);
                        //do nothing yet
                        break;
                    case MessageSource.NotOnUs:
                        //redirect to interswitch
                        msg.Fields.Add(39, "31");   //bank not supported
                        break;
                    default:
                        break;
                }

                PeerRequest request = new PeerRequest(client, msg);
                request.Send();
                client.Close();
                client.Dispose();
            }
            catch (Exception ex)
            {
                UtilityLogic.LogError("Error processing the incoming meaasgae");
                UtilityLogic.LogError("Message: " + ex.Message + " \t InnerException " + ex.InnerException);
            }
        }

        static MessageSource GetTransactionSource(Iso8583Message msg)
        {
            string ourBIN = "519894";
            //string ourTerminalId = "60";
            string msgBin = msg.Fields[2].ToString().Substring(0, 6);   //first 6 digits of the card PAN
            string msgTerminalId = msg.Fields[41].ToString();  //.Substring(2, 2);   //the third and first digits

            if (msgBin.Equals(ourBIN) && IsValidTerminal(msgTerminalId))   //our terminal, our card
            {
                return MessageSource.OnUs;
            }
            else if (msgBin.Equals(ourBIN))     //our card, not our terminal
            {
                return MessageSource.RemoteOnUs;
            }
            else
            {
                return MessageSource.NotOnUs;
            }
        }

        static bool IsValidTerminal(string terminalCode)
        {
            return Processor.OurTerminals.Any(t => t.Code == terminalCode);
        }

        

       

        static void Listener_Connected(object sender, EventArgs e)
        {
            var listener = sender as ListenerPeer;
            UtilityLogic.LogMessage(listener.Name + " is now connected");
            //Console.WriteLine("Client Connected!");
        }
        static void Listener_Disconnected(object sender, EventArgs e)
        {
            var listener = sender as ListenerPeer;
            UtilityLogic.LogMessage(listener.Name + " is disonnected");
            //Console.WriteLine("Client Disconnected!");
        }

        public static void StartUpListener(string name, string hostName, int port)     //create conn
        {
            Trx.Messaging.FlowControl.TcpListener tcpListener = new Trx.Messaging.FlowControl.TcpListener(port);
            tcpListener.LocalInterface = hostName;
            ListenerPeer listener = new ListenerPeer(name,
                     new TwoBytesNboHeaderChannel(new Iso8583Ascii1987BinaryBitmapMessageFormatter()),
                     new BasicMessagesIdentifier(11, 41),
                     tcpListener);
            listener.Receive += new PeerReceiveEventHandler(Listener_Receive);
            listener.Connected += new PeerConnectedEventHandler(Listener_Connected);
            listener.Disconnected += new PeerDisconnectedEventHandler(Listener_Disconnected);
            listener.Connect();

            //Console.WriteLine("Waiting for connection...");
        }
    }
}

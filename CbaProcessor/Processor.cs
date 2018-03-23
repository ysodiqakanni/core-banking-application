using CbaSodiq.Core.Models;
using CbaSodiq.Data.Repositories;
using CbaSodiq.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaProcessor
{
    public class Processor
    {
        public static List<AtmTerminal> OurTerminals = new AtmTerminalRepo().GetAll();
        //UtilityLogic utility = new UtilityLogic();
        public void BeginProcess()
        {
            UtilityLogic.LogMessage("Initializing nodes...");
            var nodes = new NodeRepository().GetAll();
            if (nodes == null || nodes.Count() < 1)
            {
                UtilityLogic.LogError("No node is configured!");
            }
            else
            {
                foreach (var node in nodes)
                {
                    try
                    {
                        CbaListener.StartUpListener(node.ID.ToString(), node.HostName, Convert.ToInt32(node.Port));
                        UtilityLogic.LogMessage(node.Name + " now listening on port " + node.Port);
                    }
                    catch (Exception ex)
                    {
                        UtilityLogic.LogError("Message: " + ex.Message + " \t InnerException " + ex.InnerException);
                    }
                }
            }            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CbaService
{
    public partial class CbaService : ServiceBase
    {
        //private System.ComponentModel.IContainer components;
        //private System.Diagnostics.EventLog eventLog1;
        int eventId = 0;
        public CbaService()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";


        }
        public CbaService(string[] args) 
        { 
            InitializeComponent(); 
            string eventSourceName = "MySource"; 
            string logName = "MyNewLog"; 
            if (args.Count() > 0) 
            { 
                eventSourceName = args[0]; 
            } 
            if (args.Count() > 1) 
            { 
                logName = args[1]; 
            } 
            eventLog1 = new System.Diagnostics.EventLog(); 
            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName)) 
            { 
                System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
            }
            eventLog1.Source = eventSourceName; eventLog1.Log = logName; 
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Event started at: "+DateTime.Now);

            // Set up a timer to trigger every minute.
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 20 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Event stopped at: " + DateTime.Now);
        }
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }
    }
}

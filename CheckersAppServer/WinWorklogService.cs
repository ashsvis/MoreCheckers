using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace CheckersAppServer
{
    partial class WinCheckersService : ServiceBase
    {
        private WcfCheckersService _wcf;

        public WinCheckersService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _wcf = WcfCheckersService.Service;
                _wcf.Start();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Information);
            }
        }

        protected override void OnStop()
        {
            if (_wcf != null)
            {
                _wcf.Stop();
            }
        }
    }
}

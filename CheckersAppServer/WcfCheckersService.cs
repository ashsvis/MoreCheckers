using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace CheckersAppServer
{
    /*
    * Класс реализации запуска WCF-сервиса. 
    * Реализован с использованием шаблона Singleton
    */
    public sealed class WcfCheckersService
    {
        private readonly TimeSpan _timeout = new TimeSpan(0, 1, 30);
        private static WcfCheckersService _wcfService;
        private readonly ServiceHost _svcHost;

        public static WcfCheckersService Service
        {
            get
            {
                _wcfService = _wcfService ?? new WcfCheckersService();
                return _wcfService;
            }
        }

        // Конструктор по умолчанию определяется как private
        private WcfCheckersService()
        {
            // Регистрация сервиса и его метаданных
            _svcHost = new ServiceHost(typeof(CheckersService),
                                       new[]
                                           {
                                               new Uri("net.pipe://localhost/CheckersAppServer"),
                                               new Uri("net.tcp://localhost:5528/CheckersAppServer")
                                           });
            _svcHost.AddServiceEndpoint(typeof(ICheckersService),
                                        new NetNamedPipeBinding(), "");
            _svcHost.AddServiceEndpoint(typeof(ICheckersService),
                                        new NetTcpBinding
                                        {
                                            OpenTimeout = _timeout,
                                            SendTimeout = _timeout,
                                            ReceiveTimeout = _timeout,
                                            CloseTimeout = _timeout
                                        }, "");
            var behavior = new ServiceMetadataBehavior();
            _svcHost.Description.Behaviors.Add(behavior);
            _svcHost.AddServiceEndpoint(typeof(IMetadataExchange),
                                        MetadataExchangeBindings.CreateMexNamedPipeBinding(), "mex");
            _svcHost.AddServiceEndpoint(typeof(IMetadataExchange),
                                        MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
        }

        public void Start()
        {
            _svcHost.Open();
        }

        public void Stop()
        {
            _svcHost.Close();
        }
    }
}

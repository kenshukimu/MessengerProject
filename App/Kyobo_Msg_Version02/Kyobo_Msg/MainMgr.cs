using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Kyobo_Msg_Server
{
    public sealed class MainMgr
    {
        public static MainMgr Instance { get; } = new MainMgr();

        MainMgr() { }

        ServerMain frmMain;
        ServiceHost host;

        public static ServerMain GetFrmMain() { return Instance.frmMain; }

        public void Init()
        {
            WcfService();
            this.frmMain = new ServerMain();

            //ReadRegConfig();
            //ReadSQLite("Data Source=APCExam.db;Pooling=true;FailIfMissing=false");
            //FirstReadExamData();            
            Console.WriteLine("MainProgram Init Server");
        }

        private void WcfService()
        {
            //Uri baseUri = new Uri("http://localhost:5443/WcfService");
            //using (ServiceHost serviceHost = new ServiceHost(typeof(WcfService), baseUri))
            //{
            //    //Add Service Endpoint
            //    serviceHost.AddServiceEndpoint(typeof(WcfIService), new BasicHttpBinding(), "");

            //    //Add Metadata Behavior
            //    ServiceMetadataBehavior mexBehavior = new ServiceMetadataBehavior();
            //    mexBehavior.HttpGetEnabled = true;
            //    serviceHost.Description.Behaviors.Add(mexBehavior);

            //    serviceHost.Open();
            //}
            try
            {
                // Address 
                //string address = "net.tcp://10.65.21.157:5443/WcfService";
                //string address = "net.tcp://localhost:5443/WcfService";
                //string address = "net.tcp://52.141.56.159:5443/WcfService";
                string address = "net.tcp://10.0.0.4:5443/WcfService";

                NetTcpBinding netTcpBinding = new NetTcpBinding();
                netTcpBinding.Security.Mode = SecurityMode.None;
                
                // Binding : TCP 사용
                NetTcpBinding binding = netTcpBinding;
                // Service Host 만들기
                host = new ServiceHost(typeof(WcfService));
                // End Point 추가
                host.AddServiceEndpoint(typeof(WcfIService), binding, address);
                // Service Host 시작
                host.Open();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

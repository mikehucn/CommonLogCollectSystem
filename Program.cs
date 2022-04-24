using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;
using StrategyMonitorCenter.Helper;
using log4net;
using EdgeJs;
using WcfStrategyContract;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace StrategyMonitorCenter
{
    static class Program 
    {
        static ILog log = LogManager.GetLogger(typeof(Program));
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          
            try
            {
                Task.Factory.StartNew(new Action(() =>
                {
                    try
                    {
                        StartLocalWS();
                        ServiceStart();
                       
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                        log.Error(ex.StackTrace);
                         
                    }
                   
                }));
                Application.ThreadException += (sender, e) =>
                {

                    log.Error(e.Exception.Message);
                    log.Error(e.Exception.StackTrace);
                };
                AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                {
                    Exception ex = (e.ExceptionObject as Exception);
                    log.Error(ex.Message);
                    log.Error(ex.StackTrace);

                };
                Application.Run(new XtraFormHostMain());
               

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
            }
          
        }
        static void ServiceStart()
        {
            Uri address = new Uri(string.Format("net.tcp://{0}:{1}/ServiceDataExchange",
                ConfigurationManager.AppSettings["wcfTcpAddress"].Trim().ToString(),
                ConfigurationManager.AppSettings["wcfPort"].Trim().ToString()));
            //ServiceDataExchange server = new ServiceDataExchange();
            //GlobalHelper.GetInstance().ServerMain = server;
            using (ServiceHost host = new ServiceHost(typeof(ServiceDataExchange)))
            {
                try
                {
                    host.Open();
                    while (true)
                    {
                        Application.DoEvents();
                        Thread.Sleep(1); 
                    }
                }
                catch (Exception ex)
                {

                    
                }
             
                Console.WriteLine("服务已经启动！");
                Console.Read();

            }
        }


        static async void StartLocalWS()
        {
            // Define an event handler to be called for every message from the client
            try
            {
                var onMessage = (Func<object, Task<object>>)(async (message) =>
                {
                     
                    if (message.ToString().Contains("TableName"))
                    {
                        //DealMarketData(message.ToString());
                        //GlobalHelper.SendLog(message.ToString(), "", "candles");
                        GlobalHelper.GetInstance().OnOtherDataArrived(new List<StrategyData>() {

                        JsonConvert.DeserializeObject<StrategyData>(message.ToString())  });
                    } 
                    if(message is System.Dynamic.ExpandoObject)
                    {

                        GlobalHelper.GetInstance().OnOtherDataArrived(new List<StrategyData>() { JsonConvert.DeserializeObject<StrategyData>(JsonConvert.SerializeObject(message)) });
                    }
                    return "";
                });




                // The WebSocket server delegates handling of messages from clients
                // to the supplied .NET handler

                var js = string.Format(@"  
 
                 return function(options, cb) [
                    var httpServer = require('http').createServer();
                    var io = require('{0}socket.io')(httpServer);
                    httpServer.listen({1}); 
                    io.on('connection', function(ws) [
                        ws.on('message', function(message) [
                            options.onMessage(message, function(error, result) [
                                //if (error) throw error;
                                //ws.emit('servercall',result);
                            ]);
                        ]); 
                      
                    ]);
                    cb();
                ];
                ", ConfigurationManager.AppSettings["LWSPath"].Trim().ToString()  , ConfigurationManager.AppSettings["LWSListenPort"].Trim().ToString()).Replace("[", "{").Replace("]", "}").Trim();

                var createWebSocketServer = Edge.Func(js);

                // Create a WebSocket server on a specific TCP port and using the .NET event handler

                await createWebSocketServer(new
                {
                    port = int.Parse(ConfigurationManager.AppSettings["LWSPort"].Trim().ToString()),
                    onMessage = onMessage
                });
            }
            catch (Exception ex)
            {

                log.Error(ex.Message);
                log.Error(ex.StackTrace);
            }


        }

    }


}

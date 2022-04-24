using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfServiceStrategyMonitor;
using WcfStrategyContract;

namespace NETDemo
{
    public partial class Form1 : Form, ICallBack
    {
        IDataExchange iExchangeProxy = null;
        bool isWCFConnected = false;
        int iConnectCount = 0;
        public Form1()
        {
            InitializeComponent();
            this.Load += (s, e) => ConnectWcfServer();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            iExchangeProxy.OtherDataTrasfer(new List<StrategyData>() {

                new StrategyData()
            {
                Content = "Test function ...",
                Name = "Test name",
                TableName = $"{ConfigurationManager.AppSettings["LocalHostName"]}," + "TestTab1",
                Time = DateTime.Now.ToString("MM:dd HH:mm:ss:fff"),
                Id = 1234,
                Type = "Test Type"
            } });

        }

        /// <summary>
        /// Used to auto connect WCF server.
        /// </summary>
        void ConnectWcfServer()
        {
            Task.Factory.StartNew(new Action(() =>
            {

                while (true)
                {
                    try
                    {

                        InstanceContext context = new InstanceContext(this);
                        iExchangeProxy = new ClientProxy(context);
                        var reponse = iExchangeProxy.HandShake("hi", ConfigurationManager.AppSettings["LocalHostName"]);
                        this.Invoke(new Action(() => this.Text += reponse));
                        if (!string.IsNullOrEmpty(reponse))
                        {


                            isWCFConnected = true;
                            iConnectCount = 0;
                            Task.Factory.StartNew(new Action(() =>
                            {

                                while (true)
                                {
                                    try
                                    {
                                        var reponse2 = iExchangeProxy.HandShake("hi", ConfigurationManager.AppSettings["LocalHostName"]);

                                        if (!string.IsNullOrEmpty(reponse2))
                                        {
                                            this.BeginInvoke(new Action(() => this.Text = reponse2));
                                            isWCFConnected = true;
                                            iConnectCount = 0;
                                        }

                                        Thread.Sleep(1000);
                                    }
                                    catch (Exception ex)
                                    {

                                        ConnectWcfServer();
                                        break;
                                    }

                                }

                            }));
                            break;
                        }

                    }
                    catch (Exception ex)
                    {
                        isWCFConnected = false;

                        if (iConnectCount < 12)
                        {
                            Thread.Sleep(5000);
                        }
                        else
                        {

                            Thread.Sleep(1000 * 60 * 2);
                        }
                        iConnectCount++;
                    }
                }


            }));


        }

        public void HandShakeResponse()
        {
            Console.WriteLine("server call back .");
            //DONOT DELETE
        }
        public void ServerParamsUpdatedResponse(List<TempStrategyParamUpdateObject> listdata, string clientTableName)
        {
            //DONOT DELETE

        }
        public void ServerRequestParamsResponse(string clientTableName)
        {
            //DONOT DELETE

        }
    }


}

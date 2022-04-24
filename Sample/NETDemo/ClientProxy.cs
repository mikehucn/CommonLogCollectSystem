using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using WcfStrategyContract;
using System.Windows;
using System.Windows.Forms;

namespace WcfServiceStrategyMonitor
{
    public class ClientProxy : DuplexClientBase<IDataExchange>, IDataExchange
    {
        public ClientProxy(InstanceContext instanceContext)
            : base(instanceContext)
        {


        }

        #region IDataExchange 成员
        public string HandShake(string txt, string clientName)
        {

            return Channel.HandShake(txt, clientName);
        }

        public void ClientParamsTrasfer(List<TempStrategyParamUpdateObject> listdata, string clientTableName)
        {
             Channel.ClientParamsTrasfer(listdata, clientTableName);
        }
        public object StrategyDataTrasfer(StrategyData b)
        {
            try
            {
                return Channel.StrategyDataTrasfer(b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);

            }
            return null;
        }

        public void OtherDataTrasfer(List<StrategyData> b)
        {
            Channel.OtherDataTrasfer(b);
        }
        #endregion
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Log4NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            //记录日志
            ILog logWrite = log4net.LogManager.GetLogger("Log4NetDemoWrite");

            /*
            logWrite.Debug("Debug级别");//调试
            logWrite.Error("Error级别");//异常
            */


        }
    }
}

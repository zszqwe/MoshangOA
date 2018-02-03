using log4net;

namespace Moshang.OA.Common
{
    public class Log4NetWriter:ILogwriter
    {
        public void WriteLogInfo(string txt)
        {
            ILog logWrite = log4net.LogManager.GetLogger("demo");
            logWrite.Error(txt);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moshang.OA.Common
{
    public delegate void WriteLogDal(string str);

    public class LogHelper
    {
        public static Queue<string> ExceptionStringQueue=new Queue<string>();

        public static List<ILogwriter>LofWriterList=new List<ILogwriter>();
        public static WriteLogDal WriteLogDalFunc;
        
        public LogHelper()
        {
            //WriteLogDalFunc=new WriteLogDal(WriteLogFile);
            //WriteLogDalFunc += WriteLogMongodb;
            //从队列中获取错误信息 写到日志中

            LofWriterList.Add(new TextFileWriter());
            LofWriterList.Add(new SqlServerWriter());

            ThreadPool.QueueUserWorkItem(o =>
            {
                lock (ExceptionStringQueue)
                {
                    string strerror = ExceptionStringQueue.Dequeue();

                    //WriteLogDalFunc(strerror);
                    //ILogwriter writer = new TextFileWriter();
                    //writer.WriteLogInfo(strerror);

                    foreach (var LogWriter in LofWriterList)
                    {
                        LogWriter.WriteLogInfo(strerror);
                    }

                }

            });
        }
        
        /*
        public static void WriteLogFile(string txt)
        {
            
        }
        
        public static void WriteLogMongodb(string txt)
        {
            
        }
        */

        public static void WriteLog(string exceptionText)
        {
            lock (ExceptionStringQueue)
            {
                ExceptionStringQueue.Enqueue(exceptionText);
            }
        }
    }
    
}

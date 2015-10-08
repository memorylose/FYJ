using FYJ.Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.Utility
{
    public class FileLog : ILog
    {
        public void WriteLog(string logType, string content)
        {
            //log path
            string logPath = LogUtility.GetLogPath();

            //check log exist
            FileInfo finfo = new FileInfo(logPath);
            if (!finfo.Exists)
            {
                FileStream fs = finfo.Create();
                fs.Close();
            }

            using (FileStream fs = finfo.OpenWrite())
            {
                StreamWriter w = new StreamWriter(fs);
                w.BaseStream.Seek(0, SeekOrigin.End);
                w.Write("----------------------- {0} -----------------------", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                w.Write("\r\n{0}:", logType);
                w.Write(content + "\r\n");
                w.Flush();
                w.Close();
            }
        }
    }

    public class Log
    {
        public static void Error(string content)
        {
            ILog _log = new FileLog();
            _log.WriteLog(LogUtility.MessageType.Error.ToString(), content);
        }

        public static void Info(string content)
        {
            ILog _log = new FileLog();
            _log.WriteLog(LogUtility.MessageType.Information.ToString(), content);
        }
    }
}

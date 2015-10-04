using FYJ.Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYJ.Utility
{
    public class Log
    {
        private static string GetLogPath()
        {
            string logPath = "~/" + LogPath.LOG_FOLDER_PATH;
            string folderPath = string.Empty;
            FileOperator.CheckFileFolder(logPath, ref folderPath);
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            return folderPath + fileName;
        }

        private enum MessageType
        {
            Unknown,
            Information,
            Warning,
            Error,
            Success
        }

        private void WriteLog(string logType, string content)
        {
            //log path
            string logPath = Log.GetLogPath();

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

        public static void Error(string content)
        {
            Log _log = new Log();
            _log.WriteLog(MessageType.Error.ToString(), content);
        }
    }
}

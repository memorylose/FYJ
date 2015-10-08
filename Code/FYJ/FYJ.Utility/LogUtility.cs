using FYJ.Constant;
using System;
using System.IO;

namespace FYJ.Utility
{
    public class LogUtility
    {
        public static string GetLogPath()
        {
            string logPath = "~/" + LogPath.LOG_FOLDER_PATH;
            string folderPath = string.Empty;
            string datePath = string.Empty;
            FileOperator.CheckLogFileFolder(logPath, ref folderPath);
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            return folderPath + fileName;
        }

        public enum MessageType
        {
            Unknown,
            Information,
            Warning,
            Error,
            Success
        }
    }
}

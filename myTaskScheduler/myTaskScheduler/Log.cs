/*
 * Name: Log.cs
 * Purpose: 提供寫入Log功能
 * DateTime: 2015/07/02
 * Author: Chi-Hsu Chen
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myTaskScheduler
{
    class Log
    {        
        private const string _LOG_FILE_EXT = ".log";
        private const string _24H_TIME_FORMAT = "yyyy/MM/dd HH:mm:ss";
        private const string _YYYYMMDD = "yyyyMMdd";

        private string logPath;
        private System.IO.StreamWriter oLogger;

        public Log(string lsLogPath)
        {
            string lsLogFileFullFileName;
            try
            {
                logPath = lsLogPath;
                checkLogFolder(lsLogPath);
                lsLogFileFullFileName = logPath + "\\" + getLogFileName() + _LOG_FILE_EXT;
                oLogger = new System.IO.StreamWriter(lsLogFileFullFileName, true, Encoding.Default);

                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteLog(string lsMsg)
        {
            try
            {
                string lsLogFileFullFileName;

                if (getLogFileName() != DateTime.Now.ToString(_YYYYMMDD))
                {
                    oLogger.Close();
                    lsLogFileFullFileName = logPath + "\\" + getLogFileName() + _LOG_FILE_EXT;
                    oLogger = new System.IO.StreamWriter(lsLogFileFullFileName, true, Encoding.Default);
                }

                oLogger.WriteLine(DateTime.Now.ToString(_24H_TIME_FORMAT) + " " + lsMsg);
                oLogger.Flush();

                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getLogFileName()
        {
            try
            {
                return DateTime.Now.ToString(_YYYYMMDD);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void checkLogFolder(string lsLogPath)
        {
            try
            {
                if (System.IO.Directory.Exists(lsLogPath) == false)
                    System.IO.Directory.CreateDirectory(lsLogPath);
                                
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

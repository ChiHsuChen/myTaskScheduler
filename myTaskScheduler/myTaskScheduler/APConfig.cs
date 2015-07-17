/*
 * Name: myAPConfig.cs
 * Purpose: 存放AP讀入的config value
 * DateTime: 2015/07/02
 * Author: Chi-Hsu Chen
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myTaskScheduler
{
    class APConfig
    {
        // ini基本設定flag
        private List<BasicTask> myConfig = new List<BasicTask>();
        private Boolean lsAutoStart = false;
        private const char _INI_COL_DELIMITER = ',';
        private const char _INI_CONFIGITEM_DELIMITER = '=';
        private string INIPath;

        public APConfig(string lsINIPath)
        {
            INIPath = lsINIPath;
            load(lsINIPath);
        }

        private void load(string lsINIPath)
        {
            try
            {
                System.IO.StreamReader oINIReader;
                string tmpLine;
                string[] lsItem;
                string[] lsValue;
                BasicTask tmpConfig = new BasicTask();

                tmpConfig.lsAPName = "";
                tmpConfig.lsFilePath = "";
                tmpConfig.lsInterval = "";

                // 如無設定檔，則不填入設定值
                if (System.IO.File.Exists(lsINIPath) != true)
                    return;

                oINIReader = new System.IO.StreamReader(lsINIPath, Encoding.Default);

                // INI content sample
                // AP1=TESTSENDER.bat,C:\test\test.bat, 10
                while (oINIReader.Peek() >= 0)
                {
                    tmpLine = oINIReader.ReadLine();
                    lsItem = tmpLine.Split(_INI_CONFIGITEM_DELIMITER);

                    // to read autostart
                    switch (lsItem[0].ToLower())
                    {
                        case "autostart":
                            if (lsItem[1].ToLower() == "true") lsAutoStart = true;
                            break;

                    }

                    // to read Task info
                    if (lsItem[0].StartsWith("AP") == true)
                    {
                        lsValue = lsItem[1].Split(_INI_COL_DELIMITER);
                        tmpConfig.lsAPName = "";
                        tmpConfig.lsFilePath = "";
                        tmpConfig.lsInterval = "";
                        tmpConfig.lsWorkingDir = "";
                        tmpConfig.lsArguments = "";

                        for (int i = 0; i <= lsValue.GetLength(0) - 1; i++)
                        {
                            if (i == 0) tmpConfig.lsAPName = lsValue[i].ToString();
                            else if (i == 1) tmpConfig.lsFilePath = lsValue[i].ToString();
                            else if (i == 2) tmpConfig.lsInterval = lsValue[i].ToString();
                            else if (i == 3) tmpConfig.lsWorkingDir = lsValue[i].ToString();
                            else if (i == 4) tmpConfig.lsArguments = lsValue[i].ToString();
                        }
                        myConfig.Add(tmpConfig);
                    }
                }
                oINIReader.Close();

                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BasicTask> getConfig()
        {
            return myConfig; 
        }

        public Boolean AutoStart
        {
            get { return lsAutoStart; }
            set { lsAutoStart = value; }
        }

        public void ReloadTaskFromINI()
        {
            myConfig.Clear();
            load(INIPath);
            return;
        }
    }
}

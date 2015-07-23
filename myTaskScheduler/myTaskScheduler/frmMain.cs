/*
 * Name: Task Scheduler
 * Purpose: can execute some executables periodically
 * Date: 20150509
 * Author: Chi-Hsu Chen
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace myTaskScheduler
{
    public partial class frmMain : Form
    {
        // columns defined for a task
        private readonly string[] _COL_TASK_LIST = { "AppName", "FilePath", "Interval(Min.)", "WorkingDir", "Arguments", "LastRunTime", "NextRunTime", "Status", "Enabled" };

        // column position initialization
        private const int _COL_TASK_APPNAME = 0;
        private const int _COL_TASK_FILEPATH = 1;
        private const int _COL_TASK_INTERVAL = 2;
        private const int _COL_TASK_WORKINGDIR = 3;
        private const int _COL_TASK_ARGUMENTS = 4;
        private const int _COL_TASK_LASTRUNTIME = 5;
        private const int _COL_TASK_NEXTRUNTIME = 6;
        private const int _COL_TASK_STATUS = 7;
        private const int _COL_TASK_ENABLED = 8;
        private const string _INI_FILE = "myTaskScheduler.ini";
        private const string _24H_TIME_FORMAT = "yyyy/MM/dd HH:mm:ss";

        private List<BasicTask> lsTaskList;   // list that stored tasks
        private Log myLog;
        private Boolean lsToken;

        private APConfig lsConfig;
        private string lsINIPath;

        private void showErrorMessage(Exception ex)
        {
            MessageBox.Show(ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            return;
        }

        private void makeHeader(DataTable lsTable, string[] lsColumns)
        {
            try
            {
                DataColumn lsCol;
                
                if (lsTable.Columns.Count == 0)
                    lsTable.Columns.Clear();

                foreach (string s in lsColumns)
                {
                    lsCol = new DataColumn();
                    lsCol.DataType = System.Type.GetType("System.String");
                    lsCol.ColumnName = s;
                    lsTable.Columns.Add(lsCol);
                    lsCol.Dispose();
                }

                return;
            }
            catch (Exception ex) 
            {
                showErrorMessage(ex);
            }
        }

        private void loadTaskInfoIntoGrid(DataGridView lsDataGrid)
        {
            DataTable lsTaskView = new DataTable();
            DataRow lsRow;
            
            try
            {
                // init table for task info.
                makeHeader(lsTaskView, _COL_TASK_LIST);

                foreach (BasicTask T in lsTaskList)
                {
                    lsRow = lsTaskView.NewRow();
                    lsRow[_COL_TASK_APPNAME] = T.lsAPName;
                    lsRow[_COL_TASK_FILEPATH] = T.lsFilePath;
                    lsRow[_COL_TASK_INTERVAL] = T.lsInterval;
                    lsRow[_COL_TASK_WORKINGDIR] = T.lsWorkingDir;
                    lsRow[_COL_TASK_ARGUMENTS] = T.lsArguments;
                    lsRow[_COL_TASK_LASTRUNTIME] = DateTime.Now.ToString(_24H_TIME_FORMAT);
                    lsRow[_COL_TASK_NEXTRUNTIME] = (DateTime.Parse(lsRow[_COL_TASK_LASTRUNTIME].ToString()).AddMinutes(double.Parse(T.lsInterval))).ToString(_24H_TIME_FORMAT);
                    lsRow[_COL_TASK_STATUS] = "Ready";
                    lsRow[_COL_TASK_ENABLED] = "Y";

                    lsTaskView.Rows.Add(lsRow);
                }
                lsDataGrid.DataSource = lsTaskView;
                lsDataGrid.Columns[_COL_TASK_APPNAME].Frozen = true;

                return;
            }
            catch (Exception ex)
            {
                showErrorMessage(ex);
            }
        }

        private void doExecuteTask(Boolean isStartImmediately)
        {
            string lsLastRunTime;
            string lsNextRunTime;
            int lsInterval;
            int lsTaskRunCount = 0;
            int lsTaskRunFailCount = 0;
            int lsTaskNoRunCount = 0;
            ProcessStartInfo lsProc;
            Process lsProcInfo;

            try
            {
                if (lsToken == false) return;   // check if last event not finished

                lsToken = false;
                foreach (DataGridViewRow r in dgrTaskList.Rows)
                {
                    if (r.Cells[_COL_TASK_APPNAME].Value == null || r.Cells[_COL_TASK_APPNAME].Value.ToString() == "") break;
                    if (r.Cells[_COL_TASK_ENABLED].Value.ToString() == "N") continue;

                    lsNextRunTime = r.Cells[_COL_TASK_NEXTRUNTIME].Value.ToString();
                    lsLastRunTime = r.Cells[_COL_TASK_LASTRUNTIME].Value.ToString();
                    lsInterval = int.Parse(r.Cells[_COL_TASK_INTERVAL].Value.ToString());

                    if ((DateTime.Now > DateTime.Parse(lsNextRunTime)) || isStartImmediately)
                    {
                        // check if file to execute exist or not
                        if (System.IO.File.Exists(r.Cells[_COL_TASK_FILEPATH].Value.ToString()) == true)
                        {
                            lsProc = new ProcessStartInfo();
                            lsProc.FileName = r.Cells[_COL_TASK_FILEPATH].Value.ToString();
                            lsProc.Arguments = r.Cells[_COL_TASK_ARGUMENTS].Value.ToString();
                            if (r.Cells[_COL_TASK_WORKINGDIR].Value.ToString() == "")  // if workingdir is empty, take dir of this file as default
                                lsProc.WorkingDirectory = System.IO.Path.GetDirectoryName(lsProc.FileName);
                            else
                                lsProc.WorkingDirectory = r.Cells[_COL_TASK_WORKINGDIR].Value.ToString();

                            lsProcInfo = Process.Start(lsProc);
                            myLog.WriteLog("executing task Success-"
                                                    + "AP=" + r.Cells[_COL_TASK_APPNAME].Value.ToString()
                                                    + ";File=" + r.Cells[_COL_TASK_FILEPATH].Value.ToString()
                                                    + ";LastRunTime=" + r.Cells[_COL_TASK_LASTRUNTIME].Value.ToString()
                                                    + ";NextRunTime=" + r.Cells[_COL_TASK_NEXTRUNTIME].Value.ToString()
                                                    + ";PID=" + lsProcInfo.Id.ToString());
                            r.Cells[_COL_TASK_STATUS].Value = "Success";
                            r.Cells[_COL_TASK_APPNAME].Style.BackColor = Color.Green;
                            r.Cells[_COL_TASK_STATUS].Style.BackColor = Color.Green;
                            lsTaskRunCount++;
                        }
                        else  // file not exist
                        {
                            myLog.WriteLog("executing task Fail - file not exist. skip executing. "
                                                    + "AP=" + r.Cells[_COL_TASK_APPNAME].Value.ToString()
                                                    + ";File=" + r.Cells[_COL_TASK_FILEPATH].Value.ToString());
                            r.Cells[_COL_TASK_STATUS].Value = "Fail";
                            r.Cells[_COL_TASK_APPNAME].Style.BackColor = Color.Red;
                            r.Cells[_COL_TASK_STATUS].Style.BackColor = Color.Red;
                            lsTaskRunFailCount++;
                        }

                        // refresh
                        r.Cells[_COL_TASK_LASTRUNTIME].Value = DateTime.Now.ToString(_24H_TIME_FORMAT);
                        r.Cells[_COL_TASK_NEXTRUNTIME].Value = (DateTime.Parse(r.Cells[_COL_TASK_LASTRUNTIME].Value.ToString()).AddMinutes(double.Parse(lsInterval.ToString()))).ToString(_24H_TIME_FORMAT);
                    }
                    else  // not exceeding next run time
                    {
                        myLog.WriteLog("waiting for next run time. skip. "
                                                    + "AP=" + r.Cells[_COL_TASK_APPNAME].Value.ToString()
                                                    + ";File=" + r.Cells[_COL_TASK_FILEPATH].Value.ToString());
                        r.Cells[_COL_TASK_STATUS].Value = "Waiting";
                        r.Cells[_COL_TASK_STATUS].Style.BackColor = Color.Yellow;
                        lsTaskNoRunCount++;
                    }
                }

                myLog.WriteLog("Task Running Summary: RunCount=" + lsTaskRunCount.ToString() + ";NoRunCount=" + lsTaskNoRunCount.ToString() + ";RunFailCount=" + lsTaskRunFailCount.ToString());
                lsToken = true;

                return;
            }
            catch (Exception ex)
            {
                myLog.WriteLog("[doExecuteTask] Error " + ex.ToString());
            }
        }

        public frmMain()
        {
            InitializeComponent();

            try
            {
                // init form basic info.
                this.Text = "Simple Task Scheduler- v" + Application.ProductVersion;
                this.Left = (Screen.GetWorkingArea(this).Width - this.Width) / 2;
                this.Top = (Screen.GetWorkingArea(this).Height - this.Height) / 2;

                // init logging
                myLog = new Log(Application.StartupPath + "\\Log");
                
                // read ini
                lsINIPath = Application.StartupPath + "\\" + _INI_FILE;
                if (System.IO.File.Exists(lsINIPath) == false)
                {
                    MessageBox.Show("INI " + lsINIPath + " NOT EXIST!", this.Text, MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    lsConfig = new APConfig(lsINIPath);
                }

                // get task info from ini
                lsTaskList = lsConfig.getConfig();
                loadTaskInfoIntoGrid(dgrTaskList);
                tmrTaskExecutor.Interval = 30000;
                lsToken = true;
                if (lsConfig.AutoStart)
                {
                    btnStart_Click(null, null);
                    myLog.WriteLog("autostart set to true. auto enable tmrTaskExecutor");
                }

                myLog.WriteLog("timer interval set to " + tmrTaskExecutor.Interval.ToString());
                myLog.WriteLog("loading config file OK - " + lsINIPath);
                myLog.WriteLog("ap started successfully");
                
                return;
            }
            catch (Exception ex)
            {
                showErrorMessage(ex);
            }
        }

        private void tmrTaskExecutor_Tick(object sender, EventArgs e)
        {
            doExecuteTask(false);

            return;
        }

        private void tmrShowTime_Tick(object sender, EventArgs e)
        {
            slblShowCurrentTime.Text = "Current Time:" + DateTime.Now.ToString(_24H_TIME_FORMAT);

            return;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                tmrTaskExecutor.Enabled = false;
                btnPause.Enabled = false;
                btnStart.Enabled = true;
                myLog.WriteLog("[btnPause_Click] timer paused");

                return;
            }
            catch (Exception ex)
            {
                showErrorMessage(ex);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                tmrTaskExecutor.Enabled = true;
                btnStart.Enabled = false;
                btnPause.Enabled = true;
                myLog.WriteLog("[btnStart_Click] timer started");

                return;
            }
            catch (Exception ex)
            {
                showErrorMessage(ex);
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                myLog.WriteLog("ap closed");

                return;
            }
            catch (Exception ex)
            {
                showErrorMessage(ex);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lsToken == false)  // if still running some task, don't close ap
                e.Cancel = true;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            tabControl1.Height = this.Height - (btnStart.Height + statusStrip1.Height + 20) - btnPause.Height;
            tabControl1.Width = this.Width - 20;

            btnPause.Left = (this.Width - (btnPause.Width + 20));
            btnPause.Top = (tabControl1.Top + tabControl1.Height);

            btnStart.Left = (this.Width - (btnStart.Width + 20) * 2);
            btnStart.Top = (tabControl1.Top + tabControl1.Height);

            btnReloadINI.Left = (this.Width - (btnReloadINI.Width + 20) * 3);
            btnReloadINI.Top = (tabControl1.Top + tabControl1.Height);

            dgrTaskList.Height = tabControl1.Height - 30;
            dgrTaskList.Width = tabControl1.Width - 20;

            return;
        }

        private void dgrTaskList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStrip1.Show(MousePosition);

            return;
        }

        private void startImmediatelyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doExecuteTask(true);

            return;
        }

        private void dgrTaskList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == _COL_TASK_ENABLED)
                {
                    if (dgrTaskList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Y")
                    {
                        dgrTaskList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N";
                        dgrTaskList.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dgrTaskList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Y";
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                myLog.WriteLog("[dgrTaskList_CellContentClick]-Error " + ex.ToString());
            }
        }

        private void btnReloadINI_Click(object sender, EventArgs e)
        {
            try
            {
                lsConfig.ReloadTaskFromINI();
                lsTaskList = lsConfig.getConfig();
                loadTaskInfoIntoGrid(dgrTaskList);

                return;
            }
            catch (Exception ex)
            {
                myLog.WriteLog("[btnReloadINI_Click]-Error " + ex.ToString());
            }
        }

        private void dgrTaskList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in dgrTaskList.SelectedRows)
                {
                    r.Cells[_COL_TASK_ENABLED].Value = "Y";
                }

                return;
            }
            catch (Exception ex)
            {
                myLog.WriteLog("[enableToolStripMenuItem_Click]-Error " + ex.ToString());
            }
        }

        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in dgrTaskList.SelectedRows)
                {
                    r.Cells[_COL_TASK_ENABLED].Value = "N";
                    r.Cells[_COL_TASK_ENABLED].Style.BackColor = Color.Red;
                }

                return;
            }
            catch (Exception ex)
            {
                myLog.WriteLog("[disableToolStripMenuItem_Click]-Error " + ex.ToString());
            }
        }

        private void enableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow r in dgrTaskList.SelectedRows)
                {
                    r.Cells[_COL_TASK_ENABLED].Value = "Y";
                    r.Cells[_COL_TASK_ENABLED].Style.BackColor = Color.White;
                }

                return;
            }
            catch (Exception ex)
            {
                myLog.WriteLog("[enableToolStripMenuItem_Click]-Error " + ex.ToString());
            }
        }
    }
}

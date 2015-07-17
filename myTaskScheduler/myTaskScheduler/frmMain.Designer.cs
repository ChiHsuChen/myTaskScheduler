namespace myTaskScheduler
{
    partial class frmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgrTaskList = new System.Windows.Forms.DataGridView();
            this.tmrTaskExecutor = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slblShowCurrentTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrShowTime = new System.Windows.Forms.Timer(this.components);
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startImmediatelyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReloadINI = new System.Windows.Forms.Button();
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrTaskList)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(799, 267);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgrTaskList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(791, 241);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Task List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgrTaskList
            // 
            this.dgrTaskList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrTaskList.Location = new System.Drawing.Point(8, 6);
            this.dgrTaskList.Name = "dgrTaskList";
            this.dgrTaskList.ReadOnly = true;
            this.dgrTaskList.RowTemplate.Height = 24;
            this.dgrTaskList.Size = new System.Drawing.Size(777, 229);
            this.dgrTaskList.TabIndex = 0;
            this.dgrTaskList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrTaskList_CellClick);
            this.dgrTaskList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrTaskList_CellContentClick);
            this.dgrTaskList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgrTaskList_MouseUp);
            // 
            // tmrTaskExecutor
            // 
            this.tmrTaskExecutor.Interval = 1000;
            this.tmrTaskExecutor.Tick += new System.EventHandler(this.tmrTaskExecutor_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slblShowCurrentTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 309);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slblShowCurrentTime
            // 
            this.slblShowCurrentTime.Name = "slblShowCurrentTime";
            this.slblShowCurrentTime.Size = new System.Drawing.Size(0, 17);
            // 
            // tmrShowTime
            // 
            this.tmrShowTime.Enabled = true;
            this.tmrShowTime.Interval = 1000;
            this.tmrShowTime.Tick += new System.EventHandler(this.tmrShowTime_Tick);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(658, 269);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(131, 35);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(520, 269);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(131, 35);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startImmediatelyToolStripMenuItem,
            this.toolStripMenuItem1,
            this.enableToolStripMenuItem,
            this.disableToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(204, 98);
            // 
            // startImmediatelyToolStripMenuItem
            // 
            this.startImmediatelyToolStripMenuItem.Name = "startImmediatelyToolStripMenuItem";
            this.startImmediatelyToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.startImmediatelyToolStripMenuItem.Text = "Start Task Immediately";
            this.startImmediatelyToolStripMenuItem.Click += new System.EventHandler(this.startImmediatelyToolStripMenuItem_Click);
            // 
            // btnReloadINI
            // 
            this.btnReloadINI.Location = new System.Drawing.Point(383, 269);
            this.btnReloadINI.Name = "btnReloadINI";
            this.btnReloadINI.Size = new System.Drawing.Size(131, 35);
            this.btnReloadINI.TabIndex = 4;
            this.btnReloadINI.Text = "Reload INI";
            this.btnReloadINI.UseVisualStyleBackColor = true;
            this.btnReloadINI.Click += new System.EventHandler(this.btnReloadINI_Click);
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.enableToolStripMenuItem.Text = "Enable Task";
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.disableToolStripMenuItem.Text = "Disable Task";
            this.disableToolStripMenuItem.Click += new System.EventHandler(this.disableToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(200, 6);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 331);
            this.Controls.Add(this.btnReloadINI);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = "Task Scheduler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrTaskList)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgrTaskList;
        private System.Windows.Forms.Timer tmrTaskExecutor;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer tmrShowTime;
        private System.Windows.Forms.ToolStripStatusLabel slblShowCurrentTime;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem startImmediatelyToolStripMenuItem;
        private System.Windows.Forms.Button btnReloadINI;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}


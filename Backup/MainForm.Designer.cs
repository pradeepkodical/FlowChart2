namespace FlowChart2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewDoc = new System.Windows.Forms.ToolStripButton();
            this.btnOpenDoc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveDoc = new System.Windows.Forms.ToolStripButton();
            this.btnExportDoc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nPanel1 = new FlowChart2.NPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnAddBox = new System.Windows.Forms.ToolStripButton();
            this.btnAddRound = new System.Windows.Forms.ToolStripButton();
            this.btnDatabase = new System.Windows.Forms.ToolStripButton();
            this.btnAddProcess = new System.Windows.Forms.ToolStripButton();
            this.btnCloud = new System.Windows.Forms.ToolStripButton();
            this.btnAddRhombus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAddConnector = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pnlUndo = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewDoc,
            this.btnOpenDoc,
            this.toolStripSeparator1,
            this.btnSaveDoc,
            this.btnExportDoc,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(899, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNewDoc
            // 
            this.btnNewDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewDoc.Image = ((System.Drawing.Image)(resources.GetObject("btnNewDoc.Image")));
            this.btnNewDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewDoc.Name = "btnNewDoc";
            this.btnNewDoc.Size = new System.Drawing.Size(23, 22);
            this.btnNewDoc.Click += new System.EventHandler(this.btnNewDoc_Click);
            // 
            // btnOpenDoc
            // 
            this.btnOpenDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenDoc.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDoc.Image")));
            this.btnOpenDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenDoc.Name = "btnOpenDoc";
            this.btnOpenDoc.Size = new System.Drawing.Size(23, 22);
            this.btnOpenDoc.Click += new System.EventHandler(this.btnOpenDoc_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSaveDoc
            // 
            this.btnSaveDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveDoc.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveDoc.Image")));
            this.btnSaveDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveDoc.Name = "btnSaveDoc";
            this.btnSaveDoc.Size = new System.Drawing.Size(23, 22);
            this.btnSaveDoc.Text = "toolStripButton1";
            this.btnSaveDoc.Click += new System.EventHandler(this.btnSaveDoc_Click);
            // 
            // btnExportDoc
            // 
            this.btnExportDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportDoc.Image = ((System.Drawing.Image)(resources.GetObject("btnExportDoc.Image")));
            this.btnExportDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportDoc.Name = "btnExportDoc";
            this.btnExportDoc.Size = new System.Drawing.Size(23, 22);
            this.btnExportDoc.Text = "toolStripButton1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.nPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 450);
            this.panel1.TabIndex = 4;
            // 
            // nPanel1
            // 
            this.nPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nPanel1.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nPanel1.Location = new System.Drawing.Point(113, 58);
            this.nPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.nPanel1.Name = "nPanel1";
            this.nPanel1.SelectedComponent = null;
            this.nPanel1.Size = new System.Drawing.Size(410, 244);
            this.nPanel1.TabIndex = 2;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddBox,
            this.btnAddRound,
            this.btnDatabase,
            this.btnAddProcess,
            this.btnCloud,
            this.btnAddRhombus,
            this.toolStripSeparator3,
            this.btnAddConnector});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(37, 450);
            this.toolStrip2.TabIndex = 5;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnAddBox
            // 
            this.btnAddBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddBox.Image = ((System.Drawing.Image)(resources.GetObject("btnAddBox.Image")));
            this.btnAddBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddBox.Name = "btnAddBox";
            this.btnAddBox.Size = new System.Drawing.Size(34, 36);
            this.btnAddBox.Text = "Box";
            this.btnAddBox.Click += new System.EventHandler(this.btnAddBox_Click);
            // 
            // btnAddRound
            // 
            this.btnAddRound.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddRound.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRound.Image")));
            this.btnAddRound.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRound.Name = "btnAddRound";
            this.btnAddRound.Size = new System.Drawing.Size(34, 36);
            this.btnAddRound.Text = "Circle";
            this.btnAddRound.Click += new System.EventHandler(this.btnAddRound_Click);
            // 
            // btnDatabase
            // 
            this.btnDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btnDatabase.Image")));
            this.btnDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDatabase.Name = "btnDatabase";
            this.btnDatabase.Size = new System.Drawing.Size(34, 36);
            this.btnDatabase.Text = "Database";
            this.btnDatabase.Click += new System.EventHandler(this.btnDatabase_Click);
            // 
            // btnAddProcess
            // 
            this.btnAddProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnAddProcess.Image")));
            this.btnAddProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddProcess.Name = "btnAddProcess";
            this.btnAddProcess.Size = new System.Drawing.Size(34, 36);
            this.btnAddProcess.Text = "Process";
            this.btnAddProcess.Click += new System.EventHandler(this.btnAddProcess_Click);
            // 
            // btnCloud
            // 
            this.btnCloud.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloud.Image = ((System.Drawing.Image)(resources.GetObject("btnCloud.Image")));
            this.btnCloud.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloud.Name = "btnCloud";
            this.btnCloud.Size = new System.Drawing.Size(34, 36);
            this.btnCloud.Text = "Cloud";
            this.btnCloud.Click += new System.EventHandler(this.btnCloud_Click);
            // 
            // btnAddRhombus
            // 
            this.btnAddRhombus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddRhombus.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRhombus.Image")));
            this.btnAddRhombus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRhombus.Name = "btnAddRhombus";
            this.btnAddRhombus.Size = new System.Drawing.Size(34, 36);
            this.btnAddRhombus.Text = "Conditional";
            this.btnAddRhombus.Click += new System.EventHandler(this.btnAddRhombus_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(34, 6);
            // 
            // btnAddConnector
            // 
            this.btnAddConnector.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddConnector.Image = ((System.Drawing.Image)(resources.GetObject("btnAddConnector.Image")));
            this.btnAddConnector.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddConnector.Name = "btnAddConnector";
            this.btnAddConnector.Size = new System.Drawing.Size(34, 36);
            this.btnAddConnector.Text = "Connector";
            this.btnAddConnector.Click += new System.EventHandler(this.btnAddConnector_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "pkf";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "PKF Files|*.pkf";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "pkf";
            this.saveFileDialog1.Filter = "PKF Files|*.pkf";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(37, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(862, 450);
            this.splitContainer1.SplitterDistance = 682;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pnlUndo);
            this.splitContainer2.Size = new System.Drawing.Size(176, 450);
            this.splitContainer2.SplitterDistance = 344;
            this.splitContainer2.TabIndex = 0;
            // 
            // pnlUndo
            // 
            this.pnlUndo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUndo.Location = new System.Drawing.Point(0, 0);
            this.pnlUndo.Name = "pnlUndo";
            this.pnlUndo.Size = new System.Drawing.Size(176, 344);
            this.pnlUndo.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 475);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "FlowChart2";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NPanel nPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNewDoc;
        private System.Windows.Forms.ToolStripButton btnOpenDoc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSaveDoc;
        private System.Windows.Forms.ToolStripButton btnExportDoc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnAddBox;
        private System.Windows.Forms.ToolStripButton btnAddRound;
        private System.Windows.Forms.ToolStripButton btnDatabase;
        private System.Windows.Forms.ToolStripButton btnCloud;
        private System.Windows.Forms.ToolStripButton btnAddRhombus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnAddConnector;
        private System.Windows.Forms.ToolStripButton btnAddProcess;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel pnlUndo;
    }
}


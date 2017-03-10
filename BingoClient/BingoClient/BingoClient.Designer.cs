namespace BingoClient
{
    partial class BingoClient
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            this.buttonALL = new System.Windows.Forms.Button();
            this.checkBoxCallBingos = new System.Windows.Forms.CheckBox();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonPower = new System.Windows.Forms.Button();
            this.buttonBingo = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.labelTotalBingos = new System.Windows.Forms.Label();
            this.labelTotalMatches = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Matches = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBoxAutoPilot = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.matchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.readNumbersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerAutoPilot = new System.Windows.Forms.Timer(this.components);
            this.buttonPlus5 = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 6);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(24, 13);
            label2.TabIndex = 2;
            label2.Text = "Call";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 84);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(39, 13);
            label1.TabIndex = 15;
            label1.Text = "History";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 239);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(78, 13);
            label3.TabIndex = 0;
            label3.Text = "Total Matches:";
            // 
            // buttonALL
            // 
            this.buttonALL.Location = new System.Drawing.Point(15, 29);
            this.buttonALL.Name = "buttonALL";
            this.buttonALL.Size = new System.Drawing.Size(75, 23);
            this.buttonALL.TabIndex = 8;
            this.buttonALL.Text = "ALL";
            this.buttonALL.UseVisualStyleBackColor = true;
            this.buttonALL.Click += new System.EventHandler(this.buttonALL_Click);
            // 
            // checkBoxCallBingos
            // 
            this.checkBoxCallBingos.AutoSize = true;
            this.checkBoxCallBingos.Location = new System.Drawing.Point(96, 33);
            this.checkBoxCallBingos.Name = "checkBoxCallBingos";
            this.checkBoxCallBingos.Size = new System.Drawing.Size(78, 17);
            this.checkBoxCallBingos.TabIndex = 9;
            this.checkBoxCallBingos.Text = "Call Bingos";
            this.checkBoxCallBingos.UseVisualStyleBackColor = true;
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(42, 3);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(48, 20);
            this.textBoxInput.TabIndex = 10;
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
            // 
            // buttonPower
            // 
            this.buttonPower.Location = new System.Drawing.Point(15, 58);
            this.buttonPower.Name = "buttonPower";
            this.buttonPower.Size = new System.Drawing.Size(75, 23);
            this.buttonPower.TabIndex = 11;
            this.buttonPower.Text = "Power";
            this.buttonPower.UseVisualStyleBackColor = true;
            this.buttonPower.Click += new System.EventHandler(this.buttonPower_Click);
            // 
            // buttonBingo
            // 
            this.buttonBingo.Location = new System.Drawing.Point(96, 58);
            this.buttonBingo.Name = "buttonBingo";
            this.buttonBingo.Size = new System.Drawing.Size(75, 23);
            this.buttonBingo.TabIndex = 12;
            this.buttonBingo.Text = "Bingo";
            this.buttonBingo.UseVisualStyleBackColor = true;
            this.buttonBingo.Click += new System.EventHandler(this.buttonBingo_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 333);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 241F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(285, 242);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonPlus5);
            this.panel1.Controls.Add(this.buttonRestart);
            this.panel1.Controls.Add(this.labelTotalBingos);
            this.panel1.Controls.Add(this.labelTotalMatches);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(label3);
            this.panel1.Controls.Add(this.dataGridViewHistory);
            this.panel1.Controls.Add(label1);
            this.panel1.Controls.Add(this.checkBoxAutoPilot);
            this.panel1.Controls.Add(label2);
            this.panel1.Controls.Add(this.buttonBingo);
            this.panel1.Controls.Add(this.checkBoxCallBingos);
            this.panel1.Controls.Add(this.textBoxInput);
            this.panel1.Controls.Add(this.buttonPower);
            this.panel1.Controls.Add(this.buttonALL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 303);
            this.panel1.TabIndex = 14;
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(266, 3);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(23, 23);
            this.buttonRestart.TabIndex = 16;
            this.buttonRestart.Text = "R";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // labelTotalBingos
            // 
            this.labelTotalBingos.AutoSize = true;
            this.labelTotalBingos.Location = new System.Drawing.Point(96, 259);
            this.labelTotalBingos.Name = "labelTotalBingos";
            this.labelTotalBingos.Size = new System.Drawing.Size(13, 13);
            this.labelTotalBingos.TabIndex = 19;
            this.labelTotalBingos.Text = "0";
            // 
            // labelTotalMatches
            // 
            this.labelTotalMatches.AutoSize = true;
            this.labelTotalMatches.Location = new System.Drawing.Point(96, 239);
            this.labelTotalMatches.Name = "labelTotalMatches";
            this.labelTotalMatches.Size = new System.Drawing.Size(13, 13);
            this.labelTotalMatches.TabIndex = 18;
            this.labelTotalMatches.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Total Bingos:";
            // 
            // dataGridViewHistory
            // 
            this.dataGridViewHistory.AllowUserToAddRows = false;
            this.dataGridViewHistory.AllowUserToDeleteRows = false;
            this.dataGridViewHistory.AllowUserToResizeColumns = false;
            this.dataGridViewHistory.AllowUserToResizeRows = false;
            this.dataGridViewHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewHistory.CausesValidation = false;
            this.dataGridViewHistory.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Matches});
            this.dataGridViewHistory.Location = new System.Drawing.Point(12, 100);
            this.dataGridViewHistory.MultiSelect = false;
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.ReadOnly = true;
            this.dataGridViewHistory.RowHeadersVisible = false;
            this.dataGridViewHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHistory.ShowCellErrors = false;
            this.dataGridViewHistory.ShowCellToolTips = false;
            this.dataGridViewHistory.ShowEditingIcon = false;
            this.dataGridViewHistory.ShowRowErrors = false;
            this.dataGridViewHistory.Size = new System.Drawing.Size(201, 131);
            this.dataGridViewHistory.TabIndex = 16;
            // 
            // Number
            // 
            this.Number.HeaderText = "Number";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            // 
            // Matches
            // 
            this.Matches.HeaderText = "Matches";
            this.Matches.Name = "Matches";
            this.Matches.ReadOnly = true;
            // 
            // checkBoxAutoPilot
            // 
            this.checkBoxAutoPilot.AutoSize = true;
            this.checkBoxAutoPilot.Location = new System.Drawing.Point(96, 5);
            this.checkBoxAutoPilot.Name = "checkBoxAutoPilot";
            this.checkBoxAutoPilot.Size = new System.Drawing.Size(71, 17);
            this.checkBoxAutoPilot.TabIndex = 13;
            this.checkBoxAutoPilot.Text = "Auto Pilot";
            this.checkBoxAutoPilot.UseVisualStyleBackColor = true;
            this.checkBoxAutoPilot.CheckedChanged += new System.EventHandler(this.checkBoxAutoPilot_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.matchToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(309, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // matchToolStripMenuItem
            // 
            this.matchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripMenuItem1,
            this.readNumbersToolStripMenuItem});
            this.matchToolStripMenuItem.Name = "matchToolStripMenuItem";
            this.matchToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.matchToolStripMenuItem.Text = "Match";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem1.Text = "Change Configuration";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // readNumbersToolStripMenuItem
            // 
            this.readNumbersToolStripMenuItem.Name = "readNumbersToolStripMenuItem";
            this.readNumbersToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.readNumbersToolStripMenuItem.Text = "Read Numbers";
            this.readNumbersToolStripMenuItem.Click += new System.EventHandler(this.readNumbersToolStripMenuItem_Click);
            // 
            // timerAutoPilot
            // 
            this.timerAutoPilot.Interval = 2000;
            this.timerAutoPilot.Tick += new System.EventHandler(this.timerAutoPilot_Tick);
            // 
            // buttonPlus5
            // 
            this.buttonPlus5.Location = new System.Drawing.Point(177, 58);
            this.buttonPlus5.Name = "buttonPlus5";
            this.buttonPlus5.Size = new System.Drawing.Size(75, 23);
            this.buttonPlus5.TabIndex = 20;
            this.buttonPlus5.Text = "+5";
            this.buttonPlus5.UseVisualStyleBackColor = true;
            this.buttonPlus5.Click += new System.EventHandler(this.buttonPlus5_Click);
            // 
            // BingoClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 587);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BingoClient";
            this.Text = "Bingo Client";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonALL;
        private System.Windows.Forms.CheckBox checkBoxCallBingos;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonPower;
        private System.Windows.Forms.Button buttonBingo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem matchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem readNumbersToolStripMenuItem;
        private System.Windows.Forms.Timer timerAutoPilot;
        private System.Windows.Forms.CheckBox checkBoxAutoPilot;
        private System.Windows.Forms.DataGridView dataGridViewHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Matches;
        private System.Windows.Forms.Label labelTotalBingos;
        private System.Windows.Forms.Label labelTotalMatches;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonRestart;
        private System.Windows.Forms.Button buttonPlus5;
    }
}


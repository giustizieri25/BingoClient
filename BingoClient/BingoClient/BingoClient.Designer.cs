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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.labelConfiguration = new System.Windows.Forms.Label();
            this.buttonB = new System.Windows.Forms.Button();
            this.buttonI = new System.Windows.Forms.Button();
            this.buttonN = new System.Windows.Forms.Button();
            this.buttonG = new System.Windows.Forms.Button();
            this.buttonO = new System.Windows.Forms.Button();
            this.buttonALL = new System.Windows.Forms.Button();
            this.checkBoxCallBingos = new System.Windows.Forms.CheckBox();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonPower = new System.Windows.Forms.Button();
            this.buttonBingo = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(72, 13);
            label1.TabIndex = 0;
            label1.Text = "Configuration:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 35);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(83, 13);
            label2.TabIndex = 2;
            label2.Text = "Check columns:";
            // 
            // labelConfiguration
            // 
            this.labelConfiguration.AutoSize = true;
            this.labelConfiguration.Location = new System.Drawing.Point(90, 9);
            this.labelConfiguration.Name = "labelConfiguration";
            this.labelConfiguration.Size = new System.Drawing.Size(0, 13);
            this.labelConfiguration.TabIndex = 1;
            // 
            // buttonB
            // 
            this.buttonB.Location = new System.Drawing.Point(93, 30);
            this.buttonB.Name = "buttonB";
            this.buttonB.Size = new System.Drawing.Size(23, 23);
            this.buttonB.TabIndex = 3;
            this.buttonB.Text = "B";
            this.buttonB.UseVisualStyleBackColor = true;
            this.buttonB.Click += new System.EventHandler(this.buttonB_Click);
            // 
            // buttonI
            // 
            this.buttonI.Location = new System.Drawing.Point(122, 30);
            this.buttonI.Name = "buttonI";
            this.buttonI.Size = new System.Drawing.Size(23, 23);
            this.buttonI.TabIndex = 4;
            this.buttonI.Text = "I";
            this.buttonI.UseVisualStyleBackColor = true;
            this.buttonI.Click += new System.EventHandler(this.buttonI_Click);
            // 
            // buttonN
            // 
            this.buttonN.Location = new System.Drawing.Point(151, 30);
            this.buttonN.Name = "buttonN";
            this.buttonN.Size = new System.Drawing.Size(23, 23);
            this.buttonN.TabIndex = 5;
            this.buttonN.Text = "N";
            this.buttonN.UseVisualStyleBackColor = true;
            this.buttonN.Click += new System.EventHandler(this.buttonN_Click);
            // 
            // buttonG
            // 
            this.buttonG.Location = new System.Drawing.Point(180, 30);
            this.buttonG.Name = "buttonG";
            this.buttonG.Size = new System.Drawing.Size(23, 23);
            this.buttonG.TabIndex = 6;
            this.buttonG.Text = "G";
            this.buttonG.UseVisualStyleBackColor = true;
            this.buttonG.Click += new System.EventHandler(this.buttonG_Click);
            // 
            // buttonO
            // 
            this.buttonO.Location = new System.Drawing.Point(209, 30);
            this.buttonO.Name = "buttonO";
            this.buttonO.Size = new System.Drawing.Size(23, 23);
            this.buttonO.TabIndex = 7;
            this.buttonO.Text = "O";
            this.buttonO.UseVisualStyleBackColor = true;
            this.buttonO.Click += new System.EventHandler(this.buttonO_Click);
            // 
            // buttonALL
            // 
            this.buttonALL.Location = new System.Drawing.Point(248, 30);
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
            this.checkBoxCallBingos.Location = new System.Drawing.Point(329, 34);
            this.checkBoxCallBingos.Name = "checkBoxCallBingos";
            this.checkBoxCallBingos.Size = new System.Drawing.Size(78, 17);
            this.checkBoxCallBingos.TabIndex = 9;
            this.checkBoxCallBingos.Text = "Call Bingos";
            this.checkBoxCallBingos.UseVisualStyleBackColor = true;
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(93, 59);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(52, 20);
            this.textBoxInput.TabIndex = 10;
            this.textBoxInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxInput_KeyUp);
            // 
            // buttonPower
            // 
            this.buttonPower.Location = new System.Drawing.Point(248, 59);
            this.buttonPower.Name = "buttonPower";
            this.buttonPower.Size = new System.Drawing.Size(75, 23);
            this.buttonPower.TabIndex = 11;
            this.buttonPower.Text = "Power";
            this.buttonPower.UseVisualStyleBackColor = true;
            this.buttonPower.Click += new System.EventHandler(this.buttonPower_Click);
            // 
            // buttonBingo
            // 
            this.buttonBingo.Location = new System.Drawing.Point(329, 59);
            this.buttonBingo.Name = "buttonBingo";
            this.buttonBingo.Size = new System.Drawing.Size(75, 23);
            this.buttonBingo.TabIndex = 12;
            this.buttonBingo.Text = "Bingo";
            this.buttonBingo.UseVisualStyleBackColor = true;
            this.buttonBingo.Click += new System.EventHandler(this.buttonBingo_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 88);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(433, 173);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // BingoClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 261);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonBingo);
            this.Controls.Add(this.buttonPower);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.checkBoxCallBingos);
            this.Controls.Add(this.buttonALL);
            this.Controls.Add(this.buttonB);
            this.Controls.Add(this.buttonO);
            this.Controls.Add(label2);
            this.Controls.Add(this.buttonI);
            this.Controls.Add(this.labelConfiguration);
            this.Controls.Add(this.buttonG);
            this.Controls.Add(label1);
            this.Controls.Add(this.buttonN);
            this.Name = "BingoClient";
            this.Text = "Bingo Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelConfiguration;
        private System.Windows.Forms.Button buttonB;
        private System.Windows.Forms.Button buttonI;
        private System.Windows.Forms.Button buttonN;
        private System.Windows.Forms.Button buttonG;
        private System.Windows.Forms.Button buttonO;
        private System.Windows.Forms.Button buttonALL;
        private System.Windows.Forms.CheckBox checkBoxCallBingos;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonPower;
        private System.Windows.Forms.Button buttonBingo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}


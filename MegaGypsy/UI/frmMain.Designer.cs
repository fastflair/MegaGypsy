namespace MegaGypsy
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.rtbPicks = new System.Windows.Forms.RichTextBox();
            this.txtPickNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtWin = new System.Windows.Forms.TextBox();
            this.btnCheckWin = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbPicks
            // 
            this.rtbPicks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbPicks.BackColor = System.Drawing.SystemColors.Window;
            this.rtbPicks.Location = new System.Drawing.Point(3, 59);
            this.rtbPicks.Name = "rtbPicks";
            this.rtbPicks.Size = new System.Drawing.Size(596, 198);
            this.rtbPicks.TabIndex = 1;
            this.rtbPicks.Text = "";
            // 
            // txtPickNum
            // 
            this.txtPickNum.Location = new System.Drawing.Point(143, 4);
            this.txtPickNum.Name = "txtPickNum";
            this.txtPickNum.Size = new System.Drawing.Size(56, 20);
            this.txtPickNum.TabIndex = 2;
            this.txtPickNum.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Number of Powerball Picks";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Pick Powerball Numbers!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtWin
            // 
            this.txtWin.Location = new System.Drawing.Point(218, 4);
            this.txtWin.Name = "txtWin";
            this.txtWin.Size = new System.Drawing.Size(190, 20);
            this.txtWin.TabIndex = 5;
            this.txtWin.Text = "1,2,3,4,5,6";
            // 
            // btnCheckWin
            // 
            this.btnCheckWin.Location = new System.Drawing.Point(218, 30);
            this.btnCheckWin.Name = "btnCheckWin";
            this.btnCheckWin.Size = new System.Drawing.Size(190, 23);
            this.btnCheckWin.TabIndex = 6;
            this.btnCheckWin.Text = "Check Winning Numbers";
            this.btnCheckWin.UseVisualStyleBackColor = true;
            this.btnCheckWin.Click += new System.EventHandler(this.btnCheckWin_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(514, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Import History";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 257);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCheckWin);
            this.Controls.Add(this.txtWin);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPickNum);
            this.Controls.Add(this.rtbPicks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "MegaGypsy Lotto Predictions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbPicks;
        private System.Windows.Forms.TextBox txtPickNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtWin;
        private System.Windows.Forms.Button btnCheckWin;
        private System.Windows.Forms.Button button2;

    }
}


namespace CSGL12Example2
{
    partial class CSGL12Example2Form
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
            this.mCSGL12Control1 = new CSGL12.CSGL12Control();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mCSGL12Control2 = new CSGL12.CSGL12Control();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mCSGL12Control1
            // 
            this.mCSGL12Control1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mCSGL12Control1.Location = new System.Drawing.Point(3, 3);
            this.mCSGL12Control1.Name = "mCSGL12Control1";
            this.mCSGL12Control1.Size = new System.Drawing.Size(320, 480);
            this.mCSGL12Control1.TabIndex = 0;
            this.mCSGL12Control1.Text = "csgl12Control1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mCSGL12Control1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mCSGL12Control2);
            this.splitContainer1.Size = new System.Drawing.Size(656, 486);
            this.splitContainer1.SplitterDistance = 326;
            this.splitContainer1.TabIndex = 1;
            // 
            // mCSGL12Control2
            // 
            this.mCSGL12Control2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mCSGL12Control2.Location = new System.Drawing.Point(3, 3);
            this.mCSGL12Control2.Name = "mCSGL12Control2";
            this.mCSGL12Control2.Size = new System.Drawing.Size(320, 480);
            this.mCSGL12Control2.TabIndex = 0;
            this.mCSGL12Control2.Text = "csgl12Control2";
            // 
            // CSGL12Example2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 510);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CSGL12Example2Form";
            this.Text = "CSGL12Example2";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CSGL12.CSGL12Control mCSGL12Control1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private CSGL12.CSGL12Control mCSGL12Control2;
    }
}


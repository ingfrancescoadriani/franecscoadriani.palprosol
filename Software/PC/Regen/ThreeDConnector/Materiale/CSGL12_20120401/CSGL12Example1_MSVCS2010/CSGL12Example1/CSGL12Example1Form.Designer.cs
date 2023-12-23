namespace CSGL12Example1
{
    partial class CSGL12Example1Form
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
            this.SuspendLayout();
            // 
            // mCSGL12Control1
            // 
            this.mCSGL12Control1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mCSGL12Control1.BackColor = System.Drawing.SystemColors.Control;
            this.mCSGL12Control1.Location = new System.Drawing.Point(12, 12);
            this.mCSGL12Control1.Name = "mCSGL12Control1";
            this.mCSGL12Control1.Size = new System.Drawing.Size(640, 480);
            this.mCSGL12Control1.TabIndex = 0;
            this.mCSGL12Control1.Text = "CSGL12Control1";
            // 
            // CSGL12Example1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 504);
            this.Controls.Add(this.mCSGL12Control1);
            this.Name = "CSGL12Example1Form";
            this.Text = "CSGL12Example1";
            this.ResumeLayout(false);

        }

        #endregion

        private CSGL12.CSGL12Control mCSGL12Control1;
    }
}


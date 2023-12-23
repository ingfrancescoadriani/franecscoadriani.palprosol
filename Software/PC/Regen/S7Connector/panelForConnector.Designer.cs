using System.Windows.Forms;

    partial class panelForConnector
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(panelForConnector));
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDbOrganization = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAddInterlayerToPayload = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lblPayloadMaxPayload = new System.Windows.Forms.Label();
            this.lblCustomOtherInfo = new System.Windows.Forms.Label();
            this.txtOtherInfo = new System.Windows.Forms.TextBox();
            this.lblCustomExtraData = new System.Windows.Forms.Label();
            this.txtExtraData = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.lblDbNumber = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.lblSlot = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.lblRack = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.lblIpCpu = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Location = new System.Drawing.Point(46, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(479, 551);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblDbOrganization, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(479, 551);
            this.tableLayoutPanel1.TabIndex = 34;
            // 
            // lblDbOrganization
            // 
            this.lblDbOrganization.AutoSize = true;
            this.lblDbOrganization.Location = new System.Drawing.Point(3, 155);
            this.lblDbOrganization.Name = "lblDbOrganization";
            this.lblDbOrganization.Size = new System.Drawing.Size(97, 13);
            this.lblDbOrganization.TabIndex = 33;
            this.lblDbOrganization.Text = "Organizzazione DB";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkAddInterlayerToPayload);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.lblPayloadMaxPayload);
            this.panel1.Controls.Add(this.lblCustomOtherInfo);
            this.panel1.Controls.Add(this.txtOtherInfo);
            this.panel1.Controls.Add(this.lblCustomExtraData);
            this.panel1.Controls.Add(this.txtExtraData);
            this.panel1.Controls.Add(this.textBox14);
            this.panel1.Controls.Add(this.lblDbNumber);
            this.panel1.Controls.Add(this.textBox13);
            this.panel1.Controls.Add(this.lblSlot);
            this.panel1.Controls.Add(this.textBox12);
            this.panel1.Controls.Add(this.lblRack);
            this.panel1.Controls.Add(this.textBox11);
            this.panel1.Controls.Add(this.lblIpCpu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 149);
            this.panel1.TabIndex = 0;
            // 
            // chkAddInterlayerToPayload
            // 
            this.chkAddInterlayerToPayload.Location = new System.Drawing.Point(231, 111);
            this.chkAddInterlayerToPayload.Name = "chkAddInterlayerToPayload";
            this.chkAddInterlayerToPayload.Size = new System.Drawing.Size(232, 32);
            this.chkAddInterlayerToPayload.TabIndex = 57;
            this.chkAddInterlayerToPayload.Text = "Aggiungi interfalde e pannello alla lista dei payload";
            this.chkAddInterlayerToPayload.UseVisualStyleBackColor = true;
            this.chkAddInterlayerToPayload.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(135, 116);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(79, 20);
            this.textBox3.TabIndex = 56;
            this.textBox3.Text = "100";
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // lblPayloadMaxPayload
            // 
            this.lblPayloadMaxPayload.AutoSize = true;
            this.lblPayloadMaxPayload.Location = new System.Drawing.Point(12, 119);
            this.lblPayloadMaxPayload.Name = "lblPayloadMaxPayload";
            this.lblPayloadMaxPayload.Size = new System.Drawing.Size(117, 13);
            this.lblPayloadMaxPayload.TabIndex = 55;
            this.lblPayloadMaxPayload.Text = "Numero max di payload";
            // 
            // lblCustomOtherInfo
            // 
            this.lblCustomOtherInfo.AutoSize = true;
            this.lblCustomOtherInfo.Location = new System.Drawing.Point(228, 64);
            this.lblCustomOtherInfo.Name = "lblCustomOtherInfo";
            this.lblCustomOtherInfo.Size = new System.Drawing.Size(130, 13);
            this.lblCustomOtherInfo.TabIndex = 54;
            this.lblCustomOtherInfo.Text = "Testo custom (Other_Info)";
            // 
            // txtOtherInfo
            // 
            this.txtOtherInfo.Location = new System.Drawing.Point(231, 83);
            this.txtOtherInfo.MaxLength = 38;
            this.txtOtherInfo.Multiline = true;
            this.txtOtherInfo.Name = "txtOtherInfo";
            this.txtOtherInfo.ReadOnly = true;
            this.txtOtherInfo.Size = new System.Drawing.Size(232, 20);
            this.txtOtherInfo.TabIndex = 53;
            // 
            // lblCustomExtraData
            // 
            this.lblCustomExtraData.AutoSize = true;
            this.lblCustomExtraData.Location = new System.Drawing.Point(228, 12);
            this.lblCustomExtraData.Name = "lblCustomExtraData";
            this.lblCustomExtraData.Size = new System.Drawing.Size(131, 13);
            this.lblCustomExtraData.TabIndex = 52;
            this.lblCustomExtraData.Text = "Testo custom (Extra_data)";
            // 
            // txtExtraData
            // 
            this.txtExtraData.Location = new System.Drawing.Point(231, 31);
            this.txtExtraData.MaxLength = 38;
            this.txtExtraData.Multiline = true;
            this.txtExtraData.Name = "txtExtraData";
            this.txtExtraData.ReadOnly = true;
            this.txtExtraData.Size = new System.Drawing.Size(232, 20);
            this.txtExtraData.TabIndex = 51;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(135, 90);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(79, 20);
            this.textBox14.TabIndex = 50;
            this.textBox14.Text = "1";
            this.textBox14.Leave += new System.EventHandler(this.textBox14_Leave);
            // 
            // lblDbNumber
            // 
            this.lblDbNumber.AutoSize = true;
            this.lblDbNumber.Location = new System.Drawing.Point(12, 93);
            this.lblDbNumber.Name = "lblDbNumber";
            this.lblDbNumber.Size = new System.Drawing.Size(59, 13);
            this.lblDbNumber.TabIndex = 49;
            this.lblDbNumber.Text = "Numero db";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(135, 64);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(79, 20);
            this.textBox13.TabIndex = 48;
            this.textBox13.Text = "0";
            this.textBox13.Leave += new System.EventHandler(this.textBox13_Leave);
            // 
            // lblSlot
            // 
            this.lblSlot.AutoSize = true;
            this.lblSlot.Location = new System.Drawing.Point(12, 67);
            this.lblSlot.Name = "lblSlot";
            this.lblSlot.Size = new System.Drawing.Size(25, 13);
            this.lblSlot.TabIndex = 47;
            this.lblSlot.Text = "Slot";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(135, 38);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(79, 20);
            this.textBox12.TabIndex = 46;
            this.textBox12.Text = "0";
            this.textBox12.Leave += new System.EventHandler(this.textBox12_Leave);
            // 
            // lblRack
            // 
            this.lblRack.AutoSize = true;
            this.lblRack.Location = new System.Drawing.Point(12, 41);
            this.lblRack.Name = "lblRack";
            this.lblRack.Size = new System.Drawing.Size(33, 13);
            this.lblRack.TabIndex = 45;
            this.lblRack.Text = "Rack";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(135, 12);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(79, 20);
            this.textBox11.TabIndex = 44;
            this.textBox11.Text = "192.168.0.1";
            this.textBox11.Leave += new System.EventHandler(this.textBox11_Leave);
            // 
            // lblIpCpu
            // 
            this.lblIpCpu.AutoSize = true;
            this.lblIpCpu.Location = new System.Drawing.Point(12, 15);
            this.lblIpCpu.Name = "lblIpCpu";
            this.lblIpCpu.Size = new System.Drawing.Size(37, 13);
            this.lblIpCpu.TabIndex = 43;
            this.lblIpCpu.Text = "Ip cpu";
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 177);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(473, 371);
            this.panel3.TabIndex = 34;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, -3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(859, 3936);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelForConnector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Name = "panelForConnector";
            this.Size = new System.Drawing.Size(630, 581);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel2;

        public Panel getPanel()
        {
            return this.panel2;
        }
        private Timer timer1;
        private TableLayoutPanel tableLayoutPanel1;
        public Label lblDbOrganization;
        private Panel panel1;
        public TextBox textBox14;
        public Label lblDbNumber;
        public TextBox textBox13;
        public Label lblSlot;
        public TextBox textBox12;
        public Label lblRack;
        public TextBox textBox11;
        public Label lblIpCpu;
        private Panel panel3;
        private PictureBox pictureBox1;
        public Label lblCustomExtraData;
        public TextBox txtExtraData;
        public Label lblCustomOtherInfo;
        public TextBox txtOtherInfo;
        public TextBox textBox3;
        public Label lblPayloadMaxPayload;
        public CheckBox chkAddInterlayerToPayload;
    }

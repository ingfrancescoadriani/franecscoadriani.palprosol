namespace ReGen
{
    partial class OpenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenForm));
            this.ButtonOpenApri = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonOpenScegliCartellaContenenteLeRicette = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelOpenCartellaContenenteLeRicette = new System.Windows.Forms.Label();
            this.labelOpenFiltra = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonOpenCerca = new System.Windows.Forms.Button();
            this.checkBoxOpenCercaAncheNeiDatiDellRicetta = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxOpenNonMostrareAllAvvio = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.ButtonOpenAnnulla = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonOpenApri
            // 
            this.ButtonOpenApri.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOpenApri.Enabled = false;
            this.ButtonOpenApri.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOpenApri.Location = new System.Drawing.Point(835, 409);
            this.ButtonOpenApri.Name = "ButtonOpenApri";
            this.ButtonOpenApri.Size = new System.Drawing.Size(122, 36);
            this.ButtonOpenApri.TabIndex = 0;
            this.ButtonOpenApri.Text = "Apri";
            this.ButtonOpenApri.UseVisualStyleBackColor = true;
            this.ButtonOpenApri.Click += new System.EventHandler(this.button1_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // buttonOpenScegliCartellaContenenteLeRicette
            // 
            this.buttonOpenScegliCartellaContenenteLeRicette.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenScegliCartellaContenenteLeRicette.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenScegliCartellaContenenteLeRicette.Location = new System.Drawing.Point(887, 4);
            this.buttonOpenScegliCartellaContenenteLeRicette.Name = "buttonOpenScegliCartellaContenenteLeRicette";
            this.buttonOpenScegliCartellaContenenteLeRicette.Size = new System.Drawing.Size(70, 28);
            this.buttonOpenScegliCartellaContenenteLeRicette.TabIndex = 28;
            this.buttonOpenScegliCartellaContenenteLeRicette.Text = "Scegli";
            this.buttonOpenScegliCartellaContenenteLeRicette.UseVisualStyleBackColor = true;
            this.buttonOpenScegliCartellaContenenteLeRicette.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Gray;
            this.textBox1.Location = new System.Drawing.Point(220, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(667, 26);
            this.textBox1.TabIndex = 27;
            // 
            // labelOpenCartellaContenenteLeRicette
            // 
            this.labelOpenCartellaContenenteLeRicette.AutoSize = true;
            this.labelOpenCartellaContenenteLeRicette.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOpenCartellaContenenteLeRicette.ForeColor = System.Drawing.Color.White;
            this.labelOpenCartellaContenenteLeRicette.Location = new System.Drawing.Point(4, 8);
            this.labelOpenCartellaContenenteLeRicette.Name = "labelOpenCartellaContenenteLeRicette";
            this.labelOpenCartellaContenenteLeRicette.Size = new System.Drawing.Size(212, 20);
            this.labelOpenCartellaContenenteLeRicette.TabIndex = 29;
            this.labelOpenCartellaContenenteLeRicette.Text = "Cartella contenente le ricette";
            // 
            // labelOpenFiltra
            // 
            this.labelOpenFiltra.AutoSize = true;
            this.labelOpenFiltra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOpenFiltra.ForeColor = System.Drawing.Color.White;
            this.labelOpenFiltra.Location = new System.Drawing.Point(4, 41);
            this.labelOpenFiltra.Name = "labelOpenFiltra";
            this.labelOpenFiltra.Size = new System.Drawing.Size(44, 20);
            this.labelOpenFiltra.TabIndex = 30;
            this.labelOpenFiltra.Text = "Filtra";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(57, 37);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(613, 26);
            this.textBox2.TabIndex = 31;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Margin = new System.Windows.Forms.Padding(0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(474, 337);
            this.listView1.TabIndex = 32;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonOpenCerca
            // 
            this.buttonOpenCerca.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenCerca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenCerca.Location = new System.Drawing.Point(670, 36);
            this.buttonOpenCerca.Name = "buttonOpenCerca";
            this.buttonOpenCerca.Size = new System.Drawing.Size(70, 28);
            this.buttonOpenCerca.TabIndex = 33;
            this.buttonOpenCerca.Text = "Cerca";
            this.buttonOpenCerca.UseVisualStyleBackColor = true;
            this.buttonOpenCerca.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBoxOpenCercaAncheNeiDatiDellRicetta
            // 
            this.checkBoxOpenCercaAncheNeiDatiDellRicetta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOpenCercaAncheNeiDatiDellRicetta.AutoSize = true;
            this.checkBoxOpenCercaAncheNeiDatiDellRicetta.ForeColor = System.Drawing.Color.White;
            this.checkBoxOpenCercaAncheNeiDatiDellRicetta.Location = new System.Drawing.Point(745, 47);
            this.checkBoxOpenCercaAncheNeiDatiDellRicetta.Name = "checkBoxOpenCercaAncheNeiDatiDellRicetta";
            this.checkBoxOpenCercaAncheNeiDatiDellRicetta.Size = new System.Drawing.Size(148, 17);
            this.checkBoxOpenCercaAncheNeiDatiDellRicetta.TabIndex = 34;
            this.checkBoxOpenCercaAncheNeiDatiDellRicetta.Text = "Cerca nei dati della ricetta";
            this.checkBoxOpenCercaAncheNeiDatiDellRicetta.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.listView1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 69);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 337F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(949, 337);
            this.tableLayoutPanel1.TabIndex = 39;
            // 
            // checkBoxOpenNonMostrareAllAvvio
            // 
            this.checkBoxOpenNonMostrareAllAvvio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxOpenNonMostrareAllAvvio.AutoSize = true;
            this.checkBoxOpenNonMostrareAllAvvio.ForeColor = System.Drawing.Color.White;
            this.checkBoxOpenNonMostrareAllAvvio.Location = new System.Drawing.Point(8, 428);
            this.checkBoxOpenNonMostrareAllAvvio.Name = "checkBoxOpenNonMostrareAllAvvio";
            this.checkBoxOpenNonMostrareAllAvvio.Size = new System.Drawing.Size(130, 17);
            this.checkBoxOpenNonMostrareAllAvvio.TabIndex = 40;
            this.checkBoxOpenNonMostrareAllAvvio.Text = "Non mostrare all\'avvio";
            this.checkBoxOpenNonMostrareAllAvvio.UseVisualStyleBackColor = true;
            this.checkBoxOpenNonMostrareAllAvvio.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox3.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox3.BackColor = System.Drawing.Color.White;
            this.checkBox3.BackgroundImage = global::ReGen.Properties.Resources.view_icons;
            this.checkBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(925, 36);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(28, 28);
            this.checkBox3.TabIndex = 38;
            this.checkBox3.UseVisualStyleBackColor = false;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            this.checkBox3.Click += new System.EventHandler(this.checkBox3_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox2.BackColor = System.Drawing.Color.White;
            this.checkBox2.BackgroundImage = global::ReGen.Properties.Resources.view_details;
            this.checkBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.checkBox2.Location = new System.Drawing.Point(897, 36);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(28, 28);
            this.checkBox2.TabIndex = 37;
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.Click += new System.EventHandler(this.checkBox2_Click);
            // 
            // ButtonOpenAnnulla
            // 
            this.ButtonOpenAnnulla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOpenAnnulla.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOpenAnnulla.Location = new System.Drawing.Point(711, 409);
            this.ButtonOpenAnnulla.Name = "ButtonOpenAnnulla";
            this.ButtonOpenAnnulla.Size = new System.Drawing.Size(122, 36);
            this.ButtonOpenAnnulla.TabIndex = 42;
            this.ButtonOpenAnnulla.Text = "Annulla";
            this.ButtonOpenAnnulla.UseVisualStyleBackColor = true;
            this.ButtonOpenAnnulla.Click += new System.EventHandler(this.button5_Click);
            // 
            // OpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(964, 453);
            this.Controls.Add(this.ButtonOpenAnnulla);
            this.Controls.Add(this.checkBoxOpenNonMostrareAllAvvio);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBoxOpenCercaAncheNeiDatiDellRicetta);
            this.Controls.Add(this.buttonOpenCerca);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labelOpenFiltra);
            this.Controls.Add(this.labelOpenCartellaContenenteLeRicette);
            this.Controls.Add(this.buttonOpenScegliCartellaContenenteLeRicette);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ButtonOpenApri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpenForm";
            this.Text = "Apri";
            this.Load += new System.EventHandler(this.OpenForm_Load);
            this.Resize += new System.EventHandler(this.OpenForm_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonOpenApri;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonOpenScegliCartellaContenenteLeRicette;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelOpenCartellaContenenteLeRicette;
        private System.Windows.Forms.Label labelOpenFiltra;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button buttonOpenCerca;
        private System.Windows.Forms.CheckBox checkBoxOpenCercaAncheNeiDatiDellRicetta;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBoxOpenNonMostrareAllAvvio;
        private System.Windows.Forms.Button ButtonOpenAnnulla;
    }
}
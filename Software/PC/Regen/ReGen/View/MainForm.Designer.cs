/*
 * Created by SharpDevelop.
 * User: Rice Cipriani
 * Date: 26/09/2012
 * Time: 01:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Windows.Forms;
namespace ReGen
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.leftPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelPayload = new System.Windows.Forms.Label();
            this.bankDepotPanel = new System.Windows.Forms.Panel();
            this.panelPayloadInsertion = new System.Windows.Forms.Panel();
            this.button33 = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.buttonPayloadInsertionReset = new System.Windows.Forms.Button();
            this.buttonPayloadInsertionPlace = new System.Windows.Forms.Button();
            this.textBoxPanelInsertionQuadrant = new System.Windows.Forms.TextBox();
            this.labelPanelInsertionQuadrant4 = new System.Windows.Forms.Label();
            this.textBoxPanelInsertionY = new System.Windows.Forms.TextBox();
            this.labelPanelInsertionY = new System.Windows.Forms.Label();
            this.textBoxPanelInsertionX = new System.Windows.Forms.TextBox();
            this.labelPanelInsertionX = new System.Windows.Forms.Label();
            this.extTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.labelSelezione = new System.Windows.Forms.Label();
            this.labelPuntatore = new System.Windows.Forms.Label();
            this.labelSelezioneSx = new System.Windows.Forms.Label();
            this.labelSelezioneBasso = new System.Windows.Forms.Label();
            this.labelPuntatoreX = new System.Windows.Forms.Label();
            this.labelPuntatoreY = new System.Windows.Forms.Label();
            this.lblSelectionXValue = new System.Windows.Forms.Label();
            this.lblSelectionYValue = new System.Windows.Forms.Label();
            this.lblPointerXValue = new System.Windows.Forms.Label();
            this.lblPointerYValue = new System.Windows.Forms.Label();
            this.depotPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableOutSchemes = new System.Windows.Forms.TableLayoutPanel();
            this.button35 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.outPanelWhereDraw = new System.Windows.Forms.Panel();
            this.panelWhereDraw = new System.Windows.Forms.Panel();
            this.labelStratiPallet = new System.Windows.Forms.Label();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.inPanel = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tableZoomPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonMinus = new System.Windows.Forms.Button();
            this.buttonPlus = new System.Windows.Forms.Button();
            this.panelScale = new System.Windows.Forms.Panel();
            this.buttonWFit = new System.Windows.Forms.Button();
            this.buttonHFit = new System.Windows.Forms.Button();
            this.zoomTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.labelStrato = new System.Windows.Forms.Label();
            this.btnTool = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.labelConnettoreImpostazioni = new System.Windows.Forms.Label();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.labelConnettoreAnteprimaDati = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDati = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.paneContainerPreview = new System.Windows.Forms.Panel();
            this.panePreview = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkboxInterfaldaPresenzaSuPallet = new System.Windows.Forms.CheckBox();
            this.txtPanelThickness = new System.Windows.Forms.TextBox();
            this.labelPannelloSpessore = new System.Windows.Forms.Label();
            this.checkboxPannelloPresenzaSuPallet = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtInterlayerThickness = new System.Windows.Forms.TextBox();
            this.button20 = new System.Windows.Forms.Button();
            this.labelInterfaldaSpessore = new System.Windows.Forms.Label();
            this.txtPalletMaxHeight = new System.Windows.Forms.TextBox();
            this.button21 = new System.Windows.Forms.Button();
            this.labelPalletAltezzaMassima = new System.Windows.Forms.Label();
            this.button22 = new System.Windows.Forms.Button();
            this.txtExtraBorderY = new System.Windows.Forms.TextBox();
            this.button23 = new System.Windows.Forms.Button();
            this.labelPalletLunghezzaExtra = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.txtExtraBorderX = new System.Windows.Forms.TextBox();
            this.labelPalletLarghezzaExtra = new System.Windows.Forms.Label();
            this.txtPalletHeight = new System.Windows.Forms.TextBox();
            this.txtPalletLenght = new System.Windows.Forms.TextBox();
            this.txtPalletWidth = new System.Windows.Forms.TextBox();
            this.labelPalletAltezza = new System.Windows.Forms.Label();
            this.labelPalletLunghezza = new System.Windows.Forms.Label();
            this.labelPalletLarghezza = new System.Windows.Forms.Label();
            this.labelPallet = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.labelPayload2Nome = new System.Windows.Forms.Label();
            this.labelPayload2Larghezza = new System.Windows.Forms.Label();
            this.txtPayload2Name = new System.Windows.Forms.TextBox();
            this.labelPayload2Lunghezza = new System.Windows.Forms.Label();
            this.labelPayload2Altezza = new System.Windows.Forms.Label();
            this.txtBox2Z = new System.Windows.Forms.TextBox();
            this.txtBox2X = new System.Windows.Forms.TextBox();
            this.txtBox2Y = new System.Windows.Forms.TextBox();
            this.cmbPayloadsNumber = new System.Windows.Forms.ComboBox();
            this.txtPayload1Name = new System.Windows.Forms.TextBox();
            this.labelPayload1Nome = new System.Windows.Forms.Label();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtBoxZ = new System.Windows.Forms.TextBox();
            this.txtBoxY = new System.Windows.Forms.TextBox();
            this.txtBoxX = new System.Windows.Forms.TextBox();
            this.labelPayload1Altezza = new System.Windows.Forms.Label();
            this.labelPayload1Lunghezza = new System.Windows.Forms.Label();
            this.labelPayload1Larghezza = new System.Windows.Forms.Label();
            this.labelPayloads = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.buttonDepositoApplica = new System.Windows.Forms.Button();
            this.txtApproachZ = new System.Windows.Forms.TextBox();
            this.txtApproachY = new System.Windows.Forms.TextBox();
            this.txtCustomPlacement = new System.Windows.Forms.TextBox();
            this.labelDepositoTestoCustomPlacementInfo = new System.Windows.Forms.Label();
            this.txtCustomPick = new System.Windows.Forms.TextBox();
            this.labelDepositoTestoCustomExtraData = new System.Windows.Forms.Label();
            this.buttonDepositoSE = new System.Windows.Forms.Button();
            this.buttonDepositoSW = new System.Windows.Forms.Button();
            this.buttonDepositoAutomatico = new System.Windows.Forms.Button();
            this.buttonDepositoNE = new System.Windows.Forms.Button();
            this.buttonDepositoNW = new System.Windows.Forms.Button();
            this.txtApproachX = new System.Windows.Forms.TextBox();
            this.labelDepositoAccostamento = new System.Windows.Forms.Label();
            this.labelOpzioniDiDeposito = new System.Windows.Forms.Label();
            this.button25 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.button30 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labelDepositoInizio = new System.Windows.Forms.Label();
            this.tabPageLayout = new System.Windows.Forms.TabPage();
            this.panelExt = new System.Windows.Forms.Panel();
            this.tabPageGenerazione = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txtConfig = new System.Windows.Forms.Label();
            this.buttonConnettoreRefreshAnteprima = new System.Windows.Forms.Button();
            this.buttonConnettoreInvia = new System.Windows.Forms.Button();
            this.labelConnettoreIstruzioni = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelConnettoreVirtuale = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.soloInLarghezzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soloInLunghezzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inEntrambiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.aSinistraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDestraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inBassoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inAltoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equidistribuisciToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inLarghezzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inAltezzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inEntrambiToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.collegaISelezionatiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scollegaISelezionatiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ricopiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.impostaProgressivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ricopiaStratoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stratoSelezionatoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primiDueStratiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tuttiGliStratiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancellaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spostaGiùToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spostaSuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capovolgiOrizzontalmenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capovolgiVerticalmenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruotaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selezionaTuttoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvaSchemaDiPallettizzazioneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.licenzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemLang = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.riguardoRegenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panelLogoMobile = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.panelPayloadInsertion.SuspendLayout();
            this.extTableLayout.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.depotPanel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableOutSchemes.SuspendLayout();
            this.outPanelWhereDraw.SuspendLayout();
            this.centerPanel.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableZoomPanel.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageDati.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.paneContainerPreview.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel12.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel13.SuspendLayout();
            this.tabPageLayout.SuspendLayout();
            this.panelExt.SuspendLayout();
            this.tabPageGenerazione.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel11.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panelLogoMobile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.leftPanel);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.extTableLayout);
            this.splitContainerMain.Size = new System.Drawing.Size(1753, 810);
            this.splitContainerMain.SplitterDistance = 311;
            this.splitContainerMain.TabIndex = 8;
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.Transparent;
            this.leftPanel.ColumnCount = 1;
            this.leftPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftPanel.Controls.Add(this.labelPayload, 0, 0);
            this.leftPanel.Controls.Add(this.bankDepotPanel, 0, 1);
            this.leftPanel.Controls.Add(this.panelPayloadInsertion, 0, 2);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Margin = new System.Windows.Forms.Padding(0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.RowCount = 3;
            this.leftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.leftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.leftPanel.Size = new System.Drawing.Size(311, 810);
            this.leftPanel.TabIndex = 3;
            // 
            // labelPayload
            // 
            this.labelPayload.AutoSize = true;
            this.labelPayload.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelPayload.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPayload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPayload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelPayload.Location = new System.Drawing.Point(3, 0);
            this.labelPayload.Name = "labelPayload";
            this.labelPayload.Size = new System.Drawing.Size(305, 20);
            this.labelPayload.TabIndex = 10;
            this.labelPayload.Text = "Payload";
            this.labelPayload.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bankDepotPanel
            // 
            this.bankDepotPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bankDepotPanel.BackColor = System.Drawing.Color.Transparent;
            this.bankDepotPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bankDepotPanel.Location = new System.Drawing.Point(0, 20);
            this.bankDepotPanel.Margin = new System.Windows.Forms.Padding(0);
            this.bankDepotPanel.Name = "bankDepotPanel";
            this.bankDepotPanel.Size = new System.Drawing.Size(311, 790);
            this.bankDepotPanel.TabIndex = 11;
            this.bankDepotPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.bankDepotPanel_Paint);
            this.bankDepotPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bankDepotPanel_MouseDown);
            // 
            // panelPayloadInsertion
            // 
            this.panelPayloadInsertion.BackColor = System.Drawing.Color.Transparent;
            this.panelPayloadInsertion.Controls.Add(this.button33);
            this.panelPayloadInsertion.Controls.Add(this.label29);
            this.panelPayloadInsertion.Controls.Add(this.buttonPayloadInsertionReset);
            this.panelPayloadInsertion.Controls.Add(this.buttonPayloadInsertionPlace);
            this.panelPayloadInsertion.Controls.Add(this.textBoxPanelInsertionQuadrant);
            this.panelPayloadInsertion.Controls.Add(this.labelPanelInsertionQuadrant4);
            this.panelPayloadInsertion.Controls.Add(this.textBoxPanelInsertionY);
            this.panelPayloadInsertion.Controls.Add(this.labelPanelInsertionY);
            this.panelPayloadInsertion.Controls.Add(this.textBoxPanelInsertionX);
            this.panelPayloadInsertion.Controls.Add(this.labelPanelInsertionX);
            this.panelPayloadInsertion.Location = new System.Drawing.Point(3, 813);
            this.panelPayloadInsertion.Name = "panelPayloadInsertion";
            this.panelPayloadInsertion.Size = new System.Drawing.Size(165, 1);
            this.panelPayloadInsertion.TabIndex = 0;
            this.panelPayloadInsertion.Visible = false;
            // 
            // button33
            // 
            this.button33.Location = new System.Drawing.Point(3, 167);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(171, 22);
            this.button33.TabIndex = 9;
            this.button33.Tag = "LIBERO";
            this.button33.Text = "Inserisci/Rimuovi interfalda";
            this.button33.UseVisualStyleBackColor = true;
            this.button33.Click += new System.EventHandler(this.button33_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(39, 7);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(99, 13);
            this.label29.TabIndex = 8;
            this.label29.Text = "Coordinate Payload";
            this.label29.Visible = false;
            // 
            // buttonPayloadInsertionReset
            // 
            this.buttonPayloadInsertionReset.Location = new System.Drawing.Point(0, 0);
            this.buttonPayloadInsertionReset.Name = "buttonPayloadInsertionReset";
            this.buttonPayloadInsertionReset.Size = new System.Drawing.Size(75, 23);
            this.buttonPayloadInsertionReset.TabIndex = 10;
            // 
            // buttonPayloadInsertionPlace
            // 
            this.buttonPayloadInsertionPlace.Location = new System.Drawing.Point(0, 0);
            this.buttonPayloadInsertionPlace.Name = "buttonPayloadInsertionPlace";
            this.buttonPayloadInsertionPlace.Size = new System.Drawing.Size(75, 23);
            this.buttonPayloadInsertionPlace.TabIndex = 11;
            // 
            // textBoxPanelInsertionQuadrant
            // 
            this.textBoxPanelInsertionQuadrant.Location = new System.Drawing.Point(109, 97);
            this.textBoxPanelInsertionQuadrant.Name = "textBoxPanelInsertionQuadrant";
            this.textBoxPanelInsertionQuadrant.Size = new System.Drawing.Size(46, 20);
            this.textBoxPanelInsertionQuadrant.TabIndex = 5;
            this.textBoxPanelInsertionQuadrant.Visible = false;
            // 
            // labelPanelInsertionQuadrant4
            // 
            this.labelPanelInsertionQuadrant4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelPanelInsertionQuadrant4.Location = new System.Drawing.Point(20, 97);
            this.labelPanelInsertionQuadrant4.Name = "labelPanelInsertionQuadrant4";
            this.labelPanelInsertionQuadrant4.Size = new System.Drawing.Size(93, 24);
            this.labelPanelInsertionQuadrant4.TabIndex = 4;
            this.labelPanelInsertionQuadrant4.Text = "Quadrante:";
            this.labelPanelInsertionQuadrant4.Visible = false;
            // 
            // textBoxPanelInsertionY
            // 
            this.textBoxPanelInsertionY.Location = new System.Drawing.Point(55, 64);
            this.textBoxPanelInsertionY.Name = "textBoxPanelInsertionY";
            this.textBoxPanelInsertionY.Size = new System.Drawing.Size(100, 20);
            this.textBoxPanelInsertionY.TabIndex = 3;
            this.textBoxPanelInsertionY.Visible = false;
            // 
            // labelPanelInsertionY
            // 
            this.labelPanelInsertionY.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.labelPanelInsertionY.Location = new System.Drawing.Point(20, 62);
            this.labelPanelInsertionY.Name = "labelPanelInsertionY";
            this.labelPanelInsertionY.Size = new System.Drawing.Size(29, 24);
            this.labelPanelInsertionY.TabIndex = 2;
            this.labelPanelInsertionY.Text = "Y:";
            this.labelPanelInsertionY.Visible = false;
            // 
            // textBoxPanelInsertionX
            // 
            this.textBoxPanelInsertionX.Location = new System.Drawing.Point(55, 29);
            this.textBoxPanelInsertionX.Name = "textBoxPanelInsertionX";
            this.textBoxPanelInsertionX.Size = new System.Drawing.Size(100, 20);
            this.textBoxPanelInsertionX.TabIndex = 1;
            this.textBoxPanelInsertionX.Visible = false;
            // 
            // labelPanelInsertionX
            // 
            this.labelPanelInsertionX.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.labelPanelInsertionX.Location = new System.Drawing.Point(20, 27);
            this.labelPanelInsertionX.Name = "labelPanelInsertionX";
            this.labelPanelInsertionX.Size = new System.Drawing.Size(29, 24);
            this.labelPanelInsertionX.TabIndex = 0;
            this.labelPanelInsertionX.Text = "X:";
            this.labelPanelInsertionX.Visible = false;
            // 
            // extTableLayout
            // 
            this.extTableLayout.ColumnCount = 2;
            this.extTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.extTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.extTableLayout.Controls.Add(this.panel5, 1, 5);
            this.extTableLayout.Controls.Add(this.depotPanel, 1, 4);
            this.extTableLayout.Controls.Add(this.labelStratiPallet, 1, 3);
            this.extTableLayout.Controls.Add(this.centerPanel, 1, 1);
            this.extTableLayout.Controls.Add(this.panel6, 1, 2);
            this.extTableLayout.Controls.Add(this.tableLayoutPanel8, 1, 0);
            this.extTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extTableLayout.Location = new System.Drawing.Point(0, 0);
            this.extTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.extTableLayout.Name = "extTableLayout";
            this.extTableLayout.RowCount = 6;
            this.extTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.extTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.extTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.extTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.extTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 181F));
            this.extTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.extTableLayout.Size = new System.Drawing.Size(1438, 810);
            this.extTableLayout.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.tableLayoutPanel4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 783);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1432, 24);
            this.panel5.TabIndex = 9;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1432, 24);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 14;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 600F));
            this.tableLayoutPanel5.Controls.Add(this.labelSelezione, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelPuntatore, 6, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelSelezioneSx, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelSelezioneBasso, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelPuntatoreX, 7, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelPuntatoreY, 9, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblSelectionXValue, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblSelectionYValue, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblPointerXValue, 8, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblPointerYValue, 10, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1432, 24);
            this.tableLayoutPanel5.TabIndex = 10;
            // 
            // labelSelezione
            // 
            this.labelSelezione.AutoSize = true;
            this.labelSelezione.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelSelezione.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelSelezione.Location = new System.Drawing.Point(3, 0);
            this.labelSelezione.Name = "labelSelezione";
            this.labelSelezione.Size = new System.Drawing.Size(82, 24);
            this.labelSelezione.TabIndex = 0;
            this.labelSelezione.Text = "Selezione";
            this.labelSelezione.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPuntatore
            // 
            this.labelPuntatore.AutoSize = true;
            this.labelPuntatore.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelPuntatore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelPuntatore.Location = new System.Drawing.Point(443, 0);
            this.labelPuntatore.Name = "labelPuntatore";
            this.labelPuntatore.Size = new System.Drawing.Size(62, 24);
            this.labelPuntatore.TabIndex = 1;
            this.labelPuntatore.Text = "Pointer";
            this.labelPuntatore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSelezioneSx
            // 
            this.labelSelezioneSx.AutoSize = true;
            this.labelSelezioneSx.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelSelezioneSx.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelSelezioneSx.Location = new System.Drawing.Point(103, 0);
            this.labelSelezioneSx.Name = "labelSelezioneSx";
            this.labelSelezioneSx.Size = new System.Drawing.Size(27, 24);
            this.labelSelezioneSx.TabIndex = 2;
            this.labelSelezioneSx.Text = "left";
            this.labelSelezioneSx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelSelezioneBasso
            // 
            this.labelSelezioneBasso.AutoSize = true;
            this.labelSelezioneBasso.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelSelezioneBasso.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelSelezioneBasso.Location = new System.Drawing.Point(263, 0);
            this.labelSelezioneBasso.Name = "labelSelezioneBasso";
            this.labelSelezioneBasso.Size = new System.Drawing.Size(55, 24);
            this.labelSelezioneBasso.TabIndex = 3;
            this.labelSelezioneBasso.Text = "bottom";
            this.labelSelezioneBasso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPuntatoreX
            // 
            this.labelPuntatoreX.AutoSize = true;
            this.labelPuntatoreX.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelPuntatoreX.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPuntatoreX.Location = new System.Drawing.Point(543, 0);
            this.labelPuntatoreX.Name = "labelPuntatoreX";
            this.labelPuntatoreX.Size = new System.Drawing.Size(14, 24);
            this.labelPuntatoreX.TabIndex = 4;
            this.labelPuntatoreX.Text = "x";
            this.labelPuntatoreX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPuntatoreY
            // 
            this.labelPuntatoreY.AutoSize = true;
            this.labelPuntatoreY.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelPuntatoreY.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPuntatoreY.Location = new System.Drawing.Point(653, 0);
            this.labelPuntatoreY.Name = "labelPuntatoreY";
            this.labelPuntatoreY.Size = new System.Drawing.Size(14, 24);
            this.labelPuntatoreY.TabIndex = 5;
            this.labelPuntatoreY.Text = "y";
            this.labelPuntatoreY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSelectionXValue
            // 
            this.lblSelectionXValue.AutoSize = true;
            this.lblSelectionXValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSelectionXValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblSelectionXValue.Location = new System.Drawing.Point(177, 0);
            this.lblSelectionXValue.Name = "lblSelectionXValue";
            this.lblSelectionXValue.Size = new System.Drawing.Size(12, 24);
            this.lblSelectionXValue.TabIndex = 6;
            this.lblSelectionXValue.Text = " ";
            this.lblSelectionXValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSelectionYValue
            // 
            this.lblSelectionYValue.AutoSize = true;
            this.lblSelectionYValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSelectionYValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblSelectionYValue.Location = new System.Drawing.Point(333, 0);
            this.lblSelectionYValue.Name = "lblSelectionYValue";
            this.lblSelectionYValue.Size = new System.Drawing.Size(12, 24);
            this.lblSelectionYValue.TabIndex = 7;
            this.lblSelectionYValue.Text = " ";
            this.lblSelectionYValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPointerXValue
            // 
            this.lblPointerXValue.AutoSize = true;
            this.lblPointerXValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPointerXValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblPointerXValue.Location = new System.Drawing.Point(563, 0);
            this.lblPointerXValue.Name = "lblPointerXValue";
            this.lblPointerXValue.Size = new System.Drawing.Size(0, 24);
            this.lblPointerXValue.TabIndex = 8;
            this.lblPointerXValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPointerYValue
            // 
            this.lblPointerYValue.AutoSize = true;
            this.lblPointerYValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPointerYValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblPointerYValue.Location = new System.Drawing.Point(673, 0);
            this.lblPointerYValue.Name = "lblPointerYValue";
            this.lblPointerYValue.Size = new System.Drawing.Size(0, 24);
            this.lblPointerYValue.TabIndex = 9;
            this.lblPointerYValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // depotPanel
            // 
            this.depotPanel.AutoScroll = true;
            this.depotPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.depotPanel.BackColor = System.Drawing.Color.White;
            this.depotPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.depotPanel.Controls.Add(this.tableLayoutPanel3);
            this.depotPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.depotPanel.Location = new System.Drawing.Point(0, 599);
            this.depotPanel.Margin = new System.Windows.Forms.Padding(0);
            this.depotPanel.Name = "depotPanel";
            this.depotPanel.Size = new System.Drawing.Size(1438, 181);
            this.depotPanel.TabIndex = 7;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableOutSchemes, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.outPanelWhereDraw, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 177F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1434, 177);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableOutSchemes
            // 
            this.tableOutSchemes.ColumnCount = 1;
            this.tableOutSchemes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableOutSchemes.Controls.Add(this.button35, 0, 4);
            this.tableOutSchemes.Controls.Add(this.button12, 0, 2);
            this.tableOutSchemes.Controls.Add(this.button6, 0, 0);
            this.tableOutSchemes.Controls.Add(this.button13, 0, 1);
            this.tableOutSchemes.Controls.Add(this.button11, 0, 3);
            this.tableOutSchemes.Location = new System.Drawing.Point(3, 3);
            this.tableOutSchemes.Name = "tableOutSchemes";
            this.tableOutSchemes.RowCount = 5;
            this.tableOutSchemes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableOutSchemes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableOutSchemes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableOutSchemes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableOutSchemes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableOutSchemes.Size = new System.Drawing.Size(44, 171);
            this.tableOutSchemes.TabIndex = 0;
            // 
            // button35
            // 
            this.button35.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button35.BackgroundImage")));
            this.button35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button35.Location = new System.Drawing.Point(1, 137);
            this.button35.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(43, 33);
            this.button35.TabIndex = 6;
            this.button35.UseVisualStyleBackColor = true;
            this.button35.Click += new System.EventHandler(this.button35_Click);
            // 
            // button12
            // 
            this.button12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button12.Image = ((System.Drawing.Image)(resources.GetObject("button12.Image")));
            this.button12.Location = new System.Drawing.Point(1, 69);
            this.button12.Margin = new System.Windows.Forms.Padding(1);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(42, 32);
            this.button12.TabIndex = 3;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button6
            // 
            this.button6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.Location = new System.Drawing.Point(1, 1);
            this.button6.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(43, 32);
            this.button6.TabIndex = 0;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button13
            // 
            this.button13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button13.Image = ((System.Drawing.Image)(resources.GetObject("button13.Image")));
            this.button13.Location = new System.Drawing.Point(1, 35);
            this.button13.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(43, 32);
            this.button13.TabIndex = 5;
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button11
            // 
            this.button11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button11.Image = ((System.Drawing.Image)(resources.GetObject("button11.Image")));
            this.button11.Location = new System.Drawing.Point(1, 103);
            this.button11.Margin = new System.Windows.Forms.Padding(1, 1, 0, 1);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(43, 32);
            this.button11.TabIndex = 4;
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // outPanelWhereDraw
            // 
            this.outPanelWhereDraw.AutoScroll = true;
            this.outPanelWhereDraw.AutoSize = true;
            this.outPanelWhereDraw.BackColor = System.Drawing.Color.White;
            this.outPanelWhereDraw.Controls.Add(this.panelWhereDraw);
            this.outPanelWhereDraw.Location = new System.Drawing.Point(50, 0);
            this.outPanelWhereDraw.Margin = new System.Windows.Forms.Padding(0);
            this.outPanelWhereDraw.Name = "outPanelWhereDraw";
            this.outPanelWhereDraw.Size = new System.Drawing.Size(506, 175);
            this.outPanelWhereDraw.TabIndex = 5;
            // 
            // panelWhereDraw
            // 
            this.panelWhereDraw.AutoScroll = true;
            this.panelWhereDraw.Location = new System.Drawing.Point(6, 0);
            this.panelWhereDraw.Margin = new System.Windows.Forms.Padding(0);
            this.panelWhereDraw.Name = "panelWhereDraw";
            this.panelWhereDraw.Size = new System.Drawing.Size(500, 175);
            this.panelWhereDraw.TabIndex = 6;
            this.panelWhereDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.panelWhereDraw_Paint);
            this.panelWhereDraw.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelWhereDraw_MouseClick);
            this.panelWhereDraw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelWhereDraw_MouseDown);
            // 
            // labelStratiPallet
            // 
            this.labelStratiPallet.AutoSize = true;
            this.labelStratiPallet.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelStratiPallet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStratiPallet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStratiPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelStratiPallet.Location = new System.Drawing.Point(3, 581);
            this.labelStratiPallet.Name = "labelStratiPallet";
            this.labelStratiPallet.Size = new System.Drawing.Size(1432, 18);
            this.labelStratiPallet.TabIndex = 8;
            this.labelStratiPallet.Text = "Layers Pallet";
            this.labelStratiPallet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // centerPanel
            // 
            this.centerPanel.AutoScroll = true;
            this.centerPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.centerPanel.BackColor = System.Drawing.Color.Transparent;
            this.centerPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.centerPanel.Controls.Add(this.inPanel);
            this.centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerPanel.Location = new System.Drawing.Point(0, 20);
            this.centerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(1438, 534);
            this.centerPanel.TabIndex = 4;
            this.centerPanel.Scroll += new System.Windows.Forms.ScrollEventHandler(this.centerPanel_Scroll);
            // 
            // inPanel
            // 
            this.inPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.inPanel.BackColor = System.Drawing.Color.White;
            this.inPanel.Location = new System.Drawing.Point(0, 0);
            this.inPanel.Margin = new System.Windows.Forms.Padding(0);
            this.inPanel.Name = "inPanel";
            this.inPanel.Size = new System.Drawing.Size(100, 221);
            this.inPanel.TabIndex = 0;
            this.inPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.inPanel_Paint);
            this.inPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.inPanel_MouseDown);
            this.inPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.inPanel_MouseUp);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tableZoomPanel);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 554);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1438, 27);
            this.panel6.TabIndex = 6;
            // 
            // tableZoomPanel
            // 
            this.tableZoomPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableZoomPanel.ColumnCount = 8;
            this.tableZoomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableZoomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableZoomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableZoomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableZoomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableZoomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableZoomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableZoomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableZoomPanel.Controls.Add(this.buttonMinus, 0, 0);
            this.tableZoomPanel.Controls.Add(this.buttonPlus, 2, 0);
            this.tableZoomPanel.Controls.Add(this.panelScale, 7, 0);
            this.tableZoomPanel.Controls.Add(this.buttonWFit, 4, 0);
            this.tableZoomPanel.Controls.Add(this.buttonHFit, 5, 0);
            this.tableZoomPanel.Controls.Add(this.zoomTextBox, 1, 0);
            this.tableZoomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableZoomPanel.Location = new System.Drawing.Point(0, 0);
            this.tableZoomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableZoomPanel.Name = "tableZoomPanel";
            this.tableZoomPanel.RowCount = 1;
            this.tableZoomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableZoomPanel.Size = new System.Drawing.Size(1438, 27);
            this.tableZoomPanel.TabIndex = 8;
            // 
            // buttonMinus
            // 
            this.buttonMinus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonMinus.Location = new System.Drawing.Point(0, 0);
            this.buttonMinus.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMinus.Name = "buttonMinus";
            this.buttonMinus.Size = new System.Drawing.Size(25, 27);
            this.buttonMinus.TabIndex = 0;
            this.buttonMinus.Text = "-";
            this.buttonMinus.UseVisualStyleBackColor = true;
            this.buttonMinus.Click += new System.EventHandler(this.buttonMinus_Click);
            // 
            // buttonPlus
            // 
            this.buttonPlus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonPlus.Location = new System.Drawing.Point(100, 0);
            this.buttonPlus.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPlus.Name = "buttonPlus";
            this.buttonPlus.Size = new System.Drawing.Size(25, 27);
            this.buttonPlus.TabIndex = 2;
            this.buttonPlus.Text = "+";
            this.buttonPlus.UseVisualStyleBackColor = true;
            this.buttonPlus.Click += new System.EventHandler(this.buttonPlus_Click);
            // 
            // panelScale
            // 
            this.panelScale.BackColor = System.Drawing.Color.Transparent;
            this.panelScale.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelScale.Location = new System.Drawing.Point(1318, 0);
            this.panelScale.Margin = new System.Windows.Forms.Padding(0);
            this.panelScale.Name = "panelScale";
            this.panelScale.Size = new System.Drawing.Size(120, 27);
            this.panelScale.TabIndex = 4;
            // 
            // buttonWFit
            // 
            this.buttonWFit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonWFit.Font = new System.Drawing.Font("Symbol", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonWFit.Location = new System.Drawing.Point(130, 0);
            this.buttonWFit.Margin = new System.Windows.Forms.Padding(0);
            this.buttonWFit.Name = "buttonWFit";
            this.buttonWFit.Size = new System.Drawing.Size(25, 27);
            this.buttonWFit.TabIndex = 5;
            this.buttonWFit.Text = "«";
            this.buttonWFit.UseVisualStyleBackColor = true;
            this.buttonWFit.Click += new System.EventHandler(this.buttonWFit_Click);
            // 
            // buttonHFit
            // 
            this.buttonHFit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonHFit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.buttonHFit.Location = new System.Drawing.Point(155, 0);
            this.buttonHFit.Margin = new System.Windows.Forms.Padding(0);
            this.buttonHFit.Name = "buttonHFit";
            this.buttonHFit.Size = new System.Drawing.Size(25, 27);
            this.buttonHFit.TabIndex = 6;
            this.buttonHFit.Text = "↨";
            this.buttonHFit.UseVisualStyleBackColor = true;
            this.buttonHFit.Click += new System.EventHandler(this.buttonHFit_Click);
            // 
            // zoomTextBox
            // 
            this.zoomTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomTextBox.Location = new System.Drawing.Point(27, 2);
            this.zoomTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.zoomTextBox.Name = "zoomTextBox";
            this.zoomTextBox.Size = new System.Drawing.Size(71, 20);
            this.zoomTextBox.TabIndex = 7;
            this.zoomTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Controls.Add(this.labelStrato, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnTool, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1438, 20);
            this.tableLayoutPanel8.TabIndex = 12;
            // 
            // labelStrato
            // 
            this.labelStrato.AutoSize = true;
            this.labelStrato.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelStrato.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStrato.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStrato.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelStrato.Location = new System.Drawing.Point(3, 0);
            this.labelStrato.Name = "labelStrato";
            this.labelStrato.Size = new System.Drawing.Size(1412, 20);
            this.labelStrato.TabIndex = 12;
            this.labelStrato.Text = "Strato";
            this.labelStrato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTool
            // 
            this.btnTool.Location = new System.Drawing.Point(1418, 0);
            this.btnTool.Margin = new System.Windows.Forms.Padding(0);
            this.btnTool.Name = "btnTool";
            this.btnTool.Size = new System.Drawing.Size(20, 20);
            this.btnTool.TabIndex = 13;
            this.btnTool.Text = "T";
            this.btnTool.UseVisualStyleBackColor = true;
            this.btnTool.Click += new System.EventHandler(this.btnTool_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel10);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel11);
            this.splitContainer1.Size = new System.Drawing.Size(1182, 422);
            this.splitContainer1.SplitterDistance = 603;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel9, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.labelConnettoreImpostazioni, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(603, 422);
            this.tableLayoutPanel10.TabIndex = 9;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(597, 396);
            this.tableLayoutPanel9.TabIndex = 2;
            // 
            // labelConnettoreImpostazioni
            // 
            this.labelConnettoreImpostazioni.AutoSize = true;
            this.labelConnettoreImpostazioni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelConnettoreImpostazioni.Location = new System.Drawing.Point(3, 0);
            this.labelConnettoreImpostazioni.Name = "labelConnettoreImpostazioni";
            this.labelConnettoreImpostazioni.Size = new System.Drawing.Size(83, 16);
            this.labelConnettoreImpostazioni.TabIndex = 1;
            this.labelConnettoreImpostazioni.Text = "Impostazioni";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.Controls.Add(this.txtPreview, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.labelConnettoreAnteprimaDati, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(575, 422);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // txtPreview
            // 
            this.txtPreview.BackColor = System.Drawing.Color.White;
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPreview.Location = new System.Drawing.Point(3, 23);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreview.Size = new System.Drawing.Size(569, 396);
            this.txtPreview.TabIndex = 7;
            // 
            // labelConnettoreAnteprimaDati
            // 
            this.labelConnettoreAnteprimaDati.AutoSize = true;
            this.labelConnettoreAnteprimaDati.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelConnettoreAnteprimaDati.Location = new System.Drawing.Point(3, 0);
            this.labelConnettoreAnteprimaDati.Name = "labelConnettoreAnteprimaDati";
            this.labelConnettoreAnteprimaDati.Size = new System.Drawing.Size(94, 16);
            this.labelConnettoreAnteprimaDati.TabIndex = 2;
            this.labelConnettoreAnteprimaDati.Text = "Anteprima dati";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageDati);
            this.tabControl1.Controls.Add(this.tabPageLayout);
            this.tabControl1.Controls.Add(this.tabPageGenerazione);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1767, 842);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageDati
            // 
            this.tabPageDati.Controls.Add(this.tableLayoutPanel1);
            this.tabPageDati.Location = new System.Drawing.Point(4, 22);
            this.tabPageDati.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageDati.Name = "tabPageDati";
            this.tabPageDati.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDati.Size = new System.Drawing.Size(1759, 816);
            this.tabPageDati.TabIndex = 0;
            this.tabPageDati.Text = "Dati";
            this.tabPageDati.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 456F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 430F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 522F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel10, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1753, 810);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 3);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.paneContainerPreview);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(459, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1291, 804);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.checkBox3);
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.progressBar1);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(1274, -2);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(0, 798);
            this.panel4.TabIndex = 0;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(9, 221);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(223, 17);
            this.checkBox3.TabIndex = 8;
            this.checkBox3.Text = "filtra soluzioni sotto al 85% di occupazione";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 198);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "animazioni";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(9, 130);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(0, 13);
            this.label25.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 13);
            this.label12.TabIndex = 5;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 72);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(244, 23);
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label10.Location = new System.Drawing.Point(3, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Tempo impiegato";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label9.Location = new System.Drawing.Point(5, 149);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Inizio: ";
            this.label9.Visible = false;
            // 
            // paneContainerPreview
            // 
            this.paneContainerPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paneContainerPreview.BackColor = System.Drawing.Color.Transparent;
            this.paneContainerPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paneContainerPreview.Controls.Add(this.panePreview);
            this.paneContainerPreview.Location = new System.Drawing.Point(0, 3);
            this.paneContainerPreview.Margin = new System.Windows.Forms.Padding(0);
            this.paneContainerPreview.Name = "paneContainerPreview";
            this.paneContainerPreview.Size = new System.Drawing.Size(1291, 814);
            this.paneContainerPreview.TabIndex = 4;
            // 
            // panePreview
            // 
            this.panePreview.Location = new System.Drawing.Point(-400, 1);
            this.panePreview.Name = "panePreview";
            this.panePreview.Size = new System.Drawing.Size(75, 88);
            this.panePreview.TabIndex = 4;
            this.panePreview.Paint += new System.Windows.Forms.PaintEventHandler(this.panePreview_Paint);
            // 
            // panel10
            // 
            this.panel10.AutoScroll = true;
            this.panel10.Controls.Add(this.panel12);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(3, 3);
            this.panel10.Name = "panel10";
            this.tableLayoutPanel1.SetRowSpan(this.panel10, 2);
            this.panel10.Size = new System.Drawing.Size(450, 804);
            this.panel10.TabIndex = 3;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.tableLayoutPanel2);
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Margin = new System.Windows.Forms.Padding(0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(450, 626);
            this.panel12.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel13, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 157F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 242F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 227F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(450, 626);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.checkboxInterfaldaPresenzaSuPallet);
            this.panel2.Controls.Add(this.txtPanelThickness);
            this.panel2.Controls.Add(this.labelPannelloSpessore);
            this.panel2.Controls.Add(this.checkboxPannelloPresenzaSuPallet);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.txtInterlayerThickness);
            this.panel2.Controls.Add(this.button20);
            this.panel2.Controls.Add(this.labelInterfaldaSpessore);
            this.panel2.Controls.Add(this.txtPalletMaxHeight);
            this.panel2.Controls.Add(this.button21);
            this.panel2.Controls.Add(this.labelPalletAltezzaMassima);
            this.panel2.Controls.Add(this.button22);
            this.panel2.Controls.Add(this.txtExtraBorderY);
            this.panel2.Controls.Add(this.button23);
            this.panel2.Controls.Add(this.labelPalletLunghezzaExtra);
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.txtExtraBorderX);
            this.panel2.Controls.Add(this.labelPalletLarghezzaExtra);
            this.panel2.Controls.Add(this.txtPalletHeight);
            this.panel2.Controls.Add(this.txtPalletLenght);
            this.panel2.Controls.Add(this.txtPalletWidth);
            this.panel2.Controls.Add(this.labelPalletAltezza);
            this.panel2.Controls.Add(this.labelPalletLunghezza);
            this.panel2.Controls.Add(this.labelPalletLarghezza);
            this.panel2.Controls.Add(this.labelPallet);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 160);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(444, 236);
            this.panel2.TabIndex = 1;
            // 
            // checkboxInterfaldaPresenzaSuPallet
            // 
            this.checkboxInterfaldaPresenzaSuPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.checkboxInterfaldaPresenzaSuPallet.Location = new System.Drawing.Point(227, 199);
            this.checkboxInterfaldaPresenzaSuPallet.Name = "checkboxInterfaldaPresenzaSuPallet";
            this.checkboxInterfaldaPresenzaSuPallet.Size = new System.Drawing.Size(243, 31);
            this.checkboxInterfaldaPresenzaSuPallet.TabIndex = 35;
            this.checkboxInterfaldaPresenzaSuPallet.Text = "presenza interfalda su pallet";
            this.checkboxInterfaldaPresenzaSuPallet.UseVisualStyleBackColor = true;
            this.checkboxInterfaldaPresenzaSuPallet.CheckedChanged += new System.EventHandler(this.textBox_Changed);
            // 
            // txtPanelThickness
            // 
            this.txtPanelThickness.AcceptsReturn = true;
            this.txtPanelThickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPanelThickness.Location = new System.Drawing.Point(149, 177);
            this.txtPanelThickness.Name = "txtPanelThickness";
            this.txtPanelThickness.Size = new System.Drawing.Size(33, 22);
            this.txtPanelThickness.TabIndex = 34;
            this.txtPanelThickness.Text = "0";
            this.txtPanelThickness.TextChanged += new System.EventHandler(this.textBox_Changed);
            // 
            // labelPannelloSpessore
            // 
            this.labelPannelloSpessore.AutoSize = true;
            this.labelPannelloSpessore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.labelPannelloSpessore.Location = new System.Drawing.Point(3, 178);
            this.labelPannelloSpessore.Name = "labelPannelloSpessore";
            this.labelPannelloSpessore.Size = new System.Drawing.Size(129, 18);
            this.labelPannelloSpessore.TabIndex = 33;
            this.labelPannelloSpessore.Text = "spessore pannello";
            // 
            // checkboxPannelloPresenzaSuPallet
            // 
            this.checkboxPannelloPresenzaSuPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.checkboxPannelloPresenzaSuPallet.Location = new System.Drawing.Point(227, 173);
            this.checkboxPannelloPresenzaSuPallet.Name = "checkboxPannelloPresenzaSuPallet";
            this.checkboxPannelloPresenzaSuPallet.Size = new System.Drawing.Size(216, 31);
            this.checkboxPannelloPresenzaSuPallet.TabIndex = 32;
            this.checkboxPannelloPresenzaSuPallet.Text = "presenza pannello su pallet";
            this.checkboxPannelloPresenzaSuPallet.UseVisualStyleBackColor = true;
            this.checkboxPannelloPresenzaSuPallet.CheckedChanged += new System.EventHandler(this.textBox_Changed);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label27.Location = new System.Drawing.Point(491, 164);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(200, 23);
            this.label27.TabIndex = 31;
            this.label27.Text = "Etichetta visibile nel lato";
            this.label27.Visible = false;
            // 
            // txtInterlayerThickness
            // 
            this.txtInterlayerThickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtInterlayerThickness.Location = new System.Drawing.Point(149, 204);
            this.txtInterlayerThickness.Name = "txtInterlayerThickness";
            this.txtInterlayerThickness.Size = new System.Drawing.Size(33, 22);
            this.txtInterlayerThickness.TabIndex = 27;
            this.txtInterlayerThickness.Text = "0";
            this.txtInterlayerThickness.TextChanged += new System.EventHandler(this.textBox_Changed);
            // 
            // button20
            // 
            this.button20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button20.Location = new System.Drawing.Point(536, 248);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(32, 30);
            this.button20.TabIndex = 30;
            this.button20.Tag = "true";
            this.button20.Text = "S";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Visible = false;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // labelInterfaldaSpessore
            // 
            this.labelInterfaldaSpessore.AutoSize = true;
            this.labelInterfaldaSpessore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.labelInterfaldaSpessore.Location = new System.Drawing.Point(3, 204);
            this.labelInterfaldaSpessore.Name = "labelInterfaldaSpessore";
            this.labelInterfaldaSpessore.Size = new System.Drawing.Size(133, 18);
            this.labelInterfaldaSpessore.TabIndex = 26;
            this.labelInterfaldaSpessore.Text = "spessore interfalda";
            // 
            // txtPalletMaxHeight
            // 
            this.txtPalletMaxHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPalletMaxHeight.Location = new System.Drawing.Point(229, 119);
            this.txtPalletMaxHeight.Name = "txtPalletMaxHeight";
            this.txtPalletMaxHeight.Size = new System.Drawing.Size(80, 22);
            this.txtPalletMaxHeight.TabIndex = 12;
            this.txtPalletMaxHeight.Text = "1500";
            this.txtPalletMaxHeight.TextChanged += new System.EventHandler(this.textBox11_TextChanged);
            // 
            // button21
            // 
            this.button21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button21.Location = new System.Drawing.Point(567, 217);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(32, 30);
            this.button21.TabIndex = 29;
            this.button21.Tag = "true";
            this.button21.Text = "E";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Visible = false;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // labelPalletAltezzaMassima
            // 
            this.labelPalletAltezzaMassima.AutoSize = true;
            this.labelPalletAltezzaMassima.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPalletAltezzaMassima.Location = new System.Drawing.Point(3, 119);
            this.labelPalletAltezzaMassima.Name = "labelPalletAltezzaMassima";
            this.labelPalletAltezzaMassima.Size = new System.Drawing.Size(221, 18);
            this.labelPalletAltezzaMassima.TabIndex = 11;
            this.labelPalletAltezzaMassima.Text = "altezza massima di riempimento";
            // 
            // button22
            // 
            this.button22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button22.Location = new System.Drawing.Point(505, 217);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(32, 30);
            this.button22.TabIndex = 28;
            this.button22.Tag = "true";
            this.button22.Text = "W";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Visible = false;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // txtExtraBorderY
            // 
            this.txtExtraBorderY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtExtraBorderY.Location = new System.Drawing.Point(229, 146);
            this.txtExtraBorderY.Name = "txtExtraBorderY";
            this.txtExtraBorderY.Size = new System.Drawing.Size(79, 22);
            this.txtExtraBorderY.TabIndex = 10;
            this.txtExtraBorderY.Text = "0";
            this.txtExtraBorderY.TextChanged += new System.EventHandler(this.textBox8_TextChanged);
            // 
            // button23
            // 
            this.button23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button23.Location = new System.Drawing.Point(536, 186);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(32, 30);
            this.button23.TabIndex = 27;
            this.button23.Tag = "true";
            this.button23.Text = "N";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Visible = false;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // labelPalletLunghezzaExtra
            // 
            this.labelPalletLunghezzaExtra.AutoSize = true;
            this.labelPalletLunghezzaExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPalletLunghezzaExtra.Location = new System.Drawing.Point(160, 148);
            this.labelPalletLunghezzaExtra.Name = "labelPalletLunghezzaExtra";
            this.labelPalletLunghezzaExtra.Size = new System.Drawing.Size(67, 18);
            this.labelPalletLunghezzaExtra.TabIndex = 9;
            this.labelPalletLunghezzaExtra.Text = "extra lun.";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(493, 288);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(162, 17);
            this.checkBox2.TabIndex = 26;
            this.checkBox2.Text = "Tutti i payload (non solo uno)";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // txtExtraBorderX
            // 
            this.txtExtraBorderX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtExtraBorderX.Location = new System.Drawing.Point(68, 146);
            this.txtExtraBorderX.Name = "txtExtraBorderX";
            this.txtExtraBorderX.Size = new System.Drawing.Size(79, 22);
            this.txtExtraBorderX.TabIndex = 8;
            this.txtExtraBorderX.Text = "0";
            this.txtExtraBorderX.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            // 
            // labelPalletLarghezzaExtra
            // 
            this.labelPalletLarghezzaExtra.AutoSize = true;
            this.labelPalletLarghezzaExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPalletLarghezzaExtra.Location = new System.Drawing.Point(3, 148);
            this.labelPalletLarghezzaExtra.Name = "labelPalletLarghezzaExtra";
            this.labelPalletLarghezzaExtra.Size = new System.Drawing.Size(64, 18);
            this.labelPalletLarghezzaExtra.TabIndex = 7;
            this.labelPalletLarghezzaExtra.Text = "extra lar.";
            // 
            // txtPalletHeight
            // 
            this.txtPalletHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPalletHeight.Location = new System.Drawing.Point(229, 92);
            this.txtPalletHeight.Name = "txtPalletHeight";
            this.txtPalletHeight.Size = new System.Drawing.Size(79, 22);
            this.txtPalletHeight.TabIndex = 6;
            this.txtPalletHeight.Text = "150";
            this.txtPalletHeight.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // txtPalletLenght
            // 
            this.txtPalletLenght.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPalletLenght.Location = new System.Drawing.Point(229, 64);
            this.txtPalletLenght.Name = "txtPalletLenght";
            this.txtPalletLenght.Size = new System.Drawing.Size(79, 22);
            this.txtPalletLenght.TabIndex = 5;
            this.txtPalletLenght.Text = "800";
            this.txtPalletLenght.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // txtPalletWidth
            // 
            this.txtPalletWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPalletWidth.Location = new System.Drawing.Point(229, 35);
            this.txtPalletWidth.Name = "txtPalletWidth";
            this.txtPalletWidth.Size = new System.Drawing.Size(79, 22);
            this.txtPalletWidth.TabIndex = 4;
            this.txtPalletWidth.Text = "1200";
            this.txtPalletWidth.TextChanged += new System.EventHandler(this.txtPalletWidth_TextChanged);
            // 
            // labelPalletAltezza
            // 
            this.labelPalletAltezza.AutoSize = true;
            this.labelPalletAltezza.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPalletAltezza.Location = new System.Drawing.Point(3, 94);
            this.labelPalletAltezza.Name = "labelPalletAltezza";
            this.labelPalletAltezza.Size = new System.Drawing.Size(55, 18);
            this.labelPalletAltezza.TabIndex = 3;
            this.labelPalletAltezza.Text = "altezza";
            // 
            // labelPalletLunghezza
            // 
            this.labelPalletLunghezza.AutoSize = true;
            this.labelPalletLunghezza.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPalletLunghezza.Location = new System.Drawing.Point(3, 68);
            this.labelPalletLunghezza.Name = "labelPalletLunghezza";
            this.labelPalletLunghezza.Size = new System.Drawing.Size(75, 18);
            this.labelPalletLunghezza.TabIndex = 2;
            this.labelPalletLunghezza.Text = "lunghezza";
            // 
            // labelPalletLarghezza
            // 
            this.labelPalletLarghezza.AutoSize = true;
            this.labelPalletLarghezza.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPalletLarghezza.Location = new System.Drawing.Point(3, 39);
            this.labelPalletLarghezza.Name = "labelPalletLarghezza";
            this.labelPalletLarghezza.Size = new System.Drawing.Size(72, 18);
            this.labelPalletLarghezza.TabIndex = 1;
            this.labelPalletLarghezza.Text = "larghezza";
            // 
            // labelPallet
            // 
            this.labelPallet.AutoSize = true;
            this.labelPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelPallet.Location = new System.Drawing.Point(3, 6);
            this.labelPallet.Name = "labelPallet";
            this.labelPallet.Size = new System.Drawing.Size(50, 18);
            this.labelPallet.TabIndex = 0;
            this.labelPallet.Text = "Pallet";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.cmbPayloadsNumber);
            this.panel1.Controls.Add(this.txtPayload1Name);
            this.panel1.Controls.Add(this.labelPayload1Nome);
            this.panel1.Controls.Add(this.checkBox4);
            this.panel1.Controls.Add(this.button16);
            this.panel1.Controls.Add(this.button17);
            this.panel1.Controls.Add(this.button18);
            this.panel1.Controls.Add(this.button19);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.textBox9);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.txtBoxZ);
            this.panel1.Controls.Add(this.txtBoxY);
            this.panel1.Controls.Add(this.txtBoxX);
            this.panel1.Controls.Add(this.labelPayload1Altezza);
            this.panel1.Controls.Add(this.labelPayload1Lunghezza);
            this.panel1.Controls.Add(this.labelPayload1Larghezza);
            this.panel1.Controls.Add(this.labelPayloads);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 151);
            this.panel1.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.labelPayload2Nome);
            this.panel9.Controls.Add(this.labelPayload2Larghezza);
            this.panel9.Controls.Add(this.txtPayload2Name);
            this.panel9.Controls.Add(this.labelPayload2Lunghezza);
            this.panel9.Controls.Add(this.labelPayload2Altezza);
            this.panel9.Controls.Add(this.txtBox2Z);
            this.panel9.Controls.Add(this.txtBox2X);
            this.panel9.Controls.Add(this.txtBox2Y);
            this.panel9.Location = new System.Drawing.Point(222, 26);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(189, 128);
            this.panel9.TabIndex = 40;
            // 
            // labelPayload2Nome
            // 
            this.labelPayload2Nome.AutoSize = true;
            this.labelPayload2Nome.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPayload2Nome.Location = new System.Drawing.Point(4, 8);
            this.labelPayload2Nome.Name = "labelPayload2Nome";
            this.labelPayload2Nome.Size = new System.Drawing.Size(46, 18);
            this.labelPayload2Nome.TabIndex = 37;
            this.labelPayload2Nome.Text = "nome";
            // 
            // labelPayload2Larghezza
            // 
            this.labelPayload2Larghezza.AutoSize = true;
            this.labelPayload2Larghezza.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPayload2Larghezza.Location = new System.Drawing.Point(4, 36);
            this.labelPayload2Larghezza.Name = "labelPayload2Larghezza";
            this.labelPayload2Larghezza.Size = new System.Drawing.Size(72, 18);
            this.labelPayload2Larghezza.TabIndex = 31;
            this.labelPayload2Larghezza.Text = "larghezza";
            // 
            // txtPayload2Name
            // 
            this.txtPayload2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPayload2Name.Location = new System.Drawing.Point(99, 4);
            this.txtPayload2Name.Name = "txtPayload2Name";
            this.txtPayload2Name.Size = new System.Drawing.Size(79, 22);
            this.txtPayload2Name.TabIndex = 38;
            this.txtPayload2Name.Text = "scatola";
            this.txtPayload2Name.TextChanged += new System.EventHandler(this.textBoxesPayload2_Changed);
            // 
            // labelPayload2Lunghezza
            // 
            this.labelPayload2Lunghezza.AutoSize = true;
            this.labelPayload2Lunghezza.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPayload2Lunghezza.Location = new System.Drawing.Point(4, 65);
            this.labelPayload2Lunghezza.Name = "labelPayload2Lunghezza";
            this.labelPayload2Lunghezza.Size = new System.Drawing.Size(75, 18);
            this.labelPayload2Lunghezza.TabIndex = 32;
            this.labelPayload2Lunghezza.Text = "lunghezza";
            // 
            // labelPayload2Altezza
            // 
            this.labelPayload2Altezza.AutoSize = true;
            this.labelPayload2Altezza.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPayload2Altezza.Location = new System.Drawing.Point(4, 92);
            this.labelPayload2Altezza.Name = "labelPayload2Altezza";
            this.labelPayload2Altezza.Size = new System.Drawing.Size(55, 18);
            this.labelPayload2Altezza.TabIndex = 33;
            this.labelPayload2Altezza.Text = "altezza";
            // 
            // txtBox2Z
            // 
            this.txtBox2Z.BackColor = System.Drawing.SystemColors.Control;
            this.txtBox2Z.Enabled = false;
            this.txtBox2Z.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtBox2Z.Location = new System.Drawing.Point(99, 90);
            this.txtBox2Z.Name = "txtBox2Z";
            this.txtBox2Z.Size = new System.Drawing.Size(79, 22);
            this.txtBox2Z.TabIndex = 36;
            this.txtBox2Z.Text = "100";
            this.txtBox2Z.TextChanged += new System.EventHandler(this.textBoxesPayload2_Changed);
            // 
            // txtBox2X
            // 
            this.txtBox2X.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtBox2X.Location = new System.Drawing.Point(99, 32);
            this.txtBox2X.Name = "txtBox2X";
            this.txtBox2X.Size = new System.Drawing.Size(79, 22);
            this.txtBox2X.TabIndex = 34;
            this.txtBox2X.Text = "600";
            this.txtBox2X.TextChanged += new System.EventHandler(this.textBoxesPayload2_Changed);
            // 
            // txtBox2Y
            // 
            this.txtBox2Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtBox2Y.Location = new System.Drawing.Point(99, 61);
            this.txtBox2Y.Name = "txtBox2Y";
            this.txtBox2Y.Size = new System.Drawing.Size(79, 22);
            this.txtBox2Y.TabIndex = 35;
            this.txtBox2Y.Text = "400";
            this.txtBox2Y.TextChanged += new System.EventHandler(this.textBoxesPayload2_Changed);
            // 
            // cmbPayloadsNumber
            // 
            this.cmbPayloadsNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayloadsNumber.FormattingEnabled = true;
            this.cmbPayloadsNumber.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmbPayloadsNumber.Location = new System.Drawing.Point(98, 4);
            this.cmbPayloadsNumber.Name = "cmbPayloadsNumber";
            this.cmbPayloadsNumber.Size = new System.Drawing.Size(43, 21);
            this.cmbPayloadsNumber.TabIndex = 39;
            this.cmbPayloadsNumber.SelectedIndexChanged += new System.EventHandler(this.cmbPayloadsNumber_SelectedIndexChanged);
            this.cmbPayloadsNumber.TextChanged += new System.EventHandler(this.textBoxesPayload2_Changed);
            // 
            // txtPayload1Name
            // 
            this.txtPayload1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPayload1Name.Location = new System.Drawing.Point(98, 31);
            this.txtPayload1Name.Name = "txtPayload1Name";
            this.txtPayload1Name.Size = new System.Drawing.Size(79, 22);
            this.txtPayload1Name.TabIndex = 30;
            this.txtPayload1Name.Text = "scatola";
            this.txtPayload1Name.TextChanged += new System.EventHandler(this.textBoxesPayload2_Changed);
            // 
            // labelPayload1Nome
            // 
            this.labelPayload1Nome.AutoSize = true;
            this.labelPayload1Nome.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPayload1Nome.Location = new System.Drawing.Point(5, 35);
            this.labelPayload1Nome.Name = "labelPayload1Nome";
            this.labelPayload1Nome.Size = new System.Drawing.Size(46, 18);
            this.labelPayload1Nome.TabIndex = 29;
            this.labelPayload1Nome.Text = "nome";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.checkBox4.Location = new System.Drawing.Point(422, 10);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(124, 22);
            this.checkBox4.TabIndex = 28;
            this.checkBox4.Text = "con etichetta";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.Visible = false;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // button16
            // 
            this.button16.Enabled = false;
            this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button16.Location = new System.Drawing.Point(464, 118);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(32, 30);
            this.button16.TabIndex = 24;
            this.button16.Tag = "true";
            this.button16.Text = "S";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Visible = false;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.button17.Enabled = false;
            this.button17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button17.Location = new System.Drawing.Point(461, 87);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(32, 30);
            this.button17.TabIndex = 23;
            this.button17.Tag = "true";
            this.button17.Text = "E";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Visible = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button18
            // 
            this.button18.Enabled = false;
            this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button18.Location = new System.Drawing.Point(433, 87);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(32, 30);
            this.button18.TabIndex = 22;
            this.button18.Tag = "true";
            this.button18.Text = "W";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Visible = false;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button19
            // 
            this.button19.Enabled = false;
            this.button19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button19.Location = new System.Drawing.Point(464, 56);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(32, 30);
            this.button19.TabIndex = 21;
            this.button19.Tag = "true";
            this.button19.Text = "N";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Visible = false;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // textBox10
            // 
            this.textBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.textBox10.Location = new System.Drawing.Point(98, 146);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(79, 22);
            this.textBox10.TabIndex = 20;
            this.textBox10.Text = "0";
            this.textBox10.Visible = false;
            this.textBox10.TextChanged += new System.EventHandler(this.textBox10_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label21.Location = new System.Drawing.Point(4, 148);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(95, 18);
            this.label21.TabIndex = 19;
            this.label21.Text = "tolleranza alt.";
            this.label21.Visible = false;
            // 
            // textBox9
            // 
            this.textBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.textBox9.Location = new System.Drawing.Point(98, 174);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(79, 22);
            this.textBox9.TabIndex = 18;
            this.textBox9.Text = "0";
            this.textBox9.Visible = false;
            this.textBox9.TextChanged += new System.EventHandler(this.textBox9_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label20.Location = new System.Drawing.Point(4, 176);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(76, 18);
            this.label20.TabIndex = 17;
            this.label20.Text = "spessore*";
            this.label20.Visible = false;
            // 
            // txtBoxZ
            // 
            this.txtBoxZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtBoxZ.Location = new System.Drawing.Point(98, 117);
            this.txtBoxZ.Name = "txtBoxZ";
            this.txtBoxZ.Size = new System.Drawing.Size(79, 22);
            this.txtBoxZ.TabIndex = 6;
            this.txtBoxZ.Text = "100";
            this.txtBoxZ.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtBoxY
            // 
            this.txtBoxY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtBoxY.Location = new System.Drawing.Point(98, 88);
            this.txtBoxY.Name = "txtBoxY";
            this.txtBoxY.Size = new System.Drawing.Size(79, 22);
            this.txtBoxY.TabIndex = 5;
            this.txtBoxY.Text = "400";
            this.txtBoxY.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtBoxX
            // 
            this.txtBoxX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtBoxX.Location = new System.Drawing.Point(98, 59);
            this.txtBoxX.Name = "txtBoxX";
            this.txtBoxX.Size = new System.Drawing.Size(79, 22);
            this.txtBoxX.TabIndex = 4;
            this.txtBoxX.Text = "600";
            this.txtBoxX.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // labelPayload1Altezza
            // 
            this.labelPayload1Altezza.AutoSize = true;
            this.labelPayload1Altezza.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPayload1Altezza.Location = new System.Drawing.Point(5, 119);
            this.labelPayload1Altezza.Name = "labelPayload1Altezza";
            this.labelPayload1Altezza.Size = new System.Drawing.Size(55, 18);
            this.labelPayload1Altezza.TabIndex = 3;
            this.labelPayload1Altezza.Text = "altezza";
            // 
            // labelPayload1Lunghezza
            // 
            this.labelPayload1Lunghezza.AutoSize = true;
            this.labelPayload1Lunghezza.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPayload1Lunghezza.Location = new System.Drawing.Point(5, 92);
            this.labelPayload1Lunghezza.Name = "labelPayload1Lunghezza";
            this.labelPayload1Lunghezza.Size = new System.Drawing.Size(75, 18);
            this.labelPayload1Lunghezza.TabIndex = 2;
            this.labelPayload1Lunghezza.Text = "lunghezza";
            // 
            // labelPayload1Larghezza
            // 
            this.labelPayload1Larghezza.AutoSize = true;
            this.labelPayload1Larghezza.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelPayload1Larghezza.Location = new System.Drawing.Point(5, 63);
            this.labelPayload1Larghezza.Name = "labelPayload1Larghezza";
            this.labelPayload1Larghezza.Size = new System.Drawing.Size(72, 18);
            this.labelPayload1Larghezza.TabIndex = 1;
            this.labelPayload1Larghezza.Text = "larghezza";
            // 
            // labelPayloads
            // 
            this.labelPayloads.AutoSize = true;
            this.labelPayloads.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelPayloads.Location = new System.Drawing.Point(5, 6);
            this.labelPayloads.Name = "labelPayloads";
            this.labelPayloads.Size = new System.Drawing.Size(77, 18);
            this.labelPayloads.TabIndex = 0;
            this.labelPayloads.Text = "Payloads";
            // 
            // label26
            // 
            this.label26.Enabled = false;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label26.Location = new System.Drawing.Point(439, 36);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(97, 23);
            this.label26.TabIndex = 25;
            this.label26.Text = "posta nel lato";
            this.label26.Visible = false;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.Transparent;
            this.panel13.Controls.Add(this.buttonDepositoApplica);
            this.panel13.Controls.Add(this.txtApproachZ);
            this.panel13.Controls.Add(this.txtApproachY);
            this.panel13.Controls.Add(this.txtCustomPlacement);
            this.panel13.Controls.Add(this.labelDepositoTestoCustomPlacementInfo);
            this.panel13.Controls.Add(this.txtCustomPick);
            this.panel13.Controls.Add(this.labelDepositoTestoCustomExtraData);
            this.panel13.Controls.Add(this.buttonDepositoSE);
            this.panel13.Controls.Add(this.buttonDepositoSW);
            this.panel13.Controls.Add(this.buttonDepositoAutomatico);
            this.panel13.Controls.Add(this.buttonDepositoNE);
            this.panel13.Controls.Add(this.buttonDepositoNW);
            this.panel13.Controls.Add(this.txtApproachX);
            this.panel13.Controls.Add(this.labelDepositoAccostamento);
            this.panel13.Controls.Add(this.labelOpzioniDiDeposito);
            this.panel13.Controls.Add(this.button25);
            this.panel13.Controls.Add(this.button27);
            this.panel13.Controls.Add(this.button28);
            this.panel13.Controls.Add(this.button30);
            this.panel13.Controls.Add(this.label11);
            this.panel13.Controls.Add(this.button8);
            this.panel13.Controls.Add(this.button9);
            this.panel13.Controls.Add(this.button10);
            this.panel13.Controls.Add(this.button5);
            this.panel13.Controls.Add(this.button7);
            this.panel13.Controls.Add(this.button4);
            this.panel13.Controls.Add(this.button3);
            this.panel13.Controls.Add(this.button2);
            this.panel13.Controls.Add(this.labelDepositoInizio);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 402);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(444, 221);
            this.panel13.TabIndex = 3;
            // 
            // buttonDepositoApplica
            // 
            this.buttonDepositoApplica.Location = new System.Drawing.Point(229, 167);
            this.buttonDepositoApplica.Margin = new System.Windows.Forms.Padding(15);
            this.buttonDepositoApplica.Name = "buttonDepositoApplica";
            this.buttonDepositoApplica.Size = new System.Drawing.Size(204, 36);
            this.buttonDepositoApplica.TabIndex = 9;
            this.buttonDepositoApplica.Text = "Applica il deposito automatico al bancale";
            this.buttonDepositoApplica.UseVisualStyleBackColor = true;
            this.buttonDepositoApplica.Click += new System.EventHandler(this.button32_Click);
            // 
            // txtApproachZ
            // 
            this.txtApproachZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtApproachZ.Location = new System.Drawing.Point(360, 128);
            this.txtApproachZ.Name = "txtApproachZ";
            this.txtApproachZ.Size = new System.Drawing.Size(46, 22);
            this.txtApproachZ.TabIndex = 68;
            this.txtApproachZ.Text = "0";
            this.txtApproachZ.TextChanged += new System.EventHandler(this.txtApproachWidth_TextChanged);
            // 
            // txtApproachY
            // 
            this.txtApproachY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtApproachY.Location = new System.Drawing.Point(249, 128);
            this.txtApproachY.Name = "txtApproachY";
            this.txtApproachY.Size = new System.Drawing.Size(46, 22);
            this.txtApproachY.TabIndex = 67;
            this.txtApproachY.Text = "0";
            this.txtApproachY.TextChanged += new System.EventHandler(this.txtApproachWidth_TextChanged);
            // 
            // txtCustomPlacement
            // 
            this.txtCustomPlacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtCustomPlacement.Location = new System.Drawing.Point(227, 97);
            this.txtCustomPlacement.Name = "txtCustomPlacement";
            this.txtCustomPlacement.Size = new System.Drawing.Size(145, 22);
            this.txtCustomPlacement.TabIndex = 65;
            this.txtCustomPlacement.TextChanged += new System.EventHandler(this.txtCustomPlacement_TextChanged);
            // 
            // labelDepositoTestoCustomPlacementInfo
            // 
            this.labelDepositoTestoCustomPlacementInfo.AutoSize = true;
            this.labelDepositoTestoCustomPlacementInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelDepositoTestoCustomPlacementInfo.Location = new System.Drawing.Point(224, 76);
            this.labelDepositoTestoCustomPlacementInfo.Name = "labelDepositoTestoCustomPlacementInfo";
            this.labelDepositoTestoCustomPlacementInfo.Size = new System.Drawing.Size(226, 18);
            this.labelDepositoTestoCustomPlacementInfo.TabIndex = 64;
            this.labelDepositoTestoCustomPlacementInfo.Text = "Testo CUSTOM (placement info)";
            // 
            // txtCustomPick
            // 
            this.txtCustomPick.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtCustomPick.Location = new System.Drawing.Point(227, 51);
            this.txtCustomPick.Name = "txtCustomPick";
            this.txtCustomPick.Size = new System.Drawing.Size(145, 22);
            this.txtCustomPick.TabIndex = 63;
            this.txtCustomPick.TextChanged += new System.EventHandler(this.txtCustomPick_TextChanged);
            // 
            // labelDepositoTestoCustomExtraData
            // 
            this.labelDepositoTestoCustomExtraData.AutoSize = true;
            this.labelDepositoTestoCustomExtraData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelDepositoTestoCustomExtraData.Location = new System.Drawing.Point(224, 30);
            this.labelDepositoTestoCustomExtraData.Name = "labelDepositoTestoCustomExtraData";
            this.labelDepositoTestoCustomExtraData.Size = new System.Drawing.Size(194, 18);
            this.labelDepositoTestoCustomExtraData.TabIndex = 62;
            this.labelDepositoTestoCustomExtraData.Text = "Testo CUSTOM (extra data)";
            // 
            // buttonDepositoSE
            // 
            this.buttonDepositoSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold);
            this.buttonDepositoSE.Location = new System.Drawing.Point(42, 81);
            this.buttonDepositoSE.Name = "buttonDepositoSE";
            this.buttonDepositoSE.Size = new System.Drawing.Size(32, 32);
            this.buttonDepositoSE.TabIndex = 57;
            this.buttonDepositoSE.Tag = "315";
            this.buttonDepositoSE.Text = "SE";
            this.buttonDepositoSE.UseVisualStyleBackColor = true;
            this.buttonDepositoSE.Click += new System.EventHandler(this.buttonAngleRanker_Click);
            // 
            // buttonDepositoSW
            // 
            this.buttonDepositoSW.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold);
            this.buttonDepositoSW.Location = new System.Drawing.Point(9, 81);
            this.buttonDepositoSW.Name = "buttonDepositoSW";
            this.buttonDepositoSW.Size = new System.Drawing.Size(32, 32);
            this.buttonDepositoSW.TabIndex = 55;
            this.buttonDepositoSW.Tag = "225";
            this.buttonDepositoSW.Text = "SW";
            this.buttonDepositoSW.UseVisualStyleBackColor = true;
            this.buttonDepositoSW.Click += new System.EventHandler(this.buttonAngleRanker_Click);
            // 
            // buttonDepositoAutomatico
            // 
            this.buttonDepositoAutomatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.buttonDepositoAutomatico.Location = new System.Drawing.Point(6, 167);
            this.buttonDepositoAutomatico.Margin = new System.Windows.Forms.Padding(15);
            this.buttonDepositoAutomatico.Name = "buttonDepositoAutomatico";
            this.buttonDepositoAutomatico.Size = new System.Drawing.Size(204, 36);
            this.buttonDepositoAutomatico.TabIndex = 1;
            this.buttonDepositoAutomatico.Tag = "TO START";
            this.buttonDepositoAutomatico.Text = "Deposito automatico";
            this.buttonDepositoAutomatico.UseVisualStyleBackColor = true;
            this.buttonDepositoAutomatico.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonDepositoNE
            // 
            this.buttonDepositoNE.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold);
            this.buttonDepositoNE.Location = new System.Drawing.Point(42, 49);
            this.buttonDepositoNE.Name = "buttonDepositoNE";
            this.buttonDepositoNE.Size = new System.Drawing.Size(32, 32);
            this.buttonDepositoNE.TabIndex = 52;
            this.buttonDepositoNE.Tag = "45";
            this.buttonDepositoNE.Text = "NE";
            this.buttonDepositoNE.UseVisualStyleBackColor = true;
            this.buttonDepositoNE.Click += new System.EventHandler(this.buttonAngleRanker_Click);
            // 
            // buttonDepositoNW
            // 
            this.buttonDepositoNW.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold);
            this.buttonDepositoNW.Location = new System.Drawing.Point(9, 49);
            this.buttonDepositoNW.Name = "buttonDepositoNW";
            this.buttonDepositoNW.Size = new System.Drawing.Size(32, 32);
            this.buttonDepositoNW.TabIndex = 50;
            this.buttonDepositoNW.Tag = "135";
            this.buttonDepositoNW.Text = "NW";
            this.buttonDepositoNW.UseVisualStyleBackColor = true;
            this.buttonDepositoNW.Click += new System.EventHandler(this.buttonAngleRanker_Click);
            // 
            // txtApproachX
            // 
            this.txtApproachX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtApproachX.Location = new System.Drawing.Point(135, 128);
            this.txtApproachX.Name = "txtApproachX";
            this.txtApproachX.Size = new System.Drawing.Size(46, 22);
            this.txtApproachX.TabIndex = 61;
            this.txtApproachX.Text = "0";
            this.txtApproachX.TextChanged += new System.EventHandler(this.txtApproachWidth_TextChanged);
            // 
            // labelDepositoAccostamento
            // 
            this.labelDepositoAccostamento.AutoSize = true;
            this.labelDepositoAccostamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelDepositoAccostamento.Location = new System.Drawing.Point(9, 131);
            this.labelDepositoAccostamento.Name = "labelDepositoAccostamento";
            this.labelDepositoAccostamento.Size = new System.Drawing.Size(434, 18);
            this.labelDepositoAccostamento.TabIndex = 60;
            this.labelDepositoAccostamento.Text = "Accostamento X:               mm    Y:               mm   Z:               mm";
            // 
            // labelOpzioniDiDeposito
            // 
            this.labelOpzioniDiDeposito.AutoSize = true;
            this.labelOpzioniDiDeposito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.labelOpzioniDiDeposito.Location = new System.Drawing.Point(3, 6);
            this.labelOpzioniDiDeposito.Name = "labelOpzioniDiDeposito";
            this.labelOpzioniDiDeposito.Size = new System.Drawing.Size(154, 18);
            this.labelOpzioniDiDeposito.TabIndex = 59;
            this.labelOpzioniDiDeposito.Text = "Opzioni di deposito";
            this.labelOpzioniDiDeposito.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button25
            // 
            this.button25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button25.Location = new System.Drawing.Point(487, 117);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(32, 30);
            this.button25.TabIndex = 56;
            this.button25.Tag = "false";
            this.button25.Text = "S";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Visible = false;
            this.button25.Click += new System.EventHandler(this.buttonAngleRanker_Click);
            // 
            // button27
            // 
            this.button27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button27.Location = new System.Drawing.Point(518, 86);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(32, 30);
            this.button27.TabIndex = 54;
            this.button27.Tag = "false";
            this.button27.Text = "E";
            this.button27.UseVisualStyleBackColor = true;
            this.button27.Visible = false;
            this.button27.Click += new System.EventHandler(this.buttonAngleRanker_Click);
            // 
            // button28
            // 
            this.button28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button28.Location = new System.Drawing.Point(456, 86);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(32, 30);
            this.button28.TabIndex = 53;
            this.button28.Tag = "false";
            this.button28.Text = "W";
            this.button28.UseVisualStyleBackColor = true;
            this.button28.Visible = false;
            this.button28.Click += new System.EventHandler(this.buttonAngleRanker_Click);
            // 
            // button30
            // 
            this.button30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button30.Location = new System.Drawing.Point(487, 55);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(32, 30);
            this.button30.TabIndex = 51;
            this.button30.Tag = "false";
            this.button30.Text = "N";
            this.button30.UseVisualStyleBackColor = true;
            this.button30.Visible = false;
            this.button30.Click += new System.EventHandler(this.buttonAngleRanker_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label11.Location = new System.Drawing.Point(558, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 18);
            this.label11.TabIndex = 49;
            this.label11.Text = "Accostamento";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label11.Visible = false;
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold);
            this.button8.Location = new System.Drawing.Point(624, 115);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(32, 30);
            this.button8.TabIndex = 48;
            this.button8.Tag = "true";
            this.button8.Text = "SE";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Visible = false;
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button9.Location = new System.Drawing.Point(593, 115);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(32, 30);
            this.button9.TabIndex = 47;
            this.button9.Tag = "true";
            this.button9.Text = "S";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Visible = false;
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold);
            this.button10.Location = new System.Drawing.Point(562, 115);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(32, 30);
            this.button10.TabIndex = 46;
            this.button10.Tag = "true";
            this.button10.Text = "SW";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Visible = false;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button5.Location = new System.Drawing.Point(624, 84);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(32, 30);
            this.button5.TabIndex = 45;
            this.button5.Tag = "true";
            this.button5.Text = "E";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button7.Location = new System.Drawing.Point(562, 84);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(32, 30);
            this.button7.TabIndex = 44;
            this.button7.Tag = "true";
            this.button7.Text = "W";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Visible = false;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold);
            this.button4.Location = new System.Drawing.Point(624, 53);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 30);
            this.button4.TabIndex = 43;
            this.button4.Tag = "true";
            this.button4.Text = "NE";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(593, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 30);
            this.button3.TabIndex = 42;
            this.button3.Tag = "true";
            this.button3.Text = "N";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(562, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 30);
            this.button2.TabIndex = 41;
            this.button2.Tag = "true";
            this.button2.Text = "NW";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // labelDepositoInizio
            // 
            this.labelDepositoInizio.AutoSize = true;
            this.labelDepositoInizio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.labelDepositoInizio.Location = new System.Drawing.Point(6, 30);
            this.labelDepositoInizio.Name = "labelDepositoInizio";
            this.labelDepositoInizio.Size = new System.Drawing.Size(103, 18);
            this.labelDepositoInizio.TabIndex = 58;
            this.labelDepositoInizio.Text = "Inizio deposito";
            this.labelDepositoInizio.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabPageLayout
            // 
            this.tabPageLayout.BackColor = System.Drawing.Color.Transparent;
            this.tabPageLayout.Controls.Add(this.panelExt);
            this.tabPageLayout.Location = new System.Drawing.Point(4, 22);
            this.tabPageLayout.Name = "tabPageLayout";
            this.tabPageLayout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLayout.Size = new System.Drawing.Size(1759, 816);
            this.tabPageLayout.TabIndex = 1;
            this.tabPageLayout.Text = "Layout";
            // 
            // panelExt
            // 
            this.panelExt.Controls.Add(this.splitContainerMain);
            this.panelExt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExt.Location = new System.Drawing.Point(3, 3);
            this.panelExt.Name = "panelExt";
            this.panelExt.Size = new System.Drawing.Size(1753, 810);
            this.panelExt.TabIndex = 1;
            // 
            // tabPageGenerazione
            // 
            this.tabPageGenerazione.Controls.Add(this.pictureBox2);
            this.tabPageGenerazione.Controls.Add(this.pictureBox1);
            this.tabPageGenerazione.Controls.Add(this.tableLayoutPanel6);
            this.tabPageGenerazione.Location = new System.Drawing.Point(4, 22);
            this.tabPageGenerazione.Name = "tabPageGenerazione";
            this.tabPageGenerazione.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGenerazione.Size = new System.Drawing.Size(1194, 684);
            this.tabPageGenerazione.TabIndex = 3;
            this.tabPageGenerazione.Text = "Generazione";
            this.tabPageGenerazione.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(866, 213);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 62);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.picBoxJpg_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(813, 213);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.picBoxPdf_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.panel11, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1188, 678);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.panel7, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel8, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 244F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1182, 244);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(941, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(238, 238);
            this.panel7.TabIndex = 5;
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel7_Paint);
            this.panel7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseClick);
            this.panel7.MouseLeave += new System.EventHandler(this.panel7_MouseLeave);
            this.panel7.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel7_MouseMove);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.txtConfig);
            this.panel8.Controls.Add(this.buttonConnettoreRefreshAnteprima);
            this.panel8.Controls.Add(this.buttonConnettoreInvia);
            this.panel8.Controls.Add(this.labelConnettoreIstruzioni);
            this.panel8.Controls.Add(this.comboBox1);
            this.panel8.Controls.Add(this.labelConnettoreVirtuale);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(932, 238);
            this.panel8.TabIndex = 6;
            // 
            // txtConfig
            // 
            this.txtConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfig.BackColor = System.Drawing.Color.White;
            this.txtConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfig.Location = new System.Drawing.Point(3, 94);
            this.txtConfig.Name = "txtConfig";
            this.txtConfig.Size = new System.Drawing.Size(926, 139);
            this.txtConfig.TabIndex = 8;
            // 
            // buttonConnettoreRefreshAnteprima
            // 
            this.buttonConnettoreRefreshAnteprima.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonConnettoreRefreshAnteprima.Location = new System.Drawing.Point(783, 21);
            this.buttonConnettoreRefreshAnteprima.Name = "buttonConnettoreRefreshAnteprima";
            this.buttonConnettoreRefreshAnteprima.Size = new System.Drawing.Size(70, 64);
            this.buttonConnettoreRefreshAnteprima.TabIndex = 7;
            this.buttonConnettoreRefreshAnteprima.Text = "Refresh anteprima";
            this.buttonConnettoreRefreshAnteprima.UseVisualStyleBackColor = true;
            this.buttonConnettoreRefreshAnteprima.Click += new System.EventHandler(this.button15_Click);
            // 
            // buttonConnettoreInvia
            // 
            this.buttonConnettoreInvia.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonConnettoreInvia.Location = new System.Drawing.Point(859, 21);
            this.buttonConnettoreInvia.Name = "buttonConnettoreInvia";
            this.buttonConnettoreInvia.Size = new System.Drawing.Size(70, 64);
            this.buttonConnettoreInvia.TabIndex = 6;
            this.buttonConnettoreInvia.Text = "Invia";
            this.buttonConnettoreInvia.UseVisualStyleBackColor = true;
            this.buttonConnettoreInvia.Click += new System.EventHandler(this.button14_Click);
            // 
            // labelConnettoreIstruzioni
            // 
            this.labelConnettoreIstruzioni.AutoSize = true;
            this.labelConnettoreIstruzioni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelConnettoreIstruzioni.Location = new System.Drawing.Point(1, 78);
            this.labelConnettoreIstruzioni.Name = "labelConnettoreIstruzioni";
            this.labelConnettoreIstruzioni.Size = new System.Drawing.Size(59, 16);
            this.labelConnettoreIstruzioni.TabIndex = 4;
            this.labelConnettoreIstruzioni.Text = "Istruzioni";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Scrittore di File"});
            this.comboBox1.Location = new System.Drawing.Point(3, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(335, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelConnettoreVirtuale
            // 
            this.labelConnettoreVirtuale.AutoSize = true;
            this.labelConnettoreVirtuale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.labelConnettoreVirtuale.Location = new System.Drawing.Point(2, 21);
            this.labelConnettoreVirtuale.Name = "labelConnettoreVirtuale";
            this.labelConnettoreVirtuale.Size = new System.Drawing.Size(122, 16);
            this.labelConnettoreVirtuale.TabIndex = 0;
            this.labelConnettoreVirtuale.Text = "Connettore virtuale ";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.splitContainer1);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(3, 253);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1182, 422);
            this.panel11.TabIndex = 6;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.equidistribuisciToolStripMenuItem,
            this.collegaISelezionatiToolStripMenuItem,
            this.scollegaISelezionatiToolStripMenuItem,
            this.ricopiaToolStripMenuItem,
            this.impostaProgressivoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(158, 158);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soloInLarghezzaToolStripMenuItem,
            this.soloInLunghezzaToolStripMenuItem,
            this.inEntrambiToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem1.Text = "Centra";
            // 
            // soloInLarghezzaToolStripMenuItem
            // 
            this.soloInLarghezzaToolStripMenuItem.Name = "soloInLarghezzaToolStripMenuItem";
            this.soloInLarghezzaToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.soloInLarghezzaToolStripMenuItem.Text = "in larghezza";
            this.soloInLarghezzaToolStripMenuItem.Click += new System.EventHandler(this.soloInLarghezzaToolStripMenuItem_Click);
            // 
            // soloInLunghezzaToolStripMenuItem
            // 
            this.soloInLunghezzaToolStripMenuItem.Name = "soloInLunghezzaToolStripMenuItem";
            this.soloInLunghezzaToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.soloInLunghezzaToolStripMenuItem.Text = "in lunghezza";
            this.soloInLunghezzaToolStripMenuItem.Click += new System.EventHandler(this.soloInAltezzaToolStripMenuItem_Click);
            // 
            // inEntrambiToolStripMenuItem
            // 
            this.inEntrambiToolStripMenuItem.Name = "inEntrambiToolStripMenuItem";
            this.inEntrambiToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.inEntrambiToolStripMenuItem.Text = "in entrambi";
            this.inEntrambiToolStripMenuItem.Click += new System.EventHandler(this.inEntrambiToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aSinistraToolStripMenuItem,
            this.aDestraToolStripMenuItem,
            this.inBassoToolStripMenuItem,
            this.inAltoToolStripMenuItem});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem3.Text = "Allinea";
            // 
            // aSinistraToolStripMenuItem
            // 
            this.aSinistraToolStripMenuItem.Name = "aSinistraToolStripMenuItem";
            this.aSinistraToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.aSinistraToolStripMenuItem.Text = "a sinistra";
            this.aSinistraToolStripMenuItem.Click += new System.EventHandler(this.aSinistraToolStripMenuItem_Click);
            // 
            // aDestraToolStripMenuItem
            // 
            this.aDestraToolStripMenuItem.Name = "aDestraToolStripMenuItem";
            this.aDestraToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.aDestraToolStripMenuItem.Text = "a destra";
            this.aDestraToolStripMenuItem.Click += new System.EventHandler(this.aDestraToolStripMenuItem_Click);
            // 
            // inBassoToolStripMenuItem
            // 
            this.inBassoToolStripMenuItem.Name = "inBassoToolStripMenuItem";
            this.inBassoToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.inBassoToolStripMenuItem.Text = "in basso";
            this.inBassoToolStripMenuItem.Click += new System.EventHandler(this.inBassoToolStripMenuItem_Click);
            // 
            // inAltoToolStripMenuItem
            // 
            this.inAltoToolStripMenuItem.Name = "inAltoToolStripMenuItem";
            this.inAltoToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.inAltoToolStripMenuItem.Text = "in alto";
            this.inAltoToolStripMenuItem.Click += new System.EventHandler(this.inAltoToolStripMenuItem_Click);
            // 
            // equidistribuisciToolStripMenuItem
            // 
            this.equidistribuisciToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inLarghezzaToolStripMenuItem,
            this.inAltezzaToolStripMenuItem,
            this.inEntrambiToolStripMenuItem1});
            this.equidistribuisciToolStripMenuItem.Name = "equidistribuisciToolStripMenuItem";
            this.equidistribuisciToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.equidistribuisciToolStripMenuItem.Text = "Equidistribuisci";
            // 
            // inLarghezzaToolStripMenuItem
            // 
            this.inLarghezzaToolStripMenuItem.Name = "inLarghezzaToolStripMenuItem";
            this.inLarghezzaToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.inLarghezzaToolStripMenuItem.Text = "in larghezza";
            this.inLarghezzaToolStripMenuItem.Click += new System.EventHandler(this.inLarghezzaToolStripMenuItem_Click);
            // 
            // inAltezzaToolStripMenuItem
            // 
            this.inAltezzaToolStripMenuItem.Name = "inAltezzaToolStripMenuItem";
            this.inAltezzaToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.inAltezzaToolStripMenuItem.Text = "in lunghezza";
            this.inAltezzaToolStripMenuItem.Click += new System.EventHandler(this.inAltezzaToolStripMenuItem_Click);
            // 
            // inEntrambiToolStripMenuItem1
            // 
            this.inEntrambiToolStripMenuItem1.Name = "inEntrambiToolStripMenuItem1";
            this.inEntrambiToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.inEntrambiToolStripMenuItem1.Text = "in entrambi";
            this.inEntrambiToolStripMenuItem1.Click += new System.EventHandler(this.inEntrambiToolStripMenuItem1_Click);
            // 
            // collegaISelezionatiToolStripMenuItem
            // 
            this.collegaISelezionatiToolStripMenuItem.Name = "collegaISelezionatiToolStripMenuItem";
            this.collegaISelezionatiToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.collegaISelezionatiToolStripMenuItem.Text = "Collega i selezionati";
            this.collegaISelezionatiToolStripMenuItem.Visible = false;
            this.collegaISelezionatiToolStripMenuItem.Click += new System.EventHandler(this.collegaISelezionatiToolStripMenuItem_Click);
            // 
            // scollegaISelezionatiToolStripMenuItem
            // 
            this.scollegaISelezionatiToolStripMenuItem.Name = "scollegaISelezionatiToolStripMenuItem";
            this.scollegaISelezionatiToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.scollegaISelezionatiToolStripMenuItem.Text = "Scollega i selezionati";
            this.scollegaISelezionatiToolStripMenuItem.Visible = false;
            this.scollegaISelezionatiToolStripMenuItem.Click += new System.EventHandler(this.scollegaISelezionatiToolStripMenuItem_Click);
            // 
            // ricopiaToolStripMenuItem
            // 
            this.ricopiaToolStripMenuItem.Name = "ricopiaToolStripMenuItem";
            this.ricopiaToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.ricopiaToolStripMenuItem.Text = "Ricopia";
            this.ricopiaToolStripMenuItem.Click += new System.EventHandler(this.ricopiaToolStripMenuItem_Click);
            // 
            // impostaProgressivoToolStripMenuItem
            // 
            this.impostaProgressivoToolStripMenuItem.Name = "impostaProgressivoToolStripMenuItem";
            this.impostaProgressivoToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.impostaProgressivoToolStripMenuItem.Text = "Imposta progressivo";
            this.impostaProgressivoToolStripMenuItem.Click += new System.EventHandler(this.impostaProgressivoToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.ricopiaStratoToolStripMenuItem,
            this.cancellaToolStripMenuItem,
            this.spostaGiùToolStripMenuItem,
            this.spostaSuToolStripMenuItem,
            this.capovolgiOrizzontalmenteToolStripMenuItem,
            this.capovolgiVerticalmenteToolStripMenuItem,
            this.ruotaToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(305, 180);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(304, 22);
            this.toolStripMenuItem2.Text = "Crea tutti gli strati basandoti sui primi due...";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // ricopiaStratoToolStripMenuItem
            // 
            this.ricopiaStratoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stratoSelezionatoToolStripMenuItem,
            this.primiDueStratiToolStripMenuItem,
            this.tuttiGliStratiToolStripMenuItem});
            this.ricopiaStratoToolStripMenuItem.Name = "ricopiaStratoToolStripMenuItem";
            this.ricopiaStratoToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
            this.ricopiaStratoToolStripMenuItem.Text = "Ricopia";
            // 
            // stratoSelezionatoToolStripMenuItem
            // 
            this.stratoSelezionatoToolStripMenuItem.Name = "stratoSelezionatoToolStripMenuItem";
            this.stratoSelezionatoToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.stratoSelezionatoToolStripMenuItem.Text = "strato selezionato";
            this.stratoSelezionatoToolStripMenuItem.Click += new System.EventHandler(this.stratoSelezionatoToolStripMenuItem_Click);
            // 
            // primiDueStratiToolStripMenuItem
            // 
            this.primiDueStratiToolStripMenuItem.Name = "primiDueStratiToolStripMenuItem";
            this.primiDueStratiToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.primiDueStratiToolStripMenuItem.Text = "primi due strati";
            this.primiDueStratiToolStripMenuItem.Click += new System.EventHandler(this.primiDueStratiToolStripMenuItem_Click);
            // 
            // tuttiGliStratiToolStripMenuItem
            // 
            this.tuttiGliStratiToolStripMenuItem.Name = "tuttiGliStratiToolStripMenuItem";
            this.tuttiGliStratiToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.tuttiGliStratiToolStripMenuItem.Text = "tutti gli strati";
            this.tuttiGliStratiToolStripMenuItem.Click += new System.EventHandler(this.tuttiGliStratiToolStripMenuItem_Click);
            // 
            // cancellaToolStripMenuItem
            // 
            this.cancellaToolStripMenuItem.Name = "cancellaToolStripMenuItem";
            this.cancellaToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
            this.cancellaToolStripMenuItem.Text = "Cancella";
            this.cancellaToolStripMenuItem.Click += new System.EventHandler(this.cancellaToolStripMenuItem_Click);
            // 
            // spostaGiùToolStripMenuItem
            // 
            this.spostaGiùToolStripMenuItem.Name = "spostaGiùToolStripMenuItem";
            this.spostaGiùToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
            this.spostaGiùToolStripMenuItem.Text = "Sposta giù";
            this.spostaGiùToolStripMenuItem.Click += new System.EventHandler(this.spostaGiùToolStripMenuItem_Click);
            // 
            // spostaSuToolStripMenuItem
            // 
            this.spostaSuToolStripMenuItem.Name = "spostaSuToolStripMenuItem";
            this.spostaSuToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
            this.spostaSuToolStripMenuItem.Text = "Sposta su";
            this.spostaSuToolStripMenuItem.Click += new System.EventHandler(this.spostaSuToolStripMenuItem_Click);
            // 
            // capovolgiOrizzontalmenteToolStripMenuItem
            // 
            this.capovolgiOrizzontalmenteToolStripMenuItem.Name = "capovolgiOrizzontalmenteToolStripMenuItem";
            this.capovolgiOrizzontalmenteToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
            this.capovolgiOrizzontalmenteToolStripMenuItem.Text = "Capovolgi orizzontalmente";
            this.capovolgiOrizzontalmenteToolStripMenuItem.Visible = false;
            this.capovolgiOrizzontalmenteToolStripMenuItem.Click += new System.EventHandler(this.capovolgiOrizzontalmenteToolStripMenuItem_Click);
            // 
            // capovolgiVerticalmenteToolStripMenuItem
            // 
            this.capovolgiVerticalmenteToolStripMenuItem.Name = "capovolgiVerticalmenteToolStripMenuItem";
            this.capovolgiVerticalmenteToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
            this.capovolgiVerticalmenteToolStripMenuItem.Text = "Capovolgi verticalmente";
            this.capovolgiVerticalmenteToolStripMenuItem.Visible = false;
            this.capovolgiVerticalmenteToolStripMenuItem.Click += new System.EventHandler(this.capovolgiVerticalmenteToolStripMenuItem_Click);
            // 
            // ruotaToolStripMenuItem
            // 
            this.ruotaToolStripMenuItem.Name = "ruotaToolStripMenuItem";
            this.ruotaToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
            this.ruotaToolStripMenuItem.Text = "Ruota...";
            this.ruotaToolStripMenuItem.Click += new System.EventHandler(this.ruotaToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.AddExtension = false;
            this.saveFileDialog1.DefaultExt = "\"jpg\"";
            this.saveFileDialog1.Filter = "Immagine jpg (*.jpg)|*.jpg|Tutti i files (*.*)|*.*";
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selezionaTuttoToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(153, 26);
            // 
            // selezionaTuttoToolStripMenuItem
            // 
            this.selezionaTuttoToolStripMenuItem.Name = "selezionaTuttoToolStripMenuItem";
            this.selezionaTuttoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selezionaTuttoToolStripMenuItem.Text = "Seleziona tutto";
            this.selezionaTuttoToolStripMenuItem.Click += new System.EventHandler(this.selezionaTuttoToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mnuItemLang,
            this.infoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1767, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salvaSchemaDiPallettizzazioneToolStripMenuItem,
            this.apriToolStripMenuItem,
            this.toolStripMenuItem4,
            this.licenzaToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // salvaSchemaDiPallettizzazioneToolStripMenuItem
            // 
            this.salvaSchemaDiPallettizzazioneToolStripMenuItem.Name = "salvaSchemaDiPallettizzazioneToolStripMenuItem";
            this.salvaSchemaDiPallettizzazioneToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.salvaSchemaDiPallettizzazioneToolStripMenuItem.Text = "Salva..";
            this.salvaSchemaDiPallettizzazioneToolStripMenuItem.Click += new System.EventHandler(this.salvaSchemaDiPallettizzazioneToolStripMenuItem_Click);
            // 
            // apriToolStripMenuItem
            // 
            this.apriToolStripMenuItem.Name = "apriToolStripMenuItem";
            this.apriToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.apriToolStripMenuItem.Text = "Apri..";
            this.apriToolStripMenuItem.Click += new System.EventHandler(this.apriToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(116, 6);
            // 
            // licenzaToolStripMenuItem
            // 
            this.licenzaToolStripMenuItem.Name = "licenzaToolStripMenuItem";
            this.licenzaToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.licenzaToolStripMenuItem.Text = "Licenza..";
            this.licenzaToolStripMenuItem.Click += new System.EventHandler(this.licenzaToolStripMenuItem_Click);
            // 
            // mnuItemLang
            // 
            this.mnuItemLang.Name = "mnuItemLang";
            this.mnuItemLang.Size = new System.Drawing.Size(55, 20);
            this.mnuItemLang.Text = "Lingua";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.riguardoRegenToolStripMenuItem});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // riguardoRegenToolStripMenuItem
            // 
            this.riguardoRegenToolStripMenuItem.Name = "riguardoRegenToolStripMenuItem";
            this.riguardoRegenToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.riguardoRegenToolStripMenuItem.Text = "Riguardo ReGen..";
            this.riguardoRegenToolStripMenuItem.Click += new System.EventHandler(this.riguardoRegenToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Schema di pallettizzazione (*.ren)|*.ren";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.DefaultExt = "ren";
            this.saveFileDialog2.Filter = "Schema di pallettizzazione (*.ren)|*.ren";
            this.saveFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog2_FileOk);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 360000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panelLogoMobile
            // 
            this.panelLogoMobile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLogoMobile.BackColor = System.Drawing.Color.White;
            this.panelLogoMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLogoMobile.Controls.Add(this.linkLabel1);
            this.panelLogoMobile.Controls.Add(this.logoPictureBox);
            this.panelLogoMobile.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panelLogoMobile.Location = new System.Drawing.Point(0, 802);
            this.panelLogoMobile.Name = "panelLogoMobile";
            this.panelLogoMobile.Size = new System.Drawing.Size(320, 134);
            this.panelLogoMobile.TabIndex = 1;
            this.panelLogoMobile.MouseDown += new System.Windows.Forms.MouseEventHandler(this.logoPictureBox_MouseDown);
            this.panelLogoMobile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.logoPictureBox_MouseMove);
            this.panelLogoMobile.MouseUp += new System.Windows.Forms.MouseEventHandler(this.logoPictureBox_MouseUp);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F);
            this.linkLabel1.Location = new System.Drawing.Point(5, 93);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(283, 30);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.sintecingegneria.it";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            this.linkLabel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.logoPictureBox_MouseDown);
            this.linkLabel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.logoPictureBox_MouseMove);
            this.linkLabel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.logoPictureBox_MouseUp);
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.logoPictureBox.Location = new System.Drawing.Point(0, 0);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(318, 88);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 15;
            this.logoPictureBox.TabStop = false;
            this.logoPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.logoPictureBox_MouseDown);
            this.logoPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.logoPictureBox_MouseMove);
            this.logoPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.logoPictureBox_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1767, 866);
            this.Controls.Add(this.panelLogoMobile);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReGen by Sintec Ingegneria S.r.l.";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.panelPayloadInsertion.ResumeLayout(false);
            this.panelPayloadInsertion.PerformLayout();
            this.extTableLayout.ResumeLayout(false);
            this.extTableLayout.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.depotPanel.ResumeLayout(false);
            this.depotPanel.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableOutSchemes.ResumeLayout(false);
            this.outPanelWhereDraw.ResumeLayout(false);
            this.centerPanel.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.tableZoomPanel.ResumeLayout(false);
            this.tableZoomPanel.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageDati.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.paneContainerPreview.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.tabPageLayout.ResumeLayout(false);
            this.panelExt.ResumeLayout(false);
            this.tabPageGenerazione.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelLogoMobile.ResumeLayout(false);
            this.panelLogoMobile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDati;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtBoxZ;
        private System.Windows.Forms.TextBox txtBoxY;
        private System.Windows.Forms.TextBox txtBoxX;
        private System.Windows.Forms.Label labelPayload1Altezza;
        private System.Windows.Forms.Label labelPayload1Lunghezza;
        private System.Windows.Forms.Label labelPayload1Larghezza;
        private System.Windows.Forms.Label labelPayloads;
        private System.Windows.Forms.TabPage tabPageLayout;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtPalletHeight;
        private System.Windows.Forms.TextBox txtPalletLenght;
        private System.Windows.Forms.TextBox txtPalletWidth;
        private System.Windows.Forms.Label labelPalletAltezza;
        private System.Windows.Forms.Label labelPalletLunghezza;
        private System.Windows.Forms.Label labelPalletLarghezza;
        private System.Windows.Forms.Label labelPallet;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonDepositoAutomatico;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panelExt;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TableLayoutPanel leftPanel;
        private System.Windows.Forms.Panel depotPanel;
        private System.Windows.Forms.TableLayoutPanel extTableLayout;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label labelSelezione;
        private System.Windows.Forms.Label labelPuntatore;
        private System.Windows.Forms.Label labelSelezioneSx;
        private System.Windows.Forms.Label labelSelezioneBasso;
        private System.Windows.Forms.Label labelPuntatoreX;
        private System.Windows.Forms.Label labelPuntatoreY;
        private System.Windows.Forms.Label lblSelectionXValue;
        private System.Windows.Forms.Label lblSelectionYValue;
        private System.Windows.Forms.Label lblPointerXValue;
        private System.Windows.Forms.Label lblPointerYValue;
        private Panel paneContainerPreview;
        /// <summary>
        /// Pannello centrale nel Layout
        /// </summary>
        private System.Windows.Forms.Panel centerPanel;
        private Panel inPanel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TableLayoutPanel tableZoomPanel;
        private System.Windows.Forms.Button buttonMinus;
        private System.Windows.Forms.Button buttonPlus;
        private System.Windows.Forms.Panel panelScale;
        private System.Windows.Forms.Button buttonWFit;
        private System.Windows.Forms.Button buttonHFit;
        private System.Windows.Forms.TextBox zoomTextBox;
        private System.Windows.Forms.Label labelStratiPallet;
        private TextBox txtExtraBorderX;
        private Label labelPalletLarghezzaExtra;
        private TextBox txtExtraBorderY;
        private Label labelPalletLunghezzaExtra;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem soloInLarghezzaToolStripMenuItem;
        private ToolStripMenuItem soloInLunghezzaToolStripMenuItem;
        private ToolStripMenuItem inEntrambiToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem aSinistraToolStripMenuItem;
        private ToolStripMenuItem aDestraToolStripMenuItem;
        private ToolStripMenuItem inBassoToolStripMenuItem;
        private ToolStripMenuItem inAltoToolStripMenuItem;
        private ToolStripMenuItem equidistribuisciToolStripMenuItem;
        private ToolStripMenuItem inLarghezzaToolStripMenuItem;
        private ToolStripMenuItem inAltezzaToolStripMenuItem;
        private ToolStripMenuItem inEntrambiToolStripMenuItem1;
        private ToolStripMenuItem collegaISelezionatiToolStripMenuItem;
        private ToolStripMenuItem scollegaISelezionatiToolStripMenuItem;
        private TextBox textBox9;
        private Label label20;
        private TextBox textBox10;
        private Label label21;
        private TabPage tabPageGenerazione;
        private Panel bankDepotPanel;
        private Button button13;
        private Button button11;
        private Button button6;
        private Button button12;
        private Panel outPanelWhereDraw;
        public Panel panelWhereDraw;
        private Timer timer1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem ricopiaStratoToolStripMenuItem;
        private ToolStripMenuItem cancellaToolStripMenuItem;
        private ToolStripMenuItem spostaGiùToolStripMenuItem;
        private ToolStripMenuItem spostaSuToolStripMenuItem;
        private ToolStripMenuItem stratoSelezionatoToolStripMenuItem;
        private ToolStripMenuItem primiDueStratiToolStripMenuItem;
        private ToolStripMenuItem tuttiGliStratiToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem capovolgiOrizzontalmenteToolStripMenuItem;
        private ToolStripMenuItem capovolgiVerticalmenteToolStripMenuItem;
        private ToolStripMenuItem ruotaToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private Panel panel7;
        private Panel panel8;
        private Label labelConnettoreIstruzioni;
        private ComboBox comboBox1;
        private Label labelConnettoreVirtuale;
        private SaveFileDialog saveFileDialog1;
        private Button buttonConnettoreInvia;
        private Button buttonConnettoreRefreshAnteprima;
        private Panel panel11;
        private Label txtConfig;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel9;
        private Label labelConnettoreImpostazioni;
        private TableLayoutPanel tableLayoutPanel11;
        private TextBox txtPreview;
        private Label labelConnettoreAnteprimaDati;
        private ToolStripMenuItem ricopiaToolStripMenuItem;
        private TextBox txtPalletMaxHeight;
        private Label labelPalletAltezzaMassima;
        private ContextMenuStrip contextMenuStrip3;
        private ToolStripMenuItem selezionaTuttoToolStripMenuItem;
        private Label label25;
        private CheckBox checkBox1;
        private Label label26;
        private Button button16;
        private Button button17;
        private Button button18;
        private Button button19;
        private CheckBox checkBox2;
        private Label label27;
        private Button button20;
        private Button button21;
        private Button button22;
        private Button button23;
        private CheckBox checkBox3;
        private Panel panelPayloadInsertion;
        private Button buttonPayloadInsertionReset;
        private Button buttonPayloadInsertionPlace;
        private TextBox textBoxPanelInsertionQuadrant;
        private Label labelPanelInsertionQuadrant4;
        private TextBox textBoxPanelInsertionY;
        private Label labelPanelInsertionY;
        private TextBox textBoxPanelInsertionX;
        private Label labelPanelInsertionX;
        private Label labelPayload;
        private TableLayoutPanel tableLayoutPanel8;
        private Label labelStrato;
        private Button btnTool;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableOutSchemes;
        private Button buttonDepositoApplica;
        private Label label29;
        private TextBox txtInterlayerThickness;
        private Label labelInterfaldaSpessore;
        private Button button33;
        private CheckBox checkBox4;
        private Button button35;
        private Label label9;
        private Panel panel13;
        private Label labelOpzioniDiDeposito;
        private Label labelDepositoInizio;
        private Button buttonDepositoSE;
        private Button button25;
        private Button buttonDepositoSW;
        private Button button27;
        private Button button28;
        private Button buttonDepositoNE;
        private Button button30;
        private Button buttonDepositoNW;
        private Label label11;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button5;
        private Button button7;
        private Button button4;
        private Button button3;
        private Button button2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem salvaSchemaDiPallettizzazioneToolStripMenuItem;
        private ToolStripMenuItem apriToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStripMenuItem riguardoRegenToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog2;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem licenzaToolStripMenuItem;
        private Timer timer2;
        private CheckBox checkboxPannelloPresenzaSuPallet;
        private ComboBox cmbPayloadsNumber;
        private TextBox txtPayload2Name;
        private Label labelPayload2Nome;
        private TextBox txtBox2Z;
        private TextBox txtBox2Y;
        private TextBox txtBox2X;
        private Label labelPayload2Altezza;
        private Label labelPayload2Lunghezza;
        private Label labelPayload2Larghezza;
        private TextBox txtPayload1Name;
        private Label labelPayload1Nome;
        private Panel panel9;
        private TextBox txtApproachX;
        private Label labelDepositoAccostamento;
        private TextBox txtCustomPlacement;
        private Label labelDepositoTestoCustomPlacementInfo;
        private TextBox txtCustomPick;
        private Label labelDepositoTestoCustomExtraData;
        private TextBox txtPanelThickness;
        private Label labelPannelloSpessore;
        private CheckBox checkboxInterfaldaPresenzaSuPallet;
        private TextBox txtApproachZ;
        private TextBox txtApproachY;
        private Panel panePreview;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel10;
        private Panel panel12;
        private Panel panelLogoMobile;
        private LinkLabel linkLabel1;
        private PictureBox logoPictureBox;
        private ToolStripMenuItem impostaProgressivoToolStripMenuItem;
        private ToolStripMenuItem mnuItemLang;
    }
}

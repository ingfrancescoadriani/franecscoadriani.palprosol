
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sintec.Tool;
using ReGen.View;
using ReGen.Model;
using System.Drawing.Drawing2D;
using System.IO;
using VirtualConnectors;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using ReGen.Model.AutoPopulate;

using System.Linq;
using System.Text;

using System.Runtime.InteropServices;
using System.Threading;
using WcfClientServer;
using System.Globalization;

namespace ReGen
{
    public partial class MainForm : Form//, CandidateLayerListener
    {
        bool typingProgressive = false;
        String tempProgressive = "";
        Registry reg = new Registry();
        WcfServer server = new WcfServer();
        public bool tokenForResetApproach = false;
        toolForm tf = null;
        List<Type> typeOnCombo;
        /// <summary>
        /// Istanza della form
        /// </summary>
        public static MainForm instance;
        public string fileToOpen = System.Windows.Forms.Application.UserAppDataPath + "\\workData.dat";
        private bool inibiteMouse = false;
        private bool _mousePressed = false;
        private bool leftPressed = false;
        private bool mouseMoving = false;
        private bool ctrlPressing = false;
        public virtual bool mousePressed
        {
            set
            {
                _mousePressed = value;
                if (!value)
                    mouseMoving = false;
            }
            get
            {
                return _mousePressed;
            }
        }
        private bool inibiteChange = false;

        public WorkData work = new WorkData();
        
       
        /// <summary>
        /// Fattore di scalatura dell'inPanel sul centerPanel
        /// </summary>
        public static double zoomLevel = 1;
        /// <summary>
        /// SizeAtOne rappresenta la dimensione ottimale (massimizza la copertura del centerPanel) della visualizzazione della workArea (pallet+extraBorder) a livello zoom 1
        /// </summary>
        public static Point sizeAtOne;
        public static Point2F recOfSelectionOrNullIfPayloadClicked = null;
        private SizeF menuDimension = new SizeF(450, 170);
        private Point2F lastPointClickedForMenu = null;
        List<Payload> listPayload = new List<Payload>();
        Point mouseOnDepot = new Point(0, 0);
        Point mouseOnPlatform = new Point(0, 0);
        PayloadGroup payloadGroupDraggingFromDepot = null;
        private PayloadGroup payloadGroupLastMoved = null;
        SortedListWithDuplicatedKey<Layer> sl = new SortedListWithDuplicatedKey<Layer>();
        public List<PayloadGroup> listPayloadGroupSelected = new List<PayloadGroup>();
        VirtualConnector connectorUsing = null;
        List<Figure> listFigureOnDepot = new List<Figure>();
        List<Figure> listFigureOnPlatform = new List<Figure>();
        /// <summary>
        /// Setta il valore booleano della variabile che rappresenta il click del mouse
        /// </summary>
        /// <param name="value">Valore del click del mouse</param>
        public void setMousePressed(bool value)
        {
            this.mousePressed = value;
            this.leftPressed = value;
        }
        /// <summary>
        /// Inizializza la form
        /// </summary>
        public MainForm()
		{
            InitializeComponent();
            DoubleBufferedPanel.SetDoubleBuffered(inPanel);
            DoubleBufferedPanel.SetDoubleBuffered(bankDepotPanel);

            #region loading language
            ToolStripItem tmpMI;
            if (!File.Exists(Program.languagesPath + "/italiano.lng"))
                File.WriteAllBytes(Program.languagesPath + "/italiano.lng", ReGen.Properties.Resources.Italiano);
            if (!File.Exists(Program.languagesPath + "/english.lng"))
                File.WriteAllBytes(Program.languagesPath + "/english.lng", ReGen.Properties.Resources.English);
            List<String> ll = Program.LanguageList();
            bool someLangSelected = false;
            foreach (String s in ll)
            {
                tmpMI = mnuItemLang.DropDownItems.Add(s);
                ((ToolStripMenuItem)tmpMI).CheckOnClick = true;
                tmpMI.Click += new EventHandler(eventMenuItem_Click);
                if (!String.IsNullOrEmpty((String)reg.GetValue("settings", "Language")) && tmpMI.Text.ToLower().Equals(((String)reg.GetValue("settings", "Language")).ToLower()))
                {
                    itemToolStripMenuItem_Click(tmpMI, ((ToolStripItem)mnuItemLang));
                    someLangSelected = true;
                }
            }
            if (mnuItemLang.DropDownItems.Count > 0)
            {
                if (!someLangSelected)
                    itemToolStripMenuItem_Click(mnuItemLang.DropDownItems[0], ((ToolStripItem)mnuItemLang));
                refreshLanguage();
            }
            #endregion

            //this.BackgroundImage = tabPage1.BackgroundImage = tabPage2.BackgroundImage = tabPage4.BackgroundImage = global::ReGen.Properties.Resources.background;
            //this.BackgroundImageLayout = tabPage1.BackgroundImageLayout = tabPage2.BackgroundImageLayout = tabPage4.BackgroundImageLayout = ImageLayout.Stretch;

            //Program.log.LogThis("InitializeComponent", eloglevel.verbose);
            //TODO fixed
            addToControlEventOfMouse(/*extTableLayout*/ splitContainerMain);
            GlobalMouseHandler globalClick = new GlobalMouseHandler(this);
            Application.AddMessageFilter(globalClick);
            //Program.log.LogThis("AddMessageFilter", eloglevel.verbose);
            server.Start();
            server.Received += new EventHandler<WcfClientServerDataReceivedEventArgs>(server_Received);
            //Program.log.LogThis("server.Start", eloglevel.verbose);
		}

        private void server_Received(object sender, WcfClientServerDataReceivedEventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Focus();
            this.TopMost = true;
            this.TopMost = false;
            if (!String.IsNullOrEmpty(e.Data))
                if (MessageBox.Show(Program.translate("string_loSchemaSostituira"), Program.translate("string_apriSchema"), MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    loadFile(e.Data);
        }
        /// <summary>
        /// Aggiunge i controlli degli eventi del mouse
        /// </summary>
        /// <param name="container">Control container</param>
        private void addToControlEventOfMouse(Control container)
        {
            foreach(Control son in container.Controls)
            {
                addToControlEventOfMouse(son);
            }
            container.MouseDown+= new MouseEventHandler(container_MouseDown);
            container.MouseUp += new MouseEventHandler(container_MouseUp);
            container.MouseMove+=new MouseEventHandler(container_MouseMove);
        }
        /// <summary>
        /// Calcola il ratio pixel_metri (S.x/W.x)
        /// </summary>
        /// <returns>Ratio pixel_metri (S.x/W.x)</returns>
        public static double getRatioPixel_Meters()
        {
            double res = sizeAtOne.X / MainForm.instance.work.sizeWorkArea.X;
            return res;
        }
        /// <summary>
        /// Calcola il ratio pixel_metri per il zoomLevel (S.x/W.x)*zl
        /// </summary>
        /// <returns>Ratio pixel_metri per il zoomLevel (S.x/W.x)*zl</returns>
        public static double getRatioPixel_MetersPerZoomLevel()
        {
            return getRatioPixel_Meters() * zoomLevel;
        }
        /// <summary>
        /// Effettua lo zoom sull'inPanel
        /// </summary>
        private void reZoom()
        {
            if (zoomLevel > 15) zoomLevel = 15.0F;
            panelScale.Refresh();
            System.Drawing.Point inPanelAfterZoomDim = new System.Drawing.Point((int)(sizeAtOne.X * zoomLevel), (int)(sizeAtOne.Y * zoomLevel));
            if (inPanelAfterZoomDim.X < centerPanel.ClientSize.Width)
            {
                inPanel.Left = (int)((double)(centerPanel.ClientSize.Width - inPanelAfterZoomDim.X) / 2.0);
            }
            else
            {
                inPanel.Left = 0;
            }
            if (inPanelAfterZoomDim.Y < centerPanel.ClientSize.Height)
            {
                inPanel.Top = (int)((double)(centerPanel.ClientSize.Height - inPanelAfterZoomDim.Y) / 2.0);
            }
            else
            {
                inPanel.Top = 0;
            }
            inPanel.Width = inPanelAfterZoomDim.X;
            inPanel.Height = inPanelAfterZoomDim.Y;

            inPanel.Refresh();

            panelScale.Refresh();
            String tmpRes = (zoomLevel * 100.0).ToString("#.00");//.Replace(".", "-").Replace(",", ".").Replace("-", ",");
            zoomTextBox.Text = tmpRes;
            if (zoomLevel > 0.39)
            {
                buttonMinus.Enabled = true;
            }
            else
            {
                buttonMinus.Enabled = false;
            }
        }
        /// <summary>
        /// Effettua il cambio di colore in base allo stato del pulsante
        /// </summary>
        private void refreshApproachAndLabelsButton()
        {
            if (double.Parse(buttonDepositoNW.Tag.ToString()) == SolutionFacade.Instance.angleChoosen)
                buttonDepositoNW.BackColor = Color.Green;
            else
                buttonDepositoNW.BackColor = Color.Gray;
            if (double.Parse(buttonDepositoSE.Tag.ToString()) == SolutionFacade.Instance.angleChoosen)
                buttonDepositoSE.BackColor = Color.Green;
            else
                buttonDepositoSE.BackColor = Color.Gray;
            if (double.Parse(buttonDepositoSW.Tag.ToString()) == SolutionFacade.Instance.angleChoosen)
                buttonDepositoSW.BackColor = Color.Green;
            else
                buttonDepositoSW.BackColor = Color.Gray;
            if (double.Parse(buttonDepositoNE.Tag.ToString()) == SolutionFacade.Instance.angleChoosen)
                buttonDepositoNE.BackColor = Color.Green;
            else
                buttonDepositoNE.BackColor = Color.Gray;
            //if (bool.Parse(button2.Tag.ToString()))
            //    button2.BackColor = Color.Green;
            //else
            //    button2.BackColor = Color.Gray;
            //if (bool.Parse(button3.Tag.ToString()))
            //    button3.BackColor = Color.Green;
            //else
            //    button3.BackColor = Color.Gray;
            //if (bool.Parse(button4.Tag.ToString()))
            //    button4.BackColor = Color.Green;
            //else
            //    button4.BackColor = Color.Gray;
            //if (bool.Parse(button5.Tag.ToString()))
            //    button5.BackColor = Color.Green;
            //else
            //    button5.BackColor = Color.Gray;
            //if (bool.Parse(button7.Tag.ToString()))
            //    button7.BackColor = Color.Green;
            //else
            //    button7.BackColor = Color.Gray;
            //if (bool.Parse(button8.Tag.ToString()))
            //    button8.BackColor = Color.Green;
            //else
            //    button8.BackColor = Color.Gray;
            //if (bool.Parse(button9.Tag.ToString()))
            //    button9.BackColor = Color.Green;
            //else
            //    button9.BackColor = Color.Gray;
            //if (bool.Parse(button10.Tag.ToString()))
            //    button10.BackColor = Color.Green;
            //else
            //    button10.BackColor = Color.Gray;
            //if (bool.Parse(button19.Tag.ToString()))
            //    button19.BackColor = Color.Green;
            //else
            //    button19.BackColor = Color.Gray;
            //if (bool.Parse(button18.Tag.ToString()))
            //    button18.BackColor = Color.Green;
            //else
            //    button18.BackColor = Color.Gray;
            //if (bool.Parse(button17.Tag.ToString()))
            //    button17.BackColor = Color.Green;
            //else
            //    button17.BackColor = Color.Gray;
            //if (bool.Parse(button16.Tag.ToString()))
            //    button16.BackColor = Color.Green;
            //else
            //    button16.BackColor = Color.Gray;
            //if (bool.Parse(button20.Tag.ToString()))
            //    button20.BackColor = Color.Green;
            //else
            //    button20.BackColor = Color.Gray;
            //if (bool.Parse(button21.Tag.ToString()))
            //    button21.BackColor = Color.Green;
            //else
            //    button21.BackColor = Color.Gray;
            //if (bool.Parse(button22.Tag.ToString()))
            //    button22.BackColor = Color.Green;
            //else
            //    button22.BackColor = Color.Gray;
            //if (bool.Parse(button23.Tag.ToString()))
            //    button23.BackColor = Color.Green;
            //else
            //    button23.BackColor = Color.Gray;
            //if (bool.Parse(button31.Tag.ToString()))
            //    button31.BackColor = Color.Green;
            //else
            //    button31.BackColor = Color.Gray;
            //if (bool.Parse(button24.Tag.ToString()))
            //    button24.BackColor = Color.Green;
            //else
            //    button24.BackColor = Color.Gray;
            //if (bool.Parse(button25.Tag.ToString()))
            //    button25.BackColor = Color.Green;
            //else
            //    button25.BackColor = Color.Gray;
            //if (bool.Parse(button26.Tag.ToString()))
            //    button26.BackColor = Color.Green;
            //else
            //    button26.BackColor = Color.Gray;
            //if (bool.Parse(button27.Tag.ToString()))
            //    button27.BackColor = Color.Green;
            //else
            //    button27.BackColor = Color.Gray;
            //if (bool.Parse(button28.Tag.ToString()))
            //    button28.BackColor = Color.Green;
            //else
            //    button28.BackColor = Color.Gray;
            //if (bool.Parse(button29.Tag.ToString()))
            //    button29.BackColor = Color.Green;
            //else
            //    button29.BackColor = Color.Gray;
            //if (bool.Parse(button30.Tag.ToString()))
            //    button30.BackColor = Color.Green;
            //else
            //    button30.BackColor = Color.Gray;
        }
        /// <summary>
        /// Cambia il valore dello stato del button
        /// </summary>
        /// <param name="sender">object sender</param>
        private void changeValueAtButton(object sender)
        {
            SolutionFacade.Instance.refreshPayloadOnPalletRanker(double.Parse(((Button)sender).Tag.ToString()));
            refreshApproachAndLabelsButton();
        }

        /// <summary>
        /// Refresh della MainForm
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">PaintEventArgs e</param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            refreshApproachAndLabelsButton();
        }
        /// <summary>
        /// Click del button2
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button2_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Click del button3
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs</param>
        private void button3_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Click del button4
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button4_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Click del button7
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button7_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Click del button5
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button5_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Click del button10
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button10_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Click del button9
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button9_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Click del button8
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button8_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }

        /// <summary>
        /// Disegna il pannello interno inPanel
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">PaintEventArgs e</param>
        private void inPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics gs = e.Graphics;
            gs.Clear(Color.FromKnownColor(KnownColor.ControlDark));

            System.Drawing.Rectangle workAreaDraw = new System.Drawing.Rectangle(1, 1, pointMetersToPixels(work.sizeWorkArea).X - 2, pointMetersToPixels(new Point2F(0, 0)).Y - 2);
            gs.FillRectangle(new SolidBrush(Color.White), workAreaDraw);

            System.Drawing.Rectangle palletDraw = new System.Drawing.Rectangle(
                pointMetersToPixels(new Point2F((int)(work.extraPallet.Width / 2), 0)).X,
                pointMetersToPixels(new Point2F(0, work.sizeWorkArea.Y - (int)(work.extraPallet.Height / 2))).Y,
                pointMetersToPixels(new Point2F(work.sizeWorkArea.X - (int)(work.extraPallet.Width), 0)).X - 2,
                pointMetersToPixels(new Point2F(0, (int)(work.extraPallet.Height))).Y - 2);

            float[] dashValues = { 6, 3, 6 };
            Pen blackPen = new Pen(Color.Black, 2);
            blackPen.DashPattern = dashValues;

            gs.FillRectangle(new SolidBrush(Program.colorOfPallet), palletDraw);
            gs.DrawRectangle(blackPen, palletDraw);

            SortedListWithDuplicatedKey<Figure> sortedListFigure = new SortedListWithDuplicatedKey<Figure>();
            foreach (Figure f in listFigureOnPlatform)
                sortedListFigure.Add(f.getPriority(), f);

            foreach (Figure f in sortedListFigure.Values)
                f.renderMeThere(gs);
            listFigureOnPlatform.Clear();

            Pen p = new Pen(Color.Black, 1);
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.ArrowAnchor;
            Point p0 = Figure.getPointForRender(new Point2F((-work.palletOnSystem.getExtraBorder().X / 2.0) + 10, (-work.palletOnSystem.getExtraBorder().Y / 2.0) + 10), new Point(0, 0));
            Point p1 = Figure.getPointForRender(new Point2F((-work.palletOnSystem.getExtraBorder().X / 2.0) + 10, (-work.palletOnSystem.getExtraBorder().Y / 2.0) + 60), new Point(0, 0));
            Point p2 = Figure.getPointForRender(new Point2F((-work.palletOnSystem.getExtraBorder().X / 2.0) + 60, (-work.palletOnSystem.getExtraBorder().Y / 2.0) + 10), new Point(0, 00));

            gs.DrawLine(p, p0, p1);
            gs.DrawLine(p, p0, p2);


            if (work.palletOnSystem.getLayerAtIndex(work.getIndexLayerUsed()).interLayer)
            {
                System.Drawing.Rectangle interfalda =
                                   new System.Drawing.Rectangle( pointMetersToPixels(new Point2F((int)(work.extraPallet.Width / 2), 0)).X,
                pointMetersToPixels(new Point2F(0, work.sizeWorkArea.Y - (int)(work.extraPallet.Height / 2))).Y,
                pointMetersToPixels(new Point2F(work.sizeWorkArea.X - (int)(work.extraPallet.Width), 0)).X - 2,
                pointMetersToPixels(new Point2F(0, (int)(work.extraPallet.Height))).Y - 2);

                gs.FillRectangle(new SolidBrush(Color.FromArgb(100, 165,42,42)), interfalda);
            }

            p.Dispose();


        }
        /// <summary>
        /// Aggiusta la grandezza dello zoom rispetto all'altezza o alla larghezza
        /// </summary>
        private void fixSize()
        {
            float ratioI = work.sizeWorkArea.X / work.sizeWorkArea.Y;
            float ratioE = centerPanel.ClientSize.Width / centerPanel.ClientSize.Height;
            if (ratioI > ratioE)
                buttonWFit.PerformClick();
            else
                buttonHFit.PerformClick();
        }
        /// <summary>
        /// Click del buttonPlus (zoom +)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void buttonPlus_Click(object sender, EventArgs e)
        {
            zoomLevel = zoomLevel + 0.2;
            reZoom();
            buttonWFit.Tag = "false";
        }

        /// <summary>
        /// CLick del buttonMinus (zoom -)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void buttonMinus_Click(object sender, EventArgs e)
        {
            zoomLevel = zoomLevel - 0.2;
            reZoom();
            buttonWFit.Tag = "false";
        }
        /// <summary>
        /// Traduce il testo della zoomTextBox nello zoom effettivo
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void zoomTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                zoomLevel = Double.Parse("0" +  Util.getIntFromString(zoomTextBox.Text)) / 100.0;
                //zoomLevel = (Double.Parse ((zoomTextBox.Text))) / 100.0;
            }
            catch { zoomLevel = 1; }
            reZoom();
        }
        /// <summary>
        /// Attiva lo zoom dalla textbox premendo Enter
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">KeyPressEventArgs e</param>
        private void zoomTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                zoomTextBox_Leave(null, null);
            }
        }
        /// <summary>
        /// Click del buttonWFit per fissare la larghezza dell'inPanel al massimo senza la scrollbar
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void buttonWFit_Click(object sender, EventArgs e)
        {
            centerPanel.AutoScrollPosition=new System.Drawing.Point(0,0);
            
            zoomLevel = (Double)centerPanel.ClientSize.Width / (Double)sizeAtOne.X;
            reZoom();
            zoomLevel = (Double)centerPanel.ClientSize.Width / (Double)sizeAtOne.X;
            reZoom();
            buttonWFit.Tag = "true";
        }
        /// <summary>
        /// Click del buttonHFit per fissare l'altezza dell'inPanel al massimo senza la scrollbar
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void buttonHFit_Click(object sender, EventArgs e)
        {
            centerPanel.AutoScrollPosition = new System.Drawing.Point(0, 0);
            zoomLevel = (Double)centerPanel.ClientSize.Height / (Double)sizeAtOne.Y;
            reZoom();
            zoomLevel = (Double)centerPanel.ClientSize.Height / (Double)sizeAtOne.Y;
            reZoom();
            buttonWFit.Tag = "false";
        }

        /// <summary>
        /// Resetta la posizione del mouse alla posizione corrente rispetto al Platform e al Depot
        /// </summary>
        private void resetMousePosition()
        {
            setMousePosition( //TODO qua
                new System.Drawing.Point((MousePosition.X - tabControl1.Left - tabPageLayout.Left - panelExt.Left - splitContainerMain.Panel2.Left - this.Left - inPanel.Left - centerPanel.Left - leftPanel.Left - extTableLayout.Left - 10), (MousePosition.Y - tabControl1.Top - tabPageLayout.Top - panelExt.Top - splitContainerMain.Panel2.Top - this.Top - inPanel.Top - centerPanel.Top - leftPanel.Top - extTableLayout.Top - 32)),
                new System.Drawing.Point((MousePosition.X - 6/*- tabControl1.Left - tabPage2.Left - panelExt.Left - splitContainerMain.Panel2.Left - this.Left - bankDepotPanel.Left- extTableLayout.Left - 4 - tableLayoutPanel1.Left*/), (MousePosition.Y - tabControl1.Top - tabPageLayout.Top - panelExt.Top - splitContainerMain.Panel2.Top - this.Top - bankDepotPanel.Top  - extTableLayout.Top - tableLayoutPanel1.Top - tableZoomPanel.Height))
                );
            //setMousePosition(coordinateOfMouseOnPlatform(),coordinateOfMouseOnDepot());
        }

        /// <summary>
        /// Setta le posizioni del mouse rispetto al Platform e al Depot
        /// </summary>
        /// <param name="mouseOnPlatform">Posizione del mouse onPlatform</param>
        /// <param name="mouseOnDepot">Posizione del mouse onDepot</param>
        private void setMousePosition(Point mouseOnPlatform, Point mouseOnDepot)
        {
            this.mouseOnPlatform = mouseOnPlatform;
            this.mouseOnDepot = mouseOnDepot;
        }
        /// <summary>
        /// Seleziona il Payload che contiene il punto
        /// </summary>
        /// <param name="p">Punto per la selezione del Payload</param>
        /// <returns>Il Payload selezionato</returns>
        private PayloadGroup getPayloadSelected(Point2F p)
        {
            PayloadGroup res = null;
            foreach (PayloadGroup pp in work.listPayloadGroupPlaced)
            {
                if (pp.contains(p))
                {
                    res = pp;
                    break;
                }
            }

            return res;
        }
        
        /// <summary>
        /// Inizializzazione per la visualizzazione dei Layer
        /// </summary>
        public void onMouseUp()
        {
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
            }
            if (tabControl1.SelectedTab == tabPageLayout)
            {
                setMousePressed(false);
                payloadGroupDraggingFromDepot = null;
                convertPositioningInPlaced();
                bankDepotPanel.Refresh();
                inibiteMouse = false;
            }
        }
        /// <summary>
        /// Setta il valore a true del click del tasto sinistro del mouse
        /// </summary>
        /// <param name="button">MouseButtons button</param>
        public void onMouseDownFirstOfAll(MouseButtons button)
        {
            leftPressed = true;
            //onMouseDown(button);
        }
        //TODO da rivedere
        /// <summary>
        /// Implementa il click del mouse
        /// </summary>
        /// <param name="button">MouseButtons button</param>
        public void onMouseDown(MouseButtons button)
        {
            if (tabControl1.SelectedTab == tabPageLayout)
            {
                setMousePressed(true);
                resetMousePosition();
                Point mouseOnD = coordinateOfMouseOnDepot();
                Point mouseOnP = coordinateOfMouseOnPlatform();
                if (lastPointClickedForMenu != null && (button == System.Windows.Forms.MouseButtons.Left))
                    inibiteMouse = new Point2F(MousePosition).isContainedOnRect(new RectangleF(lastPointClickedForMenu.toLocation(), menuDimension));
                if (!inibiteMouse)
                {
                    Point2F mouseCoo = this.coordinateOfMouseOnPlatformInMeters();
                    PayloadGroup pSelected = this.getPayloadSelected(mouseCoo);
                    payloadGroupLastMoved = pSelected;
                    if (!ctrlPressing && (mouseCoo.X < 0 || mouseCoo.Y < 0))
                        listPayloadGroupSelected.Clear();
                    bool pressingOnPressingJet = false;
                    if (pSelected != null)
                    {
                        recOfSelectionOrNullIfPayloadClicked = null;
                        if (MainForm.instance.listPayloadGroupSelected.Contains(pSelected))
                            pressingOnPressingJet = true;
                    }
                    else
                        recOfSelectionOrNullIfPayloadClicked = mouseCoo;
                    if (!(pressingOnPressingJet || ctrlPressing))
                        listPayloadGroupSelected.Clear();
                    if (pSelected != null && !MainForm.instance.listPayloadGroupSelected.Contains(pSelected))
                        listPayloadGroupSelected.Add(pSelected);
                    if (button == System.Windows.Forms.MouseButtons.Left)
                    {
                        foreach (PayloadGroup pp in listPayloadGroupSelected)
                            pp.center = new Point2F(pp.center.X - mouseCoo.X, pp.center.Y - mouseCoo.Y);
                    }
                    else
                    {
                        setMousePressed(false);
                    }
                }
                bankDepotPanel.Refresh();
                lastPointClickedForMenu = null;
                onMouseMoveSettingMousePosition(mouseOnP, mouseOnD);
            }
        }
        //TODO da capire

        static readonly object locker = new object();
        public void onMouseMoveSettingMousePosition(Point mouseOnPlatform, Point mouseOnDepot)
        {
            lock (locker)
            {
                if (tabControl1.SelectedTab == tabPageLayout)
                {
                    if (!inibiteMouse)
                    {
                        setMousePosition(mouseOnPlatform, mouseOnDepot);
                        if (mousePressed)
                            mouseMoving = true;
                        Point2F actualCoordinateOfMouse = coordinateOfMouseOnPlatformInMeters();
                        Point tmpMouseAbs = coordinateOfMouseOnPlatform();
                        Point2F tmpMouseMeters = actualCoordinateOfMouse;
                        try
                        {
                            lblPointerXValue.Text = ((int)tmpMouseMeters.X).ToString();
                            lblPointerYValue.Text = ((int)tmpMouseMeters.Y).ToString();
                        }
                        catch { }
                        
                        RectangleF rectOfWorkArea = new RectangleF(-MainForm.instance.work.extraPallet.Width / 2, MainForm.instance.work.sizeWorkArea.Y - MainForm.instance.work.extraPallet.Height / 2, MainForm.instance.work.sizeWorkArea.X, -MainForm.instance.work.sizeWorkArea.Y);
                            
                        if (mouseMoving)
                        {
                            if (payloadGroupDraggingFromDepot != null)
                            {
                                if (payloadGroupDraggingFromDepot.isContainedInRect(actualCoordinateOfMouse.toLocation(), rectOfWorkArea))
                                {
                                    listPayloadGroupSelected.Add(payloadGroupDraggingFromDepot);
                                    payloadGroupDraggingFromDepot = null;
                                    recOfSelectionOrNullIfPayloadClicked = null;
                                }
                                else
                                {
                                    listFigureOnDepot.Add(Figure.getFigure(payloadGroupDraggingFromDepot.firstOfListPayloadPlaced(), new PlacingState_onDepot(), mouseOnDepot, true));
                                    listFigureOnPlatform.Add(Figure.getFigure(payloadGroupDraggingFromDepot.firstOfListPayloadPlaced(), new PlacingState_onDepot(), mouseOnPlatform, true));
                                }
                            }
                        }

                        //predisponi figure nella piattaform
                        foreach (PayloadGroup pp in work.listPayloadGroupPlaced)
                        {
                            DateTime nowForFor = DateTime.Now;
                            if (mouseMoving && tokenForResetApproach)
                                pp.resetApproaches();
                            if (!mouseMoving)
                            {
                                if (tokenForResetApproach)
                                {
                                    pp.resetApproaches();
                                    foreach (PayloadGroup pp2 in work.listPayloadGroupPlaced)
                                        if (!pp2.isTheSame(pp))
                                        {
                                            Approacher a = new Approacher(pp, pp2, pp, work.palletOnSystem.getLayerAtIndex(work.getIndexLayerUsed()));
                                        }
                                }
                            }
                            if (pp != null)
                            {
                                bool selected = listPayloadGroupSelected.Contains(pp);
                                if ((!mousePressed && selected) || !selected)
                                {
                                    foreach (Figure fg in (pp.getListFigureToRender(new PlacingState_placed(), new Point2F(0, 0), selected)))
                                        listFigureOnPlatform.Add(fg);
                                }
                            }
                        }
                        if (tokenForResetApproach)
                            tokenForResetApproach = false;

                        if (mouseMoving)
                        {
                            tokenForResetApproach = true;
                            if (recOfSelectionOrNullIfPayloadClicked == null)
                            {
                                //predisponi figure in piazzamento
                                foreach (PayloadGroup ps in listPayloadGroupSelected)
                                {
                                    if (ps != null)
                                    {
                                        //qui vanno imposti i vincoli
                                        Point2F mouseCoo = coordinateOfMouseOnPlatformInMeters();
                                        Binder bi = new MouseBinder(ps, mouseCoo);
                                        RectangleF rectOfPalletBorder = new RectangleF(0, work.sizeWorkArea.Y - work.extraPallet.Height, work.sizeWorkArea.X - work.extraPallet.Width, -(work.sizeWorkArea.Y - work.extraPallet.Height));
                                        bi = new AlignerOfRectangle(bi, rectOfPalletBorder, ps);
                                        foreach (PayloadGroup pp in work.listPayloadGroupPlaced)
                                            if (!listPayloadGroupSelected.Contains(pp))
                                                bi = new AlignerOfPayloadGroup(bi, pp, ps);
                                        bi = new Repellers(bi, rectOfWorkArea, ps);
                                        bi.resetApproaches();
                                        ps.resetApproaches();
                                        foreach (PayloadGroup pp in work.listPayloadGroupPlaced)
                                            if (!listPayloadGroupSelected.Contains(pp))
                                                bi = new Approacher(bi, pp, ps, work.palletOnSystem.getLayerAtIndex(work.getIndexLayerUsed()));

                                        ps.listProblem.Clear();
                                        foreach (PayloadGroup pp in work.listPayloadGroupPlaced)
                                            if (pp != ps)
                                                bi = new Signaler(bi, pp, ps);

                                        foreach (Figure fg in (bi.getListFigureToRender(new PlacingState_onPlatform())))
                                            listFigureOnPlatform.Add(fg);
                                    }
                                }
                            }
                        }
                        if (payloadGroupLastMoved != null)
                        {
                            lblSelectionXValue.Text = ((int)payloadGroupLastMoved.placedMe().left()).ToString();
                            lblSelectionYValue.Text = ((int)payloadGroupLastMoved.placedMe().bottom()).ToString();
                        }
                        else
                        {
                            lblSelectionXValue.Text = "";
                            lblSelectionYValue.Text = "";
                        }

                        bankDepotPanel.Refresh();
                        //inPanel.Invalidate();
                        inPanel.Refresh();
                    }
                }
                if (!timer1.Enabled && (MouseButtons == System.Windows.Forms.MouseButtons.Left))
                {
                    timer1.Enabled = true;
                }
            }
        }

        //TODO fix refresh collisione
        /// <summary>
        /// Implementa il rilascio del click del mouse sull'inPanel (refresh dei problemi di collisione)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">MouseEventArgs e</param>
        private void inPanel_MouseUp(object sender, MouseEventArgs e)
        {
            List<PayloadGroup> payloadCollided = new List<PayloadGroup>();
            foreach (PayloadGroup pg1 in work.listPayloadGroupPlaced)
            {
                foreach (PayloadGroup pg2 in work.listPayloadGroupPlaced)
                {
                    if (pg1 != pg2)
                    {
                        if (pg1.collides(pg2))
                            payloadCollided.Add(pg1);
                    }
                }
            }
            foreach (PayloadGroup p in work.listPayloadGroupPlaced)
            {
                if (!payloadCollided.Contains(p))
                {
                    p.listProblem.Clear();
                }
            }
        }

        /// <summary>
        /// Implementa il comportamento della rotella del mouse
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">MouseEventArgs e</param>
        private void onMouseWheel(object sender, MouseEventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageLayout)
            {
                if (e.Delta != 0 && ctrlPressing)
                {
                    zoomLevel = Math.Max(0.2,zoomLevel + (e.Delta * 0.0005));
                    reZoom();
                    buttonWFit.Tag = "false";
                }
            }
        }

        /// <summary>
        /// Implementa il comportamento del movimento del mouse
        /// </summary>
        public void onMouseMove()
        {
            if (!ctrlPressing) resetMousePosition();
            onMouseMoveSettingMousePosition(coordinateOfMouseOnPlatform(), coordinateOfMouseOnDepot());
            
        }
        // TODO da rivedere
        /// <summary>
        /// Calcola la dimensione X e Y del punto in metri (p.xW.x/S.xZl - E.x/2) (1- p.yW.y/S.yZl - E.y/2)
        /// </summary>
        /// <param name="p">Punto da convertire</param>
        /// <returns>(p.xW.x/S.xZl - E.x/2) (1- p.yW.y/S.yZl - E.y/2)</returns>
        public static Point2F pointPixelsToMeters(Point p)
        {
            Point2F res = new Point2F((float)(((p.X * MainForm.instance.work.sizeWorkArea.X) / sizeAtOne.X) / zoomLevel),
                (float)(((sizeAtOne.Y - (p.Y / zoomLevel)) * MainForm.instance.work.sizeWorkArea.Y) / sizeAtOne.Y));
            res = new Point2F((float)(res.X - (MainForm.instance.work.extraPallet.Width / 2.0)), (float)(res.Y - (MainForm.instance.work.extraPallet.Height / 2.0)));
            return (res);
        }

        
        /// <summary>
        /// Dovrebbe convertire il punto da metri in pixel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Point pointMetersToPixels(Point2F p)
        {
            //TODO test usando ptmp non si vedono differenze apparenti
            Point2F pTmp = new Point2F((float)(p.X + (MainForm.instance.work.extraPallet.Width / 2.0)), (float)(p.Y + (MainForm.instance.work.extraPallet.Height / 2.0)));
            Point res = new Point((int)(p.X * getRatioPixel_MetersPerZoomLevel()), (int)((sizeAtOne.Y * zoomLevel ) - (p.Y * getRatioPixel_MetersPerZoomLevel())));
            return (res);
        }
        /// <summary>
        /// Torna le coordinate del mouse onDepot
        /// </summary>
        /// <returns>Coordinate del mouse onDepot</returns>
        public System.Drawing.Point coordinateOfMouseOnDepot()
        {
            return this.mouseOnDepot;
            //return new System.Drawing.Point((MousePosition.X - tabControl1.Left - tabPage2.Left - panelExt.Left - splitContainerMain.Panel2.Left - this.Left - bankDepotPanel.Left  - extTableLayout.Left - 4 - tableLayoutPanel1.Left), (MousePosition.Y - tabControl1.Top - tabPage2.Top - panelExt.Top - splitContainerMain.Panel2.Top - this.Top - bankDepotPanel.Top -  extTableLayout.Top - tableLayoutPanel1.Top - tableZoomPanel.Height));
        }
        /// <summary>
        /// Trova le coordinate del mouse onPlatform
        /// </summary>
        /// <returns>Coordinate del mouse onPlatform</returns>
        public System.Drawing.Point coordinateOfMouseOnPlatform()
        {
            return this.mouseOnPlatform;
            //return new System.Drawing.Point((MousePosition.X - tabControl1.Left - tabPage2.Left - panelExt.Left - splitContainerMain.Panel2.Left - this.Left - inPanel.Left - centerPanel.Left - leftPanel.Left - extTableLayout.Left - 10), (MousePosition.Y - tabControl1.Top - tabPage2.Top - panelExt.Top - splitContainerMain.Panel2.Top - this.Top - inPanel.Top - centerPanel.Top - leftPanel.Top - extTableLayout.Top - 32));
        }
        /// <summary>
        /// Converte le coordinate in pixel del mouse onPlatform in metri
        /// </summary>
        /// <returns>Coordinate del mouse onPlatform in metri</returns>
        public Point2F coordinateOfMouseOnPlatformInMeters()
        {
            Point tmpP = coordinateOfMouseOnPlatform();
            Point2F tmpPF = pointPixelsToMeters(tmpP);//.traslatedOf(0,0);
            return tmpPF;
        }

        public bool loadFile(String filePath)
        {
            bool res = false;

            listPayloadGroupSelected.Clear();
            resetLayers();

            inibiteChange = true;
            if (File.Exists(filePath) && (new FileInfo(filePath)).Length > 0)
            {
                StreamReader sr = new StreamReader(filePath);
                String fileContent = sr.ReadToEnd();
                sr.Close();
                try
                {
                    String key = (String)reg.GetValue("key", "LicenceIdPc");
                    if (!String.IsNullOrEmpty(key) && (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2]))
                    {
                        String hwCode = key.Split('-')[0];
                        String dateCode = key.Split('-')[1];
                        StringBuilder cbcDate = new StringBuilder(dateCode);
                        StringBuilder encrypt = new StringBuilder(fileContent.Substring(0, fileContent.Length - 32));
                        String decrypt;
                        String checkSum;
                        bool ok = decodeWorkData(encrypt, out decrypt, cbcDate, new StringBuilder(HwProtection.diskCodeMD5()), new StringBuilder(hwCode), out checkSum);

                        // checksum
                        String checkPreCalculated = HwProtection.getMD5("prefisso" + encrypt + hwCode + "suffisso");

                        if ((checkPreCalculated == checkSum) && ok && Sintec.Tool.HwProtection.verifyCustomCodeMD5(fileContent.Substring(fileContent.Length - 32), decrypt.ToString()))
                        {
                            this.work = new WorkData(new StringBuilder(decrypt));

                            txtCustomPick.Text = work.customPickData;
                            txtCustomPlacement.Text = work.customPlaceData;

                            txtInterlayerThickness.Text = work.palletOnSystem.interLayerThickness.ToString("0");
                            checkboxInterfaldaPresenzaSuPallet.Checked = work.palletOnSystem.interLayer;
                            txtPanelThickness.Text = work.palletOnSystem.panelOverPalletThickness.ToString("0");
                            checkboxPannelloPresenzaSuPallet.Checked = work.palletOnSystem.panelOverPallet;

                            bypass = true;
                            txtApproachX.Text = work.palletOnSystem.approachFixedDirection.X.ToString("0.00");
                            txtApproachY.Text = work.palletOnSystem.approachFixedDirection.Y.ToString("0.00");
                            txtApproachZ.Text = work.palletOnSystem.approachFixedDirection.Z.ToString("0.00");
                            bypass = false;

                            txtPalletWidth.Text = work.palletOnSystem.getSize().X.ToString();
                            txtPalletLenght.Text = work.palletOnSystem.getSize().Y.ToString();
                            txtPalletHeight.Text = work.palletOnSystem.getSize().Z.ToString();

                            txtExtraBorderX.Text = work.palletOnSystem.getExtraBorder().X.ToString();
                            txtExtraBorderY.Text = work.palletOnSystem.getExtraBorder().Y.ToString();

                            txtPayload1Name.Text = work.payloads[0].getPayloadStrategy().getName();
                            txtBoxX.Text = work.payloads[0].getOriginalSize().X.ToString();
                            txtBoxY.Text = work.payloads[0].getOriginalSize().Y.ToString();
                            txtBoxZ.Text = work.payloads[0].getOriginalSize().Z.ToString();

                            cmbPayloadsNumber.SelectedIndex = 0;
                            if (work.payloads.Count > 1)
                            {
                                cmbPayloadsNumber.SelectedIndex = 1;
                                txtPayload2Name.Text = work.payloads[1].getPayloadStrategy().getName();
                                txtBoxX.Text = work.payloads[1].getOriginalSize().X.ToString();
                                txtBoxY.Text = work.payloads[1].getOriginalSize().Y.ToString();
                                txtBoxZ.Text = work.payloads[1].getOriginalSize().Z.ToString();
                            }
                            //textBox9.Text = (work.box.getOriginalBounds().size.X - work.box.getOriginalSize().X).ToString(); //Util.getDoubleFromString(work.box.getPayloadStrategy().toDataString().Split(']')[2].Split('[')[0]).ToString();
                            //textBox10.Text = Util.getDoubleFromString(work.box.getPayloadStrategy().toDataString().Split(']')[3].Split('[')[0]).ToString();
                            txtPalletMaxHeight.Text = work.palletOnSystem.getMaxHeight().ToString();

                            res = true;
                        }
                    }
                }
                catch { }
            }
            else
            {
                return true;
            }
            inibiteChange = false;
            if (!res)
            {
                checkLicenceIdPcFromRegistry();
                MessageBox.Show(Program.translate("string_problemiNellAperturaDelFile"));
            }
            listPayload = new List<Payload>();
            foreach (Payload p in work.payloads)
                listPayload.Add(p);
            PayloadVisualizationManager.refreshSchemeList(null, listPayload);
            LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
            this.selectingLayer(LayerVisualizationManager.listLayerVisualization[0]);
            resetSizeAtOne();
            fixSize();
            reZoom();
            resetPreview();
            return res;
        }

        private void resetPreview()
        {
            if (panePreview.Tag!=null)
                ((Layer)panePreview.Tag).listPayloadGroupPlaced.Clear();
            panePreview.Refresh();
        }

        #region Language methods 
        private void refreshLanguage()
        {
            toolStripMenuItem1.Text = Program.translate("mnuCentra");//"Centra";
            toolStripMenuItem3.Text = Program.translate("mnuAllinea");// "Allinea";
            equidistribuisciToolStripMenuItem.Text = Program.translate("equidistribuisciToolStripMenuItem");//"Equidistribuisci";
            soloInLarghezzaToolStripMenuItem.Text = Program.translate("soloInLarghezzaToolStripMenuItem");//"in larghezza";
            soloInLunghezzaToolStripMenuItem.Text = Program.translate("soloInLunghezzaToolStripMenuItem");//"in lunghezza";
            inEntrambiToolStripMenuItem.Text = Program.translate("inEntrambiToolStripMenuItem");//"in entrambi";
            aSinistraToolStripMenuItem.Text = Program.translate("aSinistraToolStripMenuItem");//"a sinistra";
            aDestraToolStripMenuItem.Text = Program.translate("aDestraToolStripMenuItem");//"a destra";
            inAltoToolStripMenuItem.Text = Program.translate("inAltoToolStripMenuItem");//"in alto";
            inBassoToolStripMenuItem.Text = Program.translate("inBassoToolStripMenuItem");//"in basso";
            inLarghezzaToolStripMenuItem.Text = " " + Program.translate("soloInLarghezzaToolStripMenuItem");//" in larghezza";
            inAltezzaToolStripMenuItem.Text = " " + Program.translate("soloInLunghezzaToolStripMenuItem");//" in lunghezza";
            inEntrambiToolStripMenuItem1.Text = " " + Program.translate("inEntrambiToolStripMenuItem");//" in entrambi";
            collegaISelezionatiToolStripMenuItem.Text = Program.translate("collegaISelezionatiToolStripMenuItem");//"Collega i selezionati";
            scollegaISelezionatiToolStripMenuItem.Text = Program.translate("scollegaISelezionatiToolStripMenuItem");//"Scollega i selezionati";
            ricopiaToolStripMenuItem.Text = Program.translate("ricopiaToolStripMenuItem");//"Ricopia";
            impostaProgressivoToolStripMenuItem.Text = Program.translate("impostaProgressivoToolStripMenuItem");//"Imposta progressivo";
            toolStripMenuItem2.Text = Program.translate("toolStripMenuItem2");//"Crea tutti gli strati basandoti sui primi due...";
            ricopiaStratoToolStripMenuItem.Text = Program.translate("ricopiaStratoToolStripMenuItem");//"Ricopia";
            stratoSelezionatoToolStripMenuItem.Text = Program.translate("stratoSelezionatoToolStripMenuItem");//"strato selezionato";
            primiDueStratiToolStripMenuItem.Text = Program.translate("primiDueStratiToolStripMenuItem");//"primi due strati";
            tuttiGliStratiToolStripMenuItem.Text = Program.translate("tuttiGliStratiToolStripMenuItem");//"tutti gli strati";
            cancellaToolStripMenuItem.Text = Program.translate("cancellaToolStripMenuItem");//"Cancella";
            spostaGiùToolStripMenuItem.Text = Program.translate("spostaGiùToolStripMenuItem");//"Sposta giù";
            spostaSuToolStripMenuItem.Text = Program.translate("spostaSuToolStripMenuItem");//"Sposta su";
            capovolgiOrizzontalmenteToolStripMenuItem.Text = Program.translate("capovolgiOrizzontalmenteToolStripMenuItem");//"Capovolgi orizzontalmente";
            capovolgiVerticalmenteToolStripMenuItem.Text = Program.translate("capovolgiVerticalmenteToolStripMenuItem");//"Capovolgi verticalmente";
            ruotaToolStripMenuItem.Text = Program.translate("ruotaToolStripMenuItem");//"Ruota...";
            selezionaTuttoToolStripMenuItem.Text = Program.translate("selezionaTuttoToolStripMenuItem");//"Seleziona tutto";
            mnuItemLang.Text = Program.translate("mnuItemLang");
            fileToolStripMenuItem.Text = Program.translate("fileToolStripMenuItem");
            salvaSchemaDiPallettizzazioneToolStripMenuItem.Text = Program.translate("salvaSchemaDiPallettizzazioneToolStripMenuItem");
            apriToolStripMenuItem.Text = Program.translate("apriToolStripMenuItem");
            licenzaToolStripMenuItem.Text = Program.translate("licenzaToolStripMenuItem");
            infoToolStripMenuItem.Text = Program.translate("infoToolStripMenuItem");
            riguardoRegenToolStripMenuItem.Text = Program.translate("riguardoRegenToolStripMenuItem");
            tabPageDati.Text = Program.translate("tabPageDati");
            tabPageLayout.Text = Program.translate("tabPageLayout");
            tabPageGenerazione.Text = Program.translate("tabPageGenerazione");
            labelPayloads.Text = Program.translate("labelPayloads");
            labelPayload1Nome.Text = Program.translate("labelPayload1Nome");
            labelPayload1Larghezza.Text = Program.translate("labelPayload1Larghezza");
            labelPayload1Lunghezza.Text = Program.translate("labelPayload1Lunghezza");
            labelPayload1Altezza.Text = Program.translate("labelPayload1Altezza");
            labelPayload2Nome.Text = Program.translate("labelPayload2Nome");
            labelPayload2Larghezza.Text = Program.translate("labelPayload2Larghezza");
            labelPayload2Lunghezza.Text = Program.translate("labelPayload2Lunghezza");
            labelPayload2Altezza.Text = Program.translate("labelPayload2Altezza");
            labelPallet.Text = Program.translate("labelPallet");
            labelPalletLarghezza.Text = Program.translate("labelPalletLarghezza");
            labelPalletLunghezza.Text = Program.translate("labelPalletLunghezza");
            labelPalletAltezza.Text = Program.translate("labelPalletAltezza");
            labelPalletAltezzaMassima.Text = Program.translate("labelPalletAltezzaMassima");
            labelPalletLarghezzaExtra.Text = Program.translate("labelPalletLarghezzaExtra");
            labelPalletLunghezzaExtra.Text = Program.translate("labelPalletLunghezzaExtra");
            labelPannelloSpessore.Text = Program.translate("labelPannelloSpessore");
            labelInterfaldaSpessore.Text = Program.translate("labelInterfaldaSpessore");
            checkboxPannelloPresenzaSuPallet.Text = Program.translate("checkboxPannelloPresenzaSuPallet");
            checkboxInterfaldaPresenzaSuPallet.Text = Program.translate("checkboxInterfaldaPresenzaSuPallet");
            labelOpzioniDiDeposito.Text = Program.translate("labelOpzioniDiDeposito");
            labelDepositoInizio.Text = Program.translate("labelDepositoInizio");
            buttonDepositoNW.Text = Program.translate("buttonDepositoNW");
            buttonDepositoNE.Text = Program.translate("buttonDepositoNE");
            buttonDepositoSW.Text = Program.translate("buttonDepositoSW");
            buttonDepositoSE.Text = Program.translate("buttonDepositoSE");
            labelDepositoTestoCustomExtraData.Text = Program.translate("labelDepositoTestoCustomExtraData");
            labelDepositoTestoCustomPlacementInfo.Text = Program.translate("labelDepositoTestoCustomPlacementInfo");
            labelDepositoAccostamento.Text = Program.translate("labelDepositoAccostamento");
            buttonDepositoAutomatico.Text = Program.translate("buttonDepositoAutomatico");
            buttonDepositoApplica.Text = Program.translate("buttonDepositoApplica");
            labelPayload.Text = Program.translate("labelPayload");
            labelStrato.Text = Program.translate("labelStrato");
            labelSelezioneSx.Text = Program.translate("labelSelezioneSx");
            labelSelezioneBasso.Text = Program.translate("labelSelezioneBasso");
            labelStratiPallet.Text = Program.translate("labelStratiPallet");
            labelSelezione.Text = Program.translate("labelSelezione");
            labelPuntatore.Text = Program.translate("labelPuntatore");
            labelPuntatoreX.Text = Program.translate("labelPuntatoreX");
            labelPuntatoreY.Text = Program.translate("labelPuntatoreY");
            labelConnettoreVirtuale.Text = Program.translate("labelConnettoreVirtuale");
            buttonConnettoreRefreshAnteprima.Text = Program.translate("buttonConnettoreRefreshAnteprima");
            buttonConnettoreInvia.Text = Program.translate("buttonConnettoreInvia");
            labelConnettoreIstruzioni.Text = Program.translate("labelConnettoreIstruzioni");
            labelConnettoreImpostazioni.Text = Program.translate("labelConnettoreImpostazioni");
            labelConnettoreAnteprimaDati.Text = Program.translate("labelConnettoreAnteprimaDati");
            if (connectorUsing != null)
            {
                connectorUsing.setLanguage(Program.languagesPath + " \\" + Program.currentLanguage() + ".lng");
                txtConfig.Text = connectorUsing.getIstruction(work.ToString());
                txtPreview.Text = connectorUsing.getPreview(work.ToString(2));
            }
        }
        
        private void itemLangMenuClicked(String nomeLanguage)
        {
            reg.SetValue("settings", "Language", nomeLanguage);
            Program.setLanguage(nomeLanguage);
            refreshLanguage();
        }

        void eventMenuItem_Click(object sender, EventArgs e)
        {
            itemToolStripMenuItem_Click((ToolStripMenuItem)sender, ((ToolStripMenuItem)sender).OwnerItem);
        }

        private void itemToolStripMenuItem_Click(ToolStripItem item, ToolStripItem owner)
        {
            foreach (ToolStripItem tmpItem in ((ToolStripMenuItem)owner).DropDownItems)
            {
                if (tmpItem == ((ToolStripItem)item))
                {
                    ((ToolStripMenuItem)tmpItem).Checked = true;
                    itemLangMenuClicked(tmpItem.Text);
                }
                else
                {
                    ((ToolStripMenuItem)tmpItem).Checked = false;
                }
            }
        }
        #endregion

        /// <summary>
        /// Carica le informazioni dai Panel alla MainForm
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            String firstStartDate = (String)reg.GetValue("key", "firstStartDate");
            //Program.log.LogThis("read key firstStartDate", eloglevel.verbose);
            if (String.IsNullOrEmpty(firstStartDate))
            {
                reg.SetValue("key", "firstStartDate", HwProtection.Encrypt(DateTime.Now.ToString("ddMMyyyy")));
                reg.SetValue("key", "lastStartDate", HwProtection.Encrypt(DateTime.Now.ToString("ddMMyyyy")));
                //Program.log.LogThis("write key Date", eloglevel.verbose);
            }

            //CONTROLLO DELLA LICENZA
            checkLicenceIdPcFromRegistry();
            //Program.log.LogThis("checkLicenceIdPcFromRegistry", eloglevel.verbose);

            reg.SetValue("key", "lastStartDate", HwProtection.Encrypt(DateTime.Now.ToString("ddMMyyyy")));
            //Program.log.LogThis("write key lastdate", eloglevel.verbose);
            
            MouseWheel += new MouseEventHandler(onMouseWheel);

            MainForm_Resize(null, null);
            textBox1_TextChanged(null, null);
            textBox7_TextChanged(null, null);

            //Program.log.LogThis("before loadfile", eloglevel.verbose);
            loadFile(fileToOpen);
            //Program.log.LogThis("after loadfile", eloglevel.verbose);
            //work.payloads.Clear();
            //work.payloads.Add(new Payload(new BoxStrategy(new Point3F(400, 600, 18), "Antina DX")));
            //work.payloads.Add(new Payload(new BoxStrategy(new Point3F(400, 600, 18), "Antina SX")));
            PayloadVisualizationManager.refreshSchemeList(null, work.payloads);
            //Program.log.LogThis("refreshSchemeList", eloglevel.verbose);

            refreshApproachAndLabelsButton();
            //Program.log.LogThis("refreshApproachAndLabelsButton", eloglevel.verbose);

            if (!((reg.GetValue("openDialog", "autoStart") == null) ? false : bool.Parse((reg.GetValue("openDialog", "autoStart").ToString()))))
            {
                OpenForm of = new OpenForm();
                of.chosenRecipeHandler += new OpenForm.ChosenRecipe(of_chosenRecipeHandler);
                of.ShowDialog();
            }

            Sintec.Tool.DoubleBufferedPanel.SetDoubleBuffered(panelWhereDraw);
        }

        private void checkLicenceIdPcFromRegistry()
        {
            bool isLifeTimeLicence = false;
            if (!isLicencedIdPcFromRegistry(out isLifeTimeLicence))
            {
                timer2.Enabled = false;
                licenzaToolStripMenuItem_Click(null, null);
                if (!isLicencedIdPcFromRegistry(out isLifeTimeLicence))
                    Environment.Exit(0);
                timer2.Enabled = true;
            }
            panelLogoMobile.Visible = !isLifeTimeLicence;
        }

        private bool isLicencedIdPcFromRegistry(out bool isLifeTimeLicence)
        {
            isLifeTimeLicence = false;
            bool res = false;
            String key = (String)reg.GetValue("key", "LicenceIdPc");
            if (!String.IsNullOrEmpty(key))
            {
                String hwCode = key.Split('-')[0];
                String dateCode = key.Split('-')[1];
                String date = HwProtection.Decrypt(dateCode);
                DateTime startDate = DateTime.ParseExact(date.Substring(0, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(date.Substring(8, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                DateTime limitTolifeTime = DateTime.Now;
                limitTolifeTime = limitTolifeTime.AddYears(2049);
                limitTolifeTime = limitTolifeTime.Subtract(TimeSpan.FromDays(365* (DateTime.Now.Year + 1)));
                isLifeTimeLicence = endDate > limitTolifeTime;

                bool lastStartOk = false;

                String lastStartDateString = (String)reg.GetValue("key", "lastStartDate");
                if (!String.IsNullOrEmpty(key))
                {
                    try
                    {
                        DateTime lastStart = DateTime.ParseExact(HwProtection.Decrypt((String)reg.GetValue("key", "lastStartDate")), "ddMMyyyy", CultureInfo.InvariantCulture);
                        lastStartOk = (lastStart >= startDate) && (lastStart <= endDate) && (DateTime.Now >= lastStart);
                    }
                    catch { }
                }

                if (startDate.ToString("ddMMyyyy") != HwProtection.Decrypt((String)reg.GetValue("key", "firstStartDate")))
                    MessageBox.Show(Program.translate("string_chiaveScaduta"));
                else if (!lastStartOk)
                    MessageBox.Show(Program.translate("string_chiaveScaduta"));
                else if (DateTime.Now < startDate)
                    MessageBox.Show(Program.translate("string_chiaveData"));
                else if (DateTime.Now > endDate)
                    MessageBox.Show(Program.translate("string_chiaveScaduta"));
                else if (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) != key.Split('-')[2])
                    MessageBox.Show(Program.translate("string_chiaveNonValida"));
                else
                {
                    res = checkIdPcLicence(new StringBuilder(dateCode), new StringBuilder(HwProtection.diskCodeMD5()), new StringBuilder(hwCode));
                    if (!res)
                        MessageBox.Show(Program.translate("string_chiaveNonValidaOMancante"));
                }
            }
            isLifeTimeLicence = isLifeTimeLicence && res;
            return res;
        }

        /// <summary>
        /// Attiva le dimensioni del pallet che sono state inserite nelle TextBox
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void txtPalletWidth_TextChanged(object sender, EventArgs e)
        {
            //if (!inibiteChange && !Double.IsNaN(Util.getDoubleFromString(textBox6.Text)) && Util.getDoubleFromString(textBox6.Text) != 0)
            //{
            //    work.listPayloadGroupPlaced.Clear();
            //    work.palletOnSystem.setSizeX((float) Util.getDoubleFromString(textBox6.Text));
                
            //    LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
            //}
            //else if (!inibiteChange) {
            //    textBox6.Text = work.palletOnSystem.getSize().X.ToString();
            //}
            textBox_Changed(sender, e);
        }
        
        /// <summary>
        /// Attiva le dimensioni del pallet che sono state inserite nelle TextBox
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //if(!inibiteChange && !Double.IsNaN(Util.getDoubleFromString(textBox7.Text))){
            //    work.listPayloadGroupPlaced.Clear();
            //    work.palletOnSystem.setExtraBorderX((float)(Util.getDoubleFromString(textBox7.Text)));
            //    LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
            //} else if (!inibiteChange)
            //    textBox7.Text = work.palletOnSystem.getExtraBorder().X.ToString();
            textBox_Changed(sender, e);
        }
        /// <summary>
        /// Attiva le dimensioni del pallet che sono state inserite nelle TextBox
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void textBox8_TextChanged(object sender, EventArgs e)
        {   
            //if (!inibiteChange && !Double.IsNaN(Util.getDoubleFromString(textBox8.Text)))
            //{
            //    work.listPayloadGroupPlaced.Clear();
            //    work.palletOnSystem.setExtraBorderY((float)(Util.getDoubleFromString(textBox8.Text)));
            //    LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
            //}
            //else if (!inibiteChange)
            //    textBox8.Text = work.palletOnSystem.getExtraBorder().Y.ToString();
            textBox_Changed(sender, e);
        }
        /// <summary>
        /// Attiva le dimensioni del pallet che sono state inserite nelle TextBox
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //TODO fixed
            //if (!inibiteChange && !Double.IsNaN(Util.getDoubleFromString(textBox5.Text)) && Util.getDoubleFromString(textBox5.Text) != 0)
            //{
            //    work.listPayloadGroupPlaced.Clear();
            //    work.palletOnSystem.setSizeY((float)(Util.getDoubleFromString(textBox5.Text)));
            //    LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
            //}
            //else if (!inibiteChange)
            //    textBox5.Text = work.palletOnSystem.getSize().Y.ToString();
            textBox_Changed(sender, e);
        }

        /// <summary>
        /// Cancella tutti i layer
        /// </summary>
        private void resetLayers()
        {
            work.deleteAllLayers();
        }

        private void resetSizeAtOne()
        {
            if ((work.sizeWorkArea.X / work.sizeWorkArea.Y) > ((centerPanel.ClientSize.Width) / (centerPanel.ClientSize.Height)))
                sizeAtOne = new System.Drawing.Point(centerPanel.ClientSize.Width, (int)((centerPanel.ClientSize.Width) * MainForm.instance.work.sizeWorkArea.Y / MainForm.instance.work.sizeWorkArea.X));
            else
                sizeAtOne = new System.Drawing.Point((int)((centerPanel.ClientSize.Height) * MainForm.instance.work.sizeWorkArea.X / MainForm.instance.work.sizeWorkArea.Y), centerPanel.ClientSize.Height);
        }


        /// <summary>
        /// Parametrizza l'area di lavoro in base al tab selezionato (visualizzato)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tf != null) tf.Close();
            DoubleBuffered = true;
            if (tabControl1.SelectedTab == tabPageLayout)
            {
                bankDepotPanel.Refresh();
                if (work.sizeWorkArea.X > 0 && work.sizeWorkArea.Y > 0)
                {
                    refreshApproachAndLabelsButton();
                    resetSizeAtOne();
                    fixSize();
                    reZoom();
                }
                else
                {
                    MessageBox.Show(Program.translate("string_erroreDatiPallet"));
                    tabControl1.SelectedTab = tabPageDati;
                    if (work.sizeWorkArea.X <= 0)
                    {
                        txtPalletWidth.Focus();
                        txtPalletWidth.SelectionStart = 0;
                        txtPalletWidth.SelectionLength = txtPalletWidth.Text.Length;
                    }
                    else
                    {
                        txtPalletLenght.Focus();
                        txtPalletLenght.SelectionStart = 0;
                        txtPalletLenght.SelectionLength = txtPalletLenght.Text.Length;
                    }
                }
                if (tabPageLayout.Tag == null)
                {
                    if ((inPanel.Height / inPanel.Width) > (work.palletOnSystem.getSizeWithBorder().Y / work.palletOnSystem.getSizeWithBorder().X))
                        buttonWFit.PerformClick();
                    else
                        buttonHFit.PerformClick();
                    tabPageLayout.Tag = "noFirstTime";
                }
            }
            else if (tabControl1.SelectedTab == tabPageGenerazione)
            {
                panel7.Refresh();
                comboBox1.Items.Clear();
                typeOnCombo = VirtualConnectors.VirtualConnectors.getTypeConnectors();
                foreach (Type t in typeOnCombo)
                {
                    comboBox1.Items.Add(t.Name);
                }
                if (comboBox1.SelectedIndex == -1 && comboBox1.Items.Count>0) 
                    comboBox1.SelectedIndex = 0;
                initOutput();

            }
            else if (tabControl1.SelectedTab == tabPageDati)
            {
                try
                {
                    panePreview.CreateGraphics().Clear(System.Drawing.SystemColors.ControlDark);
                }
                catch { }
            }
        }
        //TODO non implemenentato
        private void initOutput()
        {
        }
        /// <summary>
        /// Invia le informazioni al connector
        /// </summary>
        private void send()
        {
            connectorUsing.send(work.ToString());
        }

        /// <summary>
        /// Attiva le modifiche alle scatole in base al valore della TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox_Changed(sender, e);
        }
        /// <summary>
        /// Disegna il panel del bankDepot
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">PaintEventArgs e</param>
        private void bankDepotPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics gs = e.Graphics;  //bankDepotPanel.CreateGraphics();// myBuffer.Graphics;
            
            PayloadVisualizationManager.refreshVisualization(gs);

            SortedListWithDuplicatedKey<Figure> sortedListFigure = new SortedListWithDuplicatedKey<Figure>();
            foreach (Figure f in listFigureOnDepot)
                sortedListFigure.Add(f.getPriority(), f);

            foreach (Figure f in sortedListFigure.Values)
                f.renderMeThere(gs);
            listFigureOnDepot.Clear();
        }
        /// <summary>
        /// Attiva le modifiche alle scatole in base al valore della TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox_Changed(sender, e);
        }
        /// <summary>
        /// Attiva le modifiche alle scatole in base al valore della TextBox
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox_Changed(sender, e);
            txtBox2Z.Text = txtBoxZ.Text;
        }
        //TODO added
        /// <summary>
        /// Implementa i valori per le dimensioni della scatola dalle textbox
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void textBox_Changed(object sender, EventArgs e)
        {
            float X, Y, Z;//, tollerance, surplus;
            float X2, Y2, Z2;//, tollerance, surplus;
            String payloadName1, payloadName2 = "";
            bool interlayerOnPallet, panelOnPallet;
            double interlayerThickness, panelThickness;

            float W,L,H,EX,EY,MH;
            //tollerance = 0;
            //surplus = 0;
            bool thereIsAChange = false;

            payloadName1 = txtPayload1Name.Text;
            if (payloadName1 != work.payloads[0].getPayloadStrategy().getName())
                thereIsAChange = true;
            if (cmbPayloadsNumber.SelectedIndex != 0)
            {
                payloadName2 = txtPayload2Name.Text;
                if (work.payloads.Count == 0 || ((work.payloads.Count > 1) && (payloadName2 != work.payloads[1].getPayloadStrategy().getName())))
                    thereIsAChange = true;
            }

            interlayerOnPallet = checkboxInterfaldaPresenzaSuPallet.Checked;
            if (interlayerOnPallet != work.palletOnSystem.interLayer)
                thereIsAChange = true;
            panelOnPallet = checkboxPannelloPresenzaSuPallet.Checked;
            if (panelOnPallet != work.palletOnSystem.panelOverPallet)
                thereIsAChange = true;

            double tmpDouble;
            if (double.TryParse(txtInterlayerThickness.Text.Replace(".", ","), out tmpDouble))
                interlayerThickness = tmpDouble;
            else
            {
                interlayerThickness = work.palletOnSystem.interLayerThickness;
                txtInterlayerThickness.Text = interlayerThickness.ToString("0");
            }
            if (tmpDouble != work.palletOnSystem.interLayerThickness)
                thereIsAChange=true;

            if (double.TryParse(txtPanelThickness.Text.Replace(".", ","), out tmpDouble))
                panelThickness = tmpDouble;
            else
            {
                panelThickness = work.palletOnSystem.panelOverPalletThickness;
                txtPanelThickness.Text = panelThickness.ToString("0");
            }
            if (tmpDouble != work.palletOnSystem.panelOverPalletThickness)
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtExtraBorderX.Text)))
                EX = (float)(Util.getDoubleFromString(txtExtraBorderX.Text));
            else
            {
                EX = work.palletOnSystem.getExtraBorder().X;
                txtExtraBorderX.Text = EX.ToString();
            }
            if (EX != work.palletOnSystem.getExtraBorder().X)
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtExtraBorderY.Text)))
                EY = (float)(Util.getDoubleFromString(txtExtraBorderY.Text));
            else
            {
                EY = work.palletOnSystem.getExtraBorder().Y;
                txtExtraBorderY.Text = EX.ToString();
            }
            if (EY != work.palletOnSystem.getExtraBorder().Y)
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtPalletWidth.Text)))
                W = (float)(Util.getDoubleFromString(txtPalletWidth.Text));
            else
            {
                W = work.palletOnSystem.getSize().X;
                txtPalletWidth.Text = W.ToString();
            }
            if (W != work.palletOnSystem.getSize().X)
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtPalletLenght.Text)))
                L = (float)(Util.getDoubleFromString(txtPalletLenght.Text));
            else
            {
                L = work.palletOnSystem.getSize().Y;
                txtPalletLenght.Text = L.ToString();
            }
            if (L != work.palletOnSystem.getSize().Y)
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtPalletHeight.Text)))
                H = (float)(Util.getDoubleFromString(txtPalletHeight.Text));
            else
            {
                H = work.palletOnSystem.getSize().Z;
                txtPalletHeight.Text = L.ToString();
            }
            if (H != work.palletOnSystem.getSize().Z)
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtPalletMaxHeight.Text)))
                MH = (float)(Util.getDoubleFromString(txtPalletMaxHeight.Text));
            else
            {
                MH = work.palletOnSystem.getMaxHeight();
                txtPalletMaxHeight.Text = L.ToString();
            }
            if (MH != work.palletOnSystem.getMaxHeight())
                thereIsAChange = true;


            if (!Double.IsNaN(Util.getDoubleFromString(txtBoxX.Text)))
                X = (float)Util.getDoubleFromString(txtBoxX.Text);
            else
            {
                X = MainForm.instance.work.payloads[0].getPayloadStrategy().getSize().X;
                txtBoxX.Text = X.ToString();
            }
            if (X != MainForm.instance.work.payloads[0].getPayloadStrategy().getSize().X)
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtBoxY.Text)))
                Y = (float)Util.getDoubleFromString(txtBoxY.Text);
            else
            {
                Y = MainForm.instance.work.payloads[0].getPayloadStrategy().getSize().Y;
                txtBoxY.Text = Y.ToString();
            }
            if (Y != MainForm.instance.work.payloads[0].getPayloadStrategy().getSize().Y)
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtBoxZ.Text)))
                Z = (float)Util.getDoubleFromString(txtBoxZ.Text);
            else
            {
                Z = MainForm.instance.work.payloads[0].getPayloadStrategy().getSize().Z;
                txtBoxZ.Text = Z.ToString();
            }
            if (Z != MainForm.instance.work.payloads[0].getPayloadStrategy().getSize().Z)
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtBox2X.Text)))
                X2 = (float)Util.getDoubleFromString(txtBox2X.Text);
            else
            {
                X2 = MainForm.instance.work.payloads[1].getPayloadStrategy().getSize().X;
                txtBox2X.Text = X2.ToString();
            }
            if (MainForm.instance.work.payloads.Count==0 || (MainForm.instance.work.payloads.Count>1 && X2 != MainForm.instance.work.payloads[1].getPayloadStrategy().getSize().X))
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtBox2Y.Text)))
                Y2 = (float)Util.getDoubleFromString(txtBox2Y.Text);
            else
            {
                Y2 = MainForm.instance.work.payloads[1].getPayloadStrategy().getSize().Y;
                txtBox2Y.Text = Y2.ToString();
            }
            if (MainForm.instance.work.payloads.Count==0 || (MainForm.instance.work.payloads.Count>1 && Y2 != MainForm.instance.work.payloads[1].getPayloadStrategy().getSize().Y))
                thereIsAChange = true;

            if (!Double.IsNaN(Util.getDoubleFromString(txtBox2Z.Text)))
                Z2 = (float)Util.getDoubleFromString(txtBox2Z.Text);
            else
            {
                Z2 = MainForm.instance.work.payloads[1].getPayloadStrategy().getSize().Z;
                txtBox2Z.Text = Z2.ToString();
            }
            if (MainForm.instance.work.payloads.Count==0 || (MainForm.instance.work.payloads.Count>1 && Z2 != MainForm.instance.work.payloads[1].getPayloadStrategy().getSize().Z))
                thereIsAChange = true;

            if ((cmbPayloadsNumber.SelectedIndex + 1) != work.payloads.Count)
                thereIsAChange = true;

            //if (!Double.IsNaN(Util.getDoubleFromString(textBox10.Text)))
            //    tollerance = (float)Util.getDoubleFromString(textBox10.Text);
            //else
            //    textBox10.Text = tollerance.ToString();
            //if (!Double.IsNaN(Util.getDoubleFromString(textBox9.Text)))
            //    surplus = (float)Util.getDoubleFromString(textBox9.Text);
            //else
            //    textBox9.Text = surplus.ToString();

                
            if (thereIsAChange && (!inibiteChange))
            {
                resetLayers();
                listPayloadGroupSelected.Clear();

                MainForm.instance.work.payloads[0].resetStrategy(new BoxStrategy(new Point3F(X, Y, Z), payloadName1));//, surplus, tollerance));
                if (cmbPayloadsNumber.SelectedIndex == 1)
                {
                    if (MainForm.instance.work.payloads.Count < 2)
                        MainForm.instance.work.payloads.Add(new Payload(new BoxStrategy(new Point3F(X2, Y2, Z2), payloadName2)));
                    else
                        MainForm.instance.work.payloads[1].resetStrategy(new BoxStrategy(new Point3F(X2, Y2, Z2), payloadName2));//, surplus, tollerance));
                }
                else
                {
                    if (MainForm.instance.work.payloads.Count>1)
                        MainForm.instance.work.payloads.RemoveAt(1);
                }

                MainForm.instance.work.palletOnSystem.setExtraBorder(new Point2F(EX, EY));
                MainForm.instance.work.palletOnSystem.setSize(new Point3F(W, L, H));
                MainForm.instance.work.palletOnSystem.setMaxHeight(MH);
                //MainForm.instance.work.box.resetStrategy(new BoxWithRightSurplusStrategy(new Point3F(X, Y, Z), surplus, tollerance));

                MainForm.instance.work.palletOnSystem.panelOverPallet = panelOnPallet;
                MainForm.instance.work.palletOnSystem.interLayer = interlayerOnPallet;
                MainForm.instance.work.palletOnSystem.panelOverPalletThickness = panelThickness;
                MainForm.instance.work.palletOnSystem.interLayerThickness = interlayerThickness;

                listPayload = new List<Payload>();
                foreach (Payload p in MainForm.instance.work.payloads)
                    listPayload.Add(p);
                PayloadVisualizationManager.refreshSchemeList(null, listPayload);

                work.palletOnSystem.setExtraBorderX((float)(Util.getDoubleFromString(txtExtraBorderX.Text)));
                work.palletOnSystem.setExtraBorderY((float)(Util.getDoubleFromString(txtExtraBorderY.Text)));
                LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);

                resetPreview();
            }
            
        }
        /// <summary>
        /// Comportamento del click del mouse all'interno del bankDepotPanel
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">MouseEventArgs e</param>
        private void bankDepotPanel_MouseDown(object sender, MouseEventArgs e)
        {
            listPayloadGroupSelected.Clear();
            PayloadVisualization payVisLastSelected = PayloadVisualizationManager.selected(new PointF(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y));
            if (payVisLastSelected != null)
            {
                payloadGroupDraggingFromDepot = new PayloadGroup(new PayloadPlaced(payVisLastSelected.payload));
                payloadGroupDraggingFromDepot.center = new Point2F(0, 0);
                payloadGroupDraggingFromDepot.setQuadrant(0);
                payloadGroupLastMoved = payloadGroupDraggingFromDepot;
            }
            recOfSelectionOrNullIfPayloadClicked = null;
        }
        /// <summary>
        /// Piazza i PayloadGroup sul pannello
        /// </summary>
        private void convertPositioningInPlaced()
        {
            foreach (PayloadGroup pp in listPayloadGroupSelected)
            {
                if (pp != null)
                {
                    if (listPayloadGroupSelected.Contains(pp))
                    {
                        if (work.listPayloadGroupPlaced.Contains(pp))
                            work.listPayloadGroupPlaced.Remove(pp);
                        work.listPayloadGroupPlaced.Add(pp.placeMe());
                    }
                }
            }
        }
        //TODO da implementare
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        /// <summary>
        /// Applica le funzionalità dei tasti CTRL+R per la rotazione dei Payload all'interno dei PayloadGroup selezionati e del tasto I per il display delle informazioni
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">KeyEventArgs e</param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            ctrlPressing = e.Control;
            if ((e.KeyCode & Keys.R) == Keys.R)
            {
                foreach (PayloadGroup pp in listPayloadGroupSelected)
                     pp.setQuadrant((pp.getQuadrant() + 1) % 4);
            }
            if ((e.KeyCode & Keys.I) == Keys.I)
            {
                bool panel10Refresh = false;
                if (!Program.infoRequesting)
                    panel10Refresh = true;
                Program.infoRequesting = true;
                if (panel10Refresh) panePreview.Refresh(); 
            }
            if (e.Shift && (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9))
            {
                typingProgressive = true;
                tempProgressive = tempProgressive + (char)(48 + e.KeyCode-Keys.D0);
            }
            onMouseMove();
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
            }
        }
        /// <summary>
        /// Applica le funzionalità del tasto DEL per cancellare i PayloadGroup selezionati
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">KeyEventArgs e</param>
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            
            bool panel10Refresh = false;
            if (Program.infoRequesting)
                panel10Refresh = true;
            Program.infoRequesting = false;
            if (panel10Refresh) panePreview.Refresh(); 

            ctrlPressing = e.Control;
            if ((e.KeyCode & Keys.Delete) == Keys.Delete)
            {
                foreach (PayloadGroup pp in listPayloadGroupSelected)
                {
                    if (MainForm.instance.work.listPayloadGroupPlaced.Contains(pp)) work.listPayloadGroupPlaced.Remove(pp);
                }


                List<PayloadGroup> payloadCollided = new List<PayloadGroup>();
                foreach (PayloadGroup pg1 in work.listPayloadGroupPlaced)
                {
                    foreach (PayloadGroup pg2 in work.listPayloadGroupPlaced)
                    {
                        if (pg1 != pg2)
                        {
                            if (pg1.collides(pg2))
                                payloadCollided.Add(pg1);
                        }
                    }
                }
                foreach (PayloadGroup p in work.listPayloadGroupPlaced)
                {
                    if (!payloadCollided.Contains(p))
                    {
                        p.listProblem.Clear();
                    }
                }


                listPayloadGroupSelected.Clear();
            }
            if (!e.Shift)
            {
                if (typingProgressive)
                {
                    if (listPayloadGroupSelected.Count == 1)
                    {
                        int oldProg = 0;
                        if (int.TryParse(tempProgressive, out oldProg))
                        {
                            if (oldProg > 0 && oldProg <= work.listPayloadGroupPlaced.Count)
                                Sequencer.fixPositionAt(work.getLayerUsed(), listPayloadGroupSelected[0], oldProg);
                        }
                    }
                }
                typingProgressive = false;
                tempProgressive = "";
            }
            onMouseMove();
        }
        
        
        private void inPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && recOfSelectionOrNullIfPayloadClicked == null)
            {
                bool connectable = false;
                bool connected = false;
                foreach (PayloadGroup pg in listPayloadGroupSelected)
                {
                    if (pg.countListPayloadPlaced() > 1)
                        connected = true;
                    else
                    {
                        foreach (PayloadGroup pg2 in listPayloadGroupSelected)
                        {
                            if (!pg2.isTheSame(pg))// && pg2.payloadPlacedListCount() > 0)
                                connectable = true;
                        }
                    }
                }
                collegaISelezionatiToolStripMenuItem.Visible = connectable;
                scollegaISelezionatiToolStripMenuItem.Visible = connected;
                contextMenuStrip1.Show(MousePosition);
                lastPointClickedForMenu = new Point2F(MousePosition.X - menuDimension.Width / 3, MousePosition.Y - menuDimension.Height / 3);
            }
            else if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip3.Show(MousePosition);
                lastPointClickedForMenu = new Point2F(MousePosition.X - menuDimension.Width / 3, MousePosition.Y - menuDimension.Height / 3);
            }
        }



        public void onMouseLeftDown()
        {
            this.Invoke(new Action(() => onMouseDown(System.Windows.Forms.MouseButtons.Left)));
        }

        Thread onMouseMoveThread = null;

        /// <summary>
        /// Implementa il comportamento del click sul container
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">MouseEventArgs e</param>
        public void container_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                onMouseDown(e.Button);
            else
            {
                ThreadState ts = ThreadState.Unstarted;
                if (onMouseMoveThread!=null)
                    ts = onMouseMoveThread.ThreadState;
                if ((onMouseMoveThread == null) || (onMouseMoveThread != null && ts != ThreadState.Running))
                {
                    onMouseMoveThread = new Thread(new ThreadStart(this.onMouseLeftDown));
                    onMouseMoveThread.Priority = ThreadPriority.Highest;
                    onMouseMoveThread.Start();
                }
            }
        }
        /// <summary>
        /// Implementa il comportamento del rilascio del click sul container
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">MouseEventArgs e</param>
        public void container_MouseUp(object sender, MouseEventArgs e)
        {
            onMouseUp();
        }
        //TODO da implementare
        public void container_MouseMove(object sender, MouseEventArgs e)
        {
            //onMouseMove();
        }
        public class GlobalMouseHandler : IMessageFilter
        {
            MainForm mf;
            public GlobalMouseHandler(MainForm mf)
            {
                this.mf = mf;
            }
            private const int WM_LBUTTONDOWN = 0x201;
            private const int WM_LBUTTONUP = 0x0202;
            private const int WM_MOUSEMOVE = 0x0200;
            private const int WM_RBUTTONDOWN = 0x0204;
            /*
             *  WM_CAPTURECHANGED
                WM_GETICON
                WM_LBUTTONDBLCLK
                WM_LBUTTONDOWN
                WM_LBUTTONUP
                WM_MBUTTONDOWN
                WM_MBUTTONUP
                WM_MOUSEMOVE
                WM_MOUSEWHEEL
                WM_RBUTTONDOWN
                WM_RBUTTONUP
                WM_SETCURSOR
                WM_SETICON
             */
            //TODO non utilizzato
            public bool PreFilterMessage(ref Message m)
            {
                bool res=false;
                if (m.Msg == WM_MOUSEMOVE)
                {
                    mf.onMouseMove();
                    res = false;
                }
                if (m.Msg == WM_LBUTTONUP)
                    mf.onMouseUp();
                if (m.Msg == WM_LBUTTONDOWN)
                    mf.onMouseDownFirstOfAll(MouseButtons.Left);
                //if (m.Msg == WM_RBUTTONDOWN)
                //{
                //    mf.onMouseDownFirstOfAll(MouseButtons.Right);
                //    res = true;
                //}
                return res;
            }
        }

        /// <summary>
        /// Implementa il centrameto sia in altezza che in larghezza (click destro su un gruppo di PayloadGroup)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void inEntrambiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            soloInLarghezzaToolStripMenuItem_Click(sender, e);
            soloInAltezzaToolStripMenuItem_Click(sender, e);
        }
        /// <summary>
        /// Implementa il centramento in altezza (click destro su un gruppo di PayloadGroup)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void soloInAltezzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listPayloadGroupSelected.Count > 0)
            {
                Region bound = listPayloadGroupSelected[0].regionOfMe();
                foreach (PayloadGroup pp in listPayloadGroupSelected)
                {
                    bound.Union(pp.regionOfMe());
                }
                float offset = ((work.sizeWorkArea.Y - bound.GetBounds(Program.g1).Height - work.extraPallet.Height) / 2) - bound.GetBounds(Program.g1).Top;
                foreach (PayloadGroup pp in listPayloadGroupSelected)
                {
                    pp.move(new Point2F(0, offset));
                }
                inPanel.Refresh();
            }
        }

        /// <summary>
        /// Implementa il centramento in larghezza (click destro su un gruppo di PayloadGroup)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void soloInLarghezzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listPayloadGroupSelected.Count > 0)
            {
                Region bound = listPayloadGroupSelected[0].regionOfMe();
                foreach (PayloadGroup pp in listPayloadGroupSelected)
                {
                    bound.Union(pp.regionOfMe());
                }
                float offset = ((work.sizeWorkArea.X - bound.GetBounds(Program.g1).Width - work.extraPallet.Width) / 2) - bound.GetBounds(Program.g1).Left;
                foreach (PayloadGroup pp in listPayloadGroupSelected)
                {
                    pp.move(new Point2F(offset, 0));
                }
                inPanel.Refresh();
            }
        }
        /// <summary>
        /// Implementa il collegamento di gruppi di Payload (click destro su un gruppo di PayloadGroup)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void collegaISelezionatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listPayloadGroupSelected.Count > 1)
            {
                PayloadGroup firstPg = listPayloadGroupSelected[0];
                int i = 1;
                for (i = 1; i < listPayloadGroupSelected.Count; i++)
                {
                    PayloadGroup pg = listPayloadGroupSelected[i];
                    if (!pg.isTheSame(firstPg))
                    {
                        firstPg.mergeWith(pg);
                        listPayloadGroupSelected.Remove(pg);
                        work.listPayloadGroupPlaced.Remove(pg);
                        i--;
                    }
                }
                onMouseMove();
            }
        }
        /// <summary>
        /// Implementa lo scollegamento dei PayloadGroup collegati (click destro su un PayloadGroup collegato)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void scollegaISelezionatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listPayloadGroupSelected.Count > 0)
            {
                int i = 0;
                List<PayloadGroup> payloadsTmp = new List<PayloadGroup>();
                for (i = 0; i < listPayloadGroupSelected.Count; i++)
                {
                    PayloadGroup pg = listPayloadGroupSelected[i];
                    foreach (PayloadGroup pgTmp in pg.disperse())
                        payloadsTmp.Add(pgTmp);
                    listPayloadGroupSelected.Remove(pg);
                    work.listPayloadGroupPlaced.Remove(pg);
                }
                foreach (PayloadGroup pgTmp in payloadsTmp)
                {
                    work.listPayloadGroupPlaced.Add(pgTmp);
                    listPayloadGroupSelected.Add(pgTmp);
                }
                onMouseMove();
            }
        }
        /// <summary>
        /// Implementa l'allineamento a sinistra dei PayloadGroup selezionati (click destro su un gruppo di PayloadGroup)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aSinistraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inibiteMouse = true;
            double t = Util.getDoubleFromString(Microsoft.VisualBasic.Interaction.InputBox(Program.translate("string_distanzaMargineSinistro"), Program.translate("string_inserisciUnaDistanza"), "0"));
            if (!double.IsNaN(t))
            {
                inibiteMouse = false;
                float finalPos = 0;
                float startPos = float.MaxValue;
                foreach (PayloadGroup pg in listPayloadGroupSelected)
                {
                    finalPos = (float)t + (float)(pg.center.X - pg.left());
                    float tmpStartPos = pg.center.X;
                    if (tmpStartPos < startPos)
                        startPos = pg.center.X;
                }
                foreach (PayloadGroup pg in listPayloadGroupSelected)
                {
                    pg.recentering(new Point2F(pg.center.X - startPos + finalPos, pg.center.Y));
                }
            }
        }
        /// <summary>
        /// Implementa l'allineamento a destra dei PayloadGroup selezionati (click destro su un gruppo di PayloadGroup)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void aDestraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inibiteMouse = true;
            double t = Util.getDoubleFromString(Microsoft.VisualBasic.Interaction.InputBox(Program.translate("string_distanzaMargineDestro"), Program.translate("string_inserisciUnaDistanza"), "0"));
            if (!double.IsNaN(t))
            {
                inibiteMouse = false;
                float finalPos = 0;
                float startPos = float.MinValue;
                foreach (PayloadGroup pg in listPayloadGroupSelected)
                {
                    finalPos = MainForm.instance.work.sizeWorkArea.X - (float)t - (float)(pg.center.X - pg.left());
                    float tmpStartPos = pg.center.X;
                    if (tmpStartPos > startPos)
                        startPos = pg.center.X;
                }
                foreach (PayloadGroup pg in listPayloadGroupSelected)
                {
                    pg.recentering(new Point2F(pg.center.X - startPos + finalPos, pg.center.Y));
                }
            }
        }
        /// <summary>
        /// Implementa l'allineamento in basso dei PayloadGroup selezionati (click destro su un gruppo di PayloadGroup)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void inBassoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inibiteMouse = true;
            double t = Util.getDoubleFromString(Microsoft.VisualBasic.Interaction.InputBox(Program.translate("string_distanzaMargineBasso"), Program.translate("string_inserisciUnaDistanza"), "0"));
            if (!double.IsNaN(t))
            {
                inibiteMouse = false;
                float finalPos = 0;
                float startPos = float.MaxValue;
                foreach (PayloadGroup pg in listPayloadGroupSelected)
                {
                    finalPos = (float)t + (float)(pg.center.Y - pg.bottom());
                    float tmpStartPos = pg.center.Y;
                    if (tmpStartPos < startPos)
                        startPos = pg.center.Y;
                }
                foreach (PayloadGroup pg in listPayloadGroupSelected)
                {
                    pg.recentering(new Point2F(pg.center.X, pg.center.Y - startPos + finalPos));
                }
            }
        }
        /// <summary>
        /// Implementa l'allineamento in alto dei PayloadGroup selezionati (click destro su di un gruppo di PayloadGroup)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void inAltoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inibiteMouse = true;
            string tString = Microsoft.VisualBasic.Interaction.InputBox(Program.translate("string_distanzaMargineAlto"), Program.translate("string_inserisciUnaDistanza"), "0");
            double t = Util.getDoubleFromString(tString);
            if (!double.IsNaN(t))
            {
                inibiteMouse = false;
                float finalPos = 0;
                float startPos = float.MinValue;
                foreach (PayloadGroup pg in listPayloadGroupSelected)
                {
                    finalPos = MainForm.instance.work.sizeWorkArea.Y - (float)t - (float)(pg.center.Y - pg.bottom());
                    float tmpStartPos = pg.center.Y;
                    if (tmpStartPos > startPos)
                        startPos = pg.center.Y;
                }
                foreach (PayloadGroup pg in listPayloadGroupSelected)
                {
                    pg.recentering(new Point2F(pg.center.X, pg.center.Y - startPos + finalPos));
                }
            }
        }
        //TODO dovrebbe riferirsi all'equidistribuzione
        private void inLarghezzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float epsilon = 0.001F;
            List<Point2F> rests = new List<Point2F>();
            Dictionary<PayloadGroup,Point2F> dictGroupInRest = new Dictionary<PayloadGroup,Point2F>();
            rests.Add(new Point2F(-work.extraPallet.Width / 2.0F, work.sizeWorkArea.X - (work.extraPallet.Width / 2.0F)));
            List<PayloadGroup> pGroups = new List<PayloadGroup>();

            SortedListWithDuplicatedKey<PayloadGroup> sPGroups = new SortedListWithDuplicatedKey<PayloadGroup>();
            foreach (PayloadGroup pg in listPayloadGroupSelected)
                sPGroups.Add(pg.left(), pg);
            foreach (PayloadGroup pg in sPGroups.Values)
                pGroups.Add(pg);
            foreach (PayloadGroup pg in pGroups)
            {
                List<Point2F> restsToRemove = new List<Point2F>();
                List<Point2F> restsToAdd = new List<Point2F>();
                Point2F lastRest = new Point2F(-(work.extraPallet.Width / 2.0F), -(work.extraPallet.Width / 2.0F));
                Point2F lastRestAtSx = null;
                foreach (Point2F r in rests)
                {
                    //1:    payload interno al restante
                    if (r.X <= (pg.left() + epsilon) && r.Y >= (pg.right() - epsilon))
                    {
                        float lastY = r.Y;
                        r.Y = (float)pg.left();
                        restsToAdd.Add(new Point2F((float)pg.right(),lastY));
                        dictGroupInRest.Add(pg, r);
                        break;
                    }
                    //2.1:  payload a cavallo del bordo sinistro
                    else if (r.X >= pg.left() && r.Y >= (pg.right()) && (pg.right())>= r.X)
                    {
                        r.X = (float)pg.right();
                        dictGroupInRest.Add(pg, lastRest);
                        break;
                    }
                    //2.2:  payload a cavallo del bordo destro
                    else if (r.X <= pg.left() && pg.left() <= r.Y && r.Y <= pg.right())
                    {
                        r.Y = (float)pg.left();
                        dictGroupInRest.Add(pg, r);
                        break;
                    }
                    //3.1:    payload a sinistra del restante
                    else if (r.X >= pg.right())
                    {
                        dictGroupInRest.Add(pg, lastRest);
                        break;
                    }
                    //3.2:    payload a destra del restante
                    else if (r.Y <= pg.left())
                    {
                        lastRestAtSx = r;
                    }
                    //4:    payload coprente il restante
                    else if (r.X >= pg.left() && r.Y <= pg.right())
                    {
                        dictGroupInRest.Add(pg, lastRest);
                        break;
                    }
                    lastRest = r;
                }
                if (!dictGroupInRest.ContainsKey(pg))
                {
                    if (lastRestAtSx != null)
                        dictGroupInRest.Add(pg, lastRestAtSx);
                    else
                        dictGroupInRest.Add(pg, lastRest);
                    break;
                }
                foreach (Point2F r in restsToRemove)
                    rests.Remove(r);
                foreach (Point2F r in restsToAdd)
                    rests.Add(r);

                //riordina i restanti
                SortedDictionary<double, Point2F> sRests = new SortedDictionary<double, Point2F>();
                foreach (Point2F r in rests)
                    sRests.Add(r.X, r);
                rests.Clear();
                foreach (Point2F r in sRests.Values)
                    rests.Add(r);
            }
            //ridividi i restanti
            float sum=0;
            List<Point2F> restsToRem = new List<Point2F>();
            foreach (Point2F p in rests)
            {
                if ((p.Y - p.X) < epsilon && (((p.Y + epsilon) > (work.sizeWorkArea.X - (work.extraPallet.Width / 2.0F))) || ((p.Y - epsilon) < -(work.extraPallet.Width / 2.0F))))
                    restsToRem.Add(p);
                else
                    sum += (p.Y - p.X);
            }
            foreach (Point2F r in restsToRem)
                rests.Remove(r);
            float div = sum/rests.Count;
            Dictionary<PayloadGroup, Point2F> dictNoModifiesRest = new Dictionary<PayloadGroup, Point2F>();
            foreach (PayloadGroup pg in dictGroupInRest.Keys)
                dictNoModifiesRest.Add(pg, dictGroupInRest[pg].traslatedOf(new Point2F(0,0)));
            for (int i=0;i<rests.Count;i++)
            {
                Point2F p = rests[i];
                float added = div - (p.Y - p.X);
                p.Y = p.X + div;
                for (int j = i+1; j < rests.Count; j++)
                {
                    Point2F pAf = rests[j];
                    pAf.X += added;
                    pAf.Y += added;
                }
            }

            foreach (PayloadGroup pg in listPayloadGroupSelected)
            {
                pg.move(new Point2F((float)(dictGroupInRest[pg].Y - pg.left() + (pg.left() - dictNoModifiesRest[pg].Y)), 0));
            }
        }
        //TODO dovrebbe riferirsi all'equidistribuzione
        private void inAltezzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float epsilon = 0.001F;
            List<Point2F> rests = new List<Point2F>();
            Dictionary<PayloadGroup, Point2F> dictGroupInRest = new Dictionary<PayloadGroup, Point2F>();
            rests.Add(new Point2F(-MainForm.instance.work.extraPallet.Height / 2.0F, MainForm.instance.work.sizeWorkArea.Y - (MainForm.instance.work.extraPallet.Height / 2.0F)));
            List<PayloadGroup> pGroups = new List<PayloadGroup>();

            SortedListWithDuplicatedKey<PayloadGroup> sPGroups = new SortedListWithDuplicatedKey<PayloadGroup>();
            foreach (PayloadGroup pg in listPayloadGroupSelected)
                sPGroups.Add(pg.bottom(), pg);
            foreach (PayloadGroup pg in sPGroups.Values)
                pGroups.Add(pg);
            foreach (PayloadGroup pg in pGroups)
            {
                List<Point2F> restsToRemove = new List<Point2F>();
                List<Point2F> restsToAdd = new List<Point2F>();
                Point2F lastRest = new Point2F(-(MainForm.instance.work.extraPallet.Height / 2.0F), -(MainForm.instance.work.extraPallet.Height / 2.0F));
                Point2F lastRestAtBottom = null;
                foreach (Point2F r in rests)
                {
                    //1:    payload interno al restante
                    if (r.X <= (pg.bottom() + epsilon) && r.Y >= (pg.top() - epsilon))
                    {
                        float lastY = r.Y;
                        r.Y = (float)pg.bottom();
                        restsToAdd.Add(new Point2F((float)pg.top(), lastY));
                        dictGroupInRest.Add(pg, r);
                        break;
                    }
                    //2.1:  payload a cavallo del bordo sinistro
                    else if (r.X >= pg.bottom() && r.Y >= (pg.top()) && (pg.top()) >= r.X)
                    {
                        r.X = (float)pg.top();
                        dictGroupInRest.Add(pg, lastRest);
                        break;
                    }
                    //2.2:  payload a cavallo del bordo destro
                    else if (r.X <= pg.bottom() && pg.bottom() <= r.Y && r.Y <= pg.top())
                    {
                        r.Y = (float)pg.bottom();
                        dictGroupInRest.Add(pg, r);
                        break;
                    }
                    //3.1:    payload a sinistra del restante
                    else if (r.X >= pg.top())
                    {
                        dictGroupInRest.Add(pg, lastRest);
                        break;
                    }
                    //3.2:    payload a destra del restante
                    else if (r.Y <= pg.bottom())
                    {
                        lastRestAtBottom = r;
                    }
                    //4:    payload coprente il restante
                    else if (r.X >= pg.bottom() && r.Y <= pg.top())
                    {
                        dictGroupInRest.Add(pg, lastRest);
                        break;
                    }
                    lastRest = r;
                }
                if (!dictGroupInRest.ContainsKey(pg))
                {
                    if (lastRestAtBottom != null)
                        dictGroupInRest.Add(pg, lastRestAtBottom);
                    else
                        dictGroupInRest.Add(pg, lastRest);
                    break;
                }
                foreach (Point2F r in restsToRemove)
                    rests.Remove(r);
                foreach (Point2F r in restsToAdd)
                    rests.Add(r);

                //riordina i restanti
                SortedDictionary<double, Point2F> sRests = new SortedDictionary<double, Point2F>();
                foreach (Point2F r in rests)
                    sRests.Add(r.X, r);
                rests.Clear();
                foreach (Point2F r in sRests.Values)
                    rests.Add(r);
            }
            //ridividi i restanti
            float sum = 0;
            List<Point2F> restsToRem = new List<Point2F>();
            foreach (Point2F p in rests)
            {
                if ((p.Y - p.X) < epsilon && (((p.Y + epsilon) > (MainForm.instance.work.sizeWorkArea.Y - (MainForm.instance.work.extraPallet.Height / 2.0F))) || ((p.Y - epsilon) < -(work.extraPallet.Width / 2.0F))))
                    restsToRem.Add(p);
                else
                    sum += (p.Y - p.X);
            }
            foreach (Point2F r in restsToRem)
                rests.Remove(r);
            float div = sum / rests.Count;
            Dictionary<PayloadGroup, Point2F> dictNoModifiesRest = new Dictionary<PayloadGroup, Point2F>();
            foreach (PayloadGroup pg in dictGroupInRest.Keys)
                dictNoModifiesRest.Add(pg, dictGroupInRest[pg].traslatedOf(new Point2F(0, 0)));
            for (int i = 0; i < rests.Count; i++)
            {
                Point2F p = rests[i];
                float added = div - (p.Y - p.X);
                p.Y = p.X + div;
                for (int j = i + 1; j < rests.Count; j++)
                {
                    Point2F pAf = rests[j];
                    pAf.X += added;
                    pAf.Y += added;
                }
            }

            foreach (PayloadGroup pg in listPayloadGroupSelected)
            {
                pg.move(new Point2F(0, (float)(dictGroupInRest[pg].Y - pg.bottom() + (pg.bottom() - dictNoModifiesRest[pg].Y))));
            }
        }
        //TODO dovrebbe riferirsi all'equidistribuzione
        private void inEntrambiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            inLarghezzaToolStripMenuItem_Click(sender, e);
            inAltezzaToolStripMenuItem_Click(sender, e);
        }
        /// <summary>
        /// Attiva le modifiche alle scatole in base al valore della textbox
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            textBox_Changed(sender, e);
        }
        /// <summary>
        /// Salva il file xml cone le informazioni alla chiusura della form
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">FormClosingEventArgs e</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StringBuilder s = new StringBuilder();
            work.getXmlStream(s);
            String encrypt = Sintec.Tool.HwProtection.Encrypt(s.ToString());
            String hash = Sintec.Tool.HwProtection.getMD5("prefisso" + s.ToString() + "suffisso");
            work.saveStreamToFile(System.Windows.Forms.Application.UserAppDataPath + "\\workData.dat", new StringBuilder(encrypt + hash));
            server.Stop();
        }
        /// <summary>
        /// Attiva le dimensioni del pallet che sono state inserite nelle TextBox
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //TODO fixed
            //if (!inibiteChange && !Double.IsNaN(Util.getDoubleFromString(textBox4.Text)))
            //{
            //    work.listPayloadGroupPlaced.Clear();
            //    work.palletOnSystem.setSizeZ((float)Util.getDoubleFromString(textBox4.Text));
            //    LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
            //}
            //else if (!inibiteChange)
            //    textBox4.Text = work.palletOnSystem.getSize().Z.ToString();
            textBox_Changed(sender, e);
        }
        /// <summary>
        /// Implementa il comportamento dello scroll nel centerPanel
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">ScrollEventArgs e</param>
        private void centerPanel_Scroll(object sender, ScrollEventArgs e)
        {
            onMouseMove();
        }
        /// <summary>
        /// Disegna i layer nel panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">PaintEventArgs e</param>
        private void panelWhereDraw_Paint(object sender, PaintEventArgs e)
        {
            Graphics gs = e.Graphics;
            LayerVisualizationManager.refreshVisualization(gs);
        }
        /// <summary>
        /// Implementa la selezione del layer sul pannello a sinistra
        /// </summary>
        /// <param name="layVisLastSelected">Layer da selezionare</param>
        private void selectingLayer(LayerVisualization layVisLastSelected)
        {
            work.setIndexLayerUsed(layVisLastSelected.layer.getLayerNumber());
            panelWhereDraw.Refresh();
        }
        /// <summary>
        /// Implementa il click del mouse sul pannello a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">MouseEventArgs e</param>
        private void panelWhereDraw_MouseDown(object sender, MouseEventArgs e)
        {
            LayerVisualization layVisLastSelected = LayerVisualizationManager.selected(new PointF(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y));
            if (work.getIndexLayerUsed() != layVisLastSelected.layer.getLayerNumber())
            {
                selectingLayer(layVisLastSelected);
                listPayloadGroupSelected.Clear();
            }
        }
        /// <summary>
        /// Refresh della tabella a sinistra dello schermo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1.Tag == null)
            {
                timer1.Tag = "attesa";
            }
            else
            {
                LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
                timer1.Tag = null;
                timer1.Enabled = false;
            }
        }
        /// <summary>
        /// Implementa il funzionamento del button6 (aggiunge un layer vuoto nel pannello a sinistra)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (work.palletOnSystem.canAddALayer(work.payloads[0].getOriginalSize().Z))
            {
                Layer l = work.palletOnSystem.addAnEmptyLayer();
                work.setIndexLayerUsed(l.getLayerNumber());
                LayerVisualizationManager.refreshSchemeList(null, work.palletOnSystem);
                LayerVisualizationManager.deselectAll();
                LayerVisualizationManager.listLayerVisualization[l.getLayerNumber()].selected = true;
                LayerVisualizationManager.refreshVisualization(panelWhereDraw.CreateGraphics());
            }
            else
            {
                MessageBox.Show(Program.translate("string_nonPuoiAggiungereAltriStrati"));
            }
        }
        /// <summary>
        /// Implementa il funzionamento del button13 (cancella il layer selezionato nel pannello a sinistra)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button13_Click(object sender, EventArgs e)
        {
            work.deleteUsedLayer();
            work.setIndexLayerUsed(work.palletOnSystem.getLayerCount());
            LayerVisualizationManager.refreshSchemeList(null, work.palletOnSystem);
            LayerVisualizationManager.deselectAll();
            LayerVisualizationManager.listLayerVisualization[work.palletOnSystem.getLayerCount() - 1].selected = true;
            LayerVisualizationManager.refreshVisualization(panelWhereDraw.CreateGraphics());
        }
        /// <summary>
        /// Implementa il funzionamento del button11 (sposta il layer selezionato in basso di una posizione)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button11_Click(object sender, EventArgs e)
        {
            work.moveDownSelectedLayer();
            LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
        }
        /// <summary>
        /// Implementa il funzionamento del button13 (sposta il layer selezionato in alto di una posizione)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            work.moveUpSelectedLayer();
            LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
        }
        /// <summary>
        /// Implementa la funzione "Cancella" per il click destro su un layer sul panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void cancellaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button13.PerformClick();
        }
        /// <summary>
        /// Implementa la funzione "Sposta giù" per il click destro su un layer sul panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void spostaGiùToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button11.PerformClick();
        }
        /// <summary>
        /// Implementa la funzione "Sposta su" per il click destro su un layer sul panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void spostaSuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button12.PerformClick();
        }
        /// <summary>
        /// Implementa la funzione "Copia strato selezionato" per il click destro su un layer sul panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void stratoSelezionatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!work.copySelectedLayer())
                MessageBox.Show(Program.translate("string_nonPuoiAggiungereAltriStrati"));
            else
                LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
        }
        /// <summary>
        /// Implementa la funzione "Copia i primi due strati" per il click destro su un layer sul panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void primiDueStratiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO fixed
            if (work.palletOnSystem.getLayerCount() < 2)
                MessageBox.Show(Program.translate("string_necessarioAggiungereDueStrati"));
            else if (!work.copyFirstsLayer())
                MessageBox.Show(Program.translate("string_nonPuoiAggiungereAltriStrati"));
            else
                LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
        }
        /// <summary>
        /// Implementa la funzione "Copia tutti gli strati" per il click destro su un layer sul panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void tuttiGliStratiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!work.copyAllLayer())
                MessageBox.Show(Program.translate("string_nonPuoiAggiungereAltriStrati"));
            else
                LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
        }
        /// <summary>
        /// Implementa la funzione "Crea gli strati basandoti sui primi due" per il click destro sul panel a sinistra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (work.palletOnSystem.getLayerCount() < 2)
            {
                MessageBox.Show(Program.translate("string_necessarioAggiungereDueStrati"));
            }
            else
            {
                String res = Microsoft.VisualBasic.Interaction.InputBox(Program.translate("string_numeroDiStrati"));
                if (res != null && Util.getDoubleFromString(res) != Double.NaN && Util.getDoubleFromString(res) > 2)
                {
                    int n = (int)Math.Floor(Util.getDoubleFromString(res));
                    if (n > 100)
                    {
                        n = 100;
                    }
                    if (!work.createPalletFromFirstsTwoLayer(n - 2))
                        MessageBox.Show(Program.translate("string_nonPuoiAggiungereAltriStrati"));
                    else
                        LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
                }
                else
                {
                    MessageBox.Show("Input errato");
                }
            }
        }
        /// <summary>
        /// Implementa il toolstrip per il click destro del mouse sul panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">MouseEventArgs e</param>
        private void panelWhereDraw_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip2.Show(panelWhereDraw, e.X, e.Y);
            }
        }
        //TODO vuoto?
        /// <summary>
        /// Implementa il toolstrip per il click destro del mouse su di un PayloadGroup nell'inPanel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        /// <summary>
        /// Implementa la funzione "Ruota" per il click destro del mouse su un layer del panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void ruotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (work.palletOnSystem.getSizeWithBorder().X == work.palletOnSystem.getSizeWithBorder().Y)
            {
                //TODO fixed
                int defaultRot = 1;
                String res = Microsoft.VisualBasic.Interaction.InputBox(Program.translate("string_numeroDiQuadranti"), Program.translate("string_ruota"), defaultRot.ToString());
                if (!String.IsNullOrEmpty(res) && Util.getDoubleFromString(res) != Double.NaN && Util.getDoubleFromString(res) > 0)
                {
                    int n = (int)Math.Floor(Util.getDoubleFromString(res));
                    if (n > 3)
                    {
                        n = 3;
                    }
                    work.rotateSelectedLayer(n);
                    LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
                }
                else if (String.IsNullOrEmpty(res))
                {
                    work.rotateSelectedLayer(defaultRot);
                    LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
                }
                else
                {
                    MessageBox.Show(Program.translate("string_inputErrato"));
                }
            }
            else
            {
                work.rotateSelectedLayer(2);
            }
            work.calculateOrderOnLayer();
            LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
            resetPreview();
        }
        /// <summary>
        /// Implementa la funzione "Capovolgi orizzontalmente" per il click destro del mouse su un layer  del panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void capovolgiOrizzontalmenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            work.horizontalFlipOfSelectedLayer();
            LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
        }
        /// <summary>
        /// Implementa la funzione "Capovolgi verticalmente" per il click destro del mouse su un layer del panel a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void capovolgiVerticalmenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            work.verticalFlipOfSelectedLayer();
            LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
        }
        //TODO parte dell'autogenerazione
        /// <summary>
        /// Implementa il salvataggio clickando sul panel7 del thumbnail
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">MouseEventArgs</param>
        private void panel7_MouseClick(object sender, MouseEventArgs e)
        {
           
           if (this.Cursor == Cursors.Hand)
            {
                saveFileDialog1.DefaultExt = ".jpg";
                DialogResult ds = saveFileDialog1.ShowDialog();
                if (ds == System.Windows.Forms.DialogResult.OK)
                {
                    String path = saveFileDialog1.FileName;
                    LayerThumbnail.loadThumbnail(Path.GetDirectoryName(path), Path.GetFileName(path), new LayerVisualization(work.palletOnSystem.getLayerAtIndex(0), 0), 1, 1024);
                }
            }
        }
        /// <summary>
        /// Disegna il thumbnail sul panel7
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">PaintEventArgs e</param>
        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            Bitmap b = LayerThumbnail.thumbFrom(new LayerVisualization(work.palletOnSystem.getLayerAtIndex(0), 0), 1, panel7.Width);
            Graphics gs= panel7.CreateGraphics();
            gs.DrawImage(b, new Point(0, 0));
            //System.Drawing.Font f = new System.Drawing.Font("Verdana",12,FontStyle.Underline);
            //String s = "Esporta immagine..";
            //gs.DrawString(s, f, new SolidBrush(Color.Blue), panel7.Width - gs.MeasureString(s, f).Width-3, panel7.Height - gs.MeasureString(s, f).Height-3);
        }
        /// <summary>
        /// ResizeBegin del panel7, setta la larghezza allo stesso valore dell'altezza
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            panel7.Width = panel7.Height;
        }
        /// <summary>
        /// Cambia il cursore del mouse quando si esce dal panel7
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void panel7_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// Cambia il cursore del mouse quando si entra nel panel7
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">MouseEventArgs e</param>
        private void panel7_MouseMove(object sender, MouseEventArgs e)
        {
            
            this.Cursor = Cursors.Hand;
            /*
            if (e.Y > panel7.Width * 0.85)
            {
                
                //this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                Type type = null;
                foreach (Type t in typeOnCombo)
                    if (t.Name.ToString() == comboBox1.SelectedItem.ToString())
                        type = t;
                connectorUsing = (VirtualConnector)Activator.CreateInstance(type);
                connectorUsing.init(work.ToString(), Program.languagesPath + " \\" + Program.currentLanguage() + ".lng");
                txtConfig.Text = connectorUsing.getIstruction(work.ToString());
                Panel tmpP = connectorUsing.getSettingsPanel(work.ToString());
                this.tableLayoutPanel9.Controls.Clear();
                this.tableLayoutPanel9.Controls.Add(tmpP, 0, 0);
                tmpP.Dock = System.Windows.Forms.DockStyle.Fill;
                tmpP.Location = new System.Drawing.Point(3, 3);
                buttonConnettoreRefreshAnteprima.PerformClick();
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (MainForm.instance != null)
            {
                //txtConfig.Width = panel8.Width - (653 - 644);
                //button14.Left = panel8.Width - (653 - 574);
                //button15.Left = panel8.Width - (653 - 498);
                pictureBox1.Left = MainForm.instance.Width - (939 - 813);
                pictureBox2.Left = MainForm.instance.Width - (939 - 866);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (connectorUsing!=null)
                connectorUsing.send(work.ToString(2));
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (connectorUsing!=null)
                txtPreview.Text = connectorUsing.getPreview(work.ToString(2));
        }

        private void picBoxPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog openFileDialog1 = new SaveFileDialog();
            openFileDialog1.Filter = "Pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String filename = openFileDialog1.FileName;

                PdfDocument document = new PdfDocument();
                String payloadsName = work.payloads[0].getPayloadStrategy().getName();
                for (int i = 0; i < work.payloads.Count; i++)
                    payloadsName = payloadsName + "; " + work.payloads[i].getPayloadStrategy().getName();
                document.Info.Title = Program.translate("string_esportazioneGraficaDelloSchema") + " " + payloadsName;

                // Create an empty page
                PdfPage page = document.AddPage();

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
                // Create a font
                XFont font = new XFont("Verdana", 14, XFontStyle.Bold);
                // Draw the text
                int pdfpos = 0;
                gfx.DrawString(Program.translate("string_esportazioneGraficaDelloSchema") + " " + payloadsName, font, XBrushes.Black,
                  new XRect(0, pdfpos, page.Width, 100),
                  XStringFormats.Center);
                pdfpos += 70;

                //gfx.DrawString("Strato base del pallet", font, XBrushes.Black, new XRect(0, pdfpos, page.Width, 100), XStringFormats.Center);
                //pdfpos += 80;

                //XImage image = XImage.FromGdiPlusImage(new LayerVisualization(work.palletOnSystem.getLayerAtIndex(0), 0).thumb.getBitmap());

                //TODO pdfdraw


                //page = document.AddPage();
                //gfx = XGraphics.FromPdfPage(page);
                font = new XFont("Verdana", 10, XFontStyle.Regular);

                //pdfpos = 15;
                int l = work.palletOnSystem.getLayerCount();
                Point2F psize = new Point2F(work.palletOnSystem.size.toSize());
                int maxH = (int)work.palletOnSystem.getMaxHeight();
                gfx.DrawString(Program.translate("string_dimensioneDelPallet") + ": " + Program.translate("string_larghezza") + ": " + psize.toSize().Width + "; " + Program.translate("string_lunghezza") + ": " + psize.toSize().Height + "; " + Program.translate("string_altezzaMassima") + ": " + maxH, font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                pdfpos += 20;

                if (pdfpos > page.Height - 20)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    pdfpos = 15;
                }

                Size esize = work.extraPallet.ToSize();
                gfx.DrawString(Program.translate("string_dimensioneExtraDelPallet") + ": " + Program.translate("string_larghezza") + ": " + esize.Width + "; " + Program.translate("string_lunghezza") + ": " + esize.Height, font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                pdfpos += 20;

                if (pdfpos > page.Height - 20)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    pdfpos = 15;
                }


                gfx.DrawString(Program.translate("string_numeroDiStrati") + ": " + l, font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                pdfpos += 20;

                if (pdfpos > page.Height - 20)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    pdfpos = 15;
                }


                if (work.palletOnSystem.interLayer)
                {
                    gfx.DrawString(Program.translate("string_interfaldaSopraIlPalletPresenteConSpessore") + ": " + work.palletOnSystem.interLayerThickness, font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                }
                else
                {
                    gfx.DrawString(Program.translate("string_interfaldaSopraIlPalletAssente"), font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                }
                pdfpos += 20;

                if (pdfpos > page.Height - 20)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    pdfpos = 15;
                }


                if (work.palletOnSystem.panelOverPallet)
                {
                    gfx.DrawString(Program.translate("string_pannelloSopraIlPalletPresenteConSpessore") + ": " + work.palletOnSystem.panelOverPalletThickness, font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                }
                else
                {
                    gfx.DrawString(Program.translate("string_pannelloSopraIlPalletAssente"), font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                }
                pdfpos += 20;

                if (pdfpos > page.Height - 20)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    pdfpos = 15;
                }


                int pHeight = 0;
                List<Layer> listl = work.palletOnSystem.getLayerList();
                foreach (Layer ll in listl)
                {
                    pHeight = pHeight + (int)ll.size.Z;
                }
                gfx.DrawString(Program.translate("string_altezzaAttualeDelPallet") + ": " + pHeight, font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                pdfpos += 20;

                Point3F paySize = new Point3F(int.Parse(txtBoxX.Text.ToString()), int.Parse(txtBoxY.Text.ToString()), int.Parse(txtBoxZ.Text.ToString()));
                gfx.DrawString(Program.translate("string_dimensioneDeiPayload") + ": " + Program.translate("string_larghezza") + ": " + paySize.X + "; " + Program.translate("string_lunghezza") + ": " + paySize.Y + "; " + Program.translate("string_altezza") + ": " + paySize.Z, font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                pdfpos += 30;

                int width = (int)inPanel.ClientSize.Width;
                int height = (int)inPanel.ClientSize.Height;
                resetSizeAtOne();

                double ratio = width / work.palletOnSystem.getSizeWithBorder().X;
                for (int strat = 0; strat < work.palletOnSystem.getLayerCount(); strat++)
                {
                    font = new XFont("Verdana", 10, XFontStyle.Regular);
                    Bitmap bmpnew = new Bitmap(width, height);
                    Graphics gfb = Graphics.FromImage(bmpnew);
                    gfb.FillRectangle(new SolidBrush(Program.colorOfPallet), new Rectangle((int)(((double)width - (work.palletOnSystem.getSize().X * ratio)) / 2.0), (int)(((double)height - (work.palletOnSystem.getSize().Y * ratio)) / 2.0), (int)(work.palletOnSystem.getSize().X * ratio), (int)(work.palletOnSystem.getSize().Y * ratio)));

                    float[] dashValues = { 6, 3, 6 };
                    Pen blackPen = new Pen(Color.Black, 2);
                    blackPen.DashPattern = dashValues;

                    gfb.DrawRectangle(blackPen, new Rectangle(0, 0, width, height));

                    work.setIndexLayerUsed(strat);
                    foreach (PayloadGroup gg in work.listPayloadGroupPlaced)
                    {
                        List<Figure> ff = gg.getListFigureToRender(new PlacingState_placed(), new Point2F(0, height * 2), false);
                        foreach (Figure f in ff)
                        {
                            f.renderMeThere(gfb);
                        }
                    }

                    //bmpnew.Save(@"C:\Users\sintec\Desktop\prov.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    XImage bmpimg = XImage.FromGdiPlusImage(bmpnew);

                    //bmp.Save("C:\\Users\\kanon\\Desktop\\prov.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    //XImage image = XImage.FromGdiPlusImage(bmp);
                    //gfx.DrawImage(image, new XRect(page.Width / 4, pdfpos, page.Width / 2, page.Height / 2));

                    double resizedWidth = page.Width * 0.8;
                    double resizedHeight = resizedWidth * height / width;
                    if (resizedHeight > page.Height * 0.3)
                    {
                        resizedHeight = page.Height * 0.3;
                        resizedWidth = resizedHeight * width / height;
                    }

                    if (pdfpos + resizedHeight > page.Height - 20)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        pdfpos = 15;
                    }

                    gfx.DrawImage(bmpimg, new XRect(20, pdfpos, resizedWidth, resizedHeight));
                    int pdfPosAfterImage = pdfpos + (int)resizedHeight + 20;
                    int leftOfDescr = 30 + (int)resizedWidth;

                    int lnum = strat;
                    gfx.DrawString(Program.translate("string_layer") + " " + (lnum+1).ToString("0"), font, XBrushes.Black, new XRect(leftOfDescr, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                    pdfpos += 20;

                    int paynum = work.listPayloadGroupPlaced.Count;
                    gfx.DrawString(paynum + " " + Program.translate("string_payload"), font, XBrushes.Black, new XRect(leftOfDescr, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                    pdfpos += 20;
                    if (work.getLayerUsed().interLayer)
                    {
                        gfx.DrawString(Program.translate("string_interfaldaSpessore") + " " + work.getLayerUsed().interLayerThickness, font, XBrushes.Black, new XRect(leftOfDescr, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                    }
                    else
                    {
                        gfx.DrawString(Program.translate("string_interfaldaAssente"), font, XBrushes.Black, new XRect(leftOfDescr, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                    }

                    pdfpos = pdfPosAfterImage;
                }

                if (pdfpos > page.Height - 20)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    pdfpos = 15;
                }

                /*
                gfx.DrawString("Informazioni sui layer", font, XBrushes.Blue, new XRect(0, pdfpos, page.Width, 40), XStringFormats.TopCenter);
                pdfpos += 20;

                foreach (Layer ll in listl)
                {
                    if (pdfpos > page.Height - 20)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        pdfpos = 15;
                    }
                    int lnum = ll.getLayerNumber();
                    gfx.DrawString("Layer " + lnum, font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                    pdfpos += 20;
                    if (pdfpos > page.Height - 20)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        pdfpos = 15;
                    }
                    int paynum = ll.listPayloadGroupPlaced.Count;
                    gfx.DrawString("Payload contenuti nel layer: " + paynum, font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                    pdfpos += 20;
                    if (pdfpos > page.Height - 20)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        pdfpos = 15;
                    }
                    if (ll.interLayer)
                    {
                        gfx.DrawString("Interfalda presente con spessore " + ll.interLayerThickness, font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                    }
                    else
                    {
                        gfx.DrawString("Interfalda assente", font, XBrushes.Black, new XRect(20, pdfpos, page.Width, 40), XStringFormats.TopLeft);
                    }
                    pdfpos += 20;
                    if (pdfpos > page.Height - 20)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        pdfpos = 15;
                    }


                    if (ll != listl.Last()) {
                        XPen pen = new XPen(new XColor(), 0.3);
                        pen.Color = Color.Black;
                        gfx.DrawLine(pen, new Point(10, pdfpos), new Point((int)page.Width -10, pdfpos));
                        pdfpos += 15;
                        //if (pdfpos > page.Height - 20)
                        //{
                        //    page = document.AddPage();
                        //    gfx = XGraphics.FromPdfPage(page);
                        //    pdfpos = 15;
                        //}
                    }
                }
                 * */
                // Save the document...
                document.Save(filename);
                if (MessageBox.Show(Program.translate("string_esportazioneCompletata"), Program.translate("string_esportazionePdf"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(filename);
                }
            }
        }

        private void picBoxJpg_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = ".jpg";
            DialogResult ds = saveFileDialog1.ShowDialog();
            if (ds == System.Windows.Forms.DialogResult.OK)
            {
                int dim = 1000;// (int)Util.getDoubleFromString(Microsoft.VisualBasic.Interaction.InputBox("Inserisci la dimensione dell'immagine (lato intero)"));
                String path = saveFileDialog1.FileName;
                LayerThumbnail.loadThumbnail(Path.GetDirectoryName(path), Path.GetFileName(path), new LayerVisualization(work.palletOnSystem.getLayerAtIndex(0), 0), 1.0f, dim);
                if (MessageBox.Show(Program.translate("string_esportazioneCompletata"), Program.translate("string_esportazioneJpg"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(path);
                }
            }
        }
        //TODO fine parte autogenerazione
        /// <summary>
        /// Implementa la funzione "Ricopia" del click destro su di un layer nel pannello a sinistra
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void ricopiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (listPayloadGroupSelected.Count > 0)
            {
                BoundsF2D boundsOfSelection = listPayloadGroupSelected[0].getBounds();
                foreach (PayloadGroup pg in listPayloadGroupSelected)
                {
                    boundsOfSelection.union(pg.getBounds());
                }

                if (MainForm.instance.work.palletOnSystem.getBundleOfFirstLayerWithBorder().contains(boundsOfSelection))
                {
                    List<PayloadGroup> newSelected = new List<PayloadGroup>();

                    //copia i selezionati nel layer
                    foreach (PayloadGroup pg in listPayloadGroupSelected)
                    {
                        PayloadGroup pgClone = pg.clone();
                        newSelected.Add(pgClone);
                        MainForm.instance.work.listPayloadGroupPlaced.Add(pgClone);
                    }

                    //seleziona i copiati
                    listPayloadGroupSelected = newSelected;


                    //sposta i selezionati
                    boundsOfSelection.location = new Point2F(boundsOfSelection.size.X + boundsOfSelection.location.X, boundsOfSelection.location.Y);
                    if (MainForm.instance.work.palletOnSystem.getBundleOfFirstLayerWithBorder().contains(boundsOfSelection))
                    {
                        //se ci stanno a destra
                        foreach (PayloadGroup pg in listPayloadGroupSelected)
                        {
                            pg.move(new Point2F(boundsOfSelection.size.X, 0));
                        }
                    }
                    else
                    {   //sullo 0,0 (in basso a sinistra)
                        foreach (PayloadGroup pg in listPayloadGroupSelected)
                        {
                            pg.move(new Point2F(boundsOfSelection.size.X - boundsOfSelection.location.X, -boundsOfSelection.location.Y));
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Attiva le modifiche alle scatole in base al valore della TextBox
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //TODO fixed
            textBox_Changed(sender, e);
            //textBox3_TextChanged(sender, e);
        }
        /// <summary>
        /// Attiva le dimensioni del pallet che sono state inserite nella TexBox
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            //TODO fixed
            if (!inibiteChange && !Double.IsNaN(Util.getDoubleFromString(txtPalletMaxHeight.Text)))
            {
                work.listPayloadGroupPlaced.Clear();
                work.deleteAllLayers();
                work.palletOnSystem.setMaxHeight((float)Util.getDoubleFromString(txtPalletMaxHeight.Text));
                LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);
            }
            else if (!inibiteChange)
                txtPalletMaxHeight.Text = work.palletOnSystem.getMaxHeight().ToString();
        }
        
        /// <summary>
        /// Implementa la funzione "Seleziona tutto" del click destro sull'inPanel
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void selezionaTuttoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (PayloadGroup pg in work.listPayloadGroupPlaced)
            {
                if (pg != null && !MainForm.instance.listPayloadGroupSelected.Contains(pg))
                        listPayloadGroupSelected.Add(pg);
            }
            bankDepotPanel.Refresh();
            lastPointClickedForMenu = null;
            onMouseMove();
        }


        /// <summary>
        /// Apre il toolForm
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void btnTool_Click(object sender, EventArgs e)
        {
            (tf = new toolForm(SolutionFacade.Instance.angleChoosen, Program.xWeightPower)).Show();
        }
        //TODO cosa fa?
        public void refreshWeigth()
        {
            onMouseMove();
        }

        DateTime start;
        BoolBox finishing = new BoolBox(false);

        // bool generateLayout(int L_pallet, int W_pallet, int l_box, int w_box, char* cbcDate, char* id, char* hashId, int* vecSol, int* dimension, char** out_checkSum)
        [DllImport("lApproachDll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool generateLayout(int L, int W, int l, int w, StringBuilder cbcDate, StringBuilder id, StringBuilder hashId, int[] sol, ref int dimension, [MarshalAs(UnmanagedType.LPStr)] out String checkSum);

        // bool decodeWorkData(char* cipherTextIn, char** out_stringChar, char* id, char* hashId, char** out_checkSum)
        [DllImport("lApproachDll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool decodeWorkData(StringBuilder inCharString, [MarshalAs(UnmanagedType.LPStr)] out String out_stringChar, StringBuilder cbcDate, StringBuilder id, StringBuilder hashId, [MarshalAs(UnmanagedType.LPStr)] out String checkSum);

        // bool decodeWorkData(char* cipherTextIn, char** out_stringChar, char* id, char* hashId, char** out_checkSum)
        [DllImport("lApproachDll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern bool checkIdPcLicence(StringBuilder cbcDate, StringBuilder id, StringBuilder hashId);

        //[DllImport("lApproachDll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        //public static extern bool funcMD5(StringBuilder inCharString, [MarshalAs(UnmanagedType.LPStr)] out String out_stringChar);


        /// <summary>
        /// Attiva la ricerca delle soluzioni in modo automatico
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button1_Click(object sender, EventArgs e)
        {
            bool ok = false;
            start = DateTime.Now;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            label9.Text = "Inizio: " + String.Format("{0:T}", start);
            String key = (String)reg.GetValue("key", "LicenceIdPc");
            if (!String.IsNullOrEmpty(key) && (HwProtection.getMD5(key.Split('-')[0] + key.Split('-')[1]) == key.Split('-')[2]))
            {
                double width_Pallet = work.palletOnSystem.getSizeWithBorder().X;// double.Parse(textBox6.Text.ToString()) + double.Parse(textBox7.Text.ToString()) / 2;
                double height_Pallet = work.palletOnSystem.getSizeWithBorder().Y;// double.Parse(textBox5.Text.ToString()) + double.Parse(textBox8.Text.ToString())/2;

                double width_box = work.payloads[0].getOriginalSize().X;// double.Parse(textBox1.Text.ToString());
                double height_box = work.payloads[0].getOriginalSize().Y;//.palletOnSystem.getSizeWithBorder().X;// double.Parse(textBox2.Text.ToString());

                if (width_box < 50 || height_box < 50)
                {
                    MessageBox.Show(Program.translate("string_dimensioniScatola"));
                }
                else
                {
                    PalletOnSystem pos = work.palletOnSystem;
                    Layer l = new Layer(pos);
                    // se una misura della scatola è maggiore di una misura del pallet allora disponio le scatole nella direzione del lato lungo fino a riempire
                    if (Math.Max(width_box, height_box) > Math.Min(width_Pallet, height_Pallet))
                    {
                        checkLicenceIdPcFromRegistry();

                        double widthFilled = 0;
                        double heightFilled = 0;

                        //rotazione prevista per il payload
                        bool rotationForPayload = false;
                        if (width_box > width_Pallet || height_box > height_Pallet)
                            rotationForPayload = true;

                        while (heightFilled + (rotationForPayload ? width_box : height_box) <= height_Pallet)
                        {
                            widthFilled = 0;
                            Point2F center = new Point2F(widthFilled + ((rotationForPayload ? height_box : width_box) / 2), heightFilled + ((rotationForPayload ? width_box : height_box) / 2));
                            while (widthFilled + (rotationForPayload ? height_box : width_box) <= width_Pallet)
                            {
                                PayloadGroup pg = new PayloadGroup();
                                PayloadPlaced pp = new PayloadPlaced(MainForm.instance.work.payloads[0]);

                                int quadrant = rotationForPayload ? 1 : 0;
                                
                                pp.recentering(center);
                                pp.quadrant = quadrant;

                                pg.setPointCatching(new Point3FR(0, 0, 0, 0));

                                pg.addToPayloadPlacedList(pp);

                                ReGen.Model.Sequencer.addPayloadGroupAtFixedRankList(pg.getId(), 0);

                                l.listPayloadGroupPlaced.Add(pg);

                                widthFilled += (rotationForPayload ? height_box : width_box);
                                center = new Point2F(widthFilled + ((rotationForPayload ? height_box : width_box) / 2), heightFilled + ((rotationForPayload ? width_box : height_box) / 2));
                            }
                            heightFilled += (rotationForPayload ? width_box : height_box);
                        }
                    }
                    else
                    {
                        int estimateDimension = (int)Math.Truncate((width_Pallet * height_Pallet) / (width_box * height_box));

                        int[] solution = new int[4 * estimateDimension];

                        int rightDimension = 0;
                        //listBox1.Items.Clear();
                        //PalletOnSystem pos = new PalletOnSystem(new Point3FR(0.0F, 0.0F, 0.0F, 0.0F), 1, new Point3F(width_Pallet, height_Pallet, 145.0), new Point2F(0.0F, 0.0F), 1000.0F, SolutionFacade.Instance.findGoodPayloadGroupToAddStrategy);


                        bool flagL = false;

                        if (height_Pallet > width_Pallet)
                            flagL = true;


                        String hwCode = key.Split('-')[0];
                        String dateCode = key.Split('-')[1];
                        String checkSum;

                        if (flagL)
                            ok = generateLayout((int)height_Pallet, (int)width_Pallet, (int)width_box, (int)height_box, new StringBuilder(dateCode), new StringBuilder(HwProtection.diskCodeMD5()), new StringBuilder(hwCode), solution, ref rightDimension, out checkSum);
                        else
                            ok = generateLayout((int)width_Pallet, (int)height_Pallet, (int)width_box, (int)height_box, new StringBuilder(dateCode), new StringBuilder(HwProtection.diskCodeMD5()), new StringBuilder(hwCode), solution, ref rightDimension, out checkSum);

                        String checkPreCalculated = HwProtection.getMD5("prefisso" + dateCode + hwCode + rightDimension.ToString() + "suffisso");

                        if ((checkPreCalculated == checkSum))
                        {
                            for (int i = 0; i < (rightDimension / 4); i++)
                            {
                                PayloadGroup pg = new PayloadGroup();
                                PayloadPlaced pp = new PayloadPlaced(MainForm.instance.work.payloads[0]);

                                int quadrant;
                                Point2F center = new Point2F();

                                if (!flagL)
                                {
                                    center.X = solution[i];
                                    center.Y = solution[(int)(rightDimension / 4) + i];
                                    if ((solution[(rightDimension / 2) + i] - solution[i]) == ((int)width_box))
                                        quadrant = 0;
                                    else
                                        quadrant = 1;

                                    center.X = center.X + (int)((solution[(rightDimension / 2) + i] - solution[i]) / 2) - (work.palletOnSystem.getExtraBorder().X / 2);
                                    center.Y = center.Y + (int)((solution[(3 * (rightDimension / 4)) + i] - solution[(rightDimension / 4) + i]) / 2) - (work.palletOnSystem.getExtraBorder().Y / 2);
                                    //Point2F centerMeters = pointPixelsToMeters(center);
                                    pp.recentering(center);
                                    pp.quadrant = quadrant;
                                }
                                else
                                {
                                    center.X = solution[(int)(rightDimension / 4) + i];
                                    center.Y = solution[i];
                                    if ((solution[(3 * (rightDimension / 4)) + i] - solution[(rightDimension / 4) + i]) == ((int)(width_box)))
                                        quadrant = 0;
                                    else
                                        quadrant = 1;

                                    center.X = center.X + (int)((solution[(3 * (rightDimension / 4)) + i] - solution[(rightDimension / 4) + i]) / 2) - (work.palletOnSystem.getExtraBorder().X / 2);
                                    center.Y = center.Y + (int)((solution[(rightDimension / 2) + i] - solution[i]) / 2) - (work.palletOnSystem.getExtraBorder().Y / 2);

                                    pp.recentering(center);
                                    pp.quadrant = quadrant;
                                }

                                pg.setPointCatching(new Point3FR(0, 0, 0, 0));

                                pg.addToPayloadPlacedList(pp);

                                ReGen.Model.Sequencer.addPayloadGroupAtFixedRankList(pg.getId(), 0);

                                l.listPayloadGroupPlaced.Add(pg);
                            }
                        }
                        else
                        {
                            checkLicenceIdPcFromRegistry();
                        }
                    }

                    if (l.listPayloadGroupPlaced.Count > 0)
                    {
                        Region bound = l.listPayloadGroupPlaced[0].regionOfMe();
                        foreach (PayloadGroup pp in l.listPayloadGroupPlaced)
                        {
                            bound.Union(pp.regionOfMe());
                        }
                        float offsetY = ((work.sizeWorkArea.Y - bound.GetBounds(Program.g1).Height - work.extraPallet.Height) / 2) - bound.GetBounds(Program.g1).Top;
                        float offsetX = ((work.sizeWorkArea.X - bound.GetBounds(Program.g1).Width - work.extraPallet.Width) / 2) - bound.GetBounds(Program.g1).Left;
                        foreach (PayloadGroup pp in l.listPayloadGroupPlaced)
                        {
                            pp.move(new Point2F(offsetX, offsetY));
                        }
                    }

                    Cursor.Current = Cursors.Default;
                    Application.DoEvents();

                    DateTime end = DateTime.Now;
                    label10.Text = Program.translate("string_tempoImpiegato") +  ": " + String.Format("{0:T}", (end - start));

                    //PayloadPlaced pp = new PayloadPlaced(work.box.getPayloadStrategy());


                    //listBox1.Items.Add(l);//getLayerClone());//(100 - cl.getLayerClone().occupation()).ToString("000.#") + " - " + cl.getLayerClone().ToString());
                    //listBox1.SelectedIndex = listBox1.Items.Count - 1;

                    panePreview.Tag = l;
                    double ratio = 1;
                    if (paneContainerPreview.Width / work.palletOnSystem.getSizeWithBorder().X > paneContainerPreview.Height / work.palletOnSystem.getSizeWithBorder().Y)
                        ratio = paneContainerPreview.Height / work.palletOnSystem.getSizeWithBorder().Y;
                    else
                        ratio = paneContainerPreview.Width / work.palletOnSystem.getSizeWithBorder().X;
                    panePreview.Size = new Size((int)((double)work.palletOnSystem.getSizeWithBorder().X * ratio), (int)((double)work.palletOnSystem.getSizeWithBorder().Y * ratio));
                    panePreview.Location = new Point((paneContainerPreview.Width - panePreview.Width) / 2, (paneContainerPreview.Height - panePreview.Height) / 2);

                    panePreview.Refresh();
                }
            }

            if (!ok)
                checkLicenceIdPcFromRegistry();
        }
        /// <summary>
        /// Disegna il layer selezionato dalla listbox sul panel 10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panePreview_Paint(object sender, PaintEventArgs e)
        {
            sizeAtOne = new System.Drawing.Point(panePreview.ClientSize.Width, panePreview.ClientSize.Height);  //(int)((panel10.ClientSize.Height) * MainForm.instance.work.sizeWorkArea.X / MainForm.instance.work.sizeWorkArea.Y)
            Graphics gs = panePreview.CreateGraphics();

            if (panePreview.Tag != null && ((Layer)panePreview.Tag).listPayloadGroupPlaced.Count > 0)
            {
                Layer l = ((Layer)panePreview.Tag);
                gs.Clear(Color.FromKnownColor(KnownColor.ControlDark));
                System.Drawing.Rectangle workAreaDraw = new System.Drawing.Rectangle(1, 1, pointMetersToPixels(work.sizeWorkArea).X - 2, pointMetersToPixels(new Point2F(0, 0)).Y - 2);
                gs.FillRectangle(new SolidBrush(Color.White), workAreaDraw);

                System.Drawing.Rectangle palletDraw = new System.Drawing.Rectangle(pointMetersToPixels(new Point2F((int)(work.extraPallet.Width / 2), 0)).X,
                    pointMetersToPixels(new Point2F(0, work.sizeWorkArea.Y - (int)(work.extraPallet.Height / 2))).Y,
                    pointMetersToPixels(new Point2F(work.sizeWorkArea.X - (int)(work.extraPallet.Width), 0)).X - 2,
                    pointMetersToPixels(new Point2F(0, (int)(work.extraPallet.Height))).Y - 2);
                float[] dashValues = { 6, 3, 6 };
                Pen blackPen = new Pen(Color.Black, 2);
                blackPen.DashPattern = dashValues;
                gs.FillRectangle(new SolidBrush(Program.colorOfPallet), palletDraw);
                gs.DrawRectangle(blackPen, palletDraw);

                SortedListWithDuplicatedKey<Figure> sortedListFigure = new SortedListWithDuplicatedKey<Figure>();
                List<Figure> listFigureOnPlatform = new List<Figure>();
                foreach (PayloadGroup pg in l.listPayloadGroupPlaced)
                    foreach (Figure fg in (pg.getListFigureToRender(new PlacingState_placed(), new Point2F(0, 0), false)))
                        sortedListFigure.Add(fg.getPriority(), fg);

                foreach (Figure f in sortedListFigure.Values)
                    f.renderMeThere(gs);
                listFigureOnPlatform.Clear();
            }
            else
            {
                gs.Clear(System.Drawing.SystemColors.ControlDark);
            }
        }

        /// <summary>
        /// Implementa la funzione del button19 (etichetta della scatola a nord)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button19_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }

        /// <summary>
        /// Implementa la funzione del button17 (etichetta della scatola a est)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button17_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Implementa la funzione del button16 (etichetta della scatola a sud)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button16_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Implementa la funzione del button18 (etichetta della scatola a ovest)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Implementa la funzione del button23 (etichetta visibile al lato nord del pallet)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button23_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Implementa la funzione del button 21 (etichetta visibile al lato est del pallet)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button21_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Implementa la funzione del button20 (etichetta visibile al lato sud del pallet)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button20_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }
        /// <summary>
        /// Implementa la funzione del button22 (etichetta visibile al lato ovest del pallet)
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        private void button22_Click(object sender, EventArgs e)
        {
            changeValueAtButton(sender);
        }


        private void resetButtonViewForAngle(double angle)
        {

        }

        private void buttonAngleRanker_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            Application.DoEvents();
            buttonDepositoSE.Tag = 315.0;
            //button25.Tag = false;
            buttonDepositoSW.Tag = 225.0;
            //button27.Tag = false;
            //button28.Tag = false;
            buttonDepositoNE.Tag = 45.0;
            //button30.Tag = false;
            buttonDepositoNW.Tag = 135.0;
            changeValueAtButton(sender);
            work.calculateOrderOnLayer();
            LayerVisualizationManager.refreshSchemeList(panelWhereDraw, work.palletOnSystem);

            this.UseWaitCursor = false;
            Application.DoEvents();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (panePreview.Tag != null)
            {
                if (work.palletOnSystem.canAddALayer(work.payloads[0].getOriginalSize().Z))
                {
                    Layer l = new Layer(work.palletOnSystem);
                    bool flag = false;
                    for (int i = 0; i < work.palletOnSystem.getLayerCount(); i++)
                    {
                        if (work.palletOnSystem.getLayerAtIndex(i).listPayloadGroupPlaced.Count() == 0)
                        {
                            l = work.palletOnSystem.getLayerAtIndex(i);
                           flag = true;
                           break;
                        }
                    }
                    


                    //Layer pl = (Layer)listBox1.SelectedItem;
                    if (!flag)
                        l = work.palletOnSystem.addAnEmptyLayer();

                    //l.listPayloadGroupPlaced = ((Layer)listBox1.SelectedItem).listPayloadGroupPlaced;
                    //work.palletOnSystem.addLayer(l);

                    foreach (PayloadGroup pg in ((Layer)panePreview.Tag).listPayloadGroupPlaced)
                    {
                        PayloadGroup newPg = new PayloadGroup();
                        PayloadGroup.cloneFromTo(newPg, pg);
                        l.listPayloadGroupPlaced.Add(newPg);
                    }
                    listPayloadGroupSelected.Clear();

                    work.setIndexLayerUsed(l.getLayerNumber());
                    work.calculateOrderOnLayer();
                    LayerVisualizationManager.refreshSchemeList(null, work.palletOnSystem);
                    LayerVisualizationManager.deselectAll();
                    LayerVisualizationManager.listLayerVisualization[l.getLayerNumber()].selected = true;
                    LayerVisualizationManager.refreshVisualization(panelWhereDraw.CreateGraphics());
                    tabControl1.SelectedTab = tabControl1.TabPages[1];
                }
                else
                {
                    MessageBox.Show(Program.translate("string_nonPuoiAggiungereAltriStrati"));
                }
            }    
            else
                MessageBox.Show(Program.translate("string_selezionareUnLayer"));
        
        }

        private void button33_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            button16.Enabled = button17.Enabled = button18.Enabled = button19.Enabled = label26.Visible =
                button16.Visible = button17.Visible = button18.Visible = button19.Visible = 
                checkBox4.Checked;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            int i = work.getIndexLayerUsed();
            Graphics gs = panelWhereDraw.CreateGraphics();
            if (work.palletOnSystem.getLayerAtIndex(i).interLayer)
            {
                button33.Tag = "OCCUPATO";
            }
            else
            {
                button33.Tag = "LIBERO";
            }


            if (button33.Tag.ToString() == "LIBERO")
            {
                work.palletOnSystem.getLayerAtIndex(i).interLayer = true;
                LayerVisualizationManager.refreshVisualization(gs);
            }
            else
            {
                work.palletOnSystem.getLayerAtIndex(i).interLayer = false;
                LayerVisualizationManager.refreshVisualization(gs);
            }
        }

        private void salvaSchemaDiPallettizzazioneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
        }

        private void riguardoRegenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutBoxSintec()).ShowDialog();
        }

        private void apriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm of = new OpenForm();
            of.chosenRecipeHandler += new OpenForm.ChosenRecipe(of_chosenRecipeHandler);
            of.ShowDialog();
            //openFileDialog1.ShowDialog();
        }

        private void of_chosenRecipeHandler(object sender, ChosenRecipeEventArgs a)
        {
            if (MessageBox.Show(Program.translate("string_loSchemaSostituira"), Program.translate("string_apriSchema"), MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                loadFile(a.Path);
                this.tabControl1.SelectedTab = tabPageGenerazione;
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show(Program.translate("string_loSchemaSostituira"), Program.translate("string_apriSchema"), MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                loadFile(openFileDialog1.FileName);
            }
        }

        private void saveFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool goOver = false;
            bool deleteFile = false;
            if (!File.Exists(saveFileDialog2.FileName))
                goOver = true;
            if (File.Exists(saveFileDialog2.FileName) && MessageBox.Show(Program.translate("string_siVuoleSovrascrivere") + " " + saveFileDialog2.FileName + "?", Program.translate("string_confermaScritturaFile"), MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                deleteFile = goOver = true;

            if (goOver)
            {
                StringBuilder s = new StringBuilder();
                work.getXmlStream(s);
                String encrypt = Sintec.Tool.HwProtection.Encrypt(s.ToString());
                String hash = Sintec.Tool.HwProtection.hashCodeWithSalt(s.ToString());
                if (deleteFile)
                    File.Delete(saveFileDialog2.FileName);
                work.saveStreamToFile(saveFileDialog2.FileName, new StringBuilder(encrypt + hash));
            }
        }

        private void licenzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenceForm l = new LicenceForm();
            l.ShowDialog(this);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            checkLicenceIdPcFromRegistry();
        }

        private void cmbPayloadsNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enabledSecondPayload = (int.Parse(cmbPayloadsNumber.Text) > 1);
            foreach(Control c in panel9.Controls)
                c.Enabled = enabledSecondPayload;
        }

        bool bypass = false;
        private void txtApproachWidth_TextChanged(object sender, EventArgs e)
        {
            if (!bypass)
            {
                double resX;
                double resY;
                double resZ;
                bool parseOk = double.TryParse(txtApproachX.Text, out resX);
                parseOk = double.TryParse(txtApproachY.Text, out resY) && parseOk;
                parseOk = double.TryParse(txtApproachZ.Text, out resZ) && parseOk;
                if (parseOk)
                {
                    Approaches.approachFixed.X = (float)resX;
                    Approaches.approachFixed.Y = (float)resY;
                    Approaches.approachFixed.Z = (float)resZ;
                    work.calculateOrderOnLayer();
                }
                else
                {
                    bypass = true;
                    txtApproachX.Text = Approaches.approachFixed.X.ToString("0");
                    txtApproachY.Text = Approaches.approachFixed.Y.ToString("0");
                    txtApproachZ.Text = Approaches.approachFixed.Z.ToString("0");
                    bypass = false;
                }
            }
        }

        private void textBoxesPayload2_Changed(object sender, EventArgs e)
        {
            textBox_Changed(sender, e);
        }

        private void txtCustomPick_TextChanged(object sender, EventArgs e)
        {
            work.customPickData = txtCustomPick.Text;
        }

        private void txtCustomPlacement_TextChanged(object sender, EventArgs e)
        {
            work.customPlaceData = txtCustomPlacement.Text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.sintecingegneria.it");
        }

        Point startDeltaPositionLogoMobile = new Point(int.MinValue, int.MinValue);
        private void logoPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            startDeltaPositionLogoMobile = new Point(panelLogoMobile.Location.X - Cursor.Position.X, panelLogoMobile.Location.Y - Cursor.Position.Y);
        }

        private void logoPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (startDeltaPositionLogoMobile.X > int.MinValue)
            {
                //panelLogoMobile.Location = new Point(startDeltaPositionLogoMobile.X + e.Location.X, startDeltaPositionLogoMobile.Y + e.Location.Y);
                //panelLogoMobile.Location = new Point(panelLogoMobile.Location.X + Cursor.Position.X + startDeltaPositionLogoMobile.X, panelLogoMobile.Location.Y + Cursor.Position.Y + startDeltaPositionLogoMobile.Y);
                panelLogoMobile.Location = new Point(Math.Min(Screen.PrimaryScreen.WorkingArea.Width,Math.Max(0,Cursor.Position.X + startDeltaPositionLogoMobile.X)), Math.Min(Screen.PrimaryScreen.WorkingArea.Height,Math.Max(0,Cursor.Position.Y + startDeltaPositionLogoMobile.Y)));
            }
        }

        private void logoPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            startDeltaPositionLogoMobile = new Point(int.MinValue, int.MinValue);
        }

        private void impostaProgressivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listPayloadGroupSelected.Count==1)
            {
                int oldProg = (int)Sequencer.getProgressive(listPayloadGroupSelected[0].getId(), work.getLayerUsed());
                String oldProgString = Microsoft.VisualBasic.Interaction.InputBox(Program.translate("string_impostaIlProgressivo"), "", oldProg.ToString(""));
                bool err = false;
                if (int.TryParse(oldProgString, out oldProg))
                {
                    if (oldProg > 0 && oldProg <= work.listPayloadGroupPlaced.Count)
                        Sequencer.fixPositionAt(work.getLayerUsed(), listPayloadGroupSelected[0], oldProg);
                    else
                        err = true;
                }
                else
                    err = true;
                if (err)
                {
                    MessageBox.Show(Program.translate("string_inserisciUnValoreTra")+ " " + work.listPayloadGroupPlaced.Count);
                }
            }
        }
    }
}




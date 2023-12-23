using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CSGL12;

namespace ThreeDConnectorSpace
{
    public partial class InterfacciaImpostazioni : UserControl
    {
        public ThreeDConnector mCSGL12ExampleHandler;
        private System.Windows.Forms.Timer mTimer;
        public InterfacciaImpostazioni(String workDataXmlString)
        {
            InitializeComponent();
            mCSGL12ExampleHandler = new ThreeDConnector();
            mCSGL12ExampleHandler.workDataXmlString = workDataXmlString;
            csgL12Control1.OpenGLStarted += new CSGL12Control.DelegateOpenGLStarted(mCSGL12ExampleHandler.OpenGLStarted);
            csgL12Control1.KeyDown += new KeyEventHandler(mCSGL12ExampleHandler.KeyDown);
            csgL12Control1.KeyUp += new KeyEventHandler(mCSGL12ExampleHandler.KeyUp);
            csgL12Control1.MouseDown += new MouseEventHandler(mCSGL12ExampleHandler.MouseDown);
            csgL12Control1.MouseUp += new MouseEventHandler(mCSGL12ExampleHandler.MouseUp);
            csgL12Control1.MouseMove += new MouseEventHandler(mCSGL12ExampleHandler.MouseMove);
            csgL12Control1.MouseWheel += new MouseEventHandler(mCSGL12ExampleHandler.MouseWheel);
            csgL12Control1.Paint += new PaintEventHandler(mCSGL12ExampleHandler.Paint);
            
            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 10; // 10-millisecond interval
            mTimer.Tick += new EventHandler(PrivateTimerTickEventHandler);
            mTimer.Start();

            // Set focus to a control so that it can immediately accept input
            csgL12Control1.Focus();

            // Also, whenever the form becomes activated, set focus to the main
            // control on the form.  The following sets up an event handler for
            // that purpose.
            this.Load += new EventHandler(PrivateActivatedEventHandler);

            // We want to preview dialog keys (most importantly, the cursor
            // keys: up, down, right, left) so we can forward such events to
            // the appropriate child control.

            //this.KeyPreview = true;
            this.button1.BackgroundImage = this.imageList1.Images[0];
        }

        private void InterfacciaImpostazioni_Load(object sender, EventArgs e)
        {
        }

        public Panel getPanel()
        {
            return this.panel1;
        }
        

        void PrivateTimerTickEventHandler(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                csgL12Control1.Invalidate();
            }
        }
    
        private void PrivateActivatedEventHandler(object sender, EventArgs e)
        {
            // When this form becomes activated, after some time of not
            // being active, set input focus to a CSGL12Control on the form.

            if 
            (
                (false == csgL12Control1.Focused)
            )
            {
                csgL12Control1.Focus();
            }
        }

        // Cursor keys (up,down,left,right) need to be specially captured
        // and forwarded to the control.
        // CAUTION: The KeyPreview property of this Form must be set to 'true' 
        // for the following method to be called.

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if
                (
                   (keyData == Keys.Up)
                || (keyData == Keys.Down)
                || (keyData == Keys.Left)
                || (keyData == Keys.Right)
                )
            {
                KeyEventArgs e = new KeyEventArgs(keyData);

                if (true == csgL12Control1.Focused)
                {
                    mCSGL12ExampleHandler.KeyDown(csgL12Control1, e);
                }
                else
                {
                    // Neither of the two child CSGL12Controls has focus.
                    // Let's simply drop the dialog key event.  The user
                    // may have focus on a different control.
                }

                return (true);
            }
            return base.ProcessDialogKey(keyData);
        }

        private void csgL12Control1_DoubleClick(object sender, EventArgs e)
        {
            mCSGL12ExampleHandler.startSimulation(trackBar1.Value);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            mCSGL12ExampleHandler.velocity = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mCSGL12ExampleHandler.startSimulation(trackBar1.Value);
        }
    }
}

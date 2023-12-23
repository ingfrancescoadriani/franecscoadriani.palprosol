



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CSGL12;








namespace CSGL12Example1
{




    public partial class CSGL12Example1Form : Form
    {




        public CSGL12Example1Handler        mCSGL12Example1Handler;
        private System.Windows.Forms.Timer  mTimer;








        public CSGL12Example1Form()
        {
            InitializeComponent();



            mCSGL12Example1Handler = new CSGL12Example1Handler();
            mCSGL12Control1.OpenGLStarted += new CSGL12Control.DelegateOpenGLStarted( mCSGL12Example1Handler.OpenGLStarted );
            mCSGL12Control1.KeyDown += new KeyEventHandler(mCSGL12Example1Handler.KeyDown);
            mCSGL12Control1.KeyUp += new KeyEventHandler(mCSGL12Example1Handler.KeyUp);
            mCSGL12Control1.MouseDown += new MouseEventHandler(mCSGL12Example1Handler.MouseDown);
            mCSGL12Control1.MouseUp += new MouseEventHandler(mCSGL12Example1Handler.MouseUp);
            mCSGL12Control1.MouseMove += new MouseEventHandler(mCSGL12Example1Handler.MouseMove);
            mCSGL12Control1.MouseWheel += new MouseEventHandler(mCSGL12Example1Handler.MouseWheel);
            mCSGL12Control1.Paint += new PaintEventHandler(mCSGL12Example1Handler.Paint);




            // Use a timer to trigger drawing at the desired frame rate.
            //
            // Windows timers are not very precise.  Also, if we call wglSwapIntervalEXT(1)
            // and we specify in the global OpenGL control panel that OpenGL drawing
            // should wait for vertical sync (vsync) of the display, then the frame
            // rate would be limited to 60 frames/second or 75 frames/second, for example,
            // and our program would have to draw each frame in less than 1/60 seconds
            // (16.6 milliseconds) or less than 1/75 seconds (13.3 milliseconds), 
            // otherwise the drawn frame would be forced to wait one or more full
            // frame durations before appearing on the screen.  Therefore, it would
            // be best to have the timer interval somewhat shorter than a full frame
            // interval, to ensure that even if there is a slight delay in responding
            // to the timer event we will have at least one timer event per display
            // frame interval.
            //
            // Theoretically, a timer interval of 16 milliseconds would be short enough
            // to sustain a frame rate of 62.5 frames/second, and would seemingly have
            // a corresponding rate sufficient to sustain 60 frames/second in the case
            // of a vertical-sync limited drawing rate for OpenGL.  However, in simple
            // experiments on a system with a 2.5 GHz Core 2 Duo CPU with an nVidia
            // GeForce 8600M GS with 512 MB, I found that a 16-millisecond Windows
            // timer interval results in a 33 frames/second OpenGL frame rate (where
            // vertical-sync locking is enabled, and the display refresh rate is 
            // 60 frames/second).  So, despite theoretically being slightly more rapid
            // than necessary to maintain a 60 frames/second drawing rate, a Windows
            // timer with an interval of 16-milliseconds isn't quite rapid enough to
            // ensure drawing soon enough to be ready for each display refresh; hence
            // the rather significantly lower than desired frame rate of 33 frames/second.
            //
            // Here are the OpenGL drawing frame rates (limited to 60 Hz vertical sync)
            // I observed on a particular computer for particular Windows timer intervals:
            //
            //    18-millisecond timer interval   -->   31 frames/second OpenGL drawing
            //    17-millisecond timer interval   -->   31 frames/second OpenGL drawing
            //    16-millisecond timer interval   -->   33 frames/second OpenGL drawing
            //    15-millisecond timer interval   -->   60 frames/second OpenGL drawing
            //    14-millisecond timer interval   -->   60 frames/second OpenGL drawing
            //
            // So, it seems like choosing a Windows timer interval only a couple of 
            // milliseconds shorter than the theoretical 16.6 millisecond interval
            // corresponding to a 60 frames/second rate is enough for this program to
            // submit each new frame in time for the next display refresh.
            //
            // However, computers with slower CPUs or slower GPUs might benefit from 
            // an even shorter Windows timer interval, to ensure that drawing will 
            // happen soon enough for the next display refresh.
            //
            // Some displays are set to refresh at 75 frames/second, which corresponds
            // to a frame duration of 13.3 milliseconds.  We will aim for this drawing
            // rate, and we will subtract a few milliseconds from the Windows timer 
            // interval to ensure that we receive and process the timer event soon
            // enough to submit the frame in time for the next display refresh.
            // Meanwhile, we will choose the Windows timer interval such that it isn't
            // absurdly short, lest future faster computers actually manage to draw
            // at that wasteful rate.
            //
            // Choosing a Windows timer interval of 10 milliseconds seems like it will
            // reliably be able to trigger OpenGL frame drawing in time to keep up with
            // a 75 frames/second display (13.3 millisecond frame duration), while 
            // only causing the OpenGL drawing to happen at a maximum rate of 
            // 100 frames/second in the unlikely scenario of a very fast computer 
            // actually being able to draw frames at that rate (given the overhead of
            // C#, etc).

            mTimer = new System.Windows.Forms.Timer();
            mTimer.Interval = 10; // 10-millisecond interval
            mTimer.Tick += new EventHandler(PrivateTimerTickEventHandler);
            mTimer.Start();




            // Set focus to a control so that it can immediately accept input

            mCSGL12Control1.Focus();




            // Also, whenever the form becomes activated, set focus to the main
            // control on the form.  The following sets up an event handler for
            // that purpose.

            this.Activated += new EventHandler(PrivateActivatedEventHandler);




            // We want to preview dialog keys (most importantly, the cursor
            // keys: up, down, right, left) so we can forward such events to
            // the appropriate child control.

            this.KeyPreview = true;
        }








        void PrivateTimerTickEventHandler(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                mCSGL12Control1.Invalidate();
            }
        }








        private void PrivateActivatedEventHandler(object sender, EventArgs e)
        {
            // When this form becomes activated, after some time of not
            // being active, set input focus to a GL control on the form.
            if (false == mCSGL12Control1.Focused)
            {
                mCSGL12Control1.Focus();
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

                if (true == mCSGL12Control1.Focused)
                {
                    mCSGL12Example1Handler.KeyDown(mCSGL12Control1, e);
                }
                else
                {
                    // The CSGL12Control does not have focus.
                    // Let's simply drop the dialog key event.  The user
                    // may have focus on a different control.
                }


                return (true);
            }

            return base.ProcessDialogKey(keyData);
        }




    }




}









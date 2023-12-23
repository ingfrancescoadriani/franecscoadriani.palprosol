



// All versions of the CSGL (C# OpenGL) software are created by Colin P. Fahey
// ( http://colinfahey.com ) and are declared to be in the public domain.
//
// Therefore, all versions of the CSGL (C# OpenGL) software can be used for any
// purpose (commercial or private), without payment, without restrictions, and
// without obligations.  The code can be modified, or portions reused, without
// restrictions, and without obligations.  This note may be removed from the
// code, and there is no need to mention the original author of this code.
//
// This file is part of CSGL 12 (2009 July 25).




using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;








namespace CSGL12
{




    public partial class CSGL12Control : System.Windows.Forms.Control
    {




        // Delegate definitions

        public delegate void DelegateOpenGLStarted(CSGL12Control sender);




        // Internal properties of this control.

        public event DelegateOpenGLStarted OpenGLStarted = null;
        private IntPtr mHDC = IntPtr.Zero;
        private bool mAttemptedInitialization = false;
        private int mTotalFramesDrawn = 0;
        private PrecisionTime mPrecisionTime = null;
        private double mTotalElapsedTimeSeconds = 0.0;
        private double mPreviousFrameStartTimeSeconds = 0.0;
        private double mPreviousFrameDurationSeconds = 0.0;
        private GL mGL = null;
        private IntPtr mHGLRC = IntPtr.Zero;
        private Font mFontForDesignerModeText = null;

        
        
        
        
        
        
        
        public CSGL12Control()
        {
            OpenGLStarted = null;
            mHDC = IntPtr.Zero;
            mAttemptedInitialization = false;
            mTotalFramesDrawn = 0;
            mPrecisionTime = new PrecisionTime();
            mTotalElapsedTimeSeconds = 0.0;
            mPreviousFrameStartTimeSeconds = 0.0;
            mPreviousFrameDurationSeconds = 0.0;
            mGL = new GL();
            mHGLRC = IntPtr.Zero;
            mFontForDesignerModeText = new Font("Arial", 18.0f, FontStyle.Regular); 
        }








        public IntPtr GetHDC()
        {
            return (mHDC);
        }








        public IntPtr GetHGLRC()
        {
            return (mHGLRC);
        }








        public GL GetGL()
        {
            return (mGL);
        }








        public int GetTotalFramesDrawn()
        {
            return (mTotalFramesDrawn);
        }








        public double GetTotalElapsedTimeSeconds()
        {
            return (mTotalElapsedTimeSeconds);
        }








        public double GetPreviousFrameStartTimeSeconds()
        {
            return (mPreviousFrameStartTimeSeconds);
        }








        public double GetPreviousFrameDurationSeconds()
        {
            return (mPreviousFrameDurationSeconds);
        }








        public double GetTotalTimeSecondsNotLockedToFrameUpdates()
        {
            if (null != mPrecisionTime)
            {
                double elapsedTimeSeconds = 0.0f;

                elapsedTimeSeconds = 
                    mPrecisionTime.PrecisionTime_GetElapsedTimeSeconds();

                return (elapsedTimeSeconds);
            }

            return (0.0);
        }








        // Multicast event for when OpenGL is first initialized


        protected void InvokeOpenGLStarted()
        {
            if (null != OpenGLStarted)
            {
                OpenGLStarted(this);
            }
        }








        // Methods handled by this control itself; called by the operating system.
        // In many cases, when one of the following methods is called by the 
        // operating system, the method will call a corrsponding method to invoke
        // event handlers set by the client of this control.

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Graphics graphics = e.Graphics;


            // If in designer mode, then just fill the area with the BackColor color.

            if
            (
                (true == this.DesignMode)
                || (false == this.mAttemptedInitialization)
            )
            {
                graphics.Clear(this.BackColor);

                if (true == this.DesignMode)
                {
                    int outlineX = this.ClientRectangle.Left;

                    int outlineY = this.ClientRectangle.Top;

                    int outlineWidth = (this.ClientRectangle.Width - 1);
                    if (outlineWidth < 0) { outlineWidth = 0; }

                    int outlineHeight = (this.ClientRectangle.Height - 1);
                    if (outlineHeight < 0) { outlineHeight = 0; }

                    Rectangle outlineRectangle = new Rectangle( outlineX, outlineY, outlineWidth, outlineHeight );

                    graphics.DrawRectangle(Pens.Black, outlineRectangle);

                    if (null != mFontForDesignerModeText)
                    {
                        String text = "CSGL12Control";
                        
                        SizeF textSize = graphics.MeasureString(text, mFontForDesignerModeText);

                        float textX = (float)outlineX + 0.5f * (float)outlineWidth - 0.5f * textSize.Width;
                        float textY = (float)outlineY + 0.5f * (float)outlineHeight - 0.5f * textSize.Height; 

                        graphics.DrawString
                        (
                            text, 
                            mFontForDesignerModeText, 
                            Brushes.Black,
                            textX,
                            textY,
                            StringFormat.GenericTypographic
                        );

                        text = "" + this.ClientRectangle.Width + " * " + this.ClientRectangle.Height + " px";

                        textSize = graphics.MeasureString(text, mFontForDesignerModeText);

                        textX = (float)outlineX + 0.5f * (float)outlineWidth - 0.5f * textSize.Width;
                        textY += 1.2f * textSize.Height;
                        
                        graphics.DrawString
                        (
                            text,
                            mFontForDesignerModeText,
                            Brushes.Black,
                            textX,
                            textY,
                            StringFormat.GenericTypographic
                        );
                    }

                    return;
                }
            }




            // graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            // graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            // graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;




            // Update the frame count and frame time.

            mTotalFramesDrawn++;
            mTotalElapsedTimeSeconds = mPrecisionTime.PrecisionTime_GetElapsedTimeSeconds();
            mPreviousFrameDurationSeconds =
                (mTotalElapsedTimeSeconds - mPreviousFrameStartTimeSeconds);
            mPreviousFrameStartTimeSeconds = mTotalElapsedTimeSeconds;




            // If we have not already attempted to initialize OpenGL, then do so now.

            if (false == mAttemptedInitialization)
            {
                mAttemptedInitialization = true;

                mPrecisionTime.PrecisionTime_Initialize();

                mTotalElapsedTimeSeconds = 0.0;

                mGL.InitializeOpenGL
                (
                    this.Handle,
                    ref mHDC,
                    ref mHGLRC
                );

                mGL.wglMakeCurrent(mHDC, mHGLRC);

                // Set some default drawing conditions

                mGL.glClearColor
                (
                    (float)BackColor.R / 255.0f,
                    (float)BackColor.G / 255.0f,
                    (float)BackColor.B / 255.0f,
                    1.0f
                );

                InvokeOpenGLStarted();

                base.OnPaint(e); // Triggers Paint *event*; thus, our override gets called before the event

                return;
            }




            // Set some default drawing conditions

            mGL.glClearColor
            (
                (float)BackColor.R / 255.0f,
                (float)BackColor.G / 255.0f,
                (float)BackColor.B / 255.0f,
                1.0f
            );

            mGL.wglMakeCurrent(mHDC, mHGLRC);

            base.OnPaint(e); // Triggers Paint *event*; thus, our override gets called before the event
        }








        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            // Override System.Windows.Forms.Control.OnPaintBackground() method.
            // No GDI painting of background means no flicker in OpenGL rendering.            
        }








        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this.Focus();

            base.OnMouseDown(e); // This will invoke any events connected to this window
        }

    
    
    
    }




}









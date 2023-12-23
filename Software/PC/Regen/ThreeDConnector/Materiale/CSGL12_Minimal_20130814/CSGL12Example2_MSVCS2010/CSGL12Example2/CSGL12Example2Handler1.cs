// Copyright (c) 2013, Colin P. Fahey ( http://colinfahey.com )
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following condition is met:
//
// Redistributions of source code must retain the above copyright notice, 
// this list of conditions, and the following disclaimer:
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;




// This program requires "references" to the following:
//
//         CSGL12DLL.dll       (defines CSGL12.GL)
//         CSGL12Control.dll   (defines CSGL12.CSGL12Control)

using CSGL12;








namespace CSGL12Example2
{




    public class CSGL12Example2Handler1
    {




        private Mesh mMesh;
        private ShaderProgram mShaderProgram1;
        private ShaderProgram mShaderProgram2;
        private ShaderProgram mShaderProgram3;
        private ShaderProgram mShaderProgram4;
        private ShaderProgram mShaderProgramSelected;

        private Bitmap mHUDBitmap;
        private Texture mHUDTexture;
        private Font mFont1;
        private Font mFont2;
        private Font mFont3;
        private Font mFont4;
        private Pen mPen1;
        private Brush mBrush1;

        private double mViewDistance = 800.0;
        private double mViewAzimuthDegrees = 0.0;
        private double mViewAltitudeDegrees = 0.0;

        private double mViewAzimuthDegreesVelocity = 9.0;
        private double mViewAltitudeDegreesVelocity = 5.0;

        private Point mMouseClientPositionStart;
        private double mViewAzimuthDegreesStart = 0.0;
        private double mViewAltitudeDegreesStart = 0.0;
















        public CSGL12Example2Handler1()
        {
            mMesh = new Mesh();

            mShaderProgram1 = new ShaderProgramMandelbrotSet();
            mShaderProgram2 = new ShaderProgramWood();
            mShaderProgram3 = new ShaderProgramBrick();
            mShaderProgram4 = new ShaderProgramCartoon();

            mShaderProgramSelected = mShaderProgram1;

            mHUDBitmap = new Bitmap(512, 512, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //mHUDBitmap = new Bitmap(2048, 1024, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            mHUDTexture = new Texture();

            mFont1 = new Font("Verdana", 36.0f);
            mFont2 = new Font("Verdana", 24.0f);
            mFont3 = new Font("Courier New", 16.0f);
            mFont4 = new Font("Courier New", 10.0f);

            mPen1 = new Pen(Color.Red, 3.0f);

            mBrush1 = new SolidBrush(Color.FromArgb(64, 128, 128, 140));
        }
















        public void OpenGLStarted( CSGL12Control csgl12Control )
        {
            GL gl = csgl12Control.GetGL();

            if (null == gl) { return; }




            // Load shaders and set variables

            if (true == gl.bglCreateProgramObjectARB)
            {
                if (null != mShaderProgram1)
                {
                    mShaderProgram1.ShaderProgramCreateWithMessageBoxForError(gl);
                }
                if (null != mShaderProgram2)
                {
                    mShaderProgram2.ShaderProgramCreateWithMessageBoxForError(gl);
                }
                if (null != mShaderProgram3)
                {
                    mShaderProgram3.ShaderProgramCreateWithMessageBoxForError(gl);
                }
                if (null != mShaderProgram4)
                {
                    mShaderProgram4.ShaderProgramCreateWithMessageBoxForError(gl);
                }
            }




            // Create cube mesh

            mMesh = new Mesh();
            mMesh.BuildCube(400.0f);



            // 2012 FEB 1 : WOW!  Disabling mip-map generation for the HUD texture
            // is a huge speed boost!  Obviously the mip-map generation cost is paid
            // on every texture update, but, wow...
            // Also, disabling the VSYNC blocking gives another speed boost, because
            // it seems like this app (with the extreme situation of uploading a
            // 2048 x 1024 texture every frame) is just barely missing the next frame
            // interval when VSYNC blocking is on...

            // Create a texture

            mHUDTexture.CreateTextureFromBitmap(gl, mHUDBitmap, false); // NOTE: MIP-MAPPING DISABLED FOR TEXURE UPDATE SPEED




            // To prevent "tearing" (irregular streaks) due to swapping buffers at
            // arbitrary times relative to the vsync times, we indicate that we wish 
            // to wait for vsync before swapping buffers.
            // This request applies when the display driver control panel -- in the
            // OpenGL settings area -- is set to let the application decide whether or
            // not to wait for vsync.  Otherwise, the driver control panel overrides
            // any request made here.

            if (true == gl.bwglSwapIntervalEXT)
            {
                // ALLOW TEARING
                gl.wglSwapIntervalEXT(0);
            }
        }
















        public void Paint(object sender, PaintEventArgs e)
        {
            if (null == sender) { return; }
            if (false == (sender is CSGL12Control)) { return; }




            CSGL12Control csgl12Control = (sender as CSGL12Control);
            GL gl = csgl12Control.GetGL();




            int clientWidth = csgl12Control.ClientRectangle.Width;
            int clientHeight = csgl12Control.ClientRectangle.Height;

            if (clientWidth <= 0)
            {
                clientWidth = 1;
            }

            if (clientHeight <= 0)
            {
                clientHeight = 1;
            }




            // Set the viewport 

            gl.glViewport(0, 0, clientWidth, clientHeight);




            // Clear the viewport

            gl.glClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            gl.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);




            // Basic drawing conditions

            gl.glEnable(GL.GL_DEPTH_TEST);
            gl.glDepthFunc(GL.GL_LEQUAL);
            gl.glEnable(GL.GL_CULL_FACE);
            gl.glCullFace(GL.GL_BACK);
            gl.glFrontFace(GL.GL_CCW);





            // PROJECTION matrix, typically for perspective correction or orthographic projection

            gl.glMatrixMode(GL.GL_PROJECTION);
            gl.glLoadIdentity();

            double aspectRatio = 1.0;

            if (0 != clientHeight)
            {
                aspectRatio = ((double)(clientWidth) / (double)(clientHeight));
            }

            double verticalFieldOfViewAngle = 60.0;

            gl.gluPerspective
            (
                verticalFieldOfViewAngle, // Field of view angle (Y angle; degrees)
                aspectRatio, // width/height
                0.1, // distance to near clipping plane
                64000.0 // distance to far clipping plane
            );




            // MODELVIEW matrix, typically used to transform individual models

            gl.glMatrixMode(GL.GL_MODELVIEW);
            gl.glLoadIdentity();

            // Preserve current matrix for the active matrix stack (in this case the MODELVIEW matrix)

            gl.glPushMatrix();




            if (mViewAltitudeDegreesVelocity != 0.0)
            {
                if (mViewAltitudeDegrees > 70.0)
                {
                    mViewAltitudeDegrees = 70.0;
                    mViewAltitudeDegreesVelocity *= -1.0;
                }
                else if (mViewAltitudeDegrees < -70.0)
                {
                    mViewAltitudeDegrees = -70.0;
                    mViewAltitudeDegreesVelocity *= -1.0;
                }

                mViewAzimuthDegrees += mViewAzimuthDegreesVelocity * csgl12Control.GetPreviousFrameDurationSeconds();
                mViewAltitudeDegrees += mViewAltitudeDegreesVelocity * csgl12Control.GetPreviousFrameDurationSeconds();
            }




            Vector3f from =
                new Vector3f
                (
                    (float)(mViewDistance * Math.Cos(mViewAltitudeDegrees * (Math.PI / 180.0)) * Math.Sin(mViewAzimuthDegrees * (Math.PI / 180.0))),
                    (float)(mViewDistance * Math.Sin(mViewAltitudeDegrees * (Math.PI / 180.0))),
                    (float)(mViewDistance * Math.Cos(mViewAltitudeDegrees * (Math.PI / 180.0)) * Math.Cos(mViewAzimuthDegrees * (Math.PI / 180.0)))
                );
            Vector3f to = new Vector3f(0.0f, 0.0f, 0.0f);
            Vector3f up = new Vector3f(0.0f, 1.0f, 0.0f);

            Matrix4x4f camera = Matrix4x4f.LookAt(from, to, up);




            float[] matrix = new float[16];

            matrix[0] = camera.m11;
            matrix[1] = camera.m21;
            matrix[2] = camera.m31;
            matrix[3] = 0.0f;

            matrix[4] = camera.m12;
            matrix[5] = camera.m22;
            matrix[6] = camera.m32;
            matrix[7] = 0.0f;

            matrix[8] = camera.m13;
            matrix[9] = camera.m23;
            matrix[10] = camera.m33;
            matrix[11] = 0.0f;

            matrix[12] = camera.m14;
            matrix[13] = camera.m24;
            matrix[14] = camera.m34;
            matrix[15] = 1.0f;




            gl.glMultMatrixf(matrix);




            if (mShaderProgramSelected != null)
            {
                mShaderProgramSelected.DemonstrateModificationOfVariables(gl, csgl12Control.GetPreviousFrameStartTimeSeconds(), csgl12Control.GetPreviousFrameDurationSeconds());
                mShaderProgramSelected.Select(gl);
            }




            // Draw model(s), using active texture or shader

            mMesh.Draw(gl);




            // If we used a shader, disable it now...

            if (true == gl.bglUseProgramObjectARB)
            {
                ShaderProgram.ShaderProgram_Select(gl, 0);
            }




            // Restore the previously-preserved matrix for the active matrix stack (in this case the MODELVIEW matrix)

            gl.glPopMatrix();




            // Demonstrate drawing text to a GDI+ Bitmap and then copying to
            // an OpenGL texture.

            DemonstrateDrawingTextToAGDIBitmapAndCopyingToAnOpenGLTexture(csgl12Control, gl);




            // Flush all the current rendering and flip the back buffer to the front.

            gl.wglSwapBuffers(csgl12Control.GetHDC());
        }
















        public void DemonstrateDrawingTextToAGDIBitmapAndCopyingToAnOpenGLTexture(CSGL12Control csgl12Control, GL gl)
        {
            bool updateOverlayImage = false;




            // The following code only enables an update of the Bitmap
            // and OpenGL texture every 64 frames, thus avoiding the
            // slowdown of performing updates every single frame.
            // HOWEVER, updating the Bitmap and OpenGL texture can be
            // done EVERY frame with acceptable speed.
            // Updates should be limited to once per frame, but the
            // logic to trigger updates can be based on when the relevant
            // text changes.

            // 2012 April : Update the texture every frame; Will likely be fast for most people...
            if ((csgl12Control.GetTotalFramesDrawn() % 1) == 0)
            {
                updateOverlayImage = true;
            }




            bool showOverlayImage = true;




            if (true == updateOverlayImage)
            {
                using (Graphics g = Graphics.FromImage(mHUDBitmap))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.Clear(Color.FromArgb(0, Color.White));


                    g.FillEllipse(mBrush1, new Rectangle(0, 0, 256, 256));
                    g.FillEllipse(mBrush1, new Rectangle(256, 256, 256, 256));

                    PointF center = new PointF(0.5f * (256.0f + 0.0f), 0.5f * (256.0f + 0.0f));
                    PointF displacement = new PointF();
                    double fraction = csgl12Control.GetTotalElapsedTimeSeconds() * 0.1;
                    displacement.X = 128.0f * (float)Math.Cos(2.0 * Math.PI * fraction);
                    displacement.Y = 128.0f * (float)Math.Sin(2.0 * Math.PI * fraction);

                    g.DrawLine(mPen1, center, new PointF(center.X + displacement.X, center.Y + displacement.Y));

                    PointF carat = new PointF(0.0f, 0.0f);

                    String text = "";

                    text = "C# OpenGL (CSGL)";
                    g.DrawString(text, mFont1, Brushes.Black, carat);
                    carat.Y += mFont1.GetHeight();

                    text = "Здравствуйте";
                    g.DrawString(text, mFont2, Brushes.Black, carat);
                    carat.Y += mFont2.GetHeight();

                    text = "γεια σου";
                    g.DrawString(text, mFont2, Brushes.Black, carat);
                    carat.Y += mFont2.GetHeight();

                    text = "مرحبا";
                    g.DrawString(text, mFont2, Brushes.Black, carat);
                    carat.Y += mFont2.GetHeight();

                    text = "שלום";
                    g.DrawString(text, mFont2, Brushes.Black, carat);
                    carat.Y += mFont2.GetHeight();




                    carat.Y += 64.0f;

                    text = "Shift + 0: Save BMP,PNG,JPG,GIF";
                    g.DrawString(text, mFont4, Brushes.Black, carat);
                    carat.Y += mFont4.GetHeight();

                    text = "1,2,3,4  : Switch shader program";
                    g.DrawString(text, mFont4, Brushes.Black, carat);
                    carat.Y += mFont4.GetHeight();

                    carat.Y += 12.0f;

                    text = "Text : GDI+ on 512*512 Bitmap.";
                    g.DrawString(text, mFont4, Brushes.Black, carat);
                    carat.Y += mFont4.GetHeight();

                    text = "Bitmap copied to OpenGL texture.";
                    g.DrawString(text, mFont4, Brushes.Black, carat);
                    carat.Y += mFont4.GetHeight();

                    text = "Texture update happens every frame.";
                    g.DrawString(text, mFont4, Brushes.Black, carat);
                    carat.Y += mFont4.GetHeight();

                    text = "(Modify code to update less often if too slow.)";
                    g.DrawString(text, this.mFont4, Brushes.Black, carat);
                    carat.Y += this.mFont4.GetHeight();

                    carat.Y += 12.0f;

                    text = String.Format("Frame:{0}", csgl12Control.GetTotalFramesDrawn());
                    text += " ";
                    text += String.Format("Time:{0:f2}", csgl12Control.GetTotalElapsedTimeSeconds());

                    double previousFrameDurationSeconds = 
                        csgl12Control.GetPreviousFrameDurationSeconds();

                    if (previousFrameDurationSeconds > 1.0e-10)
                    {
                        double framesPerSecondOverall =
                            1.0 / previousFrameDurationSeconds;

                        text += " ";
                        text += String.Format("FPS:{0:f2}", framesPerSecondOverall );
                    }

                    g.DrawString(text, mFont3, Brushes.Black, carat);

                    carat.Y += mFont3.GetHeight();

                    text = "GL_VERSION : " + gl.glGetString( GL.GL_VERSION );
                    g.DrawString(text, mFont3, Brushes.Black, carat);
                    carat.Y += mFont3.GetHeight();

                    text = "GL_VENDOR : " + gl.glGetString(GL.GL_VENDOR);
                    g.DrawString(text, mFont3, Brushes.Black, carat);
                    carat.Y += mFont3.GetHeight();

                    text = "GL_EXTENSIONS : " + gl.glGetString(GL.GL_EXTENSIONS);
                    g.DrawString(text, mFont3, Brushes.Black, carat);
                    carat.Y += mFont3.GetHeight();
                }

                mHUDTexture.UpdateTextureWithBitmapData(gl, mHUDBitmap);
            }




            if (true == showOverlayImage)
            {
                CSGL12Support.SupportDrawTextureImageUnrotatedAndOrthographically
                (
                    gl,
                    csgl12Control.ClientSize.Width,
                    csgl12Control.ClientSize.Height,
                    mHUDTexture,
                    0,
                    0, // i.e., 0 == draw TOP of image at TOP of viewport, Y-axis points DOWN
                    mHUDTexture.GetWidth(),  // glControl.ClientSize.Width, // mHUDTexture.GetWidth(),
                    mHUDTexture.GetHeight() // glControl.ClientSize.Height // mHUDTexture.GetHeight()
                );
            }
        }
















        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (null == sender) { return; }
            if (false == (sender is CSGL12Control)) { return; }




            CSGL12Control csgl12Control = (sender as CSGL12Control);
            GL gl = csgl12Control.GetGL();




            if (e.KeyCode == Keys.A)
            {
            }

            if (e.KeyCode == Keys.Z)
            {
            }




            if (e.KeyCode == Keys.D1)
            {
                mShaderProgramSelected = mShaderProgram1;
            }
            if (e.KeyCode == Keys.D2)
            {
                mShaderProgramSelected = mShaderProgram2;
            }
            if (e.KeyCode == Keys.D3)
            {
                mShaderProgramSelected = mShaderProgram3;
            }
            if (e.KeyCode == Keys.D4)
            {
                mShaderProgramSelected = mShaderProgram4;
            }




            // NOTE: The only way for cursor key events (up,down,left,right)
            // to make it to this function is for the main form to implement 
            // the following:
            //
            //   protected override bool ProcessDialogKey ( Keys keyData )
            //
            // and explicitly invoke this KeyDown() method with the 
            // an appropriately formed KeyEventArgs instance.

            if (e.KeyCode == Keys.Up)
            {
                mViewDistance -= 10.0;
            }
            if (e.KeyCode == Keys.Down)
            {
                mViewDistance += 10.0;
            }
            if (e.KeyCode == Keys.Left)
            {
                mViewAzimuthDegrees += 1.0;
            }
            if (e.KeyCode == Keys.Right)
            {
                mViewAzimuthDegrees -= 1.0;
            }




            // Save an image of the viewport (press Shift-0 (zero)).  The following
            // code writes out the viewport in the following image formats: BMP, PNG, GIF, JPG.

            // If you only want a single format, comment out the other file write commands.
            // BMP has no compression artifacts, but the file can be quite large.
            // PNG looks good, and supports 8-bit transparancy (good for textures, etc).
            // GIF looks bad unless you build the color table intelligently (there is a 
            //    neural network color table builder for GIF, in C#/.NET, that you can
            //    find on the Internet; perhaps Paint.NET uses that code); but GIF files
            //    can be quite small, and supports animation.
            // JPG looks good under most circumstances, and the file size can be quite small,
            //    but transparency is not supported.
            // So, for pixel-perfect images, where file size is not important, BMP might be appropriate.
            // For textures with transparency, PNG might be appropriate.
            // For good-looking images, and small file size, and use in Web pages, JPG might be appropriate.
            // For some purposes, with small file sizes, and use in Web pages, GIF might be appropriate.

            if ((e.KeyCode == Keys.D0) && (e.Shift == true))
            {
                DateTime now = DateTime.Now;

                String dateTimeString = String.Format("{0:d4}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}{6:d3}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);

                String frameIndexString = String.Format("{0:d6}", csgl12Control.GetTotalFramesDrawn());

                String fileNameWithoutExtension = "screen" + "_" + dateTimeString + "_" + frameIndexString;

                CSGL12Support.SupportWriteViewportToImageFile(gl, fileNameWithoutExtension + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                CSGL12Support.SupportWriteViewportToImageFile(gl, fileNameWithoutExtension + ".png", System.Drawing.Imaging.ImageFormat.Png);
                CSGL12Support.SupportWriteViewportToImageFile(gl, fileNameWithoutExtension + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
                CSGL12Support.SupportWriteViewportToImageFile(gl, fileNameWithoutExtension + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
















        public void KeyUp(object sender, KeyEventArgs e)
        {
        }
















        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (null == sender) { return; }
            if (false == (sender is CSGL12Control)) { return; }




            CSGL12Control csgl12Control = (sender as CSGL12Control);




            mMouseClientPositionStart = csgl12Control.PointToClient(Cursor.Position);


            mViewAzimuthDegreesStart = mViewAzimuthDegrees;
            mViewAltitudeDegreesStart = mViewAltitudeDegrees;


            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mViewAzimuthDegreesVelocity = 0.0;
                mViewAltitudeDegreesVelocity = 0.0;
            }

            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                mViewAzimuthDegreesVelocity = 9.0;
                mViewAltitudeDegreesVelocity = 5.0;
            }
        }
















        public void MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
            }
        }
















        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (null == sender) { return; }
            if (false == (sender is CSGL12Control)) { return; }




            CSGL12Control csgl12Control = (sender as CSGL12Control);




            Point mouseClientPositionCurrent = csgl12Control.PointToClient(Cursor.Position);




            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                double azimuth =
                    mViewAzimuthDegreesStart
                    - (360.0 / (double)(csgl12Control.Width + 1))
                    * (double)(mouseClientPositionCurrent.X - mMouseClientPositionStart.X);

                double altitude =
                    mViewAltitudeDegreesStart
                    + (180.0 / (double)(csgl12Control.Height + 1))
                    * (double)(mouseClientPositionCurrent.Y - mMouseClientPositionStart.Y);

                double epsilon = 0.05;

                if (azimuth < (-180 + epsilon)) { azimuth = (-180 + epsilon); }
                if (azimuth > (180 - epsilon)) { azimuth = (180 - epsilon); }

                if (altitude < (-90 + epsilon)) { altitude = (-90 + epsilon); }
                if (altitude > (90 - epsilon)) { altitude = (90 - epsilon); }

                mViewAzimuthDegrees = azimuth;
                mViewAltitudeDegrees = altitude;
            }
        }
















        public void MouseWheel(object sender, MouseEventArgs e)
        {
            mViewDistance -= 0.1 * (double)e.Delta;
        }




    }




}








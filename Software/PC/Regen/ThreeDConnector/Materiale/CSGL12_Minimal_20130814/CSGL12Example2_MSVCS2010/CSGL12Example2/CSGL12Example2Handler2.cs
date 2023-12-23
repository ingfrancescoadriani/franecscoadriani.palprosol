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




    public class CSGL12Example2Handler2
    {




        private int mLargeFontOpenGLDisplayListBase;
        private int mSmallFontOpenGLDisplayListBase;
        private Mesh mMesh;
        private Texture mTexture;
        private Vector3f mCameraPosition;
        private Bitmap mHUDBitmap;
        private Texture mHUDTexture;












        public CSGL12Example2Handler2()
        {
            mLargeFontOpenGLDisplayListBase = 0;
            mSmallFontOpenGLDisplayListBase = 0;
            mMesh = new Mesh();
            mTexture = new Texture();
            mCameraPosition.Set(0.0f, 0.0f, 800.0f);
            mHUDBitmap = new Bitmap(1024, 128, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            mHUDTexture = new Texture();
        }















        public void OpenGLStarted( CSGL12Control csgl12Control )
        {
            GL gl = csgl12Control.GetGL();

            if (null == gl) { return; }




            CSGL12Support.SupportCreateBitmapFont
            (
                gl,
                csgl12Control.GetHDC(),
                "Verdana",
                14,
                ref this.mLargeFontOpenGLDisplayListBase
            );

            CSGL12Support.SupportCreateBitmapFont
            (
                gl,
                csgl12Control.GetHDC(),
                "Verdana",
                10,
                ref this.mSmallFontOpenGLDisplayListBase
            );




            // Create a cube mesh.

            mMesh = new Mesh();
            mMesh.BuildCube(400.0f);




            String filePathAndName = @"image.jpg";

            if (true == File.Exists(filePathAndName))
            {
                mTexture.LoadTextureFromFile(gl, filePathAndName, true);
            }
            else
            {
                mTexture.CreateCheckerboardTexture(gl, 256, 256);
            }




            mHUDTexture.CreateTextureFromBitmap(gl, mHUDBitmap, true);




            // To prevent "tearing" (irregular streaks) due to swapping buffers at
            // arbitrary times relative to the vsync times, we indicate that we wish 
            // to wait for vsync before swapping buffers.
            // This request applies when the display driver control panel -- in the
            // OpenGL settings area -- is set to let the application decide whether or
            // not to wait for vsync.  Otherwise, the driver control panel overrides
            // any request made here.

            if (true == gl.bwglSwapIntervalEXT)
            {
                // DISABLE BLOCKING ON VSYNC FOR GREATER SPEED (BUT WITH TEARING)
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

            // gl.glClearColor(1.0f, 1.0f, 1.0f, 1.0f);
            gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
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

            gl.glTranslatef
            (
                (-(mCameraPosition.x)),
                (-(mCameraPosition.y)),
                (-(mCameraPosition.z))
            );




            float phase = (float)(30.0 * csgl12Control.GetTotalElapsedTimeSeconds());

            gl.glRotatef((0.11f * phase), 1.0f, 0.0f, 0.0f);
            gl.glRotatef((0.31f * phase), 0.0f, 1.0f, 0.0f);
            gl.glRotatef((0.19f * phase), 0.0f, 0.0f, 1.0f);




            // Set active texture

            if (null != mTexture)
            {
                mTexture.SetAsActiveTexture(gl);
            }




            // Draw model(s), using active texture or shader

            mMesh.Draw(gl);




            // Set active texture to nothing

            gl.glBindTexture(GL.GL_TEXTURE_2D, 0);




            // Restore the previously-preserved matrix for the active matrix stack (in this case the MODELVIEW matrix)

            gl.glPopMatrix();




            // Draw text 

            String text = "";

            text = "frame: ";
            text += String.Format("{0}", csgl12Control.GetTotalFramesDrawn());
            text += "  time: ";
            text += String.Format("{0:f3}", csgl12Control.GetTotalElapsedTimeSeconds());


            double previousFrameDurationSeconds =
                csgl12Control.GetPreviousFrameDurationSeconds();

            if (previousFrameDurationSeconds > 1.0e-10)
            {
                double framesPerSecondOverall =
                    1.0 / previousFrameDurationSeconds;

                text += "   ";
                text += String.Format("FPS:{0:f2}", framesPerSecondOverall);
            }


            gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);

            CSGL12Support.SupportDrawTextUsingPolygonFont
            (
                gl,
                mSmallFontOpenGLDisplayListBase,
                csgl12Control.ClientSize.Width,
                csgl12Control.ClientSize.Height,
                16, // fontHeight
                4, // textX
                4, // textY
                text
            );


            text = "C# OpenGL (CSGL) 12";

            CSGL12Support.SupportDrawTextUsingPolygonFont
            (
                gl,
                mLargeFontOpenGLDisplayListBase,
                csgl12Control.ClientSize.Width,
                csgl12Control.ClientSize.Height,
                20, // fontHeight
                4, // textX
                (csgl12Control.ClientSize.Height - 20), // textY
                text
            );


            // text = "colinfahey.com";

            // CSGL12Support.SupportDrawTextUsingPolygonFont
            // (
            //     gl,
            //     mLargeFontOpenGLDisplayListBase,
            //     csgl12Control.ClientSize.Width,
            //     csgl12Control.ClientSize.Height,
            //     20, // fontHeight
            //     4, // textX
            //     (csgl12Control.ClientSize.Height - 20 - 20), // textY
            //     text
            // );





            // IF YOU WANT TO EXPERIMENT WITH DYNAMIC UPDATING OF TEXTURES WITH GDI+ BITMAPS:

#if false
        {
            text = "frame: ";
            text += String.Format("{0}", csgl12Control.GetTotalFramesDrawn());
            text += "  time: ";
            text += String.Format("{0:f3}", csgl12Control.GetTotalElapsedTimeSeconds());

            using (Graphics g = Graphics.FromImage(mHUDBitmap))
            {
                g.Clear(Color.FromArgb(0, Color.White));
                g.DrawString(text, new Font("Verdana", 50.0f), Brushes.Black, new PointF(0.0f, 0.0f));
            }

            mHUDTexture.UpdateTextureWithBitmapData(gl, mHUDBitmap);

            CSGL12Support.SupportDrawTextureImageUnrotatedAndOrthographically
            (
                gl,
                csgl12Control.ClientSize.Width,
                csgl12Control.ClientSize.Height,
                mHUDTexture,
                0,
                0, // i.e., 0 == draw TOP of image at TOP of viewport, Y-axis points DOWN
                mHUDTexture.GetWidth(),  // csgl12Control.ClientSize.Width, // mHUDTexture.GetWidth(),
                mHUDTexture.GetHeight() // csgl12Control.ClientSize.Height // mHUDTexture.GetHeight()
            );
        }
#endif




            // Flush all the current rendering and flip the back buffer to the front.

            gl.wglSwapBuffers(csgl12Control.GetHDC());
        }
















        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (null == sender) { return; }
            if (false == (sender is CSGL12Control)) { return; }




            CSGL12Control csgl12Control = (sender as CSGL12Control);
            GL gl = csgl12Control.GetGL();




            if (e.KeyCode == Keys.A)
            {
                mCameraPosition.y += 10.0f;
            }

            if (e.KeyCode == Keys.Z)
            {
                mCameraPosition.y -= 10.0f;
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
                mCameraPosition.z -= 10.0f;
            }
            if (e.KeyCode == Keys.Down)
            {
                mCameraPosition.z += 10.0f;
            }
            if (e.KeyCode == Keys.Left)
            {
                mCameraPosition.x -= 10.0f;
            }
            if (e.KeyCode == Keys.Right)
            {
                mCameraPosition.x += 10.0f;
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
















        private Point mMouseClientPositionStart;
        private Vector3f mCameraPositionStart;
















        public void MouseDown(object sender, MouseEventArgs e)
        {
            if (null == sender) { return; }
            if (false == (sender is CSGL12Control)) { return; }




            CSGL12Control csgl12Control = (sender as CSGL12Control);




            mMouseClientPositionStart = csgl12Control.PointToClient(Cursor.Position);
            mCameraPositionStart = mCameraPosition;




            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
            }

            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                mCameraPosition.z += 100.0f;
            }
        }
















        public void MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                mCameraPosition.z -= 100.0f;
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
                mCameraPosition.x = mCameraPositionStart.x - 0.5f * (float)(mouseClientPositionCurrent.X - mMouseClientPositionStart.X);
                mCameraPosition.y = mCameraPositionStart.y + 0.5f * (float)(mouseClientPositionCurrent.Y - mMouseClientPositionStart.Y);
            }
        }
















        public void MouseWheel(object sender, MouseEventArgs e)
        {
            mCameraPosition.z -= 0.25f * (float)e.Delta;
        }




    }




}








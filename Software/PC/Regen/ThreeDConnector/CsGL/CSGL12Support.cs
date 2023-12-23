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

using CSGL12;








namespace CSGL12
{




    public class CSGL12Support
    {




        // The following arrays are for demonstrating the use of the OpenGL functions
        // gl.glVertexPointer(), gl.glColorPointer(), and gl.glDrawElements(), via
        // the local function DrawModelViaVertexArrays().


        public
        static
        float[]
        mCubeXYZVertexArray =
        {
            // 8 Vertices; 24 Total Floats
            -100.0f, -100.0f, -100.0f,  // Vertex 0
            -100.0f, -100.0f,  100.0f,  // Vertex 1
            -100.0f,  100.0f, -100.0f,  // Vertex 2
            -100.0f,  100.0f,  100.0f,  // Vertex 3
             100.0f, -100.0f, -100.0f,  // Vertex 4
             100.0f, -100.0f,  100.0f,  // Vertex 5
             100.0f,  100.0f, -100.0f,  // Vertex 6
             100.0f,  100.0f,  100.0f   // Vertex 7
        };




        public
        static
        float[]
        mCubeTriangleVertexColorsRGBVertexArray = 
        {
            // 8 Colors (R,G,B); 24 Total Floats
            0.0f, 0.0f, 0.0f, // Color  0 Black
            0.0f, 0.0f, 1.0f, // Color  1 Blue
            0.0f, 1.0f, 0.0f, // Color  2 Green
            0.0f, 1.0f, 1.0f, // Color  3 Cyan
            1.0f, 0.0f, 0.0f, // Color  4 Red
            1.0f, 0.0f, 1.0f, // Color  5 Magenta
            1.0f, 1.0f, 0.0f, // Color  6 Yellow
            1.0f, 1.0f, 1.0f  // Color  7 White
        };




        public
        static
        int[]
        mCubeTriangleFacesABCIndicesVertexArray =
        {
            // 12 Triangles; 36 Total Integers
            0,2,4,  // Triangle  0  Vertex Indices  (Back)
            4,2,6,  // Triangle  1  Vertex Indices  (Back)
            0,4,1,  // Triangle  2  Vertex Indices  (Bottom)
            1,4,5,  // Triangle  3  Vertex Indices  (Bottom)
            0,1,2,  // Triangle  4  Vertex Indices  (Left)
            2,1,3,  // Triangle  5  Vertex Indices  (Left)
            4,6,5,  // Triangle  6  Vertex Indices  (Right)
            5,6,7,  // Triangle  7  Vertex Indices  (Right)
            2,3,6,  // Triangle  8  Vertex Indices  (Top)
            6,3,7,  // Triangle  9  Vertex Indices  (Top)
            1,5,3,  // Triangle 10  Vertex Indices  (Front)
            3,5,7   // Triangle 11  Vertex Indices  (Front)
        };















        // The following function may be called by DrawModel() to
        // demonstrate the use of the OpenGL functions gl.glVertexPointer(), 
        // gl.glColorPointer(), and gl.glDrawElements().  


        public static void SupportDrawCubeViaVertexArrays(GL gl)
        {
            // The following code demonstrates drawing via vertex arrays.

            gl.glEnableClientState(GL.GL_VERTEX_ARRAY);
            gl.glEnableClientState(GL.GL_COLOR_ARRAY);


            gl.glVertexPointer
            (
                3,               // 'size'    : This is the number of values PER VERTEX; i.e., 3 (x,y,z), or 2 (x,y)
                GL.GL_FLOAT,     // 'type'    : The format of each value (e.g., float)
                0,               // 'stride'  : The memory gap between successive vertices
                mCubeXYZVertexArray  // 'pointer' : The pointer to an array of vertices
            );




            gl.glColorPointer
            (
                3,               // 'size'    : This is the number of values PER COLOR; i.e., 3 (r,g,b), or 4 (r,g,b,a)
                GL.GL_FLOAT,     // 'type'    : The format of each value (e.g., float)
                0,               // 'stride'  : The memory gap between successive colors
                mCubeTriangleVertexColorsRGBVertexArray  // 'pointer' : The pointer to an array of color values
            );




            gl.glDrawElements
            (
                GL.GL_TRIANGLES,    // This is the geometric primitive to render
                (mCubeTriangleFacesABCIndicesVertexArray.Length), // Total integers (not total groups of integers)
                GL.GL_UNSIGNED_INT, // GL_INT does not work!  Use GL_UNSIGNED_INT instead.
                mCubeTriangleFacesABCIndicesVertexArray     // pointer to the polygon vertex INDICES
            );


            gl.glDisableClientState(GL.GL_COLOR_ARRAY);
            gl.glDisableClientState(GL.GL_VERTEX_ARRAY);
        }
















        public static void SupportDrawCubeViaCallPerVertex(GL gl)
        {
            float[][] vertxyz = new float[8][]
            {
                new float[] { -100.0f, -100.0f, -100.0f },  // 0
                new float[] { -100.0f, -100.0f,  100.0f },  // 1
                new float[] { -100.0f,  100.0f, -100.0f },  // 2
                new float[] { -100.0f,  100.0f,  100.0f },  // 3
                new float[] {  100.0f, -100.0f, -100.0f },  // 4
                new float[] {  100.0f, -100.0f,  100.0f },  // 5
                new float[] {  100.0f,  100.0f, -100.0f },  // 6
                new float[] {  100.0f,  100.0f,  100.0f }   // 7
            };


            int[][] triabc = new int[12][]
            {
                new int[] {0,2,4}, new int[] {4,2,6}, // Back
                new int[] {0,4,1}, new int[] {1,4,5}, // Bottom
                new int[] {0,1,2}, new int[] {2,1,3}, // Left
                new int[] {4,6,5}, new int[] {5,6,7}, // Right
                new int[] {2,3,6}, new int[] {6,3,7}, // Top
                new int[] {1,5,3}, new int[] {3,5,7}  // Front
            };


            float[][] colorsrgb = new float[12][]
            {
                new float[] {0.5f,0.1f,0.1f }, new float[] {1.0f,0.1f,0.1f }, // Red
                new float[] {0.5f,0.5f,0.1f }, new float[] {1.0f,1.0f,0.1f }, // Yellow
                new float[] {0.1f,0.5f,0.1f }, new float[] {0.1f,1.0f,0.1f }, // Green
                new float[] {0.1f,0.5f,0.5f }, new float[] {0.1f,1.0f,1.0f }, // Cyan
                new float[] {0.1f,0.1f,0.5f }, new float[] {0.1f,0.1f,1.0f }, // Blue
                new float[] {0.5f,0.1f,0.5f }, new float[] {1.0f,0.1f,1.0f }  // Magenta
            };


            int triTotal = 12;


            gl.glBegin(GL.GL_TRIANGLES);

            for (int triIndex = 0; triIndex < triTotal; triIndex++)
            {
                gl.glColor3fv(colorsrgb[triIndex]);
                gl.glVertex3fv(vertxyz[triabc[triIndex][0]]);

                gl.glColor3fv(colorsrgb[triIndex]);
                gl.glVertex3fv(vertxyz[triabc[triIndex][1]]);

                gl.glColor3fv(colorsrgb[triIndex]);
                gl.glVertex3fv(vertxyz[triabc[triIndex][2]]);
            }

            gl.glEnd();
        }

















        public static void SupportDrawTextureImageUnrotatedAndOrthographically
        (
            GL gl,
            int clientWidth,
            int clientHeight,
            Texture texture,
            int drawX,
            int drawYTextMode, // i.e., 0 == draw TOP of image at TOP of viewport, Y-axis points DOWN
            int drawWidth,
            int drawHeight
        )
        {


            // Change rendering conditions
            gl.glDisable(GL.GL_DEPTH_TEST);
            gl.glDisable(GL.GL_CULL_FACE);


            // Preserve current matrices, and switch to an orthographic view, and 
            //   do scaling and translation as necessary.
            gl.glMatrixMode(GL.GL_PROJECTION);
            gl.glPushMatrix();
            gl.glMatrixMode(GL.GL_MODELVIEW);
            gl.glPushMatrix();


            gl.glMatrixMode(GL.GL_PROJECTION);
            gl.glLoadIdentity();
            gl.glOrtho
            (
                (double)0,
                (double)(clientWidth - 1),
                (double)0,
                (double)(clientHeight - 1),
                -1.0,
                1.0
            );

            gl.glMatrixMode(GL.GL_MODELVIEW);
            gl.glLoadIdentity();



            if (null != texture)
            {
                // Enable texture
                texture.SetAsActiveTexture(gl);

                gl.glEnable(GL.GL_TEXTURE_2D);
            }

            // Enable blending
            gl.glEnable(GL.GL_BLEND);
            gl.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);


            // Draw a quad

            gl.glBegin(GL.GL_QUADS);

            // TOP-LEFT
            gl.glTexCoord2f(0.0f, 1.0f);
            gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
            gl.glVertex3f((float)(drawX), (float)((clientHeight - 1) - drawYTextMode), 0.0f);

            // BOTTOM-LEFT
            gl.glTexCoord2f(0.0f, 0.0f);
            gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
            gl.glVertex3f((float)(drawX), (float)((clientHeight - 1) - (drawYTextMode + drawHeight)), 0.0f);

            // BOTTOM-RIGHT
            gl.glTexCoord2f(1.0f, 0.0f);
            gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
            gl.glVertex3f((float)(drawX + (drawWidth)), (float)((clientHeight - 1) - (drawYTextMode + drawHeight)), 0.0f);

            // TOP-RIGHT
            gl.glTexCoord2f(1.0f, 1.0f);
            gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
            gl.glVertex3f((float)(drawX + (drawWidth)), (float)((clientHeight - 1) - drawYTextMode), 0.0f);

            gl.glEnd();


            // Disable blending
            gl.glDisable(GL.GL_BLEND);


            if (null != texture)
            {
                // Disable texture
                gl.glDisable(GL.GL_TEXTURE_2D);

                gl.glBindTexture(GL.GL_TEXTURE_2D, 0);
            }


            // Restore original matrices.
            gl.glMatrixMode(GL.GL_MODELVIEW);
            gl.glPopMatrix();
            gl.glMatrixMode(GL.GL_PROJECTION);
            gl.glPopMatrix();


            // Restore rendering conditions
            gl.glFrontFace(GL.GL_CCW); // MUST DO AFTER USING wglUseFontOutlines LISTS!!!
            gl.glEnable(GL.GL_DEPTH_TEST);
            gl.glEnable(GL.GL_CULL_FACE);
        }
















        public static void SupportDrawTextUsingPolygonFont
        (
            GL gl,
            int fontOpenGLDisplayListBaseIndex,
            int clientWidth,
            int clientHeight,
            int fontHeightPixels,
            int textX,
            int textY,
            String text
        )
        {
            int stringLength = 0;
            int stringCharacterIndex = 0;
            int ASCIICharacter = 0;


            // Change rendering conditions
            gl.glDisable(GL.GL_DEPTH_TEST);
            gl.glDisable(GL.GL_CULL_FACE);


            // Preserve current matrices, and switch to an orthographic view, and 
            //   do scaling and translation as necessary.
            gl.glMatrixMode(GL.GL_PROJECTION);
            gl.glPushMatrix();
            gl.glMatrixMode(GL.GL_MODELVIEW);
            gl.glPushMatrix();


            gl.glMatrixMode(GL.GL_PROJECTION);
            gl.glLoadIdentity();
            gl.glOrtho
            (
                (double)0,
                (double)(clientWidth - 1),
                (double)0,
                (double)(clientHeight - 1),
                -1.0,
                1.0
            );

            gl.glMatrixMode(GL.GL_MODELVIEW);
            gl.glLoadIdentity();


            gl.glRasterPos2i(textX, textY); // Only affects by BITMAP fonts:


            // Only affects POLYGON fonts:
            gl.glTranslatef((float)(textX), (float)(textY), 0.0f);
            gl.glScalef((float)fontHeightPixels, (float)fontHeightPixels, 1.0f);


            /*
            gl.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
            gl.glEnable(GL.GL_BLEND);
            //gl.glEnable(GL.GL_POINT_SMOOTH);
            //gl.glHint(GL.GL_POINT_SMOOTH_HINT, GL.GL_NICEST);
            //gl.glEnable(GL.GL_LINE_SMOOTH);
            //gl.glHint(GL.GL_LINE_SMOOTH_HINT, GL.GL_NICEST);
            gl.glEnable(GL.GL_POLYGON_SMOOTH);
            gl.glHint(GL.GL_POLYGON_SMOOTH_HINT, GL.GL_NICEST);
            */


            // Call a display list for each character to be rendered.  The ASCII code
            // is used as the display list number (of the 256 display lists for this
            // font), which is added to the base number of the set of display list
            // indices.

            stringLength = text.Length;

            for
            (
                stringCharacterIndex = 0;
                stringCharacterIndex < stringLength;
                stringCharacterIndex++
            )
            {
                ASCIICharacter = (int)(text[stringCharacterIndex]);
                gl.glCallList(fontOpenGLDisplayListBaseIndex + ASCIICharacter);
            }

            // gl.glDisable(GL.GL_POLYGON_SMOOTH);


            // Restore original matrices.
            gl.glMatrixMode(GL.GL_MODELVIEW);
            gl.glPopMatrix();
            gl.glMatrixMode(GL.GL_PROJECTION);
            gl.glPopMatrix();


            // Restore rendering conditions
            gl.glFrontFace(GL.GL_CCW); // MUST DO AFTER USING wglUseFontOutlines LISTS!!!
            gl.glEnable(GL.GL_DEPTH_TEST);
            gl.glEnable(GL.GL_CULL_FACE);
        }
















        public static void SupportCreateBitmapFont
        (
            GL gl,
            IntPtr hdc,  // [in]
            String fontName, // [in] "Verdana", "Arial", "Courier New", "Symbol", "Wingdings", "Wingdings 3"
            int fontCellHeightInPoints, // [in] Cell height of font (in points)
            ref int bitmapFontOpenGLDisplayListBase  // [in]
        )
        {
            bitmapFontOpenGLDisplayListBase = gl.glGenLists(256);

            // IT IT AN ABSOLUTE NECESSITY TO SELECT A FONT IN TO THE DC FOR THE 
            // wglUseFontOutlines() METHOD TO SUCCEED WITHOUT A MYSTERIOUS 
            // ERROR 126: ERROR_MOD_NOT_FOUND : The specified module could not be found.
            // THAT ERROR CODE, OF COURSE, IS MISLEADING.

            IntPtr hfont = new IntPtr();

            System.Drawing.Font font = null;

            bool useGDICreateFontDirectly = false;

            if (true == useGDICreateFontDirectly)
            {
                hfont =
                    GL.GDI32_CreateFont
                    (
                        fontCellHeightInPoints, // height ; negative means CHARACTER height; positive means CELL height
                        0, // width 
                        0, // escapement
                        0, // orientation 
                        GL.FW_NORMAL, // weight 
                        0, // italic 
                        0, // underline 
                        0, // strikeout 
                        GL.ANSI_CHARSET, // char set 
                        GL.OUT_TT_PRECIS, // output precision 
                        GL.CLIP_DEFAULT_PRECIS, // clip precision 
                        GL.ANTIALIASED_QUALITY, // quality
                        GL.FF_DONTCARE | GL.DEFAULT_PITCH, // pitch and family 
                        fontName // font name
                    );
            }
            else
            {
                font =
                    new System.Drawing.Font
                    (
                        fontName, // family: "Verdana", "Arial", "Courier New", "Symbol", "Wingdings", "Wingdings 3"
                        (float)fontCellHeightInPoints,            // emSize: Font size (see 4th parameter for units)
                        System.Drawing.FontStyle.Regular, // style
                        System.Drawing.GraphicsUnit.Point, // unit
                        ((System.Byte)(0)) // GDI character set
                    );

                hfont = font.ToHfont();
            }


            GL.GDI32_SelectObject(hdc, hfont);



            int result = 0;

            result =
                gl.wglUseFontBitmapsA // Use wglUseFontBitmapsW() instead if this doesn't work!!!
                (
                    hdc,
                    0,
                    255,
                    bitmapFontOpenGLDisplayListBase
                );

            if (0 == result)
            {
                String message = "wglUseFontBitmaps() error";

                System.Windows.Forms.MessageBox.Show(null, message, "Error", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }
















        public static void SupportCreatePolygonFont
        (
            GL gl,
            IntPtr hdc,   // [in]
            String fontName,  // [in] "Verdana", "Arial", "Courier New", "Symbol", "Wingdings", "Wingdings 3"
            ref int polygonFontOpenGLDisplayListBase  // [out]
        )
        {
            polygonFontOpenGLDisplayListBase = gl.glGenLists(256);


            // IT IT AN ABSOLUTE NECESSITY TO SELECT A FONT IN TO THE DC FOR THE 
            // wglUseFontOutlines() METHOD TO SUCCEED WITHOUT A MYSTERIOUS 
            // ERROR 126: ERROR_MOD_NOT_FOUND : The specified module could not be found.
            // THAT ERROR CODE, OF COURSE, IS MISLEADING.


            IntPtr hfont =
                GL.GDI32_CreateFont
                (
                    -12, // height ; negative means CHARACTER height; positive means CELL height
                    0, // width 
                    0, // escapement
                    0, // orientation 
                    GL.FW_NORMAL, // weight 
                    0, // italic 
                    0, // underline 
                    0, // strikeout 
                    GL.ANSI_CHARSET, // char set 
                    GL.OUT_TT_PRECIS, // output precision 
                    GL.CLIP_DEFAULT_PRECIS, // clip precision 
                    GL.ANTIALIASED_QUALITY, // quality
                    GL.FF_DONTCARE | GL.DEFAULT_PITCH, // pitch and family 
                    fontName // font name
                );

#if false
            {
                System.Drawing.Font font =
                    new System.Drawing.Font
                    (
                        fontName, // family: "Verdana", "Arial", "Courier New", "Symbol", "Wingdings", "Wingdings 3"
                        9.0f, // IRRELEVANT FOR POLYGON FONT; emSize: Font size (in points, due to flag in 4th parameter)
                        System.Drawing.FontStyle.Regular, // style
                        System.Drawing.GraphicsUnit.Point, // unit
                        ((System.Byte)(0)) // GDI character set
                    );


                IntPtr fontH = font.ToHfont();

                GL.GDI32_SelectObject(hdc, fontH);
            }
#endif

            GL.GDI32_SelectObject(hdc, hfont);




            GL.GLYPHMETRICSFLOAT[] agmf = new GL.GLYPHMETRICSFLOAT[256];

            int result = 0;

            result =
                gl.wglUseFontOutlinesA // Use wglUseFontOutlinesW() instead if this doesn't work!!!
                (
                    hdc,        // DC with font   
                    0,                     // Starting Character
                    255,                   // Number Of Display Lists To Build
                    polygonFontOpenGLDisplayListBase, // Starting Display List index
                    0.0f,                  // Deviation From The True Outlines
                    0.0f,                 // Font Thickness In The Z Direction (Extrusion)
                    GL.WGL_FONT_POLYGONS, // Format: GL.WGL_FONT_LINES, GL.WGL_FONT_POLYGONS
                    agmf                   // Metrics pointer
                );

            if (0 == result)
            {
                String message = "wglUseFontOutlines() error";

                System.Windows.Forms.MessageBox.Show(null, message, "Error", System.Windows.Forms.MessageBoxButtons.OK);
            }
        }














        public static bool SupportWriteViewportToImageFile
        (
            GL gl, 
            String filePathAndName, 
            System.Drawing.Imaging.ImageFormat imageFormat
        )
        {
            int[] viewportXYWidthHeight = new int[4]; // 0:x, 1:y, 2:width, 3:height

            gl.glGetIntegerv(GL.GL_VIEWPORT, viewportXYWidthHeight);

            int viewportWidth = viewportXYWidthHeight[2];
            int viewportHeight = viewportXYWidthHeight[3];

            
            // Protect against absurd cases
            
            if (viewportWidth < 0) { return (false); }
            if (viewportHeight < 0) { return (false); }
            if (viewportWidth < 1) { return (false); }
            if (viewportHeight < 1) { return (false); }


            // Allocate byte buffer to contain RGBA pixel data.

            int totalBytes = 0;

            totalBytes = ((viewportWidth * 4) * viewportHeight);

            byte[] rgbaData = new byte[ totalBytes ];
            
            if (null == rgbaData)
            {
                return (false);
            }


            // Set how pixels will be transferred from OpenGL buffer to a client memory buffer; i.e., "stored".
            gl.glPixelStorei(GL.GL_PACK_ALIGNMENT, 1);
            gl.glPixelStorei(GL.GL_PACK_ROW_LENGTH, 0);
            gl.glPixelStorei(GL.GL_PACK_SKIP_ROWS, 0);
            gl.glPixelStorei(GL.GL_PACK_SKIP_PIXELS, 0);


            // Cache the current read buffer setting, change the read buffer setting to the front buffer,
            // then read the front buffer, then restore the read buffer setting.
            int[] previousBufferID = new int[1];
            gl.glGetIntegerv(GL.GL_READ_BUFFER, previousBufferID);
            gl.glReadBuffer(GL.GL_FRONT);
            gl.glReadPixels(0, 0, viewportWidth, viewportHeight, GL.GL_RGBA, GL.GL_UNSIGNED_BYTE, rgbaData);
            gl.glReadBuffer(previousBufferID[0]);


            bool result = 
                ImageData.WriteRGBABufferToImageFile
                (
                    rgbaData, 
                    viewportWidth, 
                    viewportHeight, 
                    filePathAndName, 
                    imageFormat
                );

            if (false == result)
            {
                return (false);
            }

            return (true);
        }




    }




}








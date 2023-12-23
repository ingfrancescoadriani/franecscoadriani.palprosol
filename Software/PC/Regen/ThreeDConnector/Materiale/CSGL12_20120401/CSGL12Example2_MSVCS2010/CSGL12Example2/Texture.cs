



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




// NOTE: This code file requires a reference to the "System.Drawing" DLL to be 
// added to the set of "References" of the project.  
//
// In the "Solution Explorer" panel of Microsoft Visual C#, right-click the 
// "References" item for the project and choose "Add Reference...".  In the 
// "Add Reference" dialog window that appears, click the ".NET" tab, and scroll 
// down to find "System.Drawing".  Click the "System.Drawing" item, and then 
// click "OK" to add a reference to that DLL to the project.




using System;








namespace CSGL12
{




    public class Texture
    {
        private int mWidth = 0;
        private int mHeight = 0;
        private byte[] mRGBAData = null;
        private bool mUseMipMaps = false;
        private int mOpenGLTextureID = 0;








        public int GetWidth()
        {
            return (mWidth);
        }








        public int GetHeight()
        {
            return (mHeight);
        }








        public byte[] GetRGBAData()
        {
            return (mRGBAData);
        }








        public int GetOpenGLTextureID()
        {
            return (mOpenGLTextureID);
        }








        private void Texture_ClearAllFields()
        {
            mWidth = 0;
            mHeight = 0;
            mRGBAData = null;
            mUseMipMaps = true;
            mOpenGLTextureID = 0;
        }








        public Texture()
        {
            Texture_ClearAllFields();
        }








        public void DisposeOfTextureAndData( GL gl )
        {
            if (0 != mOpenGLTextureID)
            {
                int[] textureIDArray = new int[1];

                textureIDArray[0] = mOpenGLTextureID;
                
                gl.glDeleteTextures
                (
                    1,
                    textureIDArray
                );

                mOpenGLTextureID = 0;
            }

            Texture_ClearAllFields();
        }








        private void PrivateCreateOpenGLTextureUsingInternalWidthHeightAndRBGAData( GL gl )
        {
            int[] textureIDArray = new int[1];


            gl.glGenTextures
            (
                1,
                textureIDArray
            );


            mOpenGLTextureID = textureIDArray[0];


            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                mOpenGLTextureID
            );


            if (true == mUseMipMaps)
            {
                gl.gluBuild2DMipmaps
                (
                    GL.GL_TEXTURE_2D,      // target                    
                    GL.GL_RGBA,            // internalFormat
                    mWidth,                // width
                    mHeight,               // height
                    GL.GL_RGBA,            // format
                    GL.GL_UNSIGNED_BYTE,   // type
                    mRGBAData              // data
                );
            }
            else
            {
                gl.glTexImage2D
                (
                    GL.GL_TEXTURE_2D,      // target
                    0,                     // level
                    GL.GL_RGBA,            // internalFormat
                    mWidth,                // width
                    mHeight,               // height
                    0,                     // border
                    GL.GL_RGBA,            // format
                    GL.GL_UNSIGNED_BYTE,   // type
                    mRGBAData              // data
                );
            }

            gl.glTexParameteri( GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, GL.GL_REPEAT );
            gl.glTexParameteri( GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, GL.GL_REPEAT );

            if (true == mUseMipMaps)
            {
                gl.glTexParameterf(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR_MIPMAP_LINEAR);
            }
            else
            {
                gl.glTexParameterf(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR);
            }

            gl.glTexParameterf( GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR );
        }








        public void SetWrappingModeToRepeatMode( GL gl )
        {
            // Select the texture

            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                mOpenGLTextureID
            );



            gl.glTexParameteri( GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, GL.GL_REPEAT );
            gl.glTexParameteri( GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, GL.GL_REPEAT );



            // Select no texture

            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                0
            );
        }








        public void SetWrappingModeToClampMode( GL gl )
        {
            // Select the texture

            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                mOpenGLTextureID
            );


            gl.glTexParameteri( GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, GL.GL_CLAMP );
            gl.glTexParameteri( GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, GL.GL_CLAMP );


            // Select no texture

            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                0
            );
        }








        public void SetMagnificationToUseNearestNeighbor( GL gl )
        {
            // Select the texture

            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                mOpenGLTextureID
            );


            gl.glTexParameterf( GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_NEAREST );


            // Select no texture

            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                0
            );
        }








        public void SetMagnificationToUseLinearFiltering(GL gl)
        {
            // Select the texture

            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                mOpenGLTextureID
            );


            gl.glTexParameterf(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR);


            // Select no texture

            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                0
            );
        }








        public void TransferOpenGLTextureDataBackToHostMemory( GL gl )
        {
            if (null == mRGBAData)
            {
                return;
            }

            try
            {
                gl.glGetTexImage
                (
                    GL.GL_TEXTURE_2D,     // target
                    0,                    // level (0 == top-most mip-map level)
                    GL.GL_RGBA,           // internalFormat
                    GL.GL_UNSIGNED_BYTE,  // type
                    mRGBAData             // data
                );
            }
            catch
            {
            }
        }








        public void CreateBlankTexture( GL gl, int width, int height )
        {
            DisposeOfTextureAndData( gl );

            int totalBytes = 0;

            totalBytes = ((width * 4) * height);

            mWidth = width;
            mHeight = height;
            mRGBAData = new byte[totalBytes];
            mUseMipMaps = true;

            int k = 0;

            for (k = 0; k < totalBytes; k += 4)
            {
                mRGBAData[k + 0] = 0; // red
                mRGBAData[k + 1] = 0; // green
                mRGBAData[k + 2] = 0; // blue
                mRGBAData[k + 3] = 255; // opacity (255 == opaque)
            }

            PrivateCreateOpenGLTextureUsingInternalWidthHeightAndRBGAData( gl );
        }








        public void CreateCheckerboardTexture( GL gl, int width, int height )
        {
            DisposeOfTextureAndData( gl );

            int totalBytes = 0;

            totalBytes = ((width * 4) * height);

            mWidth = width;
            mHeight = height;
            mRGBAData = new byte[totalBytes];
            mUseMipMaps = true;

            int x = 0;
            int y = 0;
            int k = 0;
            byte val = 0;

            for (y = 0; y < height; y++)
            {
                for (x = 0; x < width; x++)
                {
                    k = ((width * 4) * y) + (x * 4);

                    val = 0;

                    if (0 == (((x / 16) + (y / 16)) % 2)) { val = 255; }
                    
                    mRGBAData[k + 0] = val; // red
                    mRGBAData[k + 1] = val; // green
                    mRGBAData[k + 2] = val; // blue
                    mRGBAData[k + 3] = 255; // opacity (255 == opaque)
                }
            }

            PrivateCreateOpenGLTextureUsingInternalWidthHeightAndRBGAData( gl );
        }








        public bool CreateTextureFromBitmap( GL gl, System.Drawing.Bitmap bitmap, bool useMipMaps )
        {
            DisposeOfTextureAndData( gl );


            if (null == bitmap)
            {
                return (false);
            }


            mWidth = bitmap.Width;
            mHeight = bitmap.Height;
            mUseMipMaps = useMipMaps;


            mRGBAData = ImageData.ConvertBitmapToRGBAData( bitmap );


            PrivateCreateOpenGLTextureUsingInternalWidthHeightAndRBGAData( gl );


            return (true);
        }








        public bool UpdateTextureWithBitmapData(GL gl, System.Drawing.Bitmap bitmap)
        {
            if (null == bitmap)
            {
                return (false);
            }


            bool result = false;


            if ((mWidth != bitmap.Width) || (mHeight != bitmap.Height))
            {
                // Uh, oh!  User submitted a bitmap which has dimensions different from the
                // original dimensions!  Create a new texture!

                result = CreateTextureFromBitmap( gl, bitmap, mUseMipMaps );
                
                return (result);
            }


            // The supplied bitmap is compatible with the current texture dimensions.
            // Copy bitmap data to our already-allocated byte buffer.
            
            result = ImageData.FillInAllocatedRGBADataWithBitmapData( bitmap, mRGBAData );
            
            if (false == result)
            {
                return (false);
            }


            SubmitModifiedInternalRGBADataToTexture( gl );


            return (true);
        }








        public bool TransferOpenGLTextureDataBackToHostMemoryAndCopyToCompatibleBitmap(GL gl, System.Drawing.Bitmap bitmap)
        {
            if (null == mRGBAData)
            {
                return (false);
            }


            TransferOpenGLTextureDataBackToHostMemory( gl );


            if (null == bitmap)
            {
                return (false);
            }


            if ((mWidth != bitmap.Width) || (mHeight != bitmap.Height))
            {
                // Uh, oh!  User submitted a bitmap which has dimensions different from the
                // texture dimensions!  Give up!

                return (false);
            }

            bool result = ImageData.CopyRGBABufferToCompatibleBitmap( mRGBAData, bitmap );

            return (result);
        }








        public bool LoadTextureFromFile
        (
            GL gl,
            String filePathAndName,
            bool useMipMaps
        )
        {
            DisposeOfTextureAndData( gl );


            System.Drawing.Bitmap bitmap = 
                ImageData.ReadImageFileToBitmap(filePathAndName);


            if (null == bitmap)
            {
                return (false);
            }


            bool result = CreateTextureFromBitmap( gl, bitmap, useMipMaps );


            bitmap.Dispose();


            if (result == false)
            {
                return (false);
            }


            return (true);
        }








        public void SetAsActiveTexture( GL gl )
        {
            // Do the following before any call to glEnable( GL_TEXTURE_2D ).

            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                mOpenGLTextureID
            );
        }








        public void SubmitModifiedInternalRGBADataToTexture( GL gl )
        {
            if (null == mRGBAData)
            {
                return;
            }


            // Make sure total bytes matches ((width * RGBA) * height).

            int totalBytes = 0;
            
            totalBytes = ((mWidth * 4) * mHeight);

            if (totalBytes != mRGBAData.Length)
            {
                return;
            }


            gl.glBindTexture
            (
                GL.GL_TEXTURE_2D,
                mOpenGLTextureID
            );



            if (true == mUseMipMaps)
            {
                // Will this work?

                gl.gluBuild2DMipmaps
                (
                    GL.GL_TEXTURE_2D,      // target                    
                    GL.GL_RGBA,            // internalFormat
                    mWidth,                // width
                    mHeight,               // height
                    GL.GL_RGBA,            // format
                    GL.GL_UNSIGNED_BYTE,   // type
                    mRGBAData              // data
                );
            }
            else
            {
                gl.glTexSubImage2D
                (
                    GL.GL_TEXTURE_2D,      // target
                    0,                     // level
                    0,                     // xoffset
                    0,                     // yoffset
                    mWidth,                // width
                    mHeight,               // height
                    GL.GL_RGBA,            // format
                    GL.GL_UNSIGNED_BYTE,   // type
                    mRGBAData              // pixels
                );
            }
        }




    }




}








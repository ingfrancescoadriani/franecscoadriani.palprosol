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




// NOTE: This code file requires a reference to the "System.Drawing" DLL to be
// added to the set of "References" of the project.  
//
// In the "Solution Explorer" panel of Microsoft Visual C#, right-click the 
// "References" item for the project and choose "Add Reference...".  In the 
// "Add Reference" dialog window that appears, click the ".NET" tab, and scroll 
// down to find "System.Drawing".  Click the "System.Drawing" item, and then 
// click "OK" to add a reference to that DLL to the project.




// Remove the following #define (by putting it in a comment) to disable the use
// of optimized code in this code module.  Doing so will greatly reduce the 
// efficiency and speed of image operations.

#define ALLOW_OPTIMIZATIONS_THAT_REQUIRE_UNSAFE_CODE_COMPILE_SETTING





using System;








namespace CSGL12
{




    public class ImageData
    {








        public static bool FillInAllocatedRGBADataWithBitmapData( System.Drawing.Bitmap bitmap, byte[] rgbaData )
        {
            if (null == bitmap)
            {
                return (false);
            }

            if (null == rgbaData)
            {
                return (false);
            }

            int bitmapWidth = bitmap.Width;
            int bitmapHeight = bitmap.Height;

            if ((bitmapWidth <= 0) || (bitmapHeight <= 0))
            {
                return (false);
            }

            int totalBytes = ((bitmapWidth * 4) * bitmapHeight);

            if (rgbaData.Length != totalBytes)
            {
                return (false);
            }


            // The source has the pixel (0,0) at the upper-left of the image.
            // The destination OpenGL texture has texel (0,0) at the lower-left
            // of the texture.

            // Copy the BGRA source to the destination RGBA buffer.

            int sourceLineStart = 0;

            int destinationStride = (4 * bitmapWidth);
            int destinationHeight = (bitmapHeight);
            int destinationLineStart = (destinationStride * (destinationHeight - 1));

            int copyWidth = bitmapWidth;
            int copyHeight = bitmapHeight;

            int x = 0;
            int y = 0;
            int k = 0;

            bool exceptionFlag = false;

            try
            {

#if ALLOW_OPTIMIZATIONS_THAT_REQUIRE_UNSAFE_CODE_COMPILE_SETTING
                unsafe
                {
                    System.Drawing.Imaging.BitmapData bitmapData =
                      bitmap.LockBits
                        (
                        new System.Drawing.Rectangle( 0, 0, bitmapWidth, bitmapHeight ),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb
                        );

                    byte * sourceBGRA = (byte*)bitmapData.Scan0.ToPointer();
                    int sourceStride = bitmapData.Stride;

                    for (y = 0; y < copyHeight; y++)
                    {
                        k = 0;
                        for (x = 0; x < copyWidth; x++)
                        {
                            rgbaData[destinationLineStart + k + 0] = sourceBGRA[sourceLineStart + k + 2];
                            rgbaData[destinationLineStart + k + 1] = sourceBGRA[sourceLineStart + k + 1];
                            rgbaData[destinationLineStart + k + 2] = sourceBGRA[sourceLineStart + k + 0];
                            rgbaData[destinationLineStart + k + 3] = sourceBGRA[sourceLineStart + k + 3];
                            k += 4;
                        }
                        sourceLineStart += sourceStride;
                        destinationLineStart -= destinationStride;
                    }

                    bitmap.UnlockBits( bitmapData );
                }

#else

                // The following is an alternative to using "unsafe" code to copy bytes
                // from the bitmap to a buffer.

                {
                    Color color = Color.Black;

                    for (y = 0; y < copyHeight; y++)
                    {
                        k = destinationLineStart;

                        for (x = 0; x < copyWidth; x++)
                        {
                            color = bitmap.GetPixel(x, y);

                            rgbaData[k + 0] = color.R;
                            rgbaData[k + 1] = color.G;
                            rgbaData[k + 2] = color.B;
                            rgbaData[k + 3] = color.A;

                            k += 4;
                        }

                        destinationLineStart -= destinationStride;
                    }

                }

#endif
            }
            catch
            {
                exceptionFlag = true;
            }

            if (true == exceptionFlag)
            {
                return (false);
            }

            return (true);
        }








        public static byte[] ConvertBitmapToRGBAData(System.Drawing.Bitmap bitmap)
        {
            if (null == bitmap)
            {
                return (null);
            }

            int bitmapWidth = bitmap.Width;
            int bitmapHeight = bitmap.Height;

            if ((bitmapWidth <= 0) || (bitmapHeight <= 0))
            {
                return (null);
            }

            int totalBytes = 0;

            totalBytes = ((bitmapWidth * 4) * bitmapHeight);

            byte[] rgbaData = new byte[ totalBytes ];

            bool succeededInFillingInData = FillInAllocatedRGBADataWithBitmapData(bitmap, rgbaData);

            if (false == succeededInFillingInData)
            {
                // If we fail to convert the bitmap to data
                return (null);
            }

            return (rgbaData);
        }








        public static bool CopyRGBABufferToCompatibleBitmap(byte[] rgbaData, System.Drawing.Bitmap bitmap)
        {
            if (null == rgbaData)
            {
                return (false);
            }

            if (null == bitmap)
            {
                return (false);
            }

            int bitmapWidth = bitmap.Width;
            int bitmapHeight = bitmap.Height;

            if ((bitmapWidth <= 0) || (bitmapHeight <= 0))
            {
                return (false);
            }

            int totalBytes = 0;
            
            totalBytes = ((bitmapWidth * 4) * bitmapHeight);

            if (rgbaData.Length != totalBytes)
            {
                return (false);
            }


            // The source OpenGL RGBA buffer has pixel (0,0) at the lower-left.
            // The destination Bitmap BGRA has the pixel (0,0) at the upper-left.

            // Copy the RGBA source to the destination BGRA buffer.

            int sourceStride = (bitmapWidth * 4);
            int sourceHeight = (bitmapHeight);
            int sourceLineStart = (sourceStride * (sourceHeight - 1));

            int destinationLineStart = 0;

            int copyWidth = bitmapWidth;
            int copyHeight = bitmapHeight;

            int x = 0;
            int y = 0;
            int k = 0;



            bool exceptionFlag = false;

            try
            {

#if ALLOW_OPTIMIZATIONS_THAT_REQUIRE_UNSAFE_CODE_COMPILE_SETTING
                unsafe
                {
                    System.Drawing.Imaging.BitmapData bitmapData =
                      bitmap.LockBits
                        (
                        new System.Drawing.Rectangle( 0, 0, bitmapWidth, bitmapHeight ),
                        System.Drawing.Imaging.ImageLockMode.ReadWrite,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb
                        );

                    byte * destinationBGRA = (byte*)bitmapData.Scan0.ToPointer();
                    int destinationStride = bitmapData.Stride;

                    for (y = 0; y < copyHeight; y++)
                    {
                        k = 0;

                        for (x = 0; x < copyWidth; x++)
                        {
                            destinationBGRA[destinationLineStart + k + 0] = rgbaData[sourceLineStart + k + 2];
                            destinationBGRA[destinationLineStart + k + 1] = rgbaData[sourceLineStart + k + 1];
                            destinationBGRA[destinationLineStart + k + 2] = rgbaData[sourceLineStart + k + 0];
                            destinationBGRA[destinationLineStart + k + 3] = rgbaData[sourceLineStart + k + 3];

                            k += 4;
                        }

                        sourceLineStart -= sourceStride;
                        destinationLineStart += destinationStride;
                    }

                    bitmap.UnlockBits( bitmapData );
                }

#else

                // The following is an alternative to using "unsafe" code to copy bytes
                // from the bitmap to a buffer.

                {

                    for (y = 0; y < copyHeight; y++)
                    {
                        k = sourceLineStart;

                        for (x = 0; x < copyWidth; x++)
                        {
                            bitmap.SetPixel(x, y, Color.FromArgb(rgbaData[k + 3], rgbaData[k + 0], rgbaData[k + 1], rgbaData[k + 2]));
                
                            k += 4;
                        }

                        sourceLineStart -= sourceStride;
                    }

                }

#endif

            }
            catch
            {
                exceptionFlag = true;
            }


            if (true == exceptionFlag)
            {
                return (false);
            }


            return (true);
        }








        public static System.Drawing.Bitmap ConvertRGBABufferToBitmap(byte[] rgbaData, int width, int height)
        {
            if ((width <= 0) || (height <= 0) || (null == rgbaData))
            {
                return (null);
            }

            // Create a Bitmap object of the appropriate dimensions
            System.Drawing.Bitmap bitmap = 
                new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            if (null == bitmap)
            {
                return (null);
            }

            bool succeededInCopyingDataToBitmap = CopyRGBABufferToCompatibleBitmap(rgbaData, bitmap);

            if (false == succeededInCopyingDataToBitmap)
            {
                // Copy failed in some manner.  But the Bitmap can still be returned.
            }

            return (bitmap);
        }








        public static bool WriteBitmapToImageFile
        (
            System.Drawing.Bitmap bitmap, 
            String filePathAndName, 
            System.Drawing.Imaging.ImageFormat imageFormat
        )
        {
            if (null == bitmap)
            {
                return (false);
            }

            if (null == filePathAndName)
            {
                return (false);
            }

            if (filePathAndName.Length <= 0)
            {
                return (false);
            }

            bool exceptionFlag = false;

            try
            {

                if (System.Drawing.Imaging.ImageFormat.Jpeg == imageFormat)
                {
                    // JPEG 

                    // NOTE: .NET built-in JPEG encoder does severe color down-sampling (BAD!), 
                    // so use highest possible quality.
                    int quality100 = 100; 

                    //Get the list of available encoders
                    System.Drawing.Imaging.ImageCodecInfo[] encoderInfoArray = 
                        System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();

                    //find the encoder with the image/jpeg mime-type
                    System.Drawing.Imaging.ImageCodecInfo jpegEncoderInfo = null;

                    foreach (System.Drawing.Imaging.ImageCodecInfo imageCodecInfo in encoderInfoArray)
                    {
                        if (0 == String.Compare(imageCodecInfo.MimeType, "image/jpeg", true))
                        {
                            jpegEncoderInfo = imageCodecInfo;
                        }
                    }

                    if (null != jpegEncoderInfo)
                    {
                        //Create a collection of encoder parameters (we only need one in the collection)
                        System.Drawing.Imaging.EncoderParameters encoderParameters = 
                            new System.Drawing.Imaging.EncoderParameters(1);

                        // Create an encoder parameter for the "Quality" parameter of JPEG
                        System.Drawing.Imaging.EncoderParameter encoderParameter = 
                            new System.Drawing.Imaging.EncoderParameter
                                (System.Drawing.Imaging.Encoder.Quality, (long)quality100);

                        // Put the parameter in to the parameter array.
                        encoderParameters.Param[0] = encoderParameter;

                        bitmap.Save(filePathAndName, jpegEncoderInfo, encoderParameters);
                    }
                }


                if (System.Drawing.Imaging.ImageFormat.Bmp == imageFormat)
                {
                    // BMP 
                    bitmap.Save(filePathAndName, imageFormat);
                }


                if (System.Drawing.Imaging.ImageFormat.Gif == imageFormat)
                {
                    // GIF 

                    // Note: For high-quality GIF, use optimized color palette builder code available online...

                    bitmap.Save(filePathAndName, imageFormat);
                }


                if (System.Drawing.Imaging.ImageFormat.Png == imageFormat)
                {
                    // PNG 
                    bitmap.Save(filePathAndName, imageFormat);
                }


            }
            catch
            {
                exceptionFlag = true;
            }


            if (true == exceptionFlag)
            {
                return (false);
            }

            return (true);
        }








        public static bool WriteRGBABufferToImageFile
        (
            byte[] rgbaData, 
            int width, 
            int height, 
            String filePathAndName,
            System.Drawing.Imaging.ImageFormat imageFormat
        )
        {
            if (null == rgbaData)
            {
                return (false);
            }

            if (null == filePathAndName)
            {
                return (false);
            }

            if (filePathAndName.Length <= 0)
            {
                return (false);
            }

            // Convert RGBA data to Bitmap
            System.Drawing.Bitmap bitmap = 
                ConvertRGBABufferToBitmap(rgbaData, width, height);

            if (null == bitmap)
            {
                return (false);
            }

            bool succeededInWritingBitmapImageToFile = 
                WriteBitmapToImageFile(bitmap, filePathAndName, imageFormat);

            bitmap.Dispose();
            bitmap = null;

            if (false == succeededInWritingBitmapImageToFile)
            {
                return (false);
            }

            return (true);
        }








        public static System.Drawing.Bitmap ReadImageFileToBitmap(String filePathAndName)
        {
            System.Drawing.Bitmap bitmap = null;

            bool exceptionFlag = false;

            try
            {
                bitmap = new System.Drawing.Bitmap(filePathAndName);
            }
            catch
            {
                exceptionFlag = true;
            }

            if (true == exceptionFlag)
            {
                return (null);
            }

            return (bitmap);
        }




    }




}









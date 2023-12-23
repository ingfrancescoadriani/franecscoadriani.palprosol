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
using System.Drawing;
namespace CSGL12
{

    public class Mesh
    {
        object locker = new object();
        private List<Triangle> mTriangleList = new List<Triangle>();
        private List<Rectangle> mRectangleList = new List<Rectangle>();

        public void substituteLastParallelepiped(float width, float height, float lenght, float centerX, float centerZ, float centerY, float alpha, Color color, String tag)
        {
            lock (locker)
            {
                if (mRectangleList.Count > 6)
                    mRectangleList.RemoveRange(mRectangleList.Count - 6, 6);
            }
            addParallelepiped(width, height, lenght, centerX, centerZ, centerY, alpha, color, tag);
        }

        public void changeColorAtTagged(String tag, Color color)
        {
            foreach (Rectangle r in mRectangleList)
                if (r.TagString == tag)
                    r.changeColor(color);
        }

        private void Mesh_Initialize()
        {
            mTriangleList = new List<Triangle>();
            mRectangleList = new List<Rectangle>();
        }

        public Mesh()
        {
            Mesh_Initialize();
        }

        public void Draw(GL gl)
        {
            lock (locker)
            {
                foreach (Triangle triangle in mTriangleList)
                {
                    triangle.Draw(gl);
                    //triangle.DrawOnlyIfUntexturedAndWithoutGLBeginOrGLEnableTexture(gl);
                    //triangle.DrawOnlyIfTexturedAndWithoutGLBeginOrGLEnableTexture(gl);
                }
                foreach (Rectangle rectangle in mRectangleList)
                {
                    rectangle.Draw(gl);
                    //triangle.DrawOnlyIfUntexturedAndWithoutGLBeginOrGLEnableTexture(gl);
                    //triangle.DrawOnlyIfTexturedAndWithoutGLBeginOrGLEnableTexture(gl);
                }

            }
        }

        public void reInit()
        {
            Mesh_Initialize();
        }

        public void addParallelepiped(float width, float lenght, float height, float centerX, float centerY, float centerZ, float angle, Color color, String tag)
        {
            centerZ += (float)(height / 2.0);
            double alpha = (angle) / 180.0 * Math.PI;
            lock (locker)
            {
                Rectangle rectangle = new Rectangle();

                float H = (0.5f * lenght); // lunghezza
                float L = (0.5f * height); // altezza
                float W = (0.5f * width); // larghezza

                float diagonal;

                diagonal = (float)Math.Sqrt(W * W + H * H);

                // Back
                rectangle.mUseColors = true;
                rectangle.mUseTexture = true;
                rectangle.mUseNormals = true;
                rectangle.mUseShader = true;

                // -L;  W; -H
                rectangle.mVertexA.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
                rectangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
                rectangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                rectangle.mVertexA.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, W) + alpha) + centerY);

                // -L; -W; -H
                rectangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
                rectangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
                rectangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                rectangle.mVertexB.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, -W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, -W) + alpha) + centerY);

                //  L;  W; -H
                rectangle.mVertexD.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
                rectangle.mVertexD.mTextureCoordinates.Set(0.0f, 1.0f);
                rectangle.mVertexD.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                rectangle.mVertexD.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, W) + alpha) + centerY);

                //  L; -W; -H
                rectangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
                rectangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
                rectangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                rectangle.mVertexC.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, -W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, -W) + alpha) + centerY);

                mRectangleList.Add(rectangle);

                // Bottom
                rectangle.mVertexA.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
                rectangle.mVertexA.mTextureCoordinates.Set(0.0f, 1.0f);
                rectangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexA.mPosition.Set(-W + centerX, -L + centerY, H + centerZ);
                rectangle.mVertexA.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, -W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, -W) + alpha) + centerY);

                rectangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
                rectangle.mVertexB.mTextureCoordinates.Set(0.0f, 0.0f);
                rectangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexB.mPosition.Set(-W + centerX, -L + centerY, -H + centerZ);
                rectangle.mVertexB.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, -W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, -W) + alpha) + centerY);

                rectangle.mVertexD.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
                rectangle.mVertexD.mTextureCoordinates.Set(1.0f, 1.0f);
                rectangle.mVertexD.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexD.mPosition.Set(W + centerX, -L + centerY, H + centerZ);
                rectangle.mVertexD.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, W) + alpha) + centerY);

                rectangle.mVertexC.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
                rectangle.mVertexC.mTextureCoordinates.Set(1.0f, 0.0f);
                rectangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexC.mPosition.Set(W + centerX, -L + centerY, -H + centerZ);
                rectangle.mVertexC.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, W) + alpha) + centerY);


                mRectangleList.Add(rectangle);

                // Left
                rectangle.mVertexA.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
                rectangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
                rectangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexA.mPosition.Set(-W + centerX, -L + centerY, -H + centerZ);
                rectangle.mVertexA.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, -W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, -W) + alpha) + centerY);

                rectangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
                rectangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
                rectangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexB.mPosition.Set(-W + centerX, -L + centerY, H + centerZ);
                rectangle.mVertexB.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, -W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, -W) + alpha) + centerY);

                rectangle.mVertexD.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
                rectangle.mVertexD.mTextureCoordinates.Set(0.0f, 1.0f);
                rectangle.mVertexD.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexD.mPosition.Set(-W + centerX, L + centerY, -H + centerZ);
                rectangle.mVertexD.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, -W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, -W) + alpha) + centerY);

                rectangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
                rectangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
                rectangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexC.mPosition.Set(-W + centerX, L + centerY, H + centerZ);
                rectangle.mVertexC.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, -W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, -W) + alpha) + centerY);

                mRectangleList.Add(rectangle);




                // Right
                rectangle.mVertexA.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
                rectangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
                rectangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexA.mPosition.Set(W + centerX, -L + centerY, H + centerZ);
                rectangle.mVertexA.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, W) + alpha) + centerY);

                rectangle.mVertexB.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
                rectangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
                rectangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexB.mPosition.Set(W + centerX, -L + centerY, -H + centerZ);
                rectangle.mVertexB.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, W) + alpha) + centerY);

                rectangle.mVertexD.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
                rectangle.mVertexD.mTextureCoordinates.Set(0.0f, 1.0f);
                rectangle.mVertexD.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexD.mPosition.Set(W + centerX, L + centerY, H + centerZ);
                rectangle.mVertexD.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, W) + alpha) + centerY);

                rectangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
                rectangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
                rectangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexC.mPosition.Set(W + centerX, L + centerY, -H + centerZ);
                rectangle.mVertexC.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, W) + alpha) + centerY);

                mRectangleList.Add(rectangle);



                // Top
                rectangle.mVertexA.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
                rectangle.mVertexA.mTextureCoordinates.Set(1.0f, 1.0f);
                rectangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexA.mPosition.Set(W + centerX, L + centerY, -H + centerZ);
                rectangle.mVertexA.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, W) + alpha) + centerY);

                rectangle.mVertexB.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
                rectangle.mVertexB.mTextureCoordinates.Set(0.0f, 1.0f);
                rectangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexB.mPosition.Set(-W + centerX, L + centerY, -H + centerZ);
                rectangle.mVertexB.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(-H, -W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(-H, -W) + alpha) + centerY);

                rectangle.mVertexD.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
                rectangle.mVertexD.mTextureCoordinates.Set(1.0f, 0.0f);
                rectangle.mVertexD.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexD.mPosition.Set(W + centerX, L + centerY, H + centerZ);
                rectangle.mVertexD.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, W) + alpha) + centerY);

                rectangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
                rectangle.mVertexC.mTextureCoordinates.Set(0.0f, 0.0f);
                rectangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexC.mPosition.Set(-W + centerX, L + centerY, H + centerZ);
                rectangle.mVertexC.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, -W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, -W) + alpha) + centerY);

                mRectangleList.Add(rectangle);



                // Front
                rectangle.mVertexA.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
                rectangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
                rectangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexA.mPosition.Set(-W + centerX, -L + centerY, H + centerZ);
                rectangle.mVertexA.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, -W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, -W) + alpha) + centerY);

                rectangle.mVertexB.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
                rectangle.mVertexB.mTextureCoordinates.Set(0.0f, 0.0f);
                rectangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexB.mPosition.Set(W + centerX, -L + centerY, H + centerZ);
                rectangle.mVertexB.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, W) + alpha) + centerX, -L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, W) + alpha) + centerY);

                rectangle.mVertexD.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
                rectangle.mVertexD.mTextureCoordinates.Set(0.0f, 0.0f);
                rectangle.mVertexD.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexD.mPosition.Set(-W + centerX, L + centerY, H + centerZ);
                rectangle.mVertexD.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, -W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, -W) + alpha) + centerY);

                rectangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
                rectangle.mVertexC.mTextureCoordinates.Set(0.0f, 0.0f);
                rectangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
                //rectangle.mVertexC.mPosition.Set(W + centerX, L + centerY, H + centerZ);
                rectangle.mVertexC.mPosition.Set(diagonal * (float)Math.Cos(Math.Atan2(H, W) + alpha) + centerX, L + centerZ, diagonal * (float)Math.Sin(Math.Atan2(H, W) + alpha) + centerY);

                mRectangleList.Add(rectangle);


            }
        }

        public void addParallelepipedAsTriangle(float width, float height, float lenght, float centerX, float centerZ, float centerY, Color color)
        {
            Triangle triangle = new Triangle();


            float H = (0.5f * height);
            float L = (0.5f * lenght);
            float W = (0.5f * width);


            // Back

            triangle.mUseColors = true;
            triangle.mUseTexture = false;
            triangle.mUseNormals = false;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexA.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(W + centerX, -L + centerY, -H + centerZ);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(-W + centerX, -L + centerY, -H + centerZ);

            triangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(W + centerX, L + centerY, -H + centerZ);

            mTriangleList.Add(triangle);


            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(W + centerX, L + centerY, -H + centerZ);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(-W + centerX, -L + centerY, -H + centerZ);

            triangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexC.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(-W + centerX, L + centerY, -H + centerZ);

            mTriangleList.Add(triangle);




            // Bottom

            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexA.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(-W + centerX, -L + centerY, H + centerZ);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(-W + centerX, -L + centerY, -H + centerZ);

            triangle.mVertexC.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(W + centerX, -L + centerY, H + centerZ);

            mTriangleList.Add(triangle);


            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(W + centerX, -L + centerY, H + centerZ);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(-W + centerX, -L + centerY, -H + centerZ);

            triangle.mVertexC.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexC.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(W + centerX, -L + centerY, -H + centerZ);

            mTriangleList.Add(triangle);

            // Left

            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexA.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(-W + centerX, -L + centerY, -H + centerZ);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(-W + centerX, -L + centerY, H + centerZ);

            triangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(-W + centerX, L + centerY, -H + centerZ);

            mTriangleList.Add(triangle);


            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(-W + centerX, L + centerY, -H + centerZ);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(-W + centerX, -L + centerY, H + centerZ);

            triangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexC.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(-W + centerX, L + centerY, H + centerZ);

            mTriangleList.Add(triangle);




            // Right

            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexA.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(W + centerX, -L + centerY, H + centerZ);

            triangle.mVertexB.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(W + centerX, -L + centerY, -H + centerZ);

            triangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(W + centerX, L + centerY, H + centerZ);

            mTriangleList.Add(triangle);


            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(W + centerX, L + centerY, H + centerZ);

            triangle.mVertexB.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(W + centerX, -L + centerY, -H + centerZ);

            triangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexC.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(W + centerX, L + centerY, -H + centerZ);

            mTriangleList.Add(triangle);



            // Top

            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexA.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(W + centerX, L + centerY, -H + centerZ);

            triangle.mVertexB.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(-W + centerX, L + centerY, -H + centerZ);

            triangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(W + centerX, L + centerY, H + centerZ);

            mTriangleList.Add(triangle);


            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(W + centerX, L + centerY, H + centerZ);

            triangle.mVertexB.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(-W + centerX, L + centerY, -H + centerZ);

            triangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexC.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(-W + centerX, L + centerY, H + centerZ);

            mTriangleList.Add(triangle);



            // Front

            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexA.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(-W + centerX, -L + centerY, H + centerZ);

            triangle.mVertexB.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(W + centerX, -L + centerY, H + centerZ);

            triangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexC.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(-W + centerX, L + centerY, H + centerZ);

            mTriangleList.Add(triangle);


            //triangle.mUseColors = true;
            //triangle.mUseTexture = true;
            //triangle.mUseNormals = true;
            //triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexA.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexA.mPosition.Set(-W + centerX, L + centerY, H + centerZ);

            triangle.mVertexB.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexB.mColor.Set((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
            triangle.mVertexB.mPosition.Set(W + centerX, -L + centerY, H + centerZ);

            triangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexC.mColor.Set(0, 0, 0, (float)color.A / 255.0f);
            triangle.mVertexC.mPosition.Set(W + centerX, L + centerY, H + centerZ);

            mTriangleList.Add(triangle);

        }

    }




}









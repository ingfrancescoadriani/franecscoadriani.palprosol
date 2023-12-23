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








namespace CSGL12
{

    
    
    
    public class Mesh
    {
        private List<Triangle> mTriangleList = new List<Triangle>();








        private void Mesh_Initialize()
        {
            mTriangleList = new List<Triangle>();
        }








        public Mesh()
        {
            Mesh_Initialize();
        }








        public void Draw(GL gl)
        {
            foreach (Triangle triangle in mTriangleList)
            {
                triangle.Draw(gl);
            }
        }








        public void BuildCube( float width )
        {
            Mesh_Initialize();

            
            Triangle triangle = new Triangle();


            float L = (0.5f * width);


            // Back

            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexA.mColor.Set(1.0f, 0.0f, 0.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(L, -L, -L);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set(0.0f, 0.0f, 0.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(-L, -L, -L);

            triangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexC.mColor.Set(1.0f, 1.0f, 0.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(L, L, -L);

            mTriangleList.Add(triangle);


            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexA.mColor.Set(1.0f, 1.0f, 0.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(L, L, -L);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set(0.0f, 0.0f, 0.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(-L, -L, -L);

            triangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexC.mColor.Set(0.0f, 1.0f, 0.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(-L, L, -L);

            mTriangleList.Add(triangle);




            // Bottom

            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexA.mColor.Set(0.0f, 0.0f, 1.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(-L, -L, L);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexB.mColor.Set(0.0f, 0.0f, 0.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(-L, -L, -L);

            triangle.mVertexC.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexC.mColor.Set(1.0f, 0.0f, 1.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(L, -L, L);

            mTriangleList.Add(triangle);


            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexA.mColor.Set(1.0f, 0.0f, 1.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(L, -L, L);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexB.mColor.Set(0.0f, 0.0f, 0.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(-L, -L, -L);

            triangle.mVertexC.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexC.mColor.Set(1.0f, 0.0f, 0.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(L, -L, -L);

            mTriangleList.Add(triangle);





            // Left

            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(-0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexA.mColor.Set(0.0f, 0.0f, 0.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(-L, -L, -L);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set(0.0f, 0.0f, 1.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(-L, -L, L);

            triangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexC.mColor.Set(0.0f, 1.0f, 0.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(-L, L, -L);

            mTriangleList.Add(triangle);


            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexA.mColor.Set(0.0f, 1.0f, 0.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(-L, L, -L);

            triangle.mVertexB.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set(0.0f, 0.0f, 1.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(-L, -L, L);

            triangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexC.mColor.Set(0.0f, 1.0f, 1.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(-L, L, L);

            mTriangleList.Add(triangle);




            // Right

            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexA.mColor.Set(1.0f, 0.0f, 1.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(L, -L, L);

            triangle.mVertexB.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set(1.0f, 0.0f, 0.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(L, -L, -L);

            triangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexC.mColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(L, L, L);

            mTriangleList.Add(triangle);


            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexA.mColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(L, L, L);

            triangle.mVertexB.mNormal.Set(0.57735f, -0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set(1.0f, 0.0f, 0.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(L, -L, -L);

            triangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexC.mColor.Set(1.0f, 1.0f, 0.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(L, L, -L);

            mTriangleList.Add(triangle);



            // Top

            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexA.mColor.Set(1.0f, 1.0f, 0.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(L, L, -L);

            triangle.mVertexB.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexB.mColor.Set(0.0f, 1.0f, 0.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(-L, L, -L);

            triangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexC.mColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(L, L, L);

            mTriangleList.Add(triangle);


            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexA.mColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(L, L, L);

            triangle.mVertexB.mNormal.Set(-0.57735f, 0.57735f, -0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexB.mColor.Set(0.0f, 1.0f, 0.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(-L, L, -L);

            triangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexC.mColor.Set(0.0f, 1.0f, 1.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(-L, L, L);

            mTriangleList.Add(triangle);



            // Front

            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(-0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 0.0f);
            triangle.mVertexA.mColor.Set(0.0f, 0.0f, 1.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(-L, -L, L);

            triangle.mVertexB.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set(1.0f, 0.0f, 1.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(L, -L, L);

            triangle.mVertexC.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexC.mColor.Set(0.0f, 1.0f, 1.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(-L, L, L);

            mTriangleList.Add(triangle);


            triangle.mUseColors = true;
            triangle.mUseTexture = true;
            triangle.mUseNormals = true;
            triangle.mUseShader = false;

            triangle.mVertexA.mNormal.Set(-0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexA.mTextureCoordinates.Set(0.0f, 1.0f);
            triangle.mVertexA.mColor.Set(0.0f, 1.0f, 1.0f, 1.0f);
            triangle.mVertexA.mPosition.Set(-L, L, L);

            triangle.mVertexB.mNormal.Set(0.57735f, -0.57735f, 0.57735f);
            triangle.mVertexB.mTextureCoordinates.Set(1.0f, 0.0f);
            triangle.mVertexB.mColor.Set(1.0f, 0.0f, 1.0f, 1.0f);
            triangle.mVertexB.mPosition.Set(L, -L, L);

            triangle.mVertexC.mNormal.Set(0.57735f, 0.57735f, 0.57735f);
            triangle.mVertexC.mTextureCoordinates.Set(1.0f, 1.0f);
            triangle.mVertexC.mColor.Set(1.0f, 1.0f, 1.0f, 1.0f);
            triangle.mVertexC.mPosition.Set(L, L, L);

            mTriangleList.Add(triangle);
        }




    }




}









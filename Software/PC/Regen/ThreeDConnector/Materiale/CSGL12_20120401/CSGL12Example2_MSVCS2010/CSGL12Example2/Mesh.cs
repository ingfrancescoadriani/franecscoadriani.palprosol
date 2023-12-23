



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









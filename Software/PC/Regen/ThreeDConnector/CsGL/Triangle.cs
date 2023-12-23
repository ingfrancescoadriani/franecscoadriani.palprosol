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




namespace CSGL12
{




    public struct Triangle
    {
        public Vertex mVertexA;
        public Vertex mVertexB;
        public Vertex mVertexC;

        public bool mUseNormals;
        public bool mUseColors;
        public bool mUseTexture;
        public bool mUseShader;

        public long mTextureGUID;
        public long mShaderGUID;








        public void Draw(GL gl)
        {
            if (true == mUseTexture) { gl.glEnable(GL.GL_TEXTURE_2D); }

            gl.glBegin(GL.GL_TRIANGLES);

            // For each of the three vertices of this triangle, specify
            // the texture coordinates, normal vector, vertex color, and 
            // finally the vertex position.

            if (true == mUseTexture) { gl.glTexCoord2f(mVertexA.mTextureCoordinates.u, mVertexA.mTextureCoordinates.v); }
            if (true == mUseNormals) { gl.glNormal3f(mVertexA.mNormal.x, mVertexA.mNormal.y, mVertexA.mNormal.z); }
            if (true == mUseColors) { gl.glColor4f(mVertexA.mColor.r, mVertexA.mColor.g, mVertexA.mColor.b, mVertexA.mColor.a); }
            gl.glVertex3f(mVertexA.mPosition.x, mVertexA.mPosition.y, mVertexA.mPosition.z);

            if (true == mUseTexture) { gl.glTexCoord2f(mVertexB.mTextureCoordinates.u, mVertexB.mTextureCoordinates.v); }
            if (true == mUseNormals) { gl.glNormal3f(mVertexB.mNormal.x, mVertexB.mNormal.y, mVertexB.mNormal.z); }
            if (true == mUseColors) { gl.glColor4f(mVertexB.mColor.r, mVertexB.mColor.g, mVertexB.mColor.b, mVertexB.mColor.a); }
            gl.glVertex3f(mVertexB.mPosition.x, mVertexB.mPosition.y, mVertexB.mPosition.z);

            if (true == mUseTexture) { gl.glTexCoord2f(mVertexC.mTextureCoordinates.u, mVertexC.mTextureCoordinates.v); }
            if (true == mUseNormals) { gl.glNormal3f(mVertexC.mNormal.x, mVertexC.mNormal.y, mVertexC.mNormal.z); }
            if (true == mUseColors) { gl.glColor4f(mVertexC.mColor.r, mVertexC.mColor.g, mVertexC.mColor.b, mVertexC.mColor.a); }
            gl.glVertex3f(mVertexC.mPosition.x, mVertexC.mPosition.y, mVertexC.mPosition.z);

            gl.glDrawArrays(GL.GL_LINE_LOOP, 0, 6);

            gl.glEnd();


            if (true == mUseTexture) gl.glDisable(GL.GL_TEXTURE_2D);
        }








        public void DrawOnlyIfTexturedAndWithoutGLBeginOrGLEnableTexture(GL gl)
        {
            if (false == mUseTexture) { return; }

            // For each of the three vertices of this triangle, specify
            // the texture coordinates, normal vector, vertex color, and 
            // finally the vertex position.

            gl.glTexCoord2f(mVertexA.mTextureCoordinates.u, mVertexA.mTextureCoordinates.v);
            if (true == mUseNormals) { gl.glNormal3f(mVertexA.mNormal.x, mVertexA.mNormal.y, mVertexA.mNormal.z); }
            if (true == mUseColors) { gl.glColor4f(mVertexA.mColor.r, mVertexA.mColor.g, mVertexA.mColor.b, mVertexA.mColor.a); }
            gl.glVertex3f(mVertexA.mPosition.x, mVertexA.mPosition.y, mVertexA.mPosition.z);

            gl.glTexCoord2f(mVertexB.mTextureCoordinates.u, mVertexB.mTextureCoordinates.v);
            if (true == mUseNormals) { gl.glNormal3f(mVertexB.mNormal.x, mVertexB.mNormal.y, mVertexB.mNormal.z); }
            if (true == mUseColors) { gl.glColor4f(mVertexB.mColor.r, mVertexB.mColor.g, mVertexB.mColor.b, mVertexB.mColor.a); }
            gl.glVertex3f(mVertexB.mPosition.x, mVertexB.mPosition.y, mVertexB.mPosition.z);

            gl.glTexCoord2f(mVertexC.mTextureCoordinates.u, mVertexC.mTextureCoordinates.v);
            if (true == mUseNormals) { gl.glNormal3f(mVertexC.mNormal.x, mVertexC.mNormal.y, mVertexC.mNormal.z); }
            if (true == mUseColors) { gl.glColor4f(mVertexC.mColor.r, mVertexC.mColor.g, mVertexC.mColor.b, mVertexC.mColor.a); }
            gl.glVertex3f(mVertexC.mPosition.x, mVertexC.mPosition.y, mVertexC.mPosition.z);
        }








        public void DrawOnlyIfUntexturedAndWithoutGLBeginOrGLEnableTexture(GL gl)
        {
            if (true == mUseTexture) { return; }

            // For each of the three vertices of this triangle, specify
            // the normal vector, vertex color, and finally the vertex position.

            if (true == mUseNormals) { gl.glNormal3f(mVertexA.mNormal.x, mVertexA.mNormal.y, mVertexA.mNormal.z); }
            if (true == mUseColors) { gl.glColor4f(mVertexA.mColor.r, mVertexA.mColor.g, mVertexA.mColor.b, mVertexA.mColor.a); }
            gl.glVertex3f(mVertexA.mPosition.x, mVertexA.mPosition.y, mVertexA.mPosition.z);

            if (true == mUseNormals) { gl.glNormal3f(mVertexB.mNormal.x, mVertexB.mNormal.y, mVertexB.mNormal.z); }
            if (true == mUseColors) { gl.glColor4f(mVertexB.mColor.r, mVertexB.mColor.g, mVertexB.mColor.b, mVertexB.mColor.a); }
            gl.glVertex3f(mVertexB.mPosition.x, mVertexB.mPosition.y, mVertexB.mPosition.z);

            if (true == mUseNormals) { gl.glNormal3f(mVertexC.mNormal.x, mVertexC.mNormal.y, mVertexC.mNormal.z); }
            if (true == mUseColors) { gl.glColor4f(mVertexC.mColor.r, mVertexC.mColor.g, mVertexC.mColor.b, mVertexC.mColor.a); }
            gl.glVertex3f(mVertexC.mPosition.x, mVertexC.mPosition.y, mVertexC.mPosition.z);
        }




    }




}













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








namespace CSGL12
{




    public struct Triangle
    {
        public TriangleVertex mVertexA;
        public TriangleVertex mVertexB;
        public TriangleVertex mVertexC;

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









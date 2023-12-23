



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




    public struct TextureCoordinates2f
    {
        public float u;
        public float v;








        public void Clear()
        {
            u = 0.0f;
            v = 0.0f;
        }








        public void Set(float fu, float fv)
        {
            u = fu;
            v = fv;
        }





    }




}









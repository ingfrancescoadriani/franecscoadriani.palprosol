



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




    public struct Color4f
    {
        public float r;
        public float g;
        public float b;
        public float a;








        public void Clear()
        {
            r = 0.0f;
            g = 0.0f;
            b = 0.0f;
            a = 0.0f;
        }








        public void ClipComponentValuesTo0To1Range()
        {
            if (r < 0.0f) { r = 0.0f; }
            if (g < 0.0f) { g = 0.0f; }
            if (b < 0.0f) { b = 0.0f; }
            if (a < 0.0f) { a = 0.0f; }

            if (r > 1.0f) { r = 1.0f; }
            if (g > 1.0f) { g = 1.0f; }
            if (b > 1.0f) { b = 1.0f; }
            if (a > 1.0f) { a = 1.0f; }
        }








        public void Set(float fr, float fg, float fb, float fa)
        {
            r = fr;
            g = fg;
            b = fb;
            a = fa;

            ClipComponentValuesTo0To1Range();
        }




    }




}









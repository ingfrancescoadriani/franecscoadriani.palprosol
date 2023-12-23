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









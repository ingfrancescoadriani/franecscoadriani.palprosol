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




    public struct Vector3f
    {
        public float x;
        public float y;
        public float z;








        public void Clear()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
        }








        public Vector3f(float fx, float fy, float fz)
        {
            x = fx;
            y = fy;
            z = fz;
        }

        
        
        
        
        
        
        
        public static Vector3f Zero()
        {
            Vector3f result = new Vector3f( 0.0f, 0.0f, 0.0f );
            return (result);
        }

        
        
        
        
        
        
        
        public void Set(float fx, float fy, float fz)
        {
            x = fx;
            y = fy;
            z = fz;
        }








        public float Length()
        {
            float length = (float) System.Math.Sqrt(x * x + y * y + z * z);
            return (length);
        }








        public Vector3f Normalize()
        {
            float length = Length();

            if (length <= 1.0e-10f)
            {
                return (new Vector3f( 1.0f, 0.0f, 0.0f ));
            }

            float factor = 1.0f / length;

            Vector3f result = new Vector3f(factor * x, factor * y, factor * z);

            return (result);
        }








        public static float Dot(Vector3f a, Vector3f b)
        {
            return (a.x * b.x + a.y * b.y + a.z * b.z);
        }








        public static Vector3f Cross(Vector3f a, Vector3f b)
        {
            Vector3f result = new Vector3f
            (
                (a.y * b.z - b.y * a.z),
                (a.z * b.x - b.z * a.x),
                (a.x * b.y - b.x * a.y)
            );

            return (result);
        }








        public static Vector3f operator *(Vector3f a, float scale)
        {
            Vector3f result = new Vector3f
            (
                scale * a.x,
                scale * a.y,
                scale * a.z
            );

            return (result);
        }








        public static Vector3f operator *(float scale, Vector3f a)
        {
            Vector3f result = new Vector3f
            (
                scale * a.x,
                scale * a.y,
                scale * a.z
            );

            return (result);
        }








        public static Vector3f operator +(Vector3f a, Vector3f b)
        {
            Vector3f result = new Vector3f
            (
                a.x + b.x,
                a.y + b.y,
                a.z + b.z
            );

            return (result);
        }








        public static Vector3f operator -(Vector3f a, Vector3f b)
        {
            Vector3f result = new Vector3f
            (
                a.x - b.x,
                a.y - b.y,
                a.z - b.z
            );

            return (result);
        }








        public static Vector3f operator -(Vector3f b)
        {
            Vector3f result = new Vector3f
            (
                -b.x,
                -b.y,
                -b.z
            );

            return (result);
        }








        public static float Length(Vector3f a)
        {
            float length = (float) System.Math.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z); 
            return (length);
        }








        public static Vector3f Normalize(Vector3f v)
        {
            float length = v.Length();

            if (length <= 1.0e-10f)
            {
                return (new Vector3f(1.0f, 0.0f, 0.0f));
            }

            float factor = 1.0f / length;

            Vector3f result = new Vector3f(factor * v.x, factor * v.y, factor * v.z);

            return (result);
        }




    }




}








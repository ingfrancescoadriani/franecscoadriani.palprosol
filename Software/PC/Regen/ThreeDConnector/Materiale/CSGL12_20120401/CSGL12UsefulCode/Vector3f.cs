



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








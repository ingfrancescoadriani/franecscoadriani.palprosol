



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




    public struct Matrix4x4f
    {
        public float m11;
        public float m12;
        public float m13;
        public float m14;

        public float m21;
        public float m22;
        public float m23;
        public float m24;

        public float m31;
        public float m32;
        public float m33;
        public float m34;

        public float m41;
        public float m42;
        public float m43;
        public float m44;








        public Matrix4x4f
        (
            float fm11, float fm12, float fm13, float fm14,
            float fm21, float fm22, float fm23, float fm24,
            float fm31, float fm32, float fm33, float fm34,
            float fm41, float fm42, float fm43, float fm44
        )
        {
            m11 = fm11; m12 = fm12; m13 = fm13; m14 = fm14;
            m21 = fm21; m22 = fm22; m23 = fm23; m24 = fm24;
            m31 = fm31; m32 = fm32; m33 = fm33; m34 = fm34;
            m41 = fm41; m42 = fm42; m43 = fm43; m44 = fm44;
        }








        public void Set
        (
            float fm11, float fm12, float fm13, float fm14,
            float fm21, float fm22, float fm23, float fm24,
            float fm31, float fm32, float fm33, float fm34,
            float fm41, float fm42, float fm43, float fm44
        )
        {
            m11 = fm11; m12 = fm12; m13 = fm13; m14 = fm14;
            m21 = fm21; m22 = fm22; m23 = fm23; m24 = fm24;
            m31 = fm31; m32 = fm32; m33 = fm33; m34 = fm34;
            m41 = fm41; m42 = fm42; m43 = fm43; m44 = fm44;
        }








        public static Matrix4x4f Identity()
        {
            return
            (
                new Matrix4x4f
                (
                    1.0f, 0.0f, 0.0f, 0.0f,
                    0.0f, 1.0f, 0.0f, 0.0f,
                    0.0f, 0.0f, 1.0f, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                )
            );
        }








        public Vector3f Transform(Vector3f v)
        {
            return
            (
                new Vector3f
                (
                    m11 * v.x + m12 * v.y + m13 * v.z + m14,
                    m21 * v.x + m22 * v.y + m23 * v.z + m24,
                    m31 * v.x + m32 * v.y + m33 * v.z + m34
                )
            );
        }








        public static Matrix4x4f LookAt(Vector3f from, Vector3f to, Vector3f up)
        {
            Vector3f forward = (to - from);

            Vector3f zdir = (-(forward.Normalize()));

            Vector3f xdir = Vector3f.Cross(up, zdir).Normalize();

            Vector3f ydir = Vector3f.Cross(zdir, xdir).Normalize();

            return
            (
                new Matrix4x4f
                (
                xdir.x, xdir.y, xdir.z, (-(Vector3f.Dot(xdir, from))),
                ydir.x, ydir.y, ydir.z, (-(Vector3f.Dot(ydir, from))),
                zdir.x, zdir.y, zdir.z, (-(Vector3f.Dot(zdir, from))),
                0.0f, 0.0f, 0.0f, 1.0f
                )
            );
        }








        public Matrix4x4f Translate(Vector3f v)
        {
            return
            (
                new Matrix4x4f
                (
                    m11, m12, m13, m14 + v.x,
                    m21, m22, m23, m24 + v.y,
                    m31, m32, m33, m34 + v.z,
                    m41, m42, m43, m44
                )
            );
        }








        public Matrix4x4f SetTranslation(Vector3f v)
        {
            return
            (
                new Matrix4x4f
                (
                    m11, m12, m13, v.x,
                    m21, m22, m23, v.y,
                    m31, m32, m33, v.z,
                    m41, m42, m43, m44
                )
            );
        }








        public static Matrix4x4f SetToRotateX(float angle)
        {
            float sine = (float)System.Math.Sin((System.Math.PI / 180.0) * angle);
            float cosine = (float)System.Math.Cos((System.Math.PI / 180.0) * angle);

            return
            (
                new Matrix4x4f
                (
                    1.0f, 0.0f, 0.0f, 0.0f,
                    0.0f, cosine, -sine, 0.0f,
                    0.0f, sine, cosine, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                )
            );
        }








        public static Matrix4x4f SetToRotateY(float angle)
        {
            float sine = (float)System.Math.Sin((System.Math.PI / 180.0) * angle);
            float cosine = (float)System.Math.Cos((System.Math.PI / 180.0) * angle);

            return
            (
                new Matrix4x4f
                (
                    cosine, 0.0f, sine, 0.0f,
                    0.0f, 1.0f, 0.0f, 0.0f,
                    -sine, 0.0f, cosine, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                )
            );
        }








        public static Matrix4x4f SetToRotateZ(float angle)
        {
            float sine = (float)System.Math.Sin((System.Math.PI / 180.0) * angle);
            float cosine = (float)System.Math.Cos((System.Math.PI / 180.0) * angle);

            return
            (
                new Matrix4x4f
                (
                    cosine, -sine, 0.0f, 0.0f,
                    sine, cosine, 0.0f, 0.0f,
                    0.0f, 0.0f, 1.0f, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                )
            );
        }








        public static Matrix4x4f operator *(Matrix4x4f a, Matrix4x4f b)
        {
            return
            (
                new Matrix4x4f
                (
                    a.m11 * b.m11 + a.m12 * b.m21 + a.m13 * b.m31 + a.m14 * b.m41,
                    a.m11 * b.m12 + a.m12 * b.m22 + a.m13 * b.m32 + a.m14 * b.m42,
                    a.m11 * b.m13 + a.m12 * b.m23 + a.m13 * b.m33 + a.m14 * b.m43,
                    a.m11 * b.m14 + a.m12 * b.m24 + a.m13 * b.m34 + a.m14 * b.m44,

                    a.m21 * b.m11 + a.m22 * b.m21 + a.m23 * b.m31 + a.m24 * b.m41,
                    a.m21 * b.m12 + a.m22 * b.m22 + a.m23 * b.m32 + a.m24 * b.m42,
                    a.m21 * b.m13 + a.m22 * b.m23 + a.m23 * b.m33 + a.m24 * b.m43,
                    a.m21 * b.m14 + a.m22 * b.m24 + a.m23 * b.m34 + a.m24 * b.m44,

                    a.m31 * b.m11 + a.m32 * b.m21 + a.m33 * b.m31 + a.m34 * b.m41,
                    a.m31 * b.m12 + a.m32 * b.m22 + a.m33 * b.m32 + a.m34 * b.m42,
                    a.m31 * b.m13 + a.m32 * b.m23 + a.m33 * b.m33 + a.m34 * b.m43,
                    a.m31 * b.m14 + a.m32 * b.m24 + a.m33 * b.m34 + a.m34 * b.m44,

                    a.m41 * b.m11 + a.m42 * b.m21 + a.m43 * b.m31 + a.m44 * b.m41,
                    a.m41 * b.m12 + a.m42 * b.m22 + a.m43 * b.m32 + a.m44 * b.m42,
                    a.m41 * b.m13 + a.m42 * b.m23 + a.m43 * b.m33 + a.m44 * b.m43,
                    a.m41 * b.m14 + a.m42 * b.m24 + a.m43 * b.m34 + a.m44 * b.m44
                )
            );
        }








        public Matrix4x4f Transpose()
        {
            return
            (
                new Matrix4x4f
                (
                    m11, m21, m31, m41,
                    m12, m22, m32, m42,
                    m13, m23, m33, m43,
                    m14, m24, m34, m44
                )
            );
        }








        public float Determinant()
        {
            return
            (
                  m11 * m22 * m33 * m44
                + m11 * m23 * m34 * m42
                + m11 * m24 * m32 * m43

                + m12 * m21 * m34 * m43
                + m12 * m23 * m31 * m44
                + m12 * m24 * m33 * m41

                + m13 * m21 * m32 * m44
                + m13 * m22 * m34 * m41
                + m13 * m24 * m31 * m42

                + m14 * m21 * m33 * m42
                + m14 * m22 * m31 * m43
                + m14 * m23 * m32 * m41

                - m11 * m22 * m34 * m43
                - m11 * m23 * m32 * m44
                - m11 * m24 * m33 * m42

                - m12 * m21 * m33 * m44
                - m12 * m23 * m34 * m41
                - m12 * m24 * m31 * m43

                - m13 * m21 * m34 * m42
                - m13 * m22 * m31 * m44
                - m13 * m24 * m32 * m41

                - m14 * m21 * m32 * m43
                - m14 * m22 * m33 * m41
                - m14 * m23 * m31 * m42
            );
        }








        public Matrix4x4f Inverse()
        {
            float determinant = Determinant();

            if (determinant <= 1.0e-10f)
            {
                return (Matrix4x4f.Identity());
            }

            float factor = (1.0f / determinant);


            float b11 =
              m22 * m33 * m44
            + m23 * m34 * m42
            + m24 * m32 * m43
            - m22 * m34 * m43
            - m23 * m32 * m44
            - m24 * m33 * m42;

            float b12 =
              m12 * m34 * m43
            + m13 * m32 * m44
            + m14 * m33 * m42
            - m12 * m33 * m44
            - m13 * m34 * m42
            - m14 * m32 * m43;

            float b13 =
              m12 * m23 * m44
            + m13 * m24 * m42
            + m14 * m22 * m43
            - m12 * m24 * m43
            - m13 * m22 * m44
            - m14 * m23 * m42;

            float b14 =
              m12 * m24 * m33
            + m13 * m22 * m34
            + m14 * m23 * m32
            - m12 * m23 * m34
            - m13 * m24 * m32
            - m14 * m22 * m33;



            float b21 =
              m21 * m34 * m43
            + m23 * m31 * m44
            + m24 * m33 * m41
            - m21 * m33 * m44
            - m23 * m34 * m41
            - m24 * m31 * m43;

            float b22 =
              m11 * m33 * m44
            + m13 * m34 * m41
            + m14 * m31 * m43
            - m11 * m34 * m43
            - m13 * m31 * m44
            - m14 * m33 * m41;

            float b23 =
              m11 * m24 * m43
            + m13 * m21 * m44
            + m14 * m23 * m41
            - m11 * m23 * m44
            - m13 * m24 * m41
            - m14 * m21 * m43;

            float b24 =
              m11 * m23 * m34
            + m13 * m24 * m31
            + m14 * m21 * m33
            - m11 * m24 * m33
            - m13 * m21 * m34
            - m14 * m23 * m31;



            float b31 =
              m21 * m32 * m44
            + m22 * m34 * m41
            + m24 * m31 * m42
            - m21 * m34 * m42
            - m22 * m31 * m44
            - m24 * m32 * m41;

            float b32 =
              m11 * m34 * m42
            + m12 * m31 * m44
            + m14 * m32 * m41
            - m11 * m32 * m44
            - m12 * m34 * m41
            - m14 * m31 * m42;

            float b33 =
              m11 * m22 * m44
            + m12 * m24 * m41
            + m14 * m21 * m42
            - m11 * m24 * m42
            - m12 * m21 * m44
            - m14 * m22 * m41;

            float b34 =
              m11 * m24 * m32
            + m12 * m21 * m34
            + m14 * m22 * m31
            - m11 * m22 * m34
            - m12 * m24 * m31
            - m14 * m21 * m32;



            float b41 =
              m21 * m33 * m42
            + m22 * m31 * m43
            + m23 * m32 * m41
            - m21 * m32 * m43
            - m22 * m33 * m41
            - m23 * m31 * m42;

            float b42 =
              m11 * m32 * m43
            + m12 * m33 * m41
            + m13 * m31 * m42
            - m11 * m33 * m42
            - m12 * m31 * m43
            - m13 * m32 * m41;

            float b43 =
              m11 * m23 * m42
            + m12 * m21 * m43
            + m13 * m22 * m41
            - m11 * m22 * m43
            - m12 * m23 * m41
            - m13 * m21 * m42;

            float b44 =
              m11 * m22 * m33
            + m12 * m23 * m31
            + m13 * m21 * m32
            - m11 * m23 * m32
            - m12 * m21 * m33
            - m13 * m22 * m31;


            return
            (
                new Matrix4x4f
                (
                    factor * b11, factor * b12, factor * b13, factor * b14,
                    factor * b21, factor * b22, factor * b23, factor * b24,
                    factor * b31, factor * b32, factor * b33, factor * b34,
                    factor * b41, factor * b42, factor * b43, factor * b44
                )
            );
        }




    }




}








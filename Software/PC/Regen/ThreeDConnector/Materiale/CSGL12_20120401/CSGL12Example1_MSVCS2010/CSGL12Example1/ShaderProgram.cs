



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








namespace CSGL12
{




    public class ShaderProgram
    {
        public int mShaderProgramHandle = 0;











        public virtual String ShaderProgramFriendlyName()
        {
            return ("");
        }








        public virtual String VertexShaderSourceName()
        {
            return ("");
        }

        public virtual String VertexShaderSource()
        {
            return ("");
        }







        public virtual String FragmentShaderSourceName()
        {
            return ("");
        }

        public virtual String FragmentShaderSource()
        {
            return ("");
        }










        public string mVertexShaderInfoLog = "";
        public string mFragmentShaderInfoLog = "";
        public string mProgramInfoLog = "";

        public string GetVertexShaderInfoLog()
        {
            return(mVertexShaderInfoLog);
        }

        public string GetFragmentShaderInfoLog()
        {
            return(mFragmentShaderInfoLog);
        }

        public string GetProgramInfoLog()
        {
            return (mProgramInfoLog);
        }


        private static string GetShaderInfoLogText( GL gl, int shaderObjectHandle )
        {
            // NOTE: This function is for vertex and fragment shader objects, and NOT
            // the overall shader *program* object (use GetProgramInfoLogText() for that).

            // Get the required size for a buffer to hold the current shader "info log" string.
            int info_log_length = 0;
            int[] getShaderivParameters = new int[1];
            gl.glGetShaderiv(shaderObjectHandle, GL.GL_INFO_LOG_LENGTH, getShaderivParameters);
            info_log_length = getShaderivParameters[0];

            System.Text.StringBuilder sbInfoLog = new System.Text.StringBuilder(info_log_length + 16); // Added 16 characters for paranoia
            int info_log_retrieved_length = 0;
            gl.glGetShaderInfoLog(shaderObjectHandle, info_log_length, ref info_log_retrieved_length, sbInfoLog);
            string infoLogText = sbInfoLog.ToString();

            return (infoLogText);
        }


        private static string GetProgramInfoLogText(GL gl, int shaderObjectHandle)
        {
            // NOTE: This function is for the overall shader *program* object, and NOT
            // vertex and fragment shader objects (use GetShaderInfoLogText() for those).

            // Get the required size for a buffer to hold the current shader program "info log" string.
            int info_log_length = 0;
            int[] getShaderivParameters = new int[1];
            gl.glGetShaderiv(shaderObjectHandle, GL.GL_INFO_LOG_LENGTH, getShaderivParameters);
            info_log_length = getShaderivParameters[0];

            System.Text.StringBuilder sbInfoLog = new System.Text.StringBuilder(info_log_length + 16); // Added 16 characters for paranoia
            int info_log_retrieved_length = 0;
            gl.glGetProgramInfoLog(shaderObjectHandle, info_log_length, ref info_log_retrieved_length, sbInfoLog);
            string infoLogText = sbInfoLog.ToString();

            return (infoLogText);
        }









        public static void ShaderProgram_Create_WithInfoLogs
        (
            GL gl,
            String vertexShaderSource,
            String fragmentShaderSource,
            ref int shaderProgramHandle,
            ref string vertexShaderInfoLog,
            ref string fragmentShaderInfoLog,
            ref string programInfoLog
        )
        {
            shaderProgramHandle = 0;

            if (false == gl.bglCreateProgramObjectARB)
            {
                return;
            }

            shaderProgramHandle = gl.glCreateProgramObjectARB();




            int vertexShaderObjectHandle = 0;

            if (vertexShaderSource.Length > 0)
            {
                vertexShaderObjectHandle = gl.glCreateShaderObjectARB(GL.GL_VERTEX_SHADER_ARB);

                String[] vertexShaderSourceStringArray = null;
                vertexShaderSourceStringArray = new String[1];
                vertexShaderSourceStringArray[0] = vertexShaderSource;

                gl.glShaderSourceARB(vertexShaderObjectHandle, 1, vertexShaderSourceStringArray, null);
                gl.glCompileShaderARB(vertexShaderObjectHandle);

                vertexShaderInfoLog = GetShaderInfoLogText(gl, vertexShaderObjectHandle);

                gl.glAttachObjectARB(shaderProgramHandle, vertexShaderObjectHandle);

                // From the OpenGL orange book, chapter 7, section 7.4, p.168
                // A programming tip that might be useful in keeping things orderly is to
                // delete shader objects as soon as they have been attached to a program
                // object.  They won't be deleted at this time, but they will be flagged for deletion 
                // when they are no longer referenced.  To clean up later, the application
                // only needs to delete the program object.  All the attached shader objects will
                // be automatically detached, and, because they are flagged for deletion, they
                // will be automatically deleted at that time as well.
                gl.glDeleteObjectARB(vertexShaderObjectHandle);
            }





            int fragmentShaderObjectHandle = 0;

            if (fragmentShaderSource.Length > 0)
            {
                fragmentShaderObjectHandle = gl.glCreateShaderObjectARB(GL.GL_FRAGMENT_SHADER_ARB);

                String[] fragmentShaderSourceStringArray = null;
                fragmentShaderSourceStringArray = new String[1];
                fragmentShaderSourceStringArray[0] = fragmentShaderSource;

                gl.glShaderSourceARB(fragmentShaderObjectHandle, 1, fragmentShaderSourceStringArray, null);
                gl.glCompileShaderARB(fragmentShaderObjectHandle);

                fragmentShaderInfoLog = GetShaderInfoLogText(gl, fragmentShaderObjectHandle);

                gl.glAttachObjectARB(shaderProgramHandle, fragmentShaderObjectHandle);

                // From the OpenGL orange book, chapter 7, section 7.4, p.168
                // A programming tip that might be useful in keeping things orderly is to
                // delete shader objects as soon as they have been attached to a program
                // object.  They won't be deleted at this time, but they will be flagged for deletion 
                // when they are no longer referenced.  To clean up later, the application
                // only needs to delete the program object.  All the attached shader objects will
                // be automatically detached, and, because they are flagged for deletion, they
                // will be automatically deleted at that time as well.
                gl.glDeleteObjectARB(fragmentShaderObjectHandle);
            }


            // Link the program 
            gl.glLinkProgramARB(shaderProgramHandle);


            programInfoLog = GetProgramInfoLogText(gl, shaderProgramHandle);



            // EXAMPLE OF USING THE RESULTING SHADER PROGRAM:
            //
            // Set as active shader:
            // 
            //   gl.glUseProgramObjectARB( shaderProgramHandle );
            //
            // Setting uniforms for the active shader:
            //
            //   int shaderVarX = 0;
            //   shaderVarX = gl.glGetUniformLocation( shaderProgramHandle, "VarX" );
            //   gl.glUniform1f( shaderVarX, 4.0f );

        }








        public static void ShaderProgram_Create
        (
            GL gl,
            String vertexShaderSource,
            String fragmentShaderSource,
            ref int shaderProgramHandle
        )
        {
            string vertexShaderInfoLog = "";
            string fragmentShaderInfoLog = "";
            string programInfoLog = "";

            ShaderProgram_Create_WithInfoLogs
            (
                gl,
                vertexShaderSource,
                fragmentShaderSource,
                ref shaderProgramHandle,
                ref vertexShaderInfoLog,
                ref fragmentShaderInfoLog,
                ref programInfoLog
            );
        }





        public static void ShaderProgram_Select(GL gl, int shaderProgramHandle)
        {
            if (true == gl.bglUseProgramObjectARB)
            {
                gl.glUseProgramObjectARB(shaderProgramHandle);
            }
        }








        public static void ShaderProgram_Delete(GL gl, int shaderProgramHandle)
        {
            if (true == gl.bglUseProgramObjectARB)
            {
                // From the OpenGL orange book, chapter 7, section 7.4, p.168
                // A programming tip that might be useful in keeping things orderly is to
                // delete shader objects as soon as they have been attached to a program
                // object.  They won't be deleted at this time, but they will be flagged for deletion 
                // when they are no longer referenced.  To clean up later, the application
                // only needs to delete the program object.  All the attached shader objects will
                // be automatically detached, and, because they are flagged for deletion, they
                // will be automatically deleted at that time as well.
                gl.glDeleteObjectARB(shaderProgramHandle);
            }
        }








        public void ShaderProgramCreate(GL gl)
        {
            string vertexShaderInfoLog = "";
            string fragmentShaderInfoLog = "";
            string programInfoLog = "";

            ShaderProgram_Create_WithInfoLogs
            (
                gl,
                VertexShaderSource(),
                FragmentShaderSource(),
                ref mShaderProgramHandle,
                ref vertexShaderInfoLog,
                ref fragmentShaderInfoLog,
                ref programInfoLog
            );


            mVertexShaderInfoLog = vertexShaderInfoLog;
            mFragmentShaderInfoLog = fragmentShaderInfoLog;
            mProgramInfoLog = programInfoLog;


            GetVariableHandles(gl);

            SetVariableValuesToDefaults(gl);
        }





        public void ShaderProgramCreateWithMessageBoxForError(GL gl)
        {
            ShaderProgramCreate(gl);

            if ((mVertexShaderInfoLog.Length > 0) || (mFragmentShaderInfoLog.Length > 0) || (mProgramInfoLog.Length > 0))
            {
                string caption = "Shader Information Log(s) for: " + ShaderProgramFriendlyName();
                string message = "";

                if (mVertexShaderInfoLog.Length > 0)
                {
                    message += "VERTEX shader info log for \"" + VertexShaderSourceName() + "\" :\n\n" + mVertexShaderInfoLog + "\n\n";
                }

                if (mFragmentShaderInfoLog.Length > 0)
                {
                    message += "FRAGMENT shader info log for \"" + FragmentShaderSourceName() + "\" :\n\n" + mFragmentShaderInfoLog + "\n\n";
                }

                if (mProgramInfoLog.Length > 0)
                {
                    message += "PROGRAM info log for \"" + ShaderProgramFriendlyName() + "\" :\n\n" + mProgramInfoLog + "\n\n";
                }

                System.Windows.Forms.MessageBox.Show(message, caption);
            }
        }






        public virtual void GetVariableHandles(GL gl)
        {
        }








        public virtual void SetVariableValuesToDefaults(GL gl)
        {
            if (0 == mShaderProgramHandle)
            {
                return;
            }

            // Select the program so that we can set variables.
            ShaderProgram_Select(gl, mShaderProgramHandle);

            // ...

            // Deselect the shader program.
            ShaderProgram_Select(gl, 0);
        }








        public virtual void DemonstrateModificationOfVariables(GL gl, double absoluteTimeSeconds, double previousFrameTimeSeconds)
        {
            if (0 == mShaderProgramHandle)
            {
                return;
            }

            // Select the program so that we can set variables.
            ShaderProgram_Select(gl, mShaderProgramHandle);

            // ...

            // Deselect the shader program.
            ShaderProgram_Select(gl, 0);
        }








        public void Destroy(GL gl)
        {
            // GRSupport.SupportShaderProgramCreate(...) creates shader objects
            // and attaches them to a program object, and then "deletes" the shader
            // objects (reducing their reference count).  Therefore, 
            // GRSupport.SupportShaderProgramDelete(...) need only delete the
            // program object (which implicitly detaches shader objects, and
            // exposes them to automatic deletion).
            ShaderProgram_Delete(gl, mShaderProgramHandle);

            mShaderProgramHandle = 0;
        }








        public void Select(GL gl)
        {
            // If shaders are supported, use the shader...
            if ((true == gl.bglUseProgramObjectARB) && (mShaderProgramHandle != 0))
            {
                ShaderProgram_Select(gl, mShaderProgramHandle);
            }
        }




    }




}








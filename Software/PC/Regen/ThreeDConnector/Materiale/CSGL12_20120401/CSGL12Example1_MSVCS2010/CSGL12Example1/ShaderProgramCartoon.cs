



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




    public class ShaderProgramCartoon : ShaderProgram
    {






        public override String ShaderProgramFriendlyName()
        {
            return ("Cartoon");
        }







        public override String VertexShaderSourceName()
        {
            return ("Cartoon vertex shader (source code embedded in application)");
        }

        public override String VertexShaderSource()
        {
            return
            (
                @"
                // Simple cartoon ('toon') vertex shader

                varying vec3 Normal;
                varying vec3 LightDirection;

                void main()
                {
                    LightDirection = normalize(vec3(gl_LightSource[0].position));
                    Normal = normalize(gl_NormalMatrix * gl_Normal);

                    gl_Position = ftransform();
                }
                "
            );
        }







        public override String FragmentShaderSourceName()
        {
            return ("Cartoon fragment shader (source code embedded in application)");
        }

        public override String FragmentShaderSource()
        {
            return
            (
                @"
                // Simple cartoon ('toon') fragment shader

                varying vec3 Normal;
                varying vec3 LightDirection;

                void main()
                {
                    float intensity;
                    vec3 n;
                    vec4 color;

                    n = normalize(Normal);

                    intensity = max(dot(LightDirection,n),0.0);

                    if (intensity > 0.98)
                    {
                      color = vec4(0.8,0.8,0.8,1.0);
                    }
                    else if (intensity > 0.5)
                    {
                        color = vec4(0.4,0.4,0.8,1.0);
                    }
                    else if (intensity > 0.25)
                    {
                        color = vec4(0.2,0.2,0.4,1.0);
                    }
                    else
                    {
                        color = vec4(0.1,0.1,0.1,1.0);
                    }

                    gl_FragColor = color;
                }
                "
            );
        }








        // Handles for vertex shader uniform values
        
        // (None)



        // Handles for fragment shader uniform values
        
        // (None)








        public override void GetVariableHandles(GL gl)
        {
            // Get the handles for the vertex shader uniform values.
        
            // (None)



            // Get the handles for the fragment shader uniform values.
            
            // (None)
        }








        public override void SetVariableValuesToDefaults(GL gl)
        {
            // Select the program so that we can set variables.
            
            ShaderProgram.ShaderProgram_Select(gl, mShaderProgramHandle);


            // Set the vertex shader uniform values to defaults.
            
            // (None)


            // Set the fragment shader uniform values to defaults.
            
            // (None)


            // Deselect the shader program.

            ShaderProgram.ShaderProgram_Select(gl, 0);
        }








        public override void DemonstrateModificationOfVariables
        (
            GL gl, 
            double absoluteTimeSeconds, 
            double previousFrameTimeSeconds
        )
        {
            // Select the program so that we can set variables.

            ShaderProgram.ShaderProgram_Select(gl, mShaderProgramHandle);


            // ...


            // Deselect the shader program.

            ShaderProgram.ShaderProgram_Select(gl, 0);
        }




    }




}









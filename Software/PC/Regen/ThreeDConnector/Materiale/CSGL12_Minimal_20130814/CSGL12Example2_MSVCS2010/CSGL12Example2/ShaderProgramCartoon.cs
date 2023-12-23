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









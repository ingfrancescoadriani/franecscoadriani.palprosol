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




    public class ShaderProgramWood : ShaderProgram
    {






        public override String ShaderProgramFriendlyName()
        {
            return ("Wood");
        }





        public override String VertexShaderSourceName()
        {
            return ("Wood vertex shader (source code embedded in application)");
        }
        
        public override String VertexShaderSource()
        {
            return
            (
                @"
                // Simple vertex shader for wood
                // Author: John Kessenich
                // Copyright (c) 2002-2004 3Dlabs Inc. Ltd.

                uniform vec3 LightPosition;
                uniform float Scale;

                varying float LightIntensity;
                varying vec3 Position;

                void main(void)
                {
                    vec4 pos = gl_ModelViewMatrix * gl_Vertex;
                    Position = vec3(gl_Vertex) * Scale;
                    vec3 tnorm = normalize(gl_NormalMatrix * gl_Normal);
                    LightIntensity = max(dot(normalize(LightPosition - vec3(pos)), tnorm), 0.0) * 1.5;
                    gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                }            
                "
            );
        }







        public override String FragmentShaderSourceName()
        {
            return ("Wood fragment shader (source code embedded in application)");
        }

        public override String FragmentShaderSource()
        {
            return
            (
                @"
                // Simple fragment shader for wood
                // Author: John Kessenich
                // Copyright (c) 2002-2004 3Dlabs Inc. Ltd.

                uniform float GrainSizeReciprocal;
                uniform vec3  DarkColor;
                uniform vec3  Spread;

                varying float LightIntensity; 
                varying vec3 Position;

                void main(void)
                {
                    // Cheap noise
                    vec3 scaledPosition = Position * 0.001;
                    vec3 location = scaledPosition;
                    vec3 floorvec = vec3(floor(10.0 * scaledPosition.x), 0.0, floor(10.0 * scaledPosition.z));
                    vec3 noise = scaledPosition * 10.0 - floorvec - 0.5;
                    noise *= noise;
                    location += noise * 0.12;

                    // Distance from axis
                    float dist = location.x * location.x + location.z * location.z;
                    float grain = dist * GrainSizeReciprocal;

                    // Grain effects as function of distance
                    float brightness = fract(grain);
                    if (brightness > 0.5)
                    { 
                        brightness = (1.0 - brightness);
                    }
                    vec3 color = DarkColor + brightness * Spread;
                    
                    brightness = fract(grain * 7.0);    
                    if (brightness > 0.5)
                    { 
                        brightness = 1.0 - brightness;
                    }
                    color -= brightness * Spread;

                    // Also as a function of lines parallel to the axis
                    brightness = fract(grain * 47.0) * 0.60;
                    float line = fract(scaledPosition.z + scaledPosition.x);
                    float snap = floor(line * 20.0) * (1.0/20.0);
                    if (line < snap + 0.006)
                    {
                        color -= brightness * Spread;
                    }

                    // Apply lighting effects from vertex processor
                    color = clamp(color * LightIntensity, 0.0, 1.0); 
                    gl_FragColor = vec4(color, 1.0);
                }            
                "
            );
        }








        // Handles for vertex shader uniform values

        private int mHandleUniform3fLightPosition = 0;
        private int mHandleUniform1fScale = 0;


        // Handles for fragment shader uniform values

        private int mHandleUniform1fGrainSizeReciprocal = 0;
        private int mHandleUniform3fDarkColor = 0;
        private int mHandleUniform3fSpread = 0;








        public override void GetVariableHandles(GL gl)
        {
            // Get the handles for the vertex shader uniform values.
            
            mHandleUniform3fLightPosition = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "LightPosition");
            mHandleUniform1fScale = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "Scale");


            // Get the handles for the fragment shader uniform values.
            
            mHandleUniform1fGrainSizeReciprocal = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "GrainSizeReciprocal");
            mHandleUniform3fDarkColor = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "DarkColor");
            mHandleUniform3fSpread = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "Spread");
        }








        public override void SetVariableValuesToDefaults(GL gl)
        {
            // Select the program so that we can set variables.

            ShaderProgram.ShaderProgram_Select(gl, mShaderProgramHandle);


            // Set the vertex shader uniform values to defaults.

            gl.glUniform3fARB(mHandleUniform3fLightPosition, 400.0f, 400.0f, 400.0f);
            gl.glUniform1fARB(mHandleUniform1fScale, 10.0f);

            
            // Set the fragment shader uniform values to defaults.
            
            gl.glUniform1fARB(mHandleUniform1fGrainSizeReciprocal, 0.4f);
            gl.glUniform3fARB(mHandleUniform3fDarkColor, 0.6f, 0.3f, 0.1f);
            gl.glUniform3fARB(mHandleUniform3fSpread, 0.15f, 0.075f, 0.0f);

            
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


            // Set the vertex shader uniform values to defaults.
            
            // gl.glUniform3fARB( mHandleUniform3fLightPosition, 400.0f, 400.0f, 400.0f );
            gl.glUniform1fARB(mHandleUniform1fScale, 10.0f + 8.0f * (float)Math.Sin(0.125 * Math.PI * absoluteTimeSeconds));


            // Set the fragment shader uniform values to defaults.
            
            gl.glUniform1fARB(mHandleUniform1fGrainSizeReciprocal, 0.4f + 0.3f * (float)Math.Sin(0.5 * Math.PI * absoluteTimeSeconds));
            // gl.glUniform3fARB( mHandleUniform3fDarkColor, 0.6f, 0.3f, 0.1f );
            // gl.glUniform3fARB( mHandleUniform3fSpread, 0.15f, 0.075f, 0.0f );


            // Deselect the shader program.
            
            ShaderProgram.ShaderProgram_Select(gl, 0);
        }




    }




}









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




    public class ShaderProgramBrick : ShaderProgram
    {


        public override String ShaderProgramFriendlyName()
        {
            return ("Brick");
        }



        public override String VertexShaderSourceName()
        {
            return ("Brick vertex shader (source code embedded in application)");
        }

        public override String VertexShaderSource()
        {
            return
            (
                @"
                // From the OpenGL orange book, pp. 151

                uniform vec3 LightPosition;
                uniform float SpecularContribution; // e.g., 0.3
                uniform float DiffuseContribution; // e.g., 1.0 - SpecularContribution

                varying float LightIntensity;
                varying vec3 MCposition;

                void main(void)
                {
                    vec3 ecPosition = vec3 (gl_ModelViewMatrix * gl_Vertex);
                    vec3 tnorm = normalize(gl_NormalMatrix * gl_Normal);
                    vec3 lightVec = normalize(LightPosition - ecPosition);
                    vec3 reflectVec = reflect(-lightVec, tnorm);
                    vec3 viewVec = normalize(-ecPosition);

                    float diffuse = max(dot(lightVec, tnorm), 0.0);
                    float spec = 0.0;

                    if (diffuse > 0.0)
                    {
                        spec = max(dot(reflectVec, viewVec), 0.0);
                        spec = pow(spec, 16.0);
                    }

                    LightIntensity = 1.0; //DiffuseContribution * diffuse
                                         //SpecularContribution * spec;

                    MCposition = gl_Vertex.xyz;

                    gl_Position = ftransform();
                }
                "
            );
        }








        public override String FragmentShaderSourceName()
        {
            return ("Brick fragment shader (source code embedded in application)");
        }

        public override String FragmentShaderSource()
        {
            return
            (
                @"
                // From the OpenGL orange book, pp. 155

                uniform vec3 BrickColor;
                uniform vec3 MortarColor;
                uniform vec3 BrickSize;
                uniform vec3 BrickPct;

                varying float LightIntensity;
                varying vec3 MCposition;


                void main(void)
                {
                    vec3 color;
                    vec3 position;

                    position = MCposition / BrickSize;

                    float brick = 1.0;

                    if (fract(position.z - 0.25) > 0.9)
                    {
                        brick = 0.0;
                    }
                    
                    if (fract(0.5 * (position.z - 0.25)) > 0.5)
                    {
                        position.y += 0.5;
                    }

                    if (fract(position.y - 0.25) > 0.9)
                    {
                        brick = 0.0;
                    }

                    if (fract(0.5 * (position.y - 0.25)) > 0.5)
                    {
                        position.x += 0.5;
                    }
                    
                    if (fract(position.x - 0.25) > 0.9)
                    {
                        brick = 0.0;
                    }               

                    color = mix(MortarColor, BrickColor, brick);   
                    color *= LightIntensity;

                    gl_FragColor = vec4( color, 1.0 );
                }
                "
            );
        }








        // Handles for vertex shader uniform values

        private int mHandleUniform3fLightPosition = 0;
        private int mHandleUniform1fSpecularContribution = 0;
        private int mHandleUniform1fDiffuseContribution = 0;





        // Handles for fragment shader uniform values
        
        private int mHandleUniform3fBrickColor = 0;
        private int mHandleUniform3fMortarColor = 0;
        private int mHandleUniform3fBrickSize = 0;
        private int mHandleUniform3fBrickPct = 0;








        public override void GetVariableHandles(GL gl)
        {
            // Get the handles for the vertex shader uniform values.

            mHandleUniform3fLightPosition = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "LightPosition");
            mHandleUniform1fSpecularContribution = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "SpecularContribution");
            mHandleUniform1fDiffuseContribution = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "DiffuseContribution");


            // Get the handles for the fragment shader uniform values.
            
            mHandleUniform3fBrickColor = (int) gl.glGetUniformLocationARB( mShaderProgramHandle, "BrickColor");
            mHandleUniform3fMortarColor = (int) gl.glGetUniformLocationARB( mShaderProgramHandle, "MortarColor");
            mHandleUniform3fBrickSize = (int) gl.glGetUniformLocationARB( mShaderProgramHandle, "BrickSize");
            mHandleUniform3fBrickPct = (int) gl.glGetUniformLocationARB( mShaderProgramHandle, "BrickPct");
        }








        public override void SetVariableValuesToDefaults(GL gl)
        {
            // Select the program so that we can set variables.

            ShaderProgram.ShaderProgram_Select(gl, mShaderProgramHandle);


            // Set the vertex shader uniform values to defaults.
            
            gl.glUniform3fARB(mHandleUniform3fLightPosition, 400.0f, 400.0f, 400.0f);
            gl.glUniform1fARB(mHandleUniform1fSpecularContribution, 0.3f);
            gl.glUniform1fARB(mHandleUniform1fDiffuseContribution, 0.7f);


            // Set the fragment shader uniform values to defaults.
            
            gl.glUniform3fARB(mHandleUniform3fBrickColor, 0.6f, 0.3f, 0.1f);
            gl.glUniform3fARB(mHandleUniform3fMortarColor, 0.8f, 0.8f, 0.8f);
            gl.glUniform3fARB(mHandleUniform3fBrickSize, 100.0f, 50.0f, 50.0f);
            gl.glUniform3fARB(mHandleUniform3fBrickPct, 0.9f, 0.9f, 0.9f);


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
            
            
            // float rate = 0.03125f;
            // float sx = 100.0f + 50.0f * (float) System.Math.Sin( 3.0 * rate * Math.PI * absoluteTimeSeconds );
            // float sy =  50.0f + 25.0f * (float) System.Math.Sin( 5.0 * rate * Math.PI * absoluteTimeSeconds );
            // float sz =  50.0f + 25.0f * (float) System.Math.Sin( 7.0 * rate * Math.PI * absoluteTimeSeconds );
            // gr.glUniform3fARB( mHandleUniform3fBrickSize, sx, sy, sz );


            float r1 = 0.5f + 0.45f * (float) Math.Sin(7.0 * 0.03125 * Math.PI * absoluteTimeSeconds);
            float g1 = 0.5f + 0.45f * (float) Math.Sin(9.0 * 0.03125 * Math.PI * absoluteTimeSeconds);
            float b1 = 0.5f + 0.45f * (float) Math.Sin(11.0 * 0.03125 * Math.PI * absoluteTimeSeconds);

            float r2 = 0.5f + 0.45f * (float) Math.Sin(7.0 * 0.125 * Math.PI * absoluteTimeSeconds);
            float g2 = 0.5f + 0.45f * (float) Math.Sin(9.0 * 0.125 * Math.PI * absoluteTimeSeconds);
            float b2 = 0.5f + 0.45f * (float) Math.Sin(11.0 * 0.125 * Math.PI * absoluteTimeSeconds);

            gl.glUniform3fARB(mHandleUniform3fBrickColor, r1, g1, b1);
            gl.glUniform3fARB(mHandleUniform3fMortarColor, r2, g2, b2);


            // Deselect the shader program.

            ShaderProgram.ShaderProgram_Select(gl, 0);
        }




    }




}








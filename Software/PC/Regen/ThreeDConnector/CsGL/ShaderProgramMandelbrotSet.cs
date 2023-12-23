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




    public class ShaderProgramMandelbrotSet : ShaderProgram
    {






        public override String ShaderProgramFriendlyName()
        {
            return ("Mandelbrot");
        }





        public override String VertexShaderSourceName()
        {
            return ("Mandelbrot vertex shader (source code embedded in application)");
        }

        public override String VertexShaderSource()
        {
            return
            (
                @"
                // Vertex shader for drawing the Mandelbrot set

                uniform vec3 LightPosition;
                uniform float SpecularContribution;
                uniform float DiffuseContribution;
                uniform float Shininess;

                varying float LightIntensity;
                varying vec3  Position;



                void main(void)
                {
                    vec3 ecPosition = vec3 (gl_ModelViewMatrix * gl_Vertex);
                    vec3 tnorm      = normalize(gl_NormalMatrix * gl_Normal);
                    vec3 lightVec   = normalize(LightPosition - ecPosition);
                    vec3 reflectVec = reflect(-lightVec, tnorm);
                    vec3 viewVec    = normalize(-ecPosition);
                    float spec      = max(dot(reflectVec, viewVec), 0.0);
                    spec            = pow(spec, Shininess);

                    LightIntensity  = 1.0; 
                        // DiffuseContribution * 
                        // max(dot(lightVec, tnorm), 0.0) +
                        // SpecularContribution * spec;
                    Position        = vec3(gl_MultiTexCoord0 - 0.5) * 5.0;
                    gl_Position     = ftransform();
                }
                "
            );
        }








        public override String FragmentShaderSourceName()
        {
            return ("Mandelbrot fragment shader (source code embedded in application)");
        }

        public override String FragmentShaderSource()
        {
            return
            (
                @"
                // Fragment shader for drawing the Mandelbrot set

                uniform float Zoom;
                uniform vec2  Center;
                uniform vec4  InnerColor;
                uniform vec4  OuterColor1;
                uniform vec4  OuterColor2;

                varying vec3  Position;
                varying float LightIntensity;



                void main(void)
                {
                    float   real  = Position.x * Zoom + Center.x;
                    float   imag  = Position.y * Zoom + Center.y;
                    float   cReal = real;   // Change this line...
                    float   cImag = imag;   // ...and this one to get a Julia set
                    float   r2    = 0.0;
                    int     iter;


                    // perform procedural mandelbrot
                    for (iter = 0; iter < 15; ++iter)
                    {
                        float tempreal = real;

                        real = (tempreal * tempreal) - (imag * imag) + cReal;
                        imag = 2.0 * tempreal * imag + cImag;
                        r2   = (real * real) + (imag * imag);
                    }


                    // HACK : Use the real and imaginary components to create
                    // a nice looking effect.
                    // (Set parameter to 0.0 to get a plain looking Mandelbrot set.)
                    float parameter = (imag + real);
                    parameter = 0.0;
                    

                    // Base the color on the number of iterations

                    vec4 color;

                    if (r2 < 4.0)
                    {
                        color = InnerColor.rgba;
                    }
                    else
                    {     
                        color = mix(OuterColor1.rgba, OuterColor2.rgba, parameter);
                    }

                    color *= LightIntensity;

                    gl_FragColor = vec4 (clamp(color, 0.0, 1.0));
                }
                "
            );
        }








        // Handles for vertex shader uniform values

        private int mHandleUniform3fLightPosition = 0;
        private int mHandleUniform1fSpecularContribution = 0;
        private int mHandleUniform1fDiffuseContribution = 0;
        private int mHandleUniform1fShininess = 0;


        // Handles for fragment shader uniform values
        
        private int mHandleUniform1fZoom = 0;
        private int mHandleUniform2fCenter = 0;
        private int mHandleUniform4fInnerColor = 0;
        private int mHandleUniform4fOuterColor1 = 0;
        private int mHandleUniform4fOuterColor2 = 0;








        public override void GetVariableHandles(GL gl)
        {
            // Get the handles for the vertex shader uniform values.
            
            mHandleUniform3fLightPosition = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "LightPosition");
            mHandleUniform1fSpecularContribution = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "SpecularContribution");
            mHandleUniform1fDiffuseContribution = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "DiffuseContribution");
            mHandleUniform1fShininess = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "Shininess");


            // Get the handles for the fragment shader uniform values.

            mHandleUniform1fZoom = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "Zoom");
            mHandleUniform2fCenter = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "Center");
            mHandleUniform4fInnerColor = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "InnerColor");
            mHandleUniform4fOuterColor1 = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "OuterColor1");
            mHandleUniform4fOuterColor2 = (int) gl.glGetUniformLocationARB(mShaderProgramHandle, "OuterColor2");
        }








        public override void SetVariableValuesToDefaults(GL gl)
        {
            // Select the program so that we can set variables.

            ShaderProgram.ShaderProgram_Select(gl, mShaderProgramHandle);


            // Set the vertex shader uniform values to defaults.

            gl.glUniform3fARB(mHandleUniform3fLightPosition, 400.0f, 400.0f, 400.0f);
            gl.glUniform1fARB(mHandleUniform1fSpecularContribution, 0.3f);
            gl.glUniform1fARB(mHandleUniform1fDiffuseContribution, 0.7f);
            gl.glUniform1fARB(mHandleUniform1fShininess, 16.0f);


            // Set the fragment shader uniform values to defaults.

            gl.glUniform1fARB(mHandleUniform1fZoom, 0.5f);
            gl.glUniform2fARB(mHandleUniform2fCenter, -0.5f, 0.0f);
            gl.glUniform4fARB(mHandleUniform4fInnerColor, 0.7f, 0.7f, 0.7f, 0.5f); // rgba
            gl.glUniform4fARB(mHandleUniform4fOuterColor1, 1.0f, 0.0f, 0.0f, 0.5f); // rgba
            gl.glUniform4fARB(mHandleUniform4fOuterColor2, 0.0f, 1.0f, 0.0f, 0.5f); // rgba


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


            float x    = -0.5f + 0.25f * (float) Math.Sin( 3.0 * 0.0125 * Math.PI * absoluteTimeSeconds );
            float y    =  0.0f + 0.25f * (float) Math.Sin( 5.0 * 0.0125 * Math.PI * absoluteTimeSeconds );
            float zoom =  1.0f + 0.70f * (float) Math.Sin( 0.05 * Math.PI * absoluteTimeSeconds );

            float r1 = 0.8f;
            float g1 = 0.8f;
            float b1 = 0.8f;

            float r2 = 0.8f;
            float g2 = 0.8f;
            float b2 = 0.8f;

            //float r1   =  0.5f + 0.45f * (float) Math.Sin(7.0 * 0.03125 * Math.PI * absoluteTimeSeconds);
            //float g1   =  0.5f + 0.45f * (float) Math.Sin(9.0 * 0.03125 * Math.PI * absoluteTimeSeconds);
            //float b1   =  0.5f + 0.45f * (float) Math.Sin(11.0 * 0.03125 * Math.PI * absoluteTimeSeconds);

            //float r2   =  0.5f + 0.45f * (float) Math.Sin(7.0 * 0.125 * Math.PI * absoluteTimeSeconds);
            //float g2   =  0.5f + 0.45f * (float) Math.Sin(9.0 * 0.125 * Math.PI * absoluteTimeSeconds);
            //float b2   =  0.5f + 0.45f * (float) Math.Sin(11.0 * 0.125 * Math.PI * absoluteTimeSeconds);

            
            // Set the vertex shader uniform values to defaults.

            // gl.glUniform3fARB( mHandleUniform3fLightPosition, 400.0f, 400.0f, 400.0f );
            // gl.glUniform1fARB( mHandleUniform1fSpecularContribution, 0.3f );
            // gl.glUniform1fARB( mHandleUniform1fDiffuseContribution, 0.7f );
            // gl.glUniform1fARB( mHandleUniform1fShininess, 16.0f );


            // Set the fragment shader uniform values to defaults.

            gl.glUniform1fARB(mHandleUniform1fZoom, zoom);
            gl.glUniform2fARB(mHandleUniform2fCenter, x, y);
            // gl.glUniform4fARB( mHandleUniform4fInnerColor, 0.0f, 0.0f, 0.0f, 0.5f ); // rgba
            gl.glUniform4fARB(mHandleUniform4fOuterColor1, r1, g1, b1, 1.0f); // rgba
            gl.glUniform4fARB(mHandleUniform4fOuterColor2, r2, g2, b2, 1.0f); // rgba


            // Deselect the shader program.

            ShaderProgram.ShaderProgram_Select(gl, 0);
        }




    }




}









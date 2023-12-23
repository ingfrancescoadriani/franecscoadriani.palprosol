#region BSD License
/*
 BSD License
Copyright (c) 2002, Randy Ridge, The CsGL Development Team
http://csgl.sourceforge.net/
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

1. Redistributions of source code must retain the above copyright notice,
   this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

3. Neither the name of The CsGL Development Team nor the names of its
   contributors may be used to endorse or promote products derived from this
   software without specific prior written permission.

   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
   "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
   LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
   FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
   COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
   INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
   BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
   LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
   CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
   LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
   ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
   POSSIBILITY OF SUCH DAMAGE.
 */
#endregion BSD License

#region Original Credits / License
/*
 *		This Code Was Created By Jeff Molofee 2000
 *		A HUGE Thanks To Fredric Echols For Cleaning Up
 *		And Optimizing The Base Code, Making It More Flexible!
 *		If You've Found This Code Useful, Please Let Me Know.
 *		Visit My Site At nehe.gamedev.net
 */
#endregion Original Credits / License

using CsGL.Basecode;
using System.Reflection;

#region AssemblyInfo
[assembly: AssemblyCompany("The CsGL Development Team (http://csgl.sourceforge.net)")]
[assembly: AssemblyCopyright("2002 The CsGL Development Team (http://csgl.sourceforge.net)")]
[assembly: AssemblyDescription("NeHe Lesson 02")]
[assembly: AssemblyProduct("NeHe Lesson 02")]
[assembly: AssemblyTitle("NeHe Lesson 02")]
[assembly: AssemblyVersion("1.0.0.0")]
#endregion AssemblyInfo

namespace NeHeLessons {
	/// <summary>
	/// NeHe Lesson 02 -- Your First Polygon (http://nehe.gamedev.net)
	/// Implemented In C# By The CsGL Development Team (http://csgl.sourceforge.net)
	/// </summary>
	public sealed class NeHeLesson02 : Model {
		// --- Fields ---
		#region Public Properties
		/// <summary>
		/// Lesson title.
		/// </summary>
		public override string Title {
			get {
				return "NeHe Lesson 02 -- Your First Polygon";
			}
		}

		/// <summary>
		/// Lesson description.
		/// </summary>
		public override string Description {
			get {
				return "Using the framework built in lesson 01, we will now add code to create a triangle and a square on the screen.  Almost everything you create in OpenGL will be based on these two simple primitives.  You will learn about the X, Y, and Z axes, as well as, translation up, down, left, right, into and out of the screen.";
			}
		}

		/// <summary>
		/// Lesson URL.
		/// </summary>
		public override string Url {
			get {
				return "http://nehe.gamedev.net/tutorials/lesson.asp?l=02";
			}
		}
		#endregion Public Properties

		// --- Entry Point ---
		#region Main()
		/// <summary>
		/// Application's entry point, runs this NeHe lesson.
		/// </summary>
		public static void Main() {														// Entry Point
			App.Run(new NeHeLesson02());												// Run Our NeHe Lesson As A Windows Forms Application
		}
		#endregion Main()

		// --- Basecode Methods ---
		#region Draw()
		/// <summary>
		/// Draws NeHe Lesson 02 scene.
		/// </summary>
		public override void Draw() {													// Here's Where We Do All The Drawing
			glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);							// Clear Screen And Depth Buffer
			glLoadIdentity();															// Reset The Current Modelview Matrix

			glTranslatef(-1.5f, 0.0f, -6.0f);											// Move Left 1.5 Units And Into The Screen 6.0

			glBegin(GL_TRIANGLES);														// Drawing Using Triangles
				glVertex3f( 0.0f,  1.0f, 0.0f);											// Top
				glVertex3f(-1.0f, -1.0f, 0.0f);											// Bottom Left
				glVertex3f( 1.0f, -1.0f, 0.0f);											// Bottom Right
			glEnd();																	// Finished Drawing The Triangles

			glTranslatef(3.0f, 0.0f, 0.0f);												// Move Right 3 Units

			glBegin(GL_QUADS);															// Draw A Quad
				glVertex3f(-1.0f,  1.0f, 0.0f);											// Top Left
				glVertex3f( 1.0f,  1.0f, 0.0f);											// Top Right
				glVertex3f( 1.0f, -1.0f, 0.0f);											// Bottom Right
				glVertex3f(-1.0f, -1.0f, 0.0f);											// Bottom Left
			glEnd();																	// Done Drawing The Quad
		}
		#endregion Draw()
	}
}
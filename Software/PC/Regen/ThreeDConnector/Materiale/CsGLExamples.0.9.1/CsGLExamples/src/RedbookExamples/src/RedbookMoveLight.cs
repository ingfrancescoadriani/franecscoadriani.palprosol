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
 * Copyright (c) 1993-1997, Silicon Graphics, Inc.
 * ALL RIGHTS RESERVED 
 * Permission to use, copy, modify, and distribute this software for 
 * any purpose and without fee is hereby granted, provided that the above
 * copyright notice appear in all copies and that both the copyright notice
 * and this permission notice appear in supporting documentation, and that 
 * the name of Silicon Graphics, Inc. not be used in advertising
 * or publicity pertaining to distribution of the software without specific,
 * written prior permission. 
 *
 * THE MATERIAL EMBODIED ON THIS SOFTWARE IS PROVIDED TO YOU "AS-IS"
 * AND WITHOUT WARRANTY OF ANY KIND, EXPRESS, IMPLIED OR OTHERWISE,
 * INCLUDING WITHOUT LIMITATION, ANY WARRANTY OF MERCHANTABILITY OR
 * FITNESS FOR A PARTICULAR PURPOSE.  IN NO EVENT SHALL SILICON
 * GRAPHICS, INC.  BE LIABLE TO YOU OR ANYONE ELSE FOR ANY DIRECT,
 * SPECIAL, INCIDENTAL, INDIRECT OR CONSEQUENTIAL DAMAGES OF ANY
 * KIND, OR ANY DAMAGES WHATSOEVER, INCLUDING WITHOUT LIMITATION,
 * LOSS OF PROFIT, LOSS OF USE, SAVINGS OR REVENUE, OR THE CLAIMS OF
 * THIRD PARTIES, WHETHER OR NOT SILICON GRAPHICS, INC.  HAS BEEN
 * ADVISED OF THE POSSIBILITY OF SUCH LOSS, HOWEVER CAUSED AND ON
 * ANY THEORY OF LIABILITY, ARISING OUT OF OR IN CONNECTION WITH THE
 * POSSESSION, USE OR PERFORMANCE OF THIS SOFTWARE.
 * 
 * US Government Users Restricted Rights 
 * Use, duplication, or disclosure by the Government is subject to
 * restrictions set forth in FAR 52.227.19(c)(2) or subparagraph
 * (c)(1)(ii) of the Rights in Technical Data and Computer Software
 * clause at DFARS 252.227-7013 and/or in similar or successor
 * clauses in the FAR or the DOD or NASA FAR Supplement.
 * Unpublished-- rights reserved under the copyright laws of the
 * United States.  Contractor/manufacturer is Silicon Graphics,
 * Inc., 2011 N.  Shoreline Blvd., Mountain View, CA 94039-7311.
 *
 * OpenGL(R) is a registered trademark of Silicon Graphics, Inc.
 */
#endregion Original Credits / License

using CsGL.Basecode;
using System.Data;
using System.Reflection;

#region AssemblyInfo
[assembly: AssemblyCompany("The CsGL Development Team (http://csgl.sourceforge.net)")]
[assembly: AssemblyCopyright("2002 The CsGL Development Team (http://csgl.sourceforge.net)")]
[assembly: AssemblyDescription("Redbook Movelight")]
[assembly: AssemblyProduct("Redbook Movelight")]
[assembly: AssemblyTitle("Redbook Movelight")]
[assembly: AssemblyVersion("1.0.0.0")]
#endregion AssemblyInfo

namespace RedbookExamples {
	/// <summary>
	/// Redbook Movelight -- Model Transformation Lighting (http://www.opengl.org/developers/code/examples/redbook/redbook.html)
	/// Implemented In C# By The CsGL Development Team (http://csgl.sourceforge.net)
	/// </summary>
	public sealed class RedbookMovelight : Model {
		// --- Fields ---
		#region Private Fields
		private static int spin = 0;
		private static float[] position = {0.0f, 0.0f, 1.5f, 1.0f};
		#endregion Private Fields

		#region Public Properties
		/// <summary>
		/// Example title.
		/// </summary>
		public override string Title {
			get {
				return "Redbook Movelight -- Model Transformation Lighting";
			}
		}

		/// <summary>
		/// Example description.
		/// </summary>
		public override string Description {
			get {
				return "This program demonstrates when to issue lighting and transformation commands to render a model with a light which is moved by a modeling transformation (rotate or translate).  The light position is reset after the modeling transformation is called.  The eye position does not change.  A sphere is drawn using a grey material characteristic.  A single light source illuminates the object.  Interaction: pressing the left mouse button alters the modeling transformation (x rotation) by 30 degrees.  The scene is then redrawn with the light in a new position.";
			}
		}

		/// <summary>
		/// Example URL.
		/// </summary>
		public override string Url {
			get {
				return "http://www.opengl.org/developers/code/examples/redbook/redbook.html";
			}
		}
		#endregion Public Properties

		// --- Entry Point ---
		#region Main()
		/// <summary>
		/// Application's entry point, runs this Redbook example.
		/// </summary>
		public static void Main() {														// Entry Point
			App.Run(new RedbookMovelight());											// Run Our Example As A Windows Forms Application
		}
		#endregion Main()

		// --- Basecode Methods ---
		#region Initialize()
		/// <summary>
		/// Overrides OpenGL's initialization.
		/// </summary>
		public override void Initialize() {
			// Initialize material property, light source, lighting model, and depth buffer
			glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
			glShadeModel(GL_SMOOTH);
			glEnable(GL_LIGHTING);
			glEnable(GL_LIGHT0);
			glEnable(GL_DEPTH_TEST);
		}
		#endregion Initialize()

		#region Draw()
		/// <summary>
		/// Draws Redbook Movelight scene.
		/// </summary>
		public override void Draw() {													// Here's Where We Do All The Drawing
			// Here is where the light position is reset after the modeling
			// transformation (glRotated) is called.  This places the light
			// at a new position in world coordinates.  The cube represents
			// the position of the light.

			glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
			glPushMatrix();
				gluLookAt(0.0f, 0.0f, 5.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);

				glPushMatrix();
					glRotated((double) spin, 1.0, 0.0, 0.0);
					glLightfv(GL_LIGHT0, GL_POSITION, position);

					glTranslated(0.0, 0.0, 1.5);
					glDisable(GL_LIGHTING);
					glColor3f(0.0f, 1.0f, 1.0f);
					glutWireCube(0.1f);
					glEnable(GL_LIGHTING);
				glPopMatrix();

				glutSolidTorus(0.275f, 0.85f, 8, 15);
			glPopMatrix();
			glFlush();
		}
		#endregion Draw()

		#region InputHelp()
		/// <summary>
		/// Overrides default input help, supplying example-specific help information.
		/// </summary>
		public override void InputHelp() {
			base.InputHelp();															// Set Up The Default Input Help

			DataRow dataRow;															// Row To Add

			dataRow = InputHelpDataTable.NewRow();										// Left Mouse Button - Spin
			dataRow["Input"] = "Left Mouse Button";
			dataRow["Effect"] = "Spin";
			dataRow["Current State"] = "";
			InputHelpDataTable.Rows.Add(dataRow);
		}
		#endregion InputHelp()

		#region ProcessInput()
		/// <summary>
		/// Overrides default input handling, adding example-specific input handling.
		/// </summary>
		public override void ProcessInput() {
			base.ProcessInput();														// Handle The Default Basecode Keys

			if(Model.Mouse.LeftButton) {												// If Left Mouse Button Is Being Pressed
				Model.Mouse.LeftButton = false;											// Mark It As Handled
				spin = (spin + 30) % 360;												// Spin
			}
		}
		#endregion ProcessInput()

		#region Reshape(int width, int height)
		/// <summary>
		/// Overrides OpenGL reshaping.
		/// </summary>
		/// <param name="width">New width.</param>
		/// <param name="height">New height.</param>
		public override void Reshape(int width, int height) {							// Resize And Initialize The GL Window
			glViewport(0, 0, width, height);
			glMatrixMode(GL_PROJECTION);
			glLoadIdentity();
			gluPerspective(40.0f, (float) width / (float) height, 1.0f, 20.0f);
			glMatrixMode(GL_MODELVIEW);
		}
		#endregion Reshape(int width, int height)
	}
}
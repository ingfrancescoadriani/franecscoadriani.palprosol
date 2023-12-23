


MINOR UPDATE TO CSGL12


2012 April 1
Colin Fahey
http://colinfahey.com
http://colinfahey.com/csharp_wrapper_for_opengl/csharp_wrapper_for_opengl_en.html


This update to the CSGL code fixes a few
minor problems that existed in the 2009
release of the CSGL12 package.

The DLL files, and sample EXE files, were 
recompiled using Microsoft Visual C# 2010
Express.  This might affect attempts to
use the DLLs when working with Microsoft
Visual C# 2008, but I think simply recompiling
the source code for the GL DLL and the 
GL Control DLL would be sufficient to make
them work with the 2008 compiler again.

The signatures for the following items
in the GL DLL have changed:

  Delegates (not likely to be used directly by the user):

    DglGetString_I, 
    DglGetShaderInfoLog_IIrIS,
    DglGetProgramInfoLog_IIrIS

  Functions:

    glGetString
    glGetShaderInfoLog
    glGetProgramInfoLog

If you are using any of those functions in your code,
then a minor code change might be necessary.


I have wanted to make a few simple changes
(such as the ones I will mention below) 
to CSGL12 for several years, but the changes 
I wanted to make were minor and I was busy 
with other projects.  CSGL12 has worked 
reliably for many years, without any modification
necessary when Windows Vista and Windows 7
replaced Windows XP.

Unfortunately, I am still busy with other
projects, and I was unable to take the time
to go through the *documentation* to make
slight modifications to reflect the very 
minor changes I made in this update.

Also, I hope to release a C# wrapper for
OpenGL 3.3+/4.x, totally abandoning ALL 
legacy fixed pipeline symbols (enumerations
and functions), within the next year or so.
This planned C# wrapper would be a separate
project, and I would continue to host the
existing fixed-pipeline version of the 
wrapper on my web site.  OpenGL 3.3+/4.x
is a different paradigm, requiring more 
setup for some basic operations.  I *might*
choose to make the GL DLL for this new 
wrapper a "C" code DLL instead of a C# DLL,
in an effort to improve performance and 
smooth out the often awkward C# marshaling
of some parameters.  I would likely offer
both a pure-C# bridge to OpenGL and a version
which used a slim "C"-code DLL instead, leaving
the user to decide which best served their
purpose.  (Of course the C-code would be
supplied, and ready to be compiled with the
free Microsoft Visual C++ 2010 Express compiler,
or whichever compilers are in vogue.)


DETAILS OF THIS UPDATE

Here are some of the enhancements made in this
informal update to CSGL12:


  * glGetString() now works!  I'm sorry that it
    was broken.  The use of this function is
    demonstrated in the example programs, displaying
    GL_VERSION and GL_VENDOR and GL_EXTENSIONS
    on a texture.


  * glGetShaderInfoLog() and glGetProgramInfoLog()
    now work, and are demonstrated in the example
    programs.
    
    If there is a bug in the VERTEX shader source
    code, or in the FRAGMENT shader source code,
    or in the overall PROGRAM, then a message box
    will appear reporting the errors.  You can 
    try this out by editing the shader program
    source code for any of the shaders in the
    example programs (Mandelbrot, Wood, Brick,
    Cartoon), and adding a syntax error somewhere,
    and then recompiling and executing the program.
    A message box should appear at run-time, 
    showing the shader code errors.

  * In the example code involving using GDI+
    to draw text and shapes to a Bitmap, and
    copying the Bitmap to an OpenGL texture,
    I have made the following changes:

       (a) The update frequency is now every
           frame instead of every 64 frames;
           This looks great on my computer,
           but might slow the frame rate on
           slower CPU/GPU combinations.  You
           can easily modify the example code
           to make the texture updates happen less
           often, or not at all.

       (b) The texture used for this purpose
           now has mip-mapping *disabled*, which
           makes the texture update much faster!
           But keep in mind that this is a special
           situation, where the thing being drawn
           is a simple quadrangle at "actual size"
           (where texels are the same size as 
           pixels on the screen).

       (c) The GDI+ text drawing now appears much
           better, due to adding the following
           line of code:

           g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

           The reason for the improved appearance is
           interesting.  "ClearType" text drawing was
           introduced in Windows Vista, and was enabled
           by default, and thus became the default for
           GDI+ text drawing.  (The user can disable
           "ClearType" for Windows, and launching a GDI+
           would then, by default, draw text without
           "ClearType".)  In my example programs, I am
           using GDI+ to draw to a Bitmap that begins
           as a totally transparent white color (i.e.,
           alpha=0 and (r,g,b)=(255,255,255) for all
           pixels in the Bitmap).  The makes it possible
           for the drawn quad textured with this image
           to act as a transparent layer in front of 
           the scene (e.g., the rotating cube).  I can
           draw some opaque or semi-transparent text
           and shapes on the Bitmap, and they will appear
           to block parts of the scene.  However,
           the "ClearType" text drawing algorithm 
           evidently sets all pixels covered by the
           sub-pixel anti-aliased parts of the glyph
           to opaque, which makes all edges of the 
           glyph very blocky when drawn to a Bitmap
           that is initially totally transparent.

           So, the fix is to explicitly set the text
           drawing mode to be "AntiAlias" instead of
           leaving the mode to be the default (which,
           upon the release of Vista, is "ClearType").
                             
  * I have changed the code to now *disable*
    waiting for VSYNC.  This improves frame
    rate, with some small chance of "tearing"
    during rapid movement.  If you see "tearing"
    when using the example programs, then look
    for lines of code similar to the following:

            if (true == gl.bwglSwapIntervalEXT)
            {
                // ALLOW TEARING
                gl.wglSwapIntervalEXT(0);
            }

    and simply change the "0" to a "1", and the
    swap buffer operation will block until the
    next display refresh.  But pay close attention
    to the performance before and after such a
    change, to determine whether or not the change
    is for the best in diverse realistic situations.
    Most video games allow toggling this parameter
    while playing the game, through a checkbox
    like "[x] Enable VSYNC".


  * The "Avatar" demo has been removed.  (But I
    did not have time to remove all mention of
    it from the documentation.)

    The "Avatar" demo relied on a feature of 
    Windows XP which made it possible to have
    transparency in a window area through the
    use of a color key (e.g., the program could
    specify that the color magenta (255,0,255)
    represented transparency, and all parts of
    the window with that color would not appear).
    But, even neater (and, necessary, really, to
    prevent driving users crazy), only clicks
    on the opaque parts of the window would 
    actually be considered as clicks on that window.
    This was a pretty awesome feature of Windows XP.

    But with the introduction of Windows Vista,
    with its GPU-based drawing paradigm, the
    chroma-key transparency mechanism was no 
    longer practical.  So, the "Avatar" demo
    doesn't work for Vista or Windows 7.

    (There might be an alternative method for
    achieving the same effect in Vista and 
    Windows 7, but I don't have the time right
    now to investigate this possibility.
    But I *am* interested in trying to make
    such a thing to work with the more 
    contemporary versions of Windows...)

  * One very minor change I made was to add
    the following virtual functions to the 
    "ShaderProgram" class in the example code
    and in the "useful code" collection:


        public virtual String ShaderProgramFriendlyName()
        {
            return ("");
        }


        public virtual String VertexShaderSourceName()
        {
            return ("");
        }


        public virtual String FragmentShaderSourceName()
        {
            return ("");
        }

   These virtual methods are overridden in each of
   the derived shader program types, like "Mandelbrot"
   in the example code:

        public override String ShaderProgramFriendlyName()
        {
            return ("Mandelbrot");
        }

        public override String VertexShaderSourceName()
        {
            return ("Mandelbrot vertex shader (source code embedded in application)");
        }

        public override String FragmentShaderSourceName()
        {
            return ("Mandelbrot fragment shader (source code embedded in application)");
        }

    The idea here is for each shader program derived class
    to offer a "friendly name" for display purposes, so
    that a human can determine what the shader is about;
    and a vertex shader source code name, to know where
    the vertex shader source code came from (e.g., built
    in to the program, or perhaps the file name from
    which the code was loaded at run-time); and similar for
    the fragment shader code.

    These strings are now used in the example code to
    report any syntax errors in the shader code.
    You can add some errors to the example shader code,
    and then recompile and execute the program, and
    then see a message box reporting the errors.

    If you load shader code from files on disk, then
    indicating the file names using this overrides
    would be useful.

    My example code is only intended as a practical
    demonstration of things you could do -- and for
    many basic purposes, my code could likely be used
    without significant modification.  But the design
    of the example code is not as flexible as one 
    might achieve in their own implementation.
    
Well, LOL, I hope this update is fun and useful
for you! :-)

If you have a very fast computer (e.g., 3.3 GHz CPU
with 4+ cores, and an nVidia GTX 480 or above), then
you can try a fun experiment:

In the CSGL12Example1 example program, in the file
CSGL12Example1Handler.cs, in the function 
CSGL12Example1Handler() function (the constructor
for that class), you will see the following two
lines of code (among others):

    mHUDBitmap = new Bitmap(512, 512, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
    //mHUDBitmap = new Bitmap(2048, 1024, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

The example currently creates a 512x512 Bitmap.
But if you comment out the first of those lines,
and un-comment the second of those lines, like
this:

    //mHUDBitmap = new Bitmap(512, 512, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
    mHUDBitmap = new Bitmap(2048, 1024, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

then a 2048x1024 Bitmap will be created instead,
and the corresponding texture will have that size.

The drawing frame rate will likely drop, possibly
by a lot, but if the frame rate is still reasonable
(e.g., 30 Hz or 45 Hz, or above), then you are in
a position to draw GDI+ over the entire OpenGL
scene!  

You could use the Control.DrawToBitmap() function
for the various controls in Windows.Forms to 
draw a Windows.Form GUI in an OpenGL environment.
Getting events to those controls would be a minor
chore.

But consider the fact that you would be able to 
draw anything that you can draw with GDI+ to that
full-window OpenGL texture, in real time.  You
could, of course, use that texture for any purpose
in your scene (but if you choose to draw it "in
the distance", then it might look a bit messy
without mip-mapping, and enabling mip-mapping
for real-time texture updating for such a large
texture is likely to be too slow to be practical).

And any texture can be used in conjunction with
fragment shaders, so there are many fun possibilities.


Colin Fahey
http://colinfahey.com
http://colinfahey.com/csharp_wrapper_for_opengl/csharp_wrapper_for_opengl_en.html
2012 April 1




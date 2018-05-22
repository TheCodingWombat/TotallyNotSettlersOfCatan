using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TotallyNotSettlersOfCatan {

class Game
    {
	    // member variables
	    public Surface screen;
	    // initialize
	    public void Init()
	    {
	    }
	    // tick: renders one frame
	    public void Tick()
	    {

		    screen.Clear( 0 );
		    screen.Print( "hello world", 2, 2, 0xffffff );
            screen.Line(2, 20, 160, 20, 0xff0000);
        }

        int tick = 0;

        public void RenderGL() {

            //var M = Matrix4.CreatePerspectiveFieldOfView(1.6f, 1.3f, .1f, 1000);

            //GL.LoadMatrix(ref M);
            //GL.Translate(0, 0, -1);
            GL.Rotate(tick++, 0, 0, 1);
            //GL.Rotate(0 % 360, 1, 1, 1);

            float aspectRatio = (float)screen.width / (float)screen.height;

            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(-0.5f, -0.5f * screen.aspectRatio);
            GL.Vertex2(0.5f, -0.5f * screen.aspectRatio);
            GL.Vertex2(-0.5f, 0.5f * screen.aspectRatio);
            GL.End();
        }
    }

} // namespace Template
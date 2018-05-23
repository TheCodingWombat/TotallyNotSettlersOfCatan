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
		    screen.Print( "hello World", 2, 2, 0xffffff );
            screen.Line(2, 20, 160, 20, 0xff0000);
        }

        int tick = 0;

        public void RenderGL() {

            //var M = Matrix4.CreatePerspectiveFieldOfView(1.6f, 1.3f, .1f, 1000);

            //GL.LoadMatrix(ref M);
            //GL.Translate(0, 0, -1);
            //GL.Rotate(tick++, 0, 0, 1);
            //GL.Rotate(0 % 360, 1, 1, 1);

            Vector2 p = new Vector2(0, 0);
            float r = 0.5f;
            Vector3 c = new Vector3(1, 1, 1);

            DrawHexagon(p, r, c);
            DrawHexagon(new Vector2(-1.5f * r, (float) Math.Sqrt(r * r - 0.5f * r * 0.5f * r)), 0.5f, new Vector3(1.0f, 0.0f, 0.5f));
            DrawHexagon(new Vector2(1.5f * r, (float)Math.Sqrt(r * r - 0.5f * r * 0.5f * r)), 0.5f, new Vector3(1.0f, 0.8f, 0.5f));
            DrawHexagon(new Vector2(0.0f, 2 * (float)Math.Sqrt(r * r - 0.5f * r * 0.5f * r)), 0.5f, new Vector3(0.2f, 0.8f, 0.5f));
        }

        private void DrawHexagon(Vector2 p, float r, Vector3 c) {
            Vector2[] corners = new Vector2[6];

            GL.Color3(c);

            GL.Begin(PrimitiveType.Polygon);

            for (int i = 0; i <= 6; i++) {
                GL.Vertex2(p.X + r * (float)Math.Cos(i * MathHelper.PiOver3), p.Y + r * (float)Math.Sin(i * MathHelper.PiOver3));
            }

            GL.End();
            
            

            for (int j = 0; j < corners.Length; j++) {

                

            }
        }

    }

} // namespace Template


//i'm just passing by, fedde is figuring out github



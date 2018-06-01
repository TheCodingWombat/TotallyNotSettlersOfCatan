using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TotallyNotSettlersOfCatan {

    class Game
    {
	    // member variables
	    public Surface screen;
        private GameBoard gameBoard;

	    // initialize
	    public void Init()
	    {

            gameBoard = new GameBoard(12);

        }

        

	    // tick: renders one frame
	    public void Tick()
	    {
        }

        //Render OpenGL stuff
        public void RenderGL() {
            gameBoard.Draw();

            DrawAxes();
        }

        private void DrawAxes() {
            GL.Color3(new Vector3(0.1f, 0.1f, 0.1f));
            GL.Begin(PrimitiveType.Lines);

            GL.Vertex2(-1, 0);
            GL.Vertex2(1, 0);

            GL.End();

            GL.Begin(PrimitiveType.Lines);

            GL.Vertex2(0, -1);
            GL.Vertex2(0, 1);

            GL.End();

            GL.Color3(1f, 0f, 0f);

            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2(0, 0);
            GL.Vertex2(0, .4f);
            GL.Vertex2(.2f, .4f);
            GL.Vertex2(.2f, 0);

            GL.End();
        }

    }

} // namespace Template
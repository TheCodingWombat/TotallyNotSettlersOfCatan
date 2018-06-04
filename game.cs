using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace TotallyNotSettlersOfCatan {

    class Game
    {
	    // member variables
	    public Surface screen;
        private GameBoard gameBoard;
        private int boardSize = 3;
        private OpenTKApp app;

        public Game(OpenTKApp app) {
            this.app = app;
        }

	    // initialize
	    public void Init()
	    {
            DetermineZoom();
            gameBoard = new GameBoard(boardSize);

        }

        public void Mouse_Move(PointF point) {
            gameBoard.CheckTileHover(point);
        }

        public void Mouse_Down(PointF point, MouseButton button) {
            
            if (button == MouseButton.Left) {
                
                gameBoard.CheckTileClick(point);
            }
        }

        private void DetermineZoom() {
            float tilesInARow = boardSize * 2 + 1;

            OpenTKApp.Zoom = tilesInARow * Tile.ShortRadius * 1.6f; //De 1.6f moet worden vervangen door aspect ratio
            
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
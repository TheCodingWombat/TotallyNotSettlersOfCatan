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
        private int boardSize = 6;
        public static float tileRadius = 0.5f;
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
            float tilesInARow = boardSize * 2 + 6;

            OpenTKApp.Zoom = tilesInARow * Tile.ShortRadius * 1.6f; //De 1.6f moet worden vervangen door aspect ratio
            
        }        

	    // tick: renders one frame
	    public void Tick()
	    {
        }

        //Render OpenGL stuff
        public void RenderGL() {
            gameBoard.Draw();
        }

    }

} // namespace Template
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

        public int BoardSize { get => boardSize; set => boardSize = value; }

        public Game(OpenTKApp app) {
            this.app = app;
        }

	    // initialize
	    public void Init()
	    {
            gameBoard = new GameBoard(BoardSize);
        }

        public void Mouse_Move(PointF point) {
            gameBoard.CheckHover(point);
        }

        public void Mouse_Down(PointF point, MouseButton button) {
            
            if (button == MouseButton.Left) {
                
                gameBoard.CheckClick(point);
            }
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
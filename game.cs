using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TotallyNotSettlersOfCatan {

    class Game
    {
	    // member variables
	    public Surface screen;
        private Tile[] tiles;
        private Random random;

	    // initialize
	    public void Init()
	    {
            random = new Random();
            CreateTiles();

        }

        private void CreateTiles() {

            tiles = new Tile[1000*1000];

            for (int y = 0; y < 1000; y++) {
                for (int x = 0; x < 1000; x++) {
                    tiles[x + y * 1000] = new Tile(x - 500, y - 500, RandomColor());
                }
            }
        }

        private Vector3 RandomColor() {
            return new Vector3((float) random.NextDouble(), (float) random.NextDouble(), (float) random.NextDouble());
        }

	    // tick: renders one frame
	    public void Tick()
	    {
		    screen.Clear( 0 );
		    screen.Print( "hello World", 2, 2, 0xffffff );
            screen.Line(2, 20, 160, 20, 0xff0000);
        }

        public void RenderGL() {
            foreach (Tile tile in tiles)
                tile.Draw();

            DrawBackground();
        }

        private void DrawBackground() {
            GL.Color3(new Vector3(0.1f, 0.1f, 0.1f));
            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2(-1, -1);
            GL.Vertex2(1, -1);
            GL.Vertex2(1, 1);
            GL.Vertex2(-1, 1);

            GL.End();
        }

    }

} // namespace Template
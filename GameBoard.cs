using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotallyNotSettlersOfCatan {
    class GameBoard {

        private int size;
        private List<Tile> tiles;
        private Random random;

        public GameBoard(int size) {
            this.size = size;

            random = new Random();

            CreateTiles();
        }

        public void CheckTileHover(PointF point) {
            foreach (Tile tile in tiles) {
                tile.OnHover(tile.ContainsPoint(point));
            }
        }

        public void CheckTileClick(PointF point) {
            foreach (Tile tile in tiles) {
                if (tile.ContainsPoint(point))
                    tile.OnClick();
            }
        }

        private void CreateTiles() {

            tiles = new List<Tile>();

            //Middle
            for (int y = -size + 1; y < size; y++) {
                for (int x = -size + 1; x < size; x++) {

                    int cubeX = x;
                    int cubeZ = y - (x + (x & 1)) / 2;
                    int cubeY = -cubeX - cubeZ;

                    if (Math.Abs(cubeX) < size && Math.Abs(cubeY) < size && Math.Abs(cubeZ) < size) {

                        tiles.Add(new Tile(x, y, cubeX, cubeY, cubeZ, RandomType()));
                    }
                }
            }

            //Water
            for (int y = -size; y < size + 1; y++) {
                for (int x = -size; x < size + 1; x++) {

                    int cubeX = x;
                    int cubeZ = y - (x + (x & 1)) / 2;
                    int cubeY = -cubeX - cubeZ;

                    if (Math.Abs(cubeX) < size + 1 && Math.Abs(cubeY) < size + 1 && Math.Abs(cubeZ) < size + 1) {
                        if (Math.Abs(cubeX) == size || Math.Abs(cubeY) == size || Math.Abs(cubeZ) == size) {
                            tiles.Add(new Tile(x, y, cubeX, cubeY, cubeZ, 0));
                        }
                    }
                }
            }
        }

        private int RandomType() {
            return random.Next(1, 7);
        }

        private Vector3 RandomColor(int x, int y, int z) {
            int max = Math.Max(Math.Max(Math.Abs(x), Math.Abs(y)), Math.Abs(z));

            if (max % 3 == 0) {
                return new Vector3((float)random.NextDouble(), 0, 0);
            } else if (max % 3 == 1) {
                return new Vector3(0, (float)random.NextDouble(), 0);
            } else {
                return new Vector3(0, 0, (float)random.NextDouble());
            }
        }

        public void Draw() {
            foreach (Tile tile in tiles) {
                tile.Draw();
            }
        }

    }
}

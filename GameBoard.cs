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
        private List<Side> sides;
        private List<Corner> corners;
        private Random random;

        public GameBoard(int size) {
            this.size = size;

            random = new Random();

            CreateTiles();

            CreateSides();

            CreateCorners();
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
            for (int y = -size - 1; y < size + 2; y++) {
                for (int x = -size - 1; x < size + 2; x++) {

                    int cubeX = x;
                    int cubeZ = y - (x + (x & 1)) / 2;
                    int cubeY = -cubeX - cubeZ;

                    //Inner tiles
                    if (Math.Abs(cubeX) < size && Math.Abs(cubeY) < size && Math.Abs(cubeZ) < size) {
                        tiles.Add(new Tile(x, y, RandomType()));
                    }

                    //Water
                    if (Math.Abs(cubeX) < size + 1 && Math.Abs(cubeY) < size + 1 && Math.Abs(cubeZ) < size + 1) {
                        if (Math.Abs(cubeX) == size || Math.Abs(cubeY) == size || Math.Abs(cubeZ) == size) {
                            tiles.Add(new Tile(x, y, 0));
                        }
                    }

                    //Null circle
                    if (Math.Abs(cubeX) < size + 2 && Math.Abs(cubeY) < size + 2 && Math.Abs(cubeZ) < size + 2) {
                        if (Math.Abs(cubeX) == size + 1 || Math.Abs(cubeY) == size + 1 || Math.Abs(cubeZ) == size + 1) {
                            tiles.Add(new Tile(x, y, -1));
                        }
                    }
                }
            }
        }

        private void CreateSides() {

            sides = new List<Side>();

            foreach (Tile surroundingTile in Joins(1, 0, 0)) {
                Console.WriteLine(surroundingTile.U + " - " + surroundingTile.V + " - " + surroundingTile.Type);
            }
            foreach (Tile tile in tiles) {
                if (tile.Type == -1) continue; //null tile

                //TODO: use enums for 0: west, 1: north, 2: east
                for (int o = 0; o <= 2; o++) {

                    bool isSurroundedByWater = true;

                    foreach (Tile surroundingTile in Joins(tile.U, tile.V, o)) {
                        //Console.WriteLine(surroundingTile.Type + " - " + surroundingTile.U + " - " + surroundingTile.V);
                        if (surroundingTile != null && surroundingTile.Type > 0) { //Fix that we don't need the null check
                            isSurroundedByWater = false;
                        }
                    }

                    if (!isSurroundedByWater) {
                        Console.WriteLine("CreateSide: " + tile.U + " - " + tile.V + " -O: " + o);
                        sides.Add(new Side(tile.P.X, tile.P.Y, o));
                    }
                }
            }
        }

        public Tile[] Joins(int u, int v, int o) {
            switch (o) {
                case 0:
                    return new Tile[] { GetTileAt(u, v), GetTileAt(u - 1, v + 1) };
                case 1:
                    return new Tile[] { GetTileAt(u, v + 1), GetTileAt(u, v) };
                case 2:
                    return new Tile[] { GetTileAt(u + 1, v), GetTileAt(u, v) };
            }

            return null;
        }

        private Tile GetTileAt(int u, int v) {
            foreach (Tile tile in tiles) {
                if (tile.U == u && tile.V == v) {
                    return tile;
                }
            }

            Console.WriteLine(u + " - " + v);

            return null;
        }

        private void CreateCorners() {

            corners = new List<Corner>();

            foreach (Tile tile in tiles) {
                corners.Add(new Corner(tile.P.X, tile.P.Y, 0));
                corners.Add(new Corner(tile.P.X, tile.P.Y, 1));
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
            foreach (Side side in sides) {
                side.Draw();
            }

            foreach (Corner corner in corners) {
                corner.Draw();
            }

            foreach (Tile tile in tiles) {
                tile.Draw();
            }

        }

    }
}

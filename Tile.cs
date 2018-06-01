using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotallyNotSettlersOfCatan {
    class Tile {

        private int x;
        private int y;
        private int cubeX;
        private int cubeY;
        private int cubeZ;
        private float r = .5f;
        private Vector2 p;
        private int type;

        public Tile(int x, int y, int cubeX, int cubeY, int cubeZ, int type) {
            this.x = x;
            this.y = y;
            this.cubeX = cubeX;
            this.cubeY = cubeY;
            this.cubeZ = cubeZ;
            this.type = type;

            TranslateToScreenPos();
        }

        private void TranslateToScreenPos() {
            p.X = x * r * 1.5f;
            p.Y = y * r * (float)Math.Sqrt(3);

            if ((x&1) == 1)
                p.Y -= r * (float)Math.Sqrt(3) / 2;
        }

        private Vector3 GetColor() {

            switch (type) {
                case 0: //Water
                    return new Vector3(0, 0, 1);
                case 1: //Wheat
                    return new Vector3(1, 1, 0);
                case 2: //Forest
                    return new Vector3(0, .4f, 0);
                case 3: //Sheep
                    return new Vector3(0, 1, 0);
                case 4: //Ore
                    return new Vector3(.7f, .7f, .7f);
                case 5: //Stone
                    return new Vector3(1, .5f, .2f);
                case 6: //Desert
                    return new Vector3(1, 1, .7f);
            }

            return new Vector3(1, 1, 1);
        }

        public void Draw() {
            GL.Color3(GetColor());
            
            GL.Begin(PrimitiveType.Polygon);

            for (int i = 0; i <= 6; i++) {
                GL.Vertex2(p.X + r * (float)Math.Cos(i * MathHelper.PiOver3), p.Y + r * (float)Math.Sin(i * MathHelper.PiOver3));
            }

            GL.End();
        }

    }
}

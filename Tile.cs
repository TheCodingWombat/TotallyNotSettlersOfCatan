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
        private float r = .5f;
        private Vector2 p;
        private Vector3 c;

        public Tile(int x, int y, Vector3 c) {
            this.x = x;
            this.y = y;
            this.c = c;

            TranslateToScreenPos();
        }

        private void TranslateToScreenPos() {
            p.X = x * r * 1.5f;
            p.Y = y * r * (float)Math.Sqrt(3);

            if (x % 2 == 0)
                p.Y += r * (float)Math.Sqrt(3) / 2;
        }

        public void Draw() {
            GL.Color3(c);

            GL.Begin(PrimitiveType.Polygon);

            for (int i = 0; i <= 6; i++) {
                GL.Vertex2(p.X + r * (float)Math.Cos(i * MathHelper.PiOver3), p.Y + r * (float)Math.Sin(i * MathHelper.PiOver3));
            }

            GL.End();
        }

    }
}

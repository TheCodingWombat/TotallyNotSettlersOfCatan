using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace TotallyNotSettlersOfCatan {
    class Corner : IDrawable { //TODO Use implement drawable ofzo

        private float x;
        private float y;
        private int o;
        float radius = Game.tileRadius / 5;
        private Vector3 color;
        private bool hovered = false, clicked = false;
        private Vector2[] vertices = new Vector2[12];

        public Corner(float x, float y, int o) {
            this.o = o;

            color = new Vector3(1, 0, 0);

            DetermineCoordinates(x, y);

            CalculateVertices();
        }

        private void DetermineCoordinates(float x, float y) {
            
            this.y = y;
            if (o == 0) {
                this.x = x - Game.tileRadius;
            } else if (o == 1) {
                this.x = x + Game.tileRadius;
            }
        }

        private void CalculateVertices() {

            

            for (int i = 0; i < vertices.Length; i++) {
                vertices[i] = new Vector2(x + radius * (float) Math.Cos((Math.PI * i) / (vertices.Length / 2)), y + radius * (float)Math.Sin((Math.PI * i) / (vertices.Length / 2)));
            }
        }

        public bool ContainsPoint(PointF point) {
            bool result = false;
            int j = vertices.Length - 1;
            for (int i = 0; i < vertices.Length; i++) {
                if (vertices[i].Y < point.Y && vertices[j].Y >= point.Y || vertices[j].Y < point.Y && vertices[i].Y >= point.Y) {
                    if (vertices[i].X + (point.Y - vertices[i].Y) / (vertices[j].Y - vertices[i].Y) * (vertices[j].X - vertices[i].X) < point.X) {
                        result = !result;
                    }
                }
                j = i;
            }

            return result;
        }

        public void Draw() {
            Vector3 drawColor = new Vector3(0.3f, 0.3f, 0.3f);

            if (hovered)
                drawColor += color * new Vector3(.3f, .3f, .3f);

            if (clicked)
                drawColor += color * new Vector3(.7f, .7f, .7f);

            GL.Color3(drawColor);


            GL.Begin(PrimitiveType.Polygon);

            for (int i = 0; i < vertices.Length; i++) {
                GL.Vertex2(vertices[i].X, vertices[i].Y);
            }

            GL.End();
        }

        public void OnHover(bool hovered) {
            this.hovered = hovered;
        }

        public void OnClick() {
            clicked = !clicked;
        }
    }
}

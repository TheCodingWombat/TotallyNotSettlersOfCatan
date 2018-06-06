using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Drawing;

namespace TotallyNotSettlersOfCatan {
    class Side : IDrawable {

        float x;
        float y;
        int o;
        float u;
        float v;
        float length = Game.tileRadius / 2f;
        float width = Game.tileRadius / 10f;
        private Vector2[] vertices = new Vector2[4];
        private bool hovered = false, clicked = false;
        private Vector3 color;

        public Side(float x, float y, int o) {
            this.u = x; // not necessary?
            this.v = y; // not necesarry?
            this.o = o;

            color = new Vector3(1, 0, 0);

            DetermineCoordinates(x, y);

            CalculateVertices();
        }

        private void CalculateVertices() {

            PointF p1 = new PointF(x - length / 2f, y - width / 2f);
            PointF p2 = new PointF(x + length / 2f, y - width / 2f);
            PointF p3 = new PointF(x + length / 2f, y + width / 2f);
            PointF p4 = new PointF(x - length / 2f, y + width / 2f);

            double rot = 0;

            if (o == 0)
                rot = Math.PI / 3;
            else if (o == 2)
                rot = -Math.PI / 3;

            p1 = RotatePoint(rot, p1);
            p2 = RotatePoint(rot, p2);
            p3 = RotatePoint(rot, p3);
            p4 = RotatePoint(rot, p4);

            vertices[0] = new Vector2(p1.X, p1.Y);
            vertices[1] = new Vector2(p2.X, p2.Y);
            vertices[2] = new Vector2(p3.X, p3.Y);
            vertices[3] = new Vector2(p4.X, p4.Y);
        }

        public PointF RotatePoint(double angle, PointF p) {
            float s = (float) Math.Sin(angle);
            float c = (float) Math.Cos(angle);

            // translate point back to origin:
            p.X -= x;
            p.Y -= y;

            // rotate point
            float xnew = p.X * c - p.Y * s;
            float ynew = p.X * s + p.Y * c;

            // translate point back:
            p.X = xnew + x;
            p.Y = ynew + y;
            return p;
        }

        private void DetermineCoordinates(float x, float y) {
            this.x = x;
            this.y = y;

            if (o == 0) {
                this.x = x - .75f * Game.tileRadius;
                this.y = y + .5f * Game.tileRadius * (float)Math.Sqrt(3) / 2f;
            } else if (o == 1) {
                this.x = x;
                this.y = y + Game.tileRadius * (float)Math.Sqrt(3) / 2f;
            } else if (o == 2) {
                this.x = x + .75f * Game.tileRadius;
                this.y = y + .5f * Game.tileRadius * (float)Math.Sqrt(3) / 2f;
            }
        }

        public void Draw() {

            Vector3 drawColor = new Vector3(0.3f, 0.3f, 0.3f);

            if (hovered)
                drawColor += color * new Vector3(.3f, .3f, .3f);

            if (clicked)
                drawColor += color * new Vector3(.7f, .7f, .7f);

            GL.Color3(drawColor);


            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < 4; i++) {
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

        //Terminology from http://www-cs-students.stanford.edu/~amitp/game-programming/grids/#relationships



    }
}

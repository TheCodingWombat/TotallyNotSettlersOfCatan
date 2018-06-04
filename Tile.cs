using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotallyNotSettlersOfCatan {
    class Tile {

        //Use GraphicsPath for clicking

        private int x;
        private int y;
        private int cubeX;
        private int cubeY;
        private int cubeZ;
        private static float radius = .5f;
        private Vector2 p;
        private int type;
        private Vector2[] vertices = new Vector2[6];
        private Vector3 color;
        private bool hovered = false, clicked = false;

        public static float Radius { get => radius; set => radius = value; }
        public static float ShortRadius { get => radius * (float) (Math.Sqrt(3) / 2f) ; }

        public Tile(int x, int y, int cubeX, int cubeY, int cubeZ, int type) {
            this.x = x;
            this.y = y;
            this.cubeX = cubeX;
            this.cubeY = cubeY;
            this.cubeZ = cubeZ;
            this.type = type;

            color = GetColor();

            TranslateToScreenPos();

            CalculateVertices();

            //CreateRegion();

            //TODO: NOW WORKING ON GETTING SCREEN POSITION

        }

        private void CreateRegion() {
            using (GraphicsPath gp = new GraphicsPath()) {
                //gp.AddLines(vertices);
            }
        }

        private void CalculateVertices() {
            for (int i = 0; i < vertices.Length; i++)
                vertices[i] = new Vector2(p.X + Radius * (float)Math.Cos(i * MathHelper.PiOver3), p.Y + Radius * (float)Math.Sin(i * MathHelper.PiOver3));
        }

        private void TranslateToScreenPos() {
            p.X = x * Radius * 1.5f;
            p.Y = y * Radius * (float)Math.Sqrt(3);

            if ((x&1) == 1)
                p.Y -= Radius * (float)Math.Sqrt(3) / 2;
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

            Vector3 drawColor = color;

            if (clicked) {
                //drawColor -= new Vector3(.2f, .2f, .2f);

                GL.Color3(new Vector3(0, 0, 0));

                GL.Begin(PrimitiveType.Polygon);

                for (int i = 0; i < 16; i++) {
                    GL.Vertex2(new Vector2(p.X + (Radius / 3f) * (float)Math.Cos(i * Math.PI / 8f), p.Y + (Radius / 3f) * (float)Math.Sin(i * Math.PI / 8f)));
                }

                GL.End();
            }

            if (hovered) {
                drawColor += new Vector3(.6f, .6f, .6f);
            }

            GL.Color3(drawColor);
            
            GL.Begin(PrimitiveType.Polygon);

            for (int i = 0; i < 6; i++) {
                GL.Vertex2(vertices[i].X, vertices[i].Y);
            }

            GL.End();
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

        public void OnHover(bool hovered) {
            this.hovered = hovered;
        }

        public void OnClick() {
            clicked = !clicked;
        }

    }
}

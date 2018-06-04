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

        public Side(float x, float y, int o) {
            this.u = x;
            this.v = y;
            this.o = o;

            DetermineCoordinates(x, y);
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

            GL.Color3(1.0f, 0, 1.0f);

            GL.PushMatrix();

            GL.Translate(x, y, 0);

            if (o == 0)
                GL.Rotate(60, 0, 0, 1f);
            else if (o == 2)
                GL.Rotate(-60, 0, 0, 1f);


            GL.Translate(-x, -y, 0);

            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2(x - length / 2f, y - width / 2f);
            GL.Vertex2(x + length / 2f, y - width / 2f);
            GL.Vertex2(x + length / 2f, y + width / 2f);
            GL.Vertex2(x - length / 2f, y + width / 2f);

            GL.End();

            GL.PopMatrix();

        }

        //Terminology from http://www-cs-students.stanford.edu/~amitp/game-programming/grids/#relationships

        

    }
}

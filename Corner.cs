using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotallyNotSettlersOfCatan {
    class Corner : IDrawable { //TODO Use implement drawable ofzo

        private float x;
        private float y;
        private int o;

        public Corner(float x, float y, int o) {
            this.x = x;
            this.y = y;
            this.o = o;
        }

        public void Draw() {

        }

    }
}

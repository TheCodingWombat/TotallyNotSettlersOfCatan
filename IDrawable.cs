using System.Drawing;

namespace TotallyNotSettlersOfCatan {
    internal interface IDrawable {

        void Draw();

        void OnHover(bool hovered);

        bool ContainsPoint(PointF p);

    }
}
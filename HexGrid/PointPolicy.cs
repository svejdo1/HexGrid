using System.Runtime.CompilerServices;

namespace Barbar.HexGrid
{
    public sealed class PointPolicy : IPointPolicy<Point>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point Add(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public Point Create(double x, double y)
        {
            return new Point(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public double GetX(Point point)
        {
            return point.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetY(Point point)
        {
            return point.Y;
        }
    }
}

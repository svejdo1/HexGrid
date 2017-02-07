namespace Barbar.HexGrid
{
    public static class HexLayoutFactory
    {
        public static HexLayout<TPoint, TPointPolicy> CreatePointyHexLayout<TPoint, TPointPolicy>(TPoint size, TPoint orgin, Offset offset) where TPointPolicy : IPointPolicy<TPoint>, new() where TPoint : struct
        {
            return new HexLayout<TPoint, TPointPolicy>(Orientation.PointyHexagons, size, orgin, offset);
        }

        public static HexLayout<Point, PointPolicy> CreatePointyHexLayout(Point size, Point orgin, Offset offset)
        {
            return new HexLayout<Point, PointPolicy>(Orientation.PointyHexagons, size, orgin, offset);
        }

        public static HexLayout<TPoint, TPointPolicy> CreateFlatHexLayout<TPoint, TPointPolicy>(TPoint size, TPoint orgin, Offset offset) where TPointPolicy : IPointPolicy<TPoint>, new() where TPoint : struct
        {
            return new HexLayout<TPoint, TPointPolicy>(Orientation.FlatHexagons, size, orgin, offset);
        }

        public static HexLayout<Point, PointPolicy> CreateFlatHexLayout(Point size, Point orgin, Offset offset)
        {
            return new HexLayout<Point, PointPolicy>(Orientation.FlatHexagons, size, orgin, offset);
        }

    }
}

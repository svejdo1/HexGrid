namespace Barbar.HexGrid
{
    public interface IPointPolicy<TPoint> where TPoint : struct
    {
        TPoint Create(double x, double y);
        double GetX(TPoint point);
        double GetY(TPoint point);
        TPoint Add(TPoint a, TPoint b);
    }
}

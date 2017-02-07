using System;
using System.Collections.Generic;

namespace Barbar.HexGrid
{
    public struct HexLayout<TPoint, TPointPolicy> where TPointPolicy : IPointPolicy<TPoint>, new() where TPoint : struct 
    {
        private readonly Offset _offset;
        private readonly Orientation _orientation;
        private readonly TPoint _size;
        private readonly TPoint _origin;
        private static readonly TPointPolicy s_Policy = new TPointPolicy();
        
        internal HexLayout(Orientation orientation, TPoint size, TPoint origin, Offset offset)
        {
            _orientation = orientation;
            _size = size;
            _origin = origin;
            _offset = offset;
        }

        public TPoint HexToPixel(CubeCoordinates h)
        {
            double x = (_orientation.f0 * h.Q + _orientation.f1 * h.R) * s_Policy.GetX(_size);
            double y = (_orientation.f2 * h.Q + _orientation.f3 * h.R) * s_Policy.GetY(_size);
            return s_Policy.Create(x + s_Policy.GetX(_origin), y + s_Policy.GetY(_origin));
        }
        
        public CubeFractionCoordinates PixelToHex(TPoint p)
        {
            var pt = s_Policy.Create((s_Policy.GetX(p) - s_Policy.GetX(_origin)) / s_Policy.GetX(_size), (s_Policy.GetY(p) - s_Policy.GetY(_origin)) / s_Policy.GetY(_size));
            double q = _orientation.b0 * s_Policy.GetX(pt) + _orientation.b1 * s_Policy.GetY(pt);
            double r = _orientation.b2 * s_Policy.GetX(pt) + _orientation.b3 * s_Policy.GetY(pt);
            return new CubeFractionCoordinates(q, r, -q - r);
        }

        public TPoint HexCornerOffset(int corner)
        {
            double angle = 2.0 * Math.PI * (_orientation.start_angle - corner) / 6;
            return s_Policy.Create(s_Policy.GetX(_size) * Math.Cos(angle), s_Policy.GetY(_size) * Math.Sin(angle));
        }
        
        public IList<TPoint> PolygonCorners(CubeCoordinates h)
        {
            var corners = new List<TPoint>(6);
            var center = HexToPixel(h);
            for (int i = 0; i < 6; i++)
            {
                var offset = HexCornerOffset(i);
                corners.Add(s_Policy.Add(center, offset));
            }
            return corners;
        }

        public OffsetCoordinates ToOffsetCoordinates(CubeCoordinates h)
        {
            int column;
            int row;
            switch (_orientation.name)
            {
                case OrientationName.FlatHexagons:
                    column = h.Q;
                    row = h.R + ((h.Q + _offset.Value * (h.Q & 1)) / 2);
                    return new OffsetCoordinates(column, row);
                case OrientationName.PointyHexagons:
                    column = h.Q + ((h.R + _offset.Value * (h.R & 1)) / 2);
                    row = h.R;
                    return new OffsetCoordinates(column, row);
                default:
                    throw new NotImplementedException();
            }
        }

        public CubeCoordinates ToCubeCoordinates(OffsetCoordinates h)
        {
            int q, r, s;
            switch (_orientation.name)
            {
                case OrientationName.FlatHexagons:

                    q = h.Column;
                    r = h.Row - ((h.Column + _offset.Value * (h.Column & 1)) / 2);
                    s = -q - r;
                    return new CubeCoordinates(q, r, s);
                case OrientationName.PointyHexagons:
                    q = h.Column - ((h.Row + _offset.Value * (h.Row & 1)) / 2);
                    r = h.Row;
                    s = -q - r;
                    return new CubeCoordinates(q, r, s);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

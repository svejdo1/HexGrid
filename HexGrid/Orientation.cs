using System;

namespace Barbar.HexGrid
{
    internal struct Orientation
    {
        public readonly OrientationName name;
        public readonly double f0;
        public readonly double f1;
        public readonly double f2;
        public readonly double f3;
        public readonly double b0;
        public readonly double b1;
        public readonly double b2;
        public readonly double b3;
        public readonly double start_angle;

        public static readonly Orientation PointyHexagons = new Orientation(OrientationName.PointyHexagons, Math.Sqrt(3.0), Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);
        public static readonly Orientation FlatHexagons = new Orientation(OrientationName.FlatHexagons, 3.0 / 2.0, 0.0, Math.Sqrt(3.0) / 2.0, Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, Math.Sqrt(3.0) / 3.0, 0.0);

        private Orientation(OrientationName name, double f0, double f1, double f2, double f3, double b0, double b1, double b2, double b3, double start_angle)
        {
            this.name = name;
            this.f0 = f0;
            this.f1 = f1;
            this.f2 = f2;
            this.f3 = f3;
            this.b0 = b0;
            this.b1 = b1;
            this.b2 = b2;
            this.b3 = b3;
            this.start_angle = start_angle;
        }

        public override bool Equals(object obj)
        {
            var another = (Orientation)obj;
            return name == another.name;
        }

        public override string ToString()
        {
            return name.ToString();
        }

        public override int GetHashCode()
        {
            return (int)name;
        }
    }
}

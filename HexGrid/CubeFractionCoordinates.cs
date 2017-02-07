using System;
using System.Collections.Generic;

namespace Barbar.HexGrid
{
    public struct CubeFractionCoordinates
    {
        public readonly double Q;
        public readonly double R;
        public readonly double S;

        public CubeFractionCoordinates(double q, double r, double s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public CubeCoordinates Round()
        {
            int q = (int)(Math.Round(Q));
            int r = (int)(Math.Round(R));
            int s = (int)(Math.Round(S));
            double q_diff = Math.Abs(q - Q);
            double r_diff = Math.Abs(r - R);
            double s_diff = Math.Abs(s - S);
            if (q_diff > r_diff && q_diff > s_diff)
            {
                q = -r - s;
                return new CubeCoordinates(q, r, s);
            }
            if (r_diff > s_diff)
            {
                r = -q - s;
                return new CubeCoordinates(q, r, s);

            }
            s = -q - r;
            return new CubeCoordinates(q, r, s);
        }

        static public CubeFractionCoordinates HexLerp(CubeFractionCoordinates a, CubeFractionCoordinates b, double t)
        {
            return new CubeFractionCoordinates(a.Q * (1 - t) + b.Q * t, a.R * (1 - t) + b.R * t, a.S * (1 - t) + b.S * t);
        }
        
        static public IList<CubeCoordinates> HexLinedraw(CubeCoordinates a, CubeCoordinates b)
        {
            int n = CubeCoordinates.Distance(a, b);
            var a_nudge = new CubeFractionCoordinates(a.Q + 0.000001, a.R + 0.000001, a.S - 0.000002);
            var b_nudge = new CubeFractionCoordinates(b.Q + 0.000001, b.R + 0.000001, b.S - 0.000002);
            var results = new List<CubeCoordinates> { };
            double step = 1.0 / Math.Max(n, 1);
            for (int i = 0; i <= n; i++)
            {
                results.Add(HexLerp(a_nudge, b_nudge, step * i).Round());
            }
            return results;
        }
    }
}

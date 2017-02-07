using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Barbar.HexGrid
{
    public struct CubeCoordinates
    {
        public readonly int Q;
        public readonly int R;
        public readonly int S;

        public CubeCoordinates(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public override bool Equals(object obj)
        {
            var another = (CubeCoordinates)obj;
            return another.Q == Q && another.R == R && another.S == S;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Q;
                hash = hash * 23 + R;
                hash = hash * 23 + S;
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("[Q={0},R={1},S={2}]", Q, R, S);
        }

        public static CubeCoordinates operator +(CubeCoordinates a, CubeCoordinates b)
        {
            return new CubeCoordinates(a.Q + b.Q, a.R + b.R, a.S + b.S);
        }

        public static CubeCoordinates operator -(CubeCoordinates a, CubeCoordinates b)
        {
            return new CubeCoordinates(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }
        
        public static CubeCoordinates operator *(CubeCoordinates a, int k)
        {
            return new CubeCoordinates(a.Q * k, a.R * k, a.S * k);
        }

        public static readonly IList<CubeCoordinates> Directions = new ReadOnlyCollection<CubeCoordinates>(new List<CubeCoordinates> { new CubeCoordinates(1, 0, -1), new CubeCoordinates(1, -1, 0), new CubeCoordinates(0, -1, 1), new CubeCoordinates(-1, 0, 1), new CubeCoordinates(-1, 1, 0), new CubeCoordinates(0, 1, -1) });

        static public CubeCoordinates Neighbor(CubeCoordinates hex, int direction)
        {
            return hex + Directions[direction];
        }

        public static readonly IList<CubeCoordinates> Diagonals = new ReadOnlyCollection<CubeCoordinates>(new List<CubeCoordinates> { new CubeCoordinates(2, -1, -1), new CubeCoordinates(1, -2, 1), new CubeCoordinates(-1, -1, 2), new CubeCoordinates(-2, 1, 1), new CubeCoordinates(-1, 2, -1), new CubeCoordinates(1, 1, -2) });

        public static CubeCoordinates DiagonalNeighbor(CubeCoordinates hex, int direction)
        {
            return hex + Diagonals[direction];
        }
        public int Length
        {
            get { return (Math.Abs(Q) + Math.Abs(R) + Math.Abs(S)) / 2; }
        }

        public static int Distance(CubeCoordinates a, CubeCoordinates b)
        {
            return (a - b).Length;
        }
    }
}

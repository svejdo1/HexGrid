using System;
using Barbar.HexGrid;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Barbar.HexGridTest
{
    [TestClass]
    public class HexTest
    {
        public void EqualHex(string name, CubeCoordinates a, CubeCoordinates b)
        {
            if (!(a.Q == b.Q && a.S == b.S && a.R == b.R))
            {
                Assert.Fail(name);
            }
        }


        public void EqualOffsetcoord(string name, OffsetCoordinates a, OffsetCoordinates b)
        {
            if (!(a.Column == b.Column && a.Row == b.Row))
            {
                Assert.Fail(name);
            }
        }


        public void EqualInt(string name, int a, int b)
        {
            if (!(a == b))
            {
                Assert.Fail(name);
            }
        }


        public void EqualHexArray(string name, IList<CubeCoordinates> a, IList<CubeCoordinates> b)
        {
            Assert.AreEqual(a.Count, b.Count, name);
            for (int i = 0; i < a.Count; i++)
            {
                EqualHex(name, a[i], b[i]);
            }
        }

        [TestMethod]
        public void TestHexArithmetic()
        {
            EqualHex("hex_add", new CubeCoordinates(4, -10, 6), new CubeCoordinates(1, -3, 2) + new CubeCoordinates(3, -7, 4));
            EqualHex("hex_subtract", new CubeCoordinates(-2, 4, -2), new CubeCoordinates(1, -3, 2) - new CubeCoordinates(3, -7, 4));
        }

        [TestMethod]
        public void TestHexDirection()
        {
            EqualHex("hex_direction", new CubeCoordinates(0, -1, 1), CubeCoordinates.Directions[2]);
        }

        [TestMethod]
        public void TestHexNeighbor()
        {
            EqualHex("hex_neighbor", new CubeCoordinates(1, -3, 2), CubeCoordinates.Neighbor(new CubeCoordinates(1, -2, 1), 2));
        }

        [TestMethod]
        public void TestHexDiagonal()
        {
            EqualHex("hex_diagonal", new CubeCoordinates(-1, -1, 2), CubeCoordinates.DiagonalNeighbor(new CubeCoordinates(1, -2, 1), 3));
        }

        [TestMethod]
        public void TestHexDistance()
        {
            Assert.AreEqual(7, CubeCoordinates.Distance(new CubeCoordinates(3, -7, 4), new CubeCoordinates(0, 0, 0)), "hex_distance");
        }

        [TestMethod]
        public void TestHexRound()
        {
            CubeFractionCoordinates a = new CubeFractionCoordinates(0, 0, 0);
            CubeFractionCoordinates b = new CubeFractionCoordinates(1, -1, 0);
            CubeFractionCoordinates c = new CubeFractionCoordinates(0, -1, 1);
            EqualHex("hex_round 1", new CubeCoordinates(5, -10, 5), CubeFractionCoordinates.HexLerp(new CubeFractionCoordinates(0, 0, 0), new CubeFractionCoordinates(10, -20, 10), 0.5).Round());
            EqualHex("hex_round 2", a.Round(), CubeFractionCoordinates.HexLerp(a, b, 0.499).Round());
            EqualHex("hex_round 3", b.Round(), CubeFractionCoordinates.HexLerp(a, b, 0.501).Round());
            EqualHex("hex_round 4", a.Round(), new CubeFractionCoordinates(a.Q * 0.4 + b.Q * 0.3 + c.Q * 0.3, a.R * 0.4 + b.R * 0.3 + c.R * 0.3, a.S * 0.4 + b.S * 0.3 + c.S * 0.3).Round());
            EqualHex("hex_round 5", c.Round(), new CubeFractionCoordinates(a.Q * 0.3 + b.Q * 0.3 + c.Q * 0.4, a.R * 0.3 + b.R * 0.3 + c.R * 0.4, a.S * 0.3 + b.S * 0.3 + c.S * 0.4).Round());
        }

        [TestMethod]
        public void TestHexLinedraw()
        {
            EqualHexArray("hex_linedraw", new List<CubeCoordinates> { new CubeCoordinates(0, 0, 0), new CubeCoordinates(0, -1, 1), new CubeCoordinates(0, -2, 2), new CubeCoordinates(1, -3, 2), new CubeCoordinates(1, -4, 3), new CubeCoordinates(1, -5, 4) }, CubeFractionCoordinates.HexLinedraw(new CubeCoordinates(0, 0, 0), new CubeCoordinates(1, -5, 4)));
        }

        [TestMethod]
        public void TestLayout()
        {
            CubeCoordinates h = new CubeCoordinates(3, 4, -7);
            var flat = HexLayoutFactory.CreateFlatHexLayout(new Point(10, 15), new Point(35, 71), Offset.Even);
            EqualHex("layout", h, flat.PixelToHex(flat.HexToPixel(h)).Round());
            var pointy = HexLayoutFactory.CreatePointyHexLayout(new Point(10, 15), new Point(35, 71), Offset.Even);
            EqualHex("layout", h, pointy.PixelToHex(pointy.HexToPixel(h)).Round());
        }

        [TestMethod]
        public void TestConversionRoundtrip()
        {
            CubeCoordinates a = new CubeCoordinates(3, 4, -7);
            OffsetCoordinates b = new OffsetCoordinates(1, -3);
            var flatOdd = HexLayoutFactory.CreateFlatHexLayout(new Point(10, 15), new Point(35, 71), Offset.Odd);
            var flatEven = HexLayoutFactory.CreateFlatHexLayout(new Point(10, 15), new Point(35, 71), Offset.Even);
            var pointyOdd = HexLayoutFactory.CreatePointyHexLayout(new Point(10, 15), new Point(35, 71), Offset.Odd);
            var pointyEven = HexLayoutFactory.CreatePointyHexLayout(new Point(10, 15), new Point(35, 71), Offset.Even);

            EqualHex("conversion_roundtrip even-Q", a, flatEven.ToCubeCoordinates(flatEven.ToOffsetCoordinates(a)));
            EqualOffsetcoord("conversion_roundtrip even-Q", b, flatEven.ToOffsetCoordinates(flatEven.ToCubeCoordinates(b)));
            EqualHex("conversion_roundtrip odd-Q", a, flatOdd.ToCubeCoordinates(flatOdd.ToOffsetCoordinates(a)));
            EqualOffsetcoord("conversion_roundtrip odd-Q", b, flatOdd.ToOffsetCoordinates(flatOdd.ToCubeCoordinates(b)));
            EqualHex("conversion_roundtrip even-R", a, pointyEven.ToCubeCoordinates(pointyEven.ToOffsetCoordinates(a)));
            EqualOffsetcoord("conversion_roundtrip even-R", b, pointyEven.ToOffsetCoordinates(pointyEven.ToCubeCoordinates(b)));
            EqualHex("conversion_roundtrip odd-R", a, pointyOdd.ToCubeCoordinates(pointyOdd.ToOffsetCoordinates(a)));
            EqualOffsetcoord("conversion_roundtrip odd-R", b, pointyOdd.ToOffsetCoordinates(pointyOdd.ToCubeCoordinates(b)));
        }

        [TestMethod]
        public void TestOffsetFromCube()
        {
            var flatOdd = HexLayoutFactory.CreateFlatHexLayout(new Point(10, 15), new Point(35, 71), Offset.Odd);
            var flatEven = HexLayoutFactory.CreateFlatHexLayout(new Point(10, 15), new Point(35, 71), Offset.Even);
            EqualOffsetcoord("offset_from_cube even-Q", new OffsetCoordinates(1, 3), flatEven.ToOffsetCoordinates(new CubeCoordinates(1, 2, -3)));
            EqualOffsetcoord("offset_from_cube odd-Q", new OffsetCoordinates(1, 2), flatOdd.ToOffsetCoordinates(new CubeCoordinates(1, 2, -3)));
        }

        [TestMethod]
        public void TestOffsetToCube()
        {
            var flatOdd = HexLayoutFactory.CreateFlatHexLayout(new Point(10, 15), new Point(35, 71), Offset.Odd);
            var flatEven = HexLayoutFactory.CreateFlatHexLayout(new Point(10, 15), new Point(35, 71), Offset.Even);
            EqualHex("offset_to_cube even-Q", new CubeCoordinates(1, 2, -3), flatEven.ToCubeCoordinates(new OffsetCoordinates(1, 3)));
            EqualHex("offset_to_cube odd-Q", new CubeCoordinates(1, 2, -3), flatOdd.ToCubeCoordinates(new OffsetCoordinates(1, 2)));
        }
    }
}
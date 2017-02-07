using Barbar.HexGrid;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Barbar.HexGridExample
{
    public partial class FormExample : Form
    {
        private HexLayout<Barbar.HexGrid.Point, PointPolicy> _grid = HexLayoutFactory.CreatePointyHexLayout(new Barbar.HexGrid.Point(64, 64), new Barbar.HexGrid.Point(0, 0), Offset.Even);

        public FormExample()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var cube = _grid.PixelToHex(new Barbar.HexGrid.Point(e.X, e.Y));
            var offset = _grid.ToOffsetCoordinates(cube.Round());
            Text = "Position: " + offset;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            
            for (var x = 0; x < 6; x++)
                for (var y = 0; y < 6; y++)
                {
                    var offset = new OffsetCoordinates(x, y);
                    var cube = _grid.ToCubeCoordinates(offset);
                    var points = _grid.PolygonCorners(cube).Select(p => new System.Drawing.Point((int)p.X, (int)p.Y)).ToList();
                    points.Add(points[0]);
                    g.DrawLines(Pens.Brown, points.ToArray());
                }
        }
    }
}

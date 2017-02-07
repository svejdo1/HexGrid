namespace Barbar.HexGrid
{
    public struct OffsetCoordinates
    {
        public readonly int Column;
        public readonly int Row;
                
        public OffsetCoordinates(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public override bool Equals(object obj)
        {
            var another = (OffsetCoordinates)obj;
            return another.Column == Column && another.Row == Row;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Column;
                hash = hash * 23 + Row;
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("[C={0},R={1}]", Column, Row);
        }
    }
}

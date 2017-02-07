using System;
using System.Collections;
using System.Collections.Generic;

namespace Barbar.HexGrid
{
    public class Map<T> : IEnumerable<T>
    {
        private readonly int _width;
        private readonly int _height;
        private T[] _data;

        public T GetTile(int x, int y)
        {
            return _data[x + y * _width];
        }

        public Map(int width, int height)
        {
            if (width < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }
            if (height < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }
            _width = width;
            _height = height;
            _data = new T[width * height];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}

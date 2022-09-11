using System;
using UnityEngine;

namespace Grids
{
    public class Grid2d<T>
    {
        private T[] _data;
        private int _width;
        private int _height;

        public int Width => _width;
        public int Height => _height;
        public T[] Data => _data;

        public Grid2d(int width, int height)
        {
            InitGridAtSize(width, height);
        }

        public Grid2d(int width, int height, T defaultValue)
        {
            InitGridAtSize(width, height);
            var size = width * height;
            
            for (var i = 0; i < size; i++)
            {
                _data[i] = defaultValue;
            }
        }
        
        private void InitGridAtSize(int width, int height)
        {
            _width = width;
            _height = height;
            var size = _width * _height;
            _data = new T[size];
        }
        
        public bool TryGetData(int col, int row, out T result)
        {
            if (!AreCoordsInBounds(col, row))
            {
                result = default;
                return false;
            }

            result = GetData(col, row);
            return true;
        }
        
        public T GetData(int col, int row)
        {
            col = Mathf.Clamp(col, 0, _width - 1);
            row = Mathf.Clamp(row, 0, _height - 1);

            var index = GetIndexFromCoords(col, row);
            return _data[index];
        }

        public bool TrySetData(int col, int row, T value)
        {
            if (!AreCoordsInBounds(col, row))
            {
                return false;
            }

            SetData(col, row, value);
            return true;
        }
        
        public void SetData(int col, int row, T value)
        {
            if (!AreCoordsInBounds(col, row))
            {
                Debug.LogError($"Called {nameof(SetData)} for out of bounds coordinates {col} / {Width}, {row} / {Height}");
                return;
            }

            var index = GetIndexFromCoords(col, row);
            _data[index] = value;
        }

        public bool AreCoordsInBounds(int col, int row)
        {
            return col >= 0 && col < _width && row >= 0 && row < _height;
        }

        private int GetIndexFromCoords(int col, int row)
        {
            return row * _width + col;
        }
    }
}

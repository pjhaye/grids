using UnityEngine;

namespace Grids
{
    public class Grid3d<T>
    {
        private T[] _data;
        private int _width;
        private int _height;
        private int _depth;

        public int Width => _width;
        public int Height => _height;
        public int Depth => _depth;
        public T[] Data => _data;

        public Grid3d(int width, int height, int depth)
        {
            InitGridAtSize(width, height, depth);
        }

        public Grid3d(int width, int height, int depth, T defaultValue)
        {
            InitGridAtSize(width, height, depth);
            
            var size = width * height * depth;
            
            for (var i = 0; i < size; i++)
            {
                _data[i] = defaultValue;
            }
        }
        
        private void InitGridAtSize(int width, int height, int depth)
        {
            _width = width;
            _height = height;
            _depth = depth;
            
            var size = _width * _height * _depth;
            _data = new T[size];
        }
        
        public bool TryGetData(int x, int y, int z, out T result)
        {
            if (!AreCoordsInBounds(x, y, z))
            {
                result = default;
                return false;
            }

            result = GetData(x, y, z);
            return true;
        }
        
        public T GetData(int x, int y, int z)
        {
            x = Mathf.Clamp(x, 0, _width - 1);
            y = Mathf.Clamp(y, 0, _height - 1);
            z = Mathf.Clamp(z, 0, _depth - 1);

            var index = GetIndexFromCoords(x, y, z);
            return _data[index];
        }

        public bool TrySetData(int x, int y, int z, T value)
        {
            if (!AreCoordsInBounds(x, y, z))
            {
                return false;
            }

            SetData(x, y, z, value);
            return true;
        }
        
        public void SetData(int x, int y, int z, T value)
        {
            if (!AreCoordsInBounds(x, y, z))
            {
                Debug.LogError($"Called {nameof(SetData)} for out of bounds coordinates");
                return;
            }

            var index = GetIndexFromCoords(x, y, z);
            _data[index] = value;
        }

        public bool AreCoordsInBounds(int x, int y, int z)
        {
            return x >= 0 && x < _width && y >= 0 && y < _height && z >= 0 && z < _depth;
        }

        private int GetIndexFromCoords(int x, int y, int z)
        {
            return (z * _width * _height) + (y * _width + x);
        }
    }
}

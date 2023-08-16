using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Hommage
{
    public struct Vector3
    {
        private OpenTK.Mathematics.Vector3 _vector;

        public Vector3(float x, float y, float z)
        {
            _vector = new(x, y, z);
        }

        public Vector3 Normalized()
        {
            return _vector.Normalized();
        }

        public void Normalize()
        {
            _vector.Normalize();
        }

        public void NormalizeFast()
        {
            _vector.NormalizeFast();
        }

        public bool Equals(OpenTK.Mathematics.Vector3 other)
        {
            return _vector.Equals(other);
        }
        
        public bool Equals(Vector3 other)
        {
            return _vector.Equals((OpenTK.Mathematics.Vector3)other);
        }

        public void Deconstruct(out float x, out float y, out float z)
        {
            _vector.Deconstruct(out x, out y, out z);
        }

        public float this[int index]
        {
            get => _vector[index];
            set => _vector[index] = value;
        }

        public float Length => _vector.Length;

        public float LengthFast => _vector.LengthFast;

        public float LengthSquared => _vector.LengthSquared;

        public Vector2 Xy
        {
            get => _vector.Xy;
            set => _vector.Xy = value;
        }

        public Vector2 Xz
        {
            get => _vector.Xz;
            set => _vector.Xz = value;
        }

        public Vector2 Yx
        {
            get => _vector.Yx;
            set => _vector.Yx = value;
        }

        public Vector2 Yz
        {
            get => _vector.Yz;
            set => _vector.Yz = value;
        }

        public Vector2 Zx
        {
            get => _vector.Zx;
            set => _vector.Zx = value;
        }

        public Vector2 Zy
        {
            get => _vector.Zy;
            set => _vector.Zy = value;
        }

        public Vector3 Xzy
        {
            get => _vector.Xzy;
            set => _vector.Xzy = value;
        }

        public Vector3 Yxz
        {
            get => _vector.Yxz;
            set => _vector.Yxz = value;
        }

        public Vector3 Yzx
        {
            get => _vector.Yzx;
            set => _vector.Yzx = value;
        }

        public Vector3 Zxy
        {
            get => _vector.Zxy;
            set => _vector.Zxy = value;
        }

        public Vector3 Zyx
        {
            get => _vector.Zyx;
            set => _vector.Zyx = value;
        }
        
        public float X
        {
            get => _vector.X;
            set => _vector.X = value;
        }
        public float Y
        {
            get => _vector.Y;
            set => _vector.Y = value;
        }
        public float Z
        {
            get => _vector.Z;
            set => _vector.Z = value;
        }

        public static implicit operator Vector3(OpenTK.Mathematics.Vector3 v)
        {
            return new(v.X, v.Y, v.Z);
        }

        public static implicit operator OpenTK.Mathematics.Vector3(Vector3 v)
        {
            return new(v.X, v.Y, v.Z);
        }
    }
}

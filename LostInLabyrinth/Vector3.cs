using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostInLabyrinth
{
    public struct Vector3
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}

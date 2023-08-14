using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostInLabyrinth
{
    public class Vector2
    {
        public float X { get; }
        public float Y { get; }
        private float Z;

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0;
        }
    }
}

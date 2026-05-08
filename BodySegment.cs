using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class BodySegment
    {
        public int X { get; set; }
        public int Y { get; set; }

        public BodySegment(int x, int y)
        {
            X = x;
            Y = y;
        }

        public (int, int) GetPosition()
        {
            return (X, Y);
        }
    }
}

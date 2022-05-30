using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CardinalPoint Orientation { get; set; }

        public Coordinate(int x, int y, CardinalPoint orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }
    }
}

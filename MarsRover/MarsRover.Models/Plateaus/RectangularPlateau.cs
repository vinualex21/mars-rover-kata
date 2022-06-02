using MarsRover.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models.Plateaus
{
    public class RectangularPlateau : IPlateau
    {
        public readonly int Length;

        public readonly int Width;

        public RectangularPlateau(int length, int width)
        {
            Length = length;
            Width = width;
        }

        public bool IsCoordinatesWithinBounds(int x, int y)
        {
            return x >= 0 && x <= Length && y >= 0 && y <= Width;
        }
    }
}

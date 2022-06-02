using MarsRover.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models.Plateaus
{
    public class CircularPlateau : IPlateau
    {
        public readonly int Radius;

        public CircularPlateau(int radius)
        {
            Radius = radius;
        }

        public bool IsCoordinatesWithinBounds(int x, int y)
        {
            return x*x + y*y <= Radius*Radius;
        }
    }
}

using MarsRover.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models
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

        
    }
}

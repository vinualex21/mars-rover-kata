using MarsRover.Models;
using MarsRover.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class MissionManager
    {
        private List<IExplorer> explorers;

        private IPlateau plateau;

        public MissionManager()
        {
            explorers = new List<IExplorer>();
            plateau = new RectangularPlateau(0,0);
        }

        public void DeployVehicle()
        {

        }
    }
}

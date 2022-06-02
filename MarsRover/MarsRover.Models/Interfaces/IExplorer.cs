using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models.Interfaces
{
    public interface IExplorer
    {
        public int ID { get; set; }
        public string GetCurrentPosition();

    }
}

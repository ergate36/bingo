using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarigoldModel.Commands
{
    public class PlantCommand : Command
    {
        public int CardIndex { get; set; }
        public int Number { get; set; }
    }
}

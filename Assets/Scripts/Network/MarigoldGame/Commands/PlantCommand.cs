using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    public class PlantCommand : Command
    {
        public int CardIndex { get; set; }
        public int Number { get; set; }

        public PlantCommand() : base(CommandType.PLANT)
        {

        }
    }
}

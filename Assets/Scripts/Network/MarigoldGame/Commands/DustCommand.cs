using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    internal class DustCommand : Command
    {
        public DustCommand() : base(CommandType.DUST)
        {

        }

        public int CardIndex { get; set; }
        public int DustTime { get; set; }
    }
}
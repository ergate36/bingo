using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    internal class BlindCommand : Command
    {
        public int BlindTime { get; set; }

        public BlindCommand() : base(CommandType.BLIND)
        {

        }
    }
}
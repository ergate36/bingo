using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Game
{
    internal class BlindCommand : Command
    {
        public int BlindTime { get; set; }

        public BlindCommand() : base(CommandType.BLIND)
        {

        }
    }
}
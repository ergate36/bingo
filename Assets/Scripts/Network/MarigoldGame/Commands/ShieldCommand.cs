using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Game
{
    internal class ShieldCommand : Command
    {
        public int BlindTime { get; set; }

        public ShieldCommand() : base(CommandType.SHIELD)
        {

        }
    }
}
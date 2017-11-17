using MarigoldGame.Common;
using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    internal class FogCommand : Command
    {
        public Card Card { get; set; }

        public FogCommand() : base(CommandType.FOG)
        {

        }
    }
}
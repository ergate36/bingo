using MarigoldGame.Common;
using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    internal class FreezeCommand : Command
    {
        public Card Card { get; set; }

        public FreezeCommand() : base(CommandType.FREEZE)
        {

        }
    }
}
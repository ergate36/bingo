using MarigoldGame.Common;
using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    public class CheckSquareCommand : Command
    {
        public CheckSquareCommand() : base(CommandType.CHECK_SQUARE)
        {

        }

        public Square Square { get; set; }
    }
}

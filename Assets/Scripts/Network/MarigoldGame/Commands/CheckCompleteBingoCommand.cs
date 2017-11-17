using MarigoldGame.Common;
using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    public class CheckCompleteBingoCommand : Command
    {
        public int Result { get; set; }
        //public GamePlayer Player { get; set; }
        public Card Card { get; set; }

        public CheckCompleteBingoCommand() : base(CommandType.CHECK_COMPLETE_BINGO)
        {

        }
    }
}

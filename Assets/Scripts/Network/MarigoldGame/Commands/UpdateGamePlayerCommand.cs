using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    class UpdateGamePlayerCommand : Command
    {
        public bool DoubleExperience { get; set; }
        public bool DoubleGameMoney { get; set; }

        public UpdateGamePlayerCommand() : base(CommandType.UPDATE_GAME_PLAYER)
        {

        }
    }
}

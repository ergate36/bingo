using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    public class SelectRandomPowerUpCommand : Command
    {
        public long SelectPowerUpId { get; set; }

        public SelectRandomPowerUpCommand() : base(CommandType.SELECT_RANDOM_POWER_UP)
        {

        }
    }
}

using MarigoldGame.Game;
using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    public class IncreasePowerUpGaugeCommand : Command
    {
        //public long SelectPowerUpId { get; set; }

        public IncreasePowerUpGaugeCommand() : base(CommandType.INCREASE_POWER_UP_GAUGE)
        {

        }

        public int PowerUpGauge { get; set; }
        public PowerUpGaugeState CurrentGaugeState { get; set; }
    }
}

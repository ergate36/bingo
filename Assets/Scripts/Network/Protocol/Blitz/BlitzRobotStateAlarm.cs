using MarigoldGame.Game.Blitz;

namespace MarigoldGame.Protocol.Blitz
{
    public class BlitzRobotStateAlarm : BaseProtocol
    {
        public BlitzRobotStateAlarm() : base(MessageType.BlitzRobotStateAlarm)
        {
        }

        public BlitzRobotPlayer BlitzRobotPlayer { get; set; }
    }
}

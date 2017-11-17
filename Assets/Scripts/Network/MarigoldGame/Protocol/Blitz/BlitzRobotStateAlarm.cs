using MarigoldGame.Common;
//using MarigoldGame.Game.Blitz;

namespace MarigoldGame.Protocol.Blitz
{
    public class BlitzRobotStateAlarm : BaseProtocol
    {
        //public RobotPlayer BlitzRobotPlayer { get; set; }

        public BlitzRobotStateAlarm() : base(MessageType.BlitzRobotStateAlarm)
        {
        }
    }
}

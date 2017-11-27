using MarigoldGame.Common;

namespace MarigoldGame.Protocol
{
    public class BlitzRobotStateAlarm : BaseProtocol
    {
        //public RobotPlayer BlitzRobotPlayer { get; set; }

        public BlitzRobotStateAlarm() : base(MessageType.BlitzRobotStateAlarm)
        {
        }
    }
}

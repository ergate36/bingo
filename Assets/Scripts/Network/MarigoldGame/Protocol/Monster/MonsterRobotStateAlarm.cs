//using MarigoldGame.Game.Monster;

namespace MarigoldGame.Protocol
{
    public class MonsterRobotStateAlarm : BaseProtocol
    {
        //public MonsterRobotPlayer MonsterRobotPlayer { get; set; }

        public MonsterRobotStateAlarm() : base(MessageType.MonsterRobotStateAlarm)
        {
        }
    }
}

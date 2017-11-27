//using MarigoldGame.Game.Monster;

namespace MarigoldGame.Protocol
{
    class MonsterOpponentStateAlarm : BaseProtocol
    {
        //public MonsterPlayer MonsterPlayer { get; set; }

        public MonsterOpponentStateAlarm() : base(MessageType.MonsterOpponentStateAlarm)
        {
        }
    }
}

using MarigoldGame.Commands;

namespace MarigoldGame.Protocol.Monster
{
    class MonsterClearRewardAlarm : BaseProtocol
    {
        public ClearRewardCommand Command { get; internal set; }

        public MonsterClearRewardAlarm() : base(MessageType.MonsterClearRewardAlarm)
        {
        }
    }
}

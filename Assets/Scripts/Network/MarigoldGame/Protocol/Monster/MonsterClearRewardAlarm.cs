using MarigoldGame.Commands;

namespace MarigoldGame.Protocol
{
    class MonsterClearRewardAlarm : BaseProtocol
    {
        public ClearRewardCommand Command { get; internal set; }

        public MonsterClearRewardAlarm() : base(MessageType.MonsterClearRewardAlarm)
        {
        }
    }
}

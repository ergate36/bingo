using MarigoldGame.Commands;

namespace MarigoldGame.Protocol
{
    class MonsterClearRewardAlarm : BaseProtocol
    {
        public ClearRewardCommand Command { get; set; }

        public MonsterClearRewardAlarm() : base(MessageType.MonsterClearRewardAlarm)
        {
        }
    }
}

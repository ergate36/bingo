namespace MarigoldGame.Protocol
{
    public class MonsterCallNumberAlarm : BaseProtocol
    {
        public int Number { get; set; }

        public MonsterCallNumberAlarm() : base(MessageType.MonsterCallNumberAlarm)
        {
        }
    }
}

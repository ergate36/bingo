namespace MarigoldGame.Protocol
{
    public class MonsterCompleteBingoAlarm : BaseProtocol
    {
        public int Ranking { get; set; }
        public string Name { get; set; }

        public MonsterCompleteBingoAlarm() : base(MessageType.MonsterCompleteBingoAlarm)
        {
        }
    }
}

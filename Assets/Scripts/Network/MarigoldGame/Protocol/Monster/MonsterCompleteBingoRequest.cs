namespace MarigoldGame.Protocol
{
    public class MonsterCompleteBingoRequest : BaseProtocol
    {
        public int CardIndex { get; set; }

        public MonsterCompleteBingoRequest() : base(MessageType.MonsterCompleteBingoRequest)
        {
        }
    }
}

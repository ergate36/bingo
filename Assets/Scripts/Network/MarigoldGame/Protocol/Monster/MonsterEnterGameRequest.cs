namespace MarigoldGame.Protocol
{
    public class MonsterEnterGameRequest : BaseProtocol
    {
        public int CardCount { get; set; }

        public MonsterEnterGameRequest() : base(MessageType.MonsterEnterGameRequest)
        {
        }
    }
}

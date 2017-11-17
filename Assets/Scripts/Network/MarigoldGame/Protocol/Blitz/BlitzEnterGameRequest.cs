namespace MarigoldGame.Protocol
{
    public class BlitzEnterGameRequest : BaseProtocol
    {
        public int CardCount { get; set; }

        public BlitzEnterGameRequest() : base(MessageType.BlitzEnterGameRequest)
        {
        }
    }
}

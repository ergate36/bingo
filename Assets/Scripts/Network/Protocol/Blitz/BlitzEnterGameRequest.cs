namespace MarigoldGame.Protocol
{
    public class BlitzEnterGameRequest : BaseProtocol
    {
        public BlitzEnterGameRequest() : base(MessageType.BlitzEnterGameRequest)
        {
        }

        public int CardCount { get; set; }
    }
}

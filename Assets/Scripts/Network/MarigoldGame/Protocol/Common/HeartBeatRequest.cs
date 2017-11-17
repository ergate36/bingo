namespace MarigoldGame.Protocol.Common
{
    public class HeartBeatRequest : BaseProtocol
    {
        public HeartBeatRequest() : base(MessageType.HeartBeatRequest)
        {
        }
    }
}

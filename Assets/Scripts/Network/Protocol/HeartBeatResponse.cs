namespace MarigoldGame.Protocol
{
    public class HeartBeatResponse : BaseResponse
    {
        public HeartBeatResponse() : base(MessageType.HeartBeatResponse)
        {
        }
    }
}


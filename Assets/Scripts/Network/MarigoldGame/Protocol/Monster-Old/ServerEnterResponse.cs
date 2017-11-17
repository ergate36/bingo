namespace MarigoldGame.Protocol
{
    public class ServerEnterResponse : BaseProtocol
	{
        public byte result;
        public string SessionId;

        public ServerEnterResponse() : base(MessageType.ServerEnterResponse)
		{
		}
	}
}

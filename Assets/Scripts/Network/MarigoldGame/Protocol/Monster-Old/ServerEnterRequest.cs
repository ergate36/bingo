namespace MarigoldGame.Protocol
{
    public class ServerEnterRequest : BaseProtocol
	{
        public int userID;
        public byte gametype;
        public string nickname;

        public ServerEnterRequest() : base(MessageType.ServerEnterRequest)
		{
		}
	}
}

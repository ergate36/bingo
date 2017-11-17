namespace MarigoldGame.Protocol
{
    public class GameCalResponse : BaseProtocol
	{
        public byte result;

        public GameCalResponse() : base(MessageType.GameCalResponse)
		{
		}
	}

}

namespace MarigoldGame.Protocol
{
    public class GameMsgRequest : BaseProtocol
	{
        public string GData;

        public GameMsgRequest()  : base(MessageType.GameMessageRequest)
		{
		}
	}
}

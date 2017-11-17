namespace MarigoldGame.Protocol
{
    public class GameMsgAlarm : BaseProtocol
	{
        public string SendSessionId { get; set; }
        public string GData;

        public GameMsgAlarm() : base(MessageType.GameMessageAlarm)
		{
		}
	}
}

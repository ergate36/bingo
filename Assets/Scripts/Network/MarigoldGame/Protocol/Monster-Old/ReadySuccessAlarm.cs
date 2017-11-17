namespace MarigoldGame.Protocol
{
    public class ReadySuccessAlarm : BaseProtocol
	{
        public int BingoMaxCount;

        public ReadySuccessAlarm() : base(MessageType.ReadySuccessAlarm)
		{
		}
	}
}

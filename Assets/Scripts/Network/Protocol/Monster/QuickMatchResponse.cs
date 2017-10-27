namespace MarigoldGame.Protocol
{
    public class QuickMatchResponse : BaseProtocol
	{
        public int waitTime;
        public int memberCount;

        public QuickMatchResponse() : base(MessageType.QuickMatchResponse)
		{
		}
	}
}

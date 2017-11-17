namespace MarigoldGame.Protocol
{
    public class ScoreUpdateResponse : BaseProtocol
	{
        public byte result;

        public ScoreUpdateResponse() : base(MessageType.ScoreUpdateResponse)
		{
		}
	}
}

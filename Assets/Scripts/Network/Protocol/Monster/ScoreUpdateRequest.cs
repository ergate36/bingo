namespace MarigoldGame.Protocol
{
    public class ScoreUpdateRequest : BaseProtocol
	{
        public int score;
        public int coin;
        public int ticket;
        public int gold_ticket;
        public int item_count;
        public int userID;

        public ScoreUpdateRequest() : base(MessageType.ScoreUpdateRequest)
		{
		}
	}
}

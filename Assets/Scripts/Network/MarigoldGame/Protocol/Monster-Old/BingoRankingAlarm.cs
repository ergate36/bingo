namespace MarigoldGame.Protocol
{
    public class BingoRankingAlarm : BaseProtocol
	{
        public string Name { get; set; }
        public int MonsterCard;
        public byte GameOver;

        public BingoRankingAlarm():base(MessageType.BingoRankingAlarm)
		{
		}
	}

}

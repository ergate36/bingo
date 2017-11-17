namespace MarigoldGame.Protocol
{
    public class MatchUser
    {
        public int userID;
        public string Name { get; set; }
        public byte monster_card;
        public int betting_index;
        public byte sheet_count;
    }

    public class MatchSuccessAlarm : BaseProtocol
	{
        public MatchUser[] List = new MatchUser[0];

        public MatchSuccessAlarm() : base(MessageType.MatchSuccessAlarm)
		{
		}
	}
}

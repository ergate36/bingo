namespace MarigoldGame.Protocol
{
    public class QuickMatchRequest : BaseProtocol
	{
        public byte gametype;
        public byte monster_index;
        public byte sheet_count;
        public int betting_index;

        public QuickMatchRequest() : base(MessageType.QuickMatchRequest)
		{
		}
	}
}

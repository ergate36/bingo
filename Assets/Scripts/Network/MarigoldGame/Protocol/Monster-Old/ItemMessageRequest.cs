namespace MarigoldGame.Protocol
{
    public class ItemMessageRequest : BaseProtocol
	{
        public string receiveSessionId;
        public int itemindex;
        public int attack_sheetIndex;
        public int sheetIndex;

        public ItemMessageRequest() : base(MessageType.ItemMessageRequest)
		{
		}
	}
}

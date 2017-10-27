namespace MarigoldGame.Protocol
{
    public class ItemMsgAlarm : BaseProtocol
	{
        public string attackSessionId;
        public int itemindex;
        public int attack_sheetIndex;
        public int sheetIndex;

        public ItemMsgAlarm() : base(MessageType.ItemMessageAlarm)
		{
		}
	}
}

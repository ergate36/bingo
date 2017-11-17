namespace MarigoldGame.Protocol
{
    public class GameUser
    {
        public string userID;
        public string Name { get; set; }
    }

    public class GameStartAlarm : BaseProtocol
	{
        public GameStartAlarm() : base(MessageType.GameStartAlarm)
		{
		}

        public byte[] BingoArray = new byte[50];
        public GameUser[] List = new GameUser[0];
	}

}

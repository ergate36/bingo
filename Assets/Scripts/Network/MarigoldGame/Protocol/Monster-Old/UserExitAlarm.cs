namespace MarigoldGame.Protocol
{
    public class UserExitAlarm : BaseProtocol
	{
        public string userID;
        public int MaxBingoCount;

        public UserExitAlarm() : base(MessageType.UserExitAlarm)
		{
		}
	}
}

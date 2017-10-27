namespace MarigoldGame.Protocol
{
    public class GameLiftConnectResponse : BaseProtocol
    {
        public int Result { get; set; }
        public string Message { get; set; }

        public GameLiftConnectResponse() : base(MessageType.GameLiftConnectResponse)
        {
        }
    }
}

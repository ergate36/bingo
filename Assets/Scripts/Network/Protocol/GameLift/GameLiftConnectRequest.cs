namespace MarigoldGame.Protocol
{
    public class GameLiftConnectRequest : BaseProtocol
    {
        public string PlayerSessionId { get; set; }
        public string SessionKey { get; set; }

        public GameLiftConnectRequest() : base(MessageType.GameLiftConnectRequest)
        {
        }
    }
}

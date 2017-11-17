using System.Collections.Generic;

namespace MarigoldGame.Protocol
{
    public class GameLiftConnectResponse : BaseResponse
    {
        public int Result { get; set; }
        public object Command { get; internal set; }

        public GameLiftConnectResponse() : base(MessageType.GameLiftConnectResponse)
        {
        }
    }
}

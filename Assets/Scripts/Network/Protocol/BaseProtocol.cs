using Newtonsoft.Json;

namespace MarigoldGame.Protocol
{
    public class BaseProtocol
    {
        [JsonIgnore]
        public readonly MessageType MessageType;

        protected BaseProtocol(MessageType messageType)
        {
            MessageType = messageType;
        }
    }
}

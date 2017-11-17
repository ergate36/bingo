using MarigoldGame.Common;

namespace MarigoldGame.Protocol
{
    public class MonsterCheckNumberRequest : BaseProtocol
    {
        public int CardIndex { get; set; }
        public int Number { get; set; }

        public MonsterCheckNumberRequest() : base(MessageType.MonsterCheckNumberRequest)
        {
        }
    }
}

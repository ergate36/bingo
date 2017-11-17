using System;

namespace MarigoldGame.Protocol
{
    public class MonsterGambleRequest : BaseProtocol
    {
        public Int64 StageGambleFeeId { get; set; } // 어떤 갬블을 하는 가에 대한 정보

        public MonsterGambleRequest() : base(MessageType.MonsterGambleRequest)
        {
        }
    }
}

using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class MonsterCheckNumberResponse : BaseResponse
    {
        public Command Command { get; set; }

        public MonsterCheckNumberResponse() : base(MessageType.MonsterCheckNumberResponse)
        {
        }
    }
}

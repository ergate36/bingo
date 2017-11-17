using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class MonsterEnterGameResponse : BaseResponse
    {
        public int Result { get; set; }
        public Command Command { get; set; }

        public MonsterEnterGameResponse() : base(MessageType.MonsterEnterGameResponse)
        {
        }
    }
}

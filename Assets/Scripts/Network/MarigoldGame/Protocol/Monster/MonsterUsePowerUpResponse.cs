using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class MonsterUsePowerUpResponse : BaseResponse
    {
        public Command Command { get; set; }

        public MonsterUsePowerUpResponse() : base(MessageType.MonsterUsePowerUpResponse)
        {
        }
    }
}

using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class MonsterRefreshPowerUpResponse : BaseProtocol
    {
        public Command Command { get; set; }

        public MonsterRefreshPowerUpResponse() : base(MessageType.MonsterRefreshPowerUpResponse)
        {
        }
    }
}

using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class MonsterRetryCollectionResponse : BaseResponse
    {
        public Command Command { get; internal set; }

        public MonsterRetryCollectionResponse() : base(MessageType.MonsterRetryCollectionResponse)
        {
        }
    }
}

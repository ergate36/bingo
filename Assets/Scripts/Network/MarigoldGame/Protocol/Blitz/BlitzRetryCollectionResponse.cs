using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class BlitzRetryCollectionResponse : BaseResponse
    {
        public Command Command { get; internal set; }

        public BlitzRetryCollectionResponse() : base(MessageType.BlitzRetryCollectionResponse)
        {
        }
    }
}

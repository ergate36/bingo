namespace MarigoldGame.Protocol
{
    public class BlitzCompleteBingoRequest : BaseProtocol
    {
        public int CardIndex { get; set; }

        public BlitzCompleteBingoRequest() : base(MessageType.BlitzCompleteBingoRequest)
        {
        }
    }
}

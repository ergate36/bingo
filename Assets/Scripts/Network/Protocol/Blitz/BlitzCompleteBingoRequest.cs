namespace MarigoldGame.Protocol
{
    public class BlitzCompleteBingoRequest : BaseProtocol
    {
        public BlitzCompleteBingoRequest() : base(MessageType.BlitzCompleteBingoRequest)
        {
        }

        public int CardIndex { get; set; }
    }
}

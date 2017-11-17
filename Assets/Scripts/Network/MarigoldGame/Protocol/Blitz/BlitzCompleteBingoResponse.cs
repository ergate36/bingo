namespace MarigoldGame.Protocol
{
    public class BlitzCompleteBingoResponse : BaseResponse
    {
        public int Result { get; set; }

        public BlitzCompleteBingoResponse() : base(MessageType.BlitzCompleteBingoResponse)
        {
        }
    }
}

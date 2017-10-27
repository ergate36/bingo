namespace MarigoldGame.Protocol
{
    public class BlitzCompleteBingoResponse : BaseResponse
    {
        public BlitzCompleteBingoResponse() : base(MessageType.BlitzCompleteBingoResponse)
        {
        }

        public int Result { get; set; }
    }
}

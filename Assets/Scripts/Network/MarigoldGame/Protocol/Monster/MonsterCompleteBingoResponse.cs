namespace MarigoldGame.Protocol
{
    public class MonsterCompleteBingoResponse : BaseResponse
    {
        public int Result { get; set; }

        public MonsterCompleteBingoResponse() : base(MessageType.MonsterCompleteBingoResponse)
        {
        }
    }
}

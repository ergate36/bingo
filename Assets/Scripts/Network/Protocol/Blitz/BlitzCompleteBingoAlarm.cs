namespace MarigoldGame.Protocol
{
    public class BlitzCompleteBingoAlarm : BaseProtocol
    {
        public BlitzCompleteBingoAlarm() : base(MessageType.BlitzCompleteBingoAlarm)
        {
        }

        public int Rank { get; set; }
        public string Name { get; set; }
    }
}

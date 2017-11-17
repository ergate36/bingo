namespace MarigoldGame.Protocol
{
    public class BlitzCompleteBingoAlarm : BaseProtocol
    {
        public int Ranking { get; set; }
        public string Name { get; set; }

        public BlitzCompleteBingoAlarm() : base(MessageType.BlitzCompleteBingoAlarm)
        {
        }
    }
}

using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    internal class MixCommand : Command
    {
        public int OldIndex { get; set; }
        public int NewIndex { get; set; }
        public long UserAccountId { get; set; }

        public MixCommand() : base(CommandType.MIX)
        {

        }
    }
}
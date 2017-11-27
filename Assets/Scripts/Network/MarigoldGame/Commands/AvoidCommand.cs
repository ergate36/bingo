using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    internal class AvoidCommand : Command
    {
        public long UserAccountId { get; set; }

        public AvoidCommand() : base(CommandType.AVOID)
        {

        }
    }
}
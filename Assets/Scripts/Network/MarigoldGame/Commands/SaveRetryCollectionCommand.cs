using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldGame.Commands
{
    public class SaveRetryCollectionCommand : Command
    {
        public UserRetryCollection UserRetryCollection { get; set; }

        public SaveRetryCollectionCommand() : base(CommandType.SAVE_RETRY_COLLECTION)
        {

        }
    }
}

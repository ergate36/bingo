using MarigoldModel.Model;

namespace MarigoldModel.Commands
{
    public class RemoveAssetCommand : Command
    {
        public AssetType AssetType { get; set; }
        public long AssetId { get; set; }
        public int AssetCount { get; set; }

        public RemoveAssetCommand() : base(CommandType.REMOVE_ASSET)
        {

        }
    }
}

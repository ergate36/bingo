using MarigoldModel.Commands;
using MarigoldModel.Model;

namespace MarigoldModel.Commands
{
    public class AddAssetCommand : Command
    {
        public AssetType AssetType { get; set; }
        public long AssetId { get; set; }
        public int AssetCount { get; set; }

        public AddAssetCommand() : base(CommandType.ADD_ASSET)
        {

        }
    }
}

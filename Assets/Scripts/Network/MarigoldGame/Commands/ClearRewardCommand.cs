using MarigoldModel.Commands;
using MarigoldModel.Model;
using System;

namespace MarigoldGame.Commands
{
    public class ClearRewardCommand : Command
    {
        public Int64 RandomRewardId { get; set; }
        public ClearRewardType ClearRewardType { get; set; }
        public AssetType AssetType { get; set; }
        public Int64 AssetId { get; set; }
        public int AssetCount { get; set; }

        public ClearRewardCommand() : base(CommandType.CLEAR_REWARD)
        {

        }
    }
}

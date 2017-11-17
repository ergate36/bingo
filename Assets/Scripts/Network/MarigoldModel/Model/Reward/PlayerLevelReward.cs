using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 플레이어가 레벨업시 받는 보상
    // 해당 PlayerLevel이 되었을때 받는 보상을 기록한다.
    [Table("PlayerLevelReward")]
    public partial class PlayerLevelReward : BaseModel
    {
        public int PlayerLevel { get; set; } //플레이어 레벨

        // asset 받게되는 보상
        public AssetType AssetType { get; set; }
        public Int64 AssetId { get; set; }
        public int AssetCount { get; set; }
    }
}

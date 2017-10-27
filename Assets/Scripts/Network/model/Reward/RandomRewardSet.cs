using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 랜덤 보상 Asset 리스트(RandomReward가 같은 것 중에서 하나의 row를 선택해서 보상을 주게 된다.
    [Table("RandomRewardSet")]
    public partial class RandomRewardSet : BaseModel
    {
        public int Rate { get; set; } // 각각 Set이 나올 확률
        // asset
        public AssetType AssetType { get; set; }
        public Int64 AssetId { get; set; }
        public int AssetCountMin { get; set; }
        public int AssetCountMax { get; set; }

        // foreign key
        public Int64 RandomRewardId { get; set; }
        public RandomReward RandomReward { get; set; }
    }
}

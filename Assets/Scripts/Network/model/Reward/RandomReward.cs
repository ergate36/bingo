using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 랜덤 보상 리스트
    [Table("RandomReward")]
    public partial class RandomReward : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string Comment { get; set; } // 설명

        public int TotalRate { get; set; } // RandomRewardSet의 확률을 전부 더한 값

        public List<RandomRewardSet> RandomRewardSetList { get; internal set; }
    }
}

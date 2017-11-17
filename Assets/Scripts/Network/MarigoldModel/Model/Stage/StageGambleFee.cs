using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 스테이지 진입후 미니게임 플레이시 지불하는 비용.(임시)
    [Table("StageGambleFee")]
    public partial class StageGambleFee : BaseModel
    {
        [Column(TypeName = "mediumtext")]
        public string Comment { get; set; } // 설명을 기록한다.

        // asset 입장료 재화
        public AssetType AssetType { get; set; }
        public Int64 AssetId { get; set; }
        public int AssetCount { get; set; }

        public Int64 RandomRewardId { get; set; } // 보상 정보

        // foreign key
        public Int64 StageId { get; set; }
        public Stage Stage { get; set; }
    }
}

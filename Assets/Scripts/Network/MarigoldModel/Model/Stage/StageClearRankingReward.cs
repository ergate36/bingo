using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 스테이지 빙고 완성시 랭킹별로 받게되는 보상의 정보
    [Table("StageClearRankingReward")]
    public partial class StageClearRankingReward : BaseModel
    {
        public int Ranking { get; set; } // 클리어한 랭킹 1,2,3등까지는 별도의 보상을 가지며 그 외에는 0으로 기록된 보상을 받는다.

        // asset 입장료 재화
        public AssetType AssetType { get; set; }
        public Int64 AssetId { get; set; }
        public int AssetCount { get; set; }

        // foreign key
        public Int64 StageId { get; set; }
        public Stage Stage { get; set; }
    }
}

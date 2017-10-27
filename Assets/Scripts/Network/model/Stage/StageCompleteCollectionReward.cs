using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 스테이지의 콜렉션 리스트의 아이템을 전부 모았을때 받게되는 보상 정보
    [Table("StageCompleteCollectionReward")]
    public partial class StageCompleteCollectionReward : BaseModel
    {
        // asset
        public AssetType AssetType { get; set; }
        public Int64 AssetId { get; set; }
        public int AssetCount { get; set; }

        // foreign key
        public Int64 StageId { get; set; }
        public Stage Stage { get; set; }
    }
}

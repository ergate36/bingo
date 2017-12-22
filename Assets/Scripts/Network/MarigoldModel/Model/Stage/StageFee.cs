using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 스테이지 입장료 (카드별)
    [Table("StageFee")]
    public partial class StageFee : BaseModel
    {
        public int CardCount { get; set; } // 플레이어가 가지고 들어갈 빙고판의 갯수

        // asset 입장료 재화
        public AssetType AssetType { get; set; }
        public Int64 AssetId { get; set; }
        public int AssetCount { get; set; }

        // 카드 갯수에 따라 추가로 얻어지는 컬렉션 카드의 갯수.
        public int CollectionCardCount { get; set; }

        // foreign key
        public Int64 StageId { get; set; }
        [JsonIgnore]
        public Stage Stage { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 상점에서 구입하게되는 상품의 정보를 기록하는 테이블
    [Table("StoreProduct")]
    public partial class StoreProduct : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string Comment { get; set; } // 상품 설명

        // 가격
        // 현재는 기획이 미정이라 가격을 하나로만 지정하게 했다.
        public AssetType PriceAssetType { get; set; }
        public Int64 PriceAssetId { get; set; }
        public int PriceAssetCount { get; set; }

        // 받게되는 상품
        // 현재는 기획이 미정이라 상품을 하나만 받도록 했다.
        public AssetType RewardAssetType { get; set; }
        public Int64 RewardAssetId { get; set; }
        public int RewardAssetCount { get; set; }
    }
}

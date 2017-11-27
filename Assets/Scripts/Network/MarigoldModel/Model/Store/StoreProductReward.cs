using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // 상점에서 구매시 지급되는 상품 정보
    [Table("StoreProductReward")]
    public partial class StoreProductReward : BaseModel
    {
        // foreign key
        public long StoreProductId { get; set; }
        public StoreProduct StoreProduct { get; set; }

        public AssetType AssetType { get; set; }
        public Int64 AssetId { get; set; }
        public int AssetCount { get; set; }
    }
}

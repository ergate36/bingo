using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 상점에서 구입하게되는 상품의 정보를 기록하는 테이블
    [Table("StoreProduct")]
    public partial class StoreProduct : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string Comment { get; set; } // 상품 설명

        public int Category { get; set; } // 상품의 카테고리

        public DateTime SaleBegin { get; set; } // 판매 시작 시간
        public DateTime SaleEnd { get; set; } // 판매 종료 시간

        public int MaxBuyCount { get; set; } // 최대 구매 횟수

        // 가격
        // 가격은 하나만 지정할 수 있다.
        public AssetType PriceAssetType { get; set; }
        public Int64 PriceAssetId { get; set; }
        public int PriceAssetCount { get; set; }

        // 구입시 받게되는 보상은 갯수가 여러개일 수 있어서 StoreProductReward 테이블에 별도로 분리되어있다.
        [JsonIgnore]
        public List<StoreProductReward> StoreProductRewardList { get; set; }
    }
}

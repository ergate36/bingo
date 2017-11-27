using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // 스페셜 상점 상품 정보
    [Table("StoreSpecialSet")]
    public partial class StoreSpecialSet : BaseModel
    {
        // 터치 영역 정보
        public float TouchX { get; set; }
        public float TouchY { get; set; }
        public float TouchWidth { get; set; }
        public float TouchHeight { get; set; }

        // foreign key
        public long StoreProductId { get; set; } // 실제로 구매하게될 상품의 정보
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // 스페셜 상점 이벤트 정보
    [Table("StoreSpecial")]
    public partial class StoreSpecial : BaseModel
    {
        public DateTime SaleBegin { get; set; } // 판매 시작 시간
        public DateTime SaleEnd { get; set; } // 판매 종료 시간. (관련된 상품의 판매 시간과 같이 맞춰야 한다.)

        public string Sprite { get; set; } // 보여줄 이미지
    }
}

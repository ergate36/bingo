using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // 페이스북 결제를 위해서 필요한 정보. 플랫폼별로 필요한 정보가 다를 수 있기때문에 별도의 테이블을 만들도록 했다.
    [Table("PaymentFacebook")]
    public partial class PaymentFacebook : BaseModel
    {
        // foreign key
        public long PaymentId { get; set; }

        public float Price { get; set; } // 상품 가격

        public string ProductId { get; set; } // 특정 제품의 결제를 위해 필요한 값
    }
}

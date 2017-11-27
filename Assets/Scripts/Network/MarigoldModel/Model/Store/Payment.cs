using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // 사용자 in app 결재에 대한 정보.
    [Table("Payment")]
    public partial class Payment : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string Comment { get; set; } // 상품 설명
    }
}

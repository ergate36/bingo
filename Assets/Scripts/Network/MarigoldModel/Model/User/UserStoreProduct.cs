using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 사용자의 StoreProduct 구입에 관련된 정보를 담는 테이블
    [Table("UserStoreProduct")]
    public partial class UserStoreProduct : BaseUserModel
    {
        public Int64 StoreProductId { get; set; } // 어떤 상품에 대한 정보인지 구별함.

        public Int64 BuyCount { get; set; } // 구입 회수
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 유저가 과금한 기록이 저장되는 곳
    [Table("UserPayment")]
    public partial class UserPayment : BaseUserModel
    {
        // foreign key
        public long PaymentId { get; set; } // 결제에 대한 정보

        public int BuyCount { get; set; } // 총 구매횟수. 총 구매 횟수를 기록으로 남기기 위해 숫자를 유지한다. 
        public int UseCount { get; set; } // 사용한 횟수
    }
}

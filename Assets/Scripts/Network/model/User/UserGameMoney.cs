using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 사용자의 GameMoney에 관련된 정보를 담는 테이블
    [Table("UserGameMoney")]
    public partial class UserGameMoney : BaseUserModel
    {
        public Int64 GameMoneyId { get; set; } // 가지고 있는 콜렉션의 아이디

        public Int64 Count { get; set; }
    }
}

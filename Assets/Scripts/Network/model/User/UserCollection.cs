using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 유저가 가지고 있는 collection의 정보를 기록한다.
    [Table("UserCollection")]
    public partial class UserCollection : BaseUserModel
    {
        public Int64 CollectionId { get; set; } // 가지고 있는 콜렉션의 아이디

        public Int64 Count { get; set; }
    }
}

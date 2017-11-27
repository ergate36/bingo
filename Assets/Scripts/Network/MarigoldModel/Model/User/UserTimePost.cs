using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // TimePost에 의해 지급된 보상을 기록한다.
    [Table("UserTimePost")]
    public partial class UserTimePost : BaseUserModel
    {
        // 받은 TimePost의 아이디
        public long TimePostId { get; set; }
    }
}

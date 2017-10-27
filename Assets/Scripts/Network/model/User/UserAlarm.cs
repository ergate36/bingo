using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 유저에게 알려줘야 하는 정보를 기록하는 테이블
    [Table("UserAlarm")]
    public partial class UserAlarm : BaseUserModel
    {
        public string Message { get; set; }

        public bool Received { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}

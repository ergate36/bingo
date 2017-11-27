using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // 채팅을 막은 유저의 목록을 기록한다.
    [Table("UserMute")]
    public partial class UserMute : BaseUserModel
    {
        public long MuteUserAccountId { get; set; }
    }
}

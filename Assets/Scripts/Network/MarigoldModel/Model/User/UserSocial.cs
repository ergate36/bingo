using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    [Table("UserSocial")]
    public partial class UserSocial : BaseUserModel
    {
        public SocialType SocialType { get; set; }

        [Column(TypeName = "varchar(100)")]
        public String SocialKey { get; set; } // 연결된 소셜 키

        [Column(TypeName = "datetime")]
        public DateTime BindAt { get; set; } // 로그인한 시각
    }
}

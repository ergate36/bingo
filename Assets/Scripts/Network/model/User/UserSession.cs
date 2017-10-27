using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 현재 접속중인 유저의 세션정보를 기록한다
    // 개발단계에서는 빠는 확인을 위해 Mysql에 기록해서 작업하고 실서버에 올라갈때는 redis에 올리도록 추가적으로 작업한다.(SessionKey를 키로 사용한다.)
    [Table("UserSession")]
    public partial class UserSession : BaseUserModel
    {
        [Column(TypeName = "varchar(100)")]
        public String SessionKey { get; set; } // 세션키
        [Column(TypeName = "datetime")]
        public DateTime LoginAt { get; set; } // 로그인한 시각

        // 현재 접속중인 게임서버 관련 정보
        [Column(TypeName = "varchar(100)")]
        public String GameSessionId { get; set; } // 유저가 현재 게임에 접속했을 경우 접속한 서버의 아이디. GameLift API의 이름이 GameSessionId이기 때문에 똑같이 이름을 만들었다.
    }
}

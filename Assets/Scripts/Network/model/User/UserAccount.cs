using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 기본적인 테이블 정보
    [Table("UserAccount")] // 따로 지정하지 않으면 Context의 변수명을 따라가게 된다.
    public partial class UserAccount : BaseModel
    {
        // 보통의 상황에서는 Session을 이용해서 UserSession을 찾는다.
        // 그러나 중복접속을 체크하는 상황에서는 UserAccount.Session값과 현재 세션의 값을 비교해서 판단한다.
        // 현재 접속한 세션 정보
        [Column(TypeName = "varchar(100)")]
        public string SessionKey { get; set; } // 현재 접속한 세션키

        // 사용자 인증을 위해 서버에서 만들어진 비밀번호.
        // 클라이언트에 전달되면 클라이언트는 이 비밀번호를 저장하고 있다가, 로그인 요청시에 보내야 한다.
        [Column(TypeName = "varchar(100)")]
        public string Secret { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        // 기본 정보로 보여주기 때문에 UserAccount에서 같이 처리하도록 함.
        public int Level { get; set; } // 캐릭터 레벨
        public int Experience { get; set; } // 캐릭터 경험치
    }
}

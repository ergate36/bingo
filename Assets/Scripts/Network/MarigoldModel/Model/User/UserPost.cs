using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // 유저에게 보내진 우편을 기록한다.
    [Table("UserPost")]
    public partial class UserPost : BaseUserModel
    {
        // 내부
        [Column(TypeName = "varchar(255)")]
        public string Category { get; set; } // 우편을 분류하기 위한 변수. 클라이언트에서 보여주진 않음. 
        public bool Received { get; set; } // 우편에 포함된 아이템이 지급되었는지 확인

        // 우편 메시지
        public string Title { get; set; } // 클라이언트에서 보여줄 메시지 제목
        public string Message { get; set; } // 클라이언트에서 보여줄 메시지 내용. 긴 내용도 쓸 수 있다.

        // 우편 만료 가능 변수
        public bool ExpireUse { get; set; }
        public DateTime ExpireBegin { get; set; }
        public DateTime ExpireEnd { get; set; }

        // 지급될 상품
        public AssetType AssetType { get; set; }
        public Int64 AssetId { get; set; }
        public int AssetCount { get; set; }
    }
}

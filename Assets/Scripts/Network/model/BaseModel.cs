using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    public class BaseModel
    {
        public Int64 Id { get; set; }

        // migration 생성시 순서가 ABC 순으로 정렬되기 때문에 수동으로 바꿔줘야 한다.
        // 만약 바꾸는데 실패했을 경우 빈 마이그레이션을 만든 후 query를 직접 입력해서 해결할 수 있다.
        // database를 롤백하는건 데이터가 사라질 위험이 있기 때문에 주의해야한다.
        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
    }
}

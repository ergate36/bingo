using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarigoldModel.Model
{
    // 콜렉션을 얻을 수 있는 곳에 대한 정보
    [Table("CollectionObtainLocation")]
    public partial class CollectionObtainLocation : BaseModel
    {
        // foreign key
        public Int64 CollectionId { get; set; }
        public Collection Collection { get; private set; }

        // location type
        [Column(TypeName = "varchar(100)")]
        public string LocationName { get; set; } // 획득처 정보
    }
}

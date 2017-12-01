using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MarigoldModel.Model
{
    // 수집품 리스트 테이블
    [Table("Collection")]
    public partial class Collection : BaseModel
    {
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } // 수집품의 이름 (다국어 지원시 테이블을 만들어서 분리할 필요가 있다.)
        [Column(TypeName = "mediumtext")]
        public string Description { get; set; } // 수집품의 설명 (다국어 지원시 테이블을 만들어서 분리할 필요가 있다.)

        [Column(TypeName = "varchar(255)")]
        public string AtlasName { get; set; } // 수집품의 이미지를 보여주기 위한 정보(client 정보)
        [Column(TypeName = "varchar(255)")]
        public string SpriteName { get; set; } // 수집품의 이미지를 보여주기 위한 정보(client 정보)

        // foreign key 
        [JsonIgnore]
        internal List<CollectionObtainLocation> CollectionObtainLocationList { get; set; }

        // foreign key
        public Int64 StageId { get; set; }
        public Stage Stage { get; set; }
    }
}

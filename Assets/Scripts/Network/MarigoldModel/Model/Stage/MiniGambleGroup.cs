using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // 미니 갬블에서 나올 수 있는 보상의 그룹을 보여준다.
    [Table("MiniGambleGroup")]
    public partial class MiniGambleGroup : BaseModel
    {
        [Column(TypeName = "varchar(255)")]
        public string Comment { get; set; } // 설명

        public int TotalRate { get; set; } // RandomRewardSet의 확률을 전부 더한 값

        // foreign key
        public Int64 MiniGamblePriceId { get; set; }
        public MiniGamblePrice MiniGamblePrice { get; set; }

        // foreign key
        [JsonIgnore]
        public List<MiniGambleGroupSet> MiniGambleGroupSetList { get; internal set; }
    }
}

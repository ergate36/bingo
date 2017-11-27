using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    [Table("MiniGambleGroupSet")]
    public partial class MiniGambleGroupSet : BaseModel
    {
        public int Rate { get; set; } // 각각 Set이 나올 확률

        public AssetType AssetType { get; set; }
        public long AssetId { get; set; }
        public int AssetCount { get; set; }

        // foreign key
        public Int64 MiniGambleGroupId { get; set; }
        public MiniGambleGroup MiniGambleGroup { get; set; }
    }
}

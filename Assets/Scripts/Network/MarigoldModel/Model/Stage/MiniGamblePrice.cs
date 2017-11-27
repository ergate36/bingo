using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    [Table("MiniGamblePrice")]
    public partial class MiniGamblePrice : BaseModel
    {
        // 기본 가격 정보
        public AssetType PriceAssetType { get; set; }
        public long PriceAssetId { get; set; }
        public int PriceAssetCount { get; set; }

        // foreign key
        public List<MiniGambleGroup> MiniGambleGroupList { get; set; }
        public List<MiniGamblePriceSet> MiniGamblePriceSetList { get; set; }
    }
}

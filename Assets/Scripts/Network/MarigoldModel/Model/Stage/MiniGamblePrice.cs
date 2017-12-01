using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        [JsonIgnore]
        public List<MiniGambleGroup> MiniGambleGroupList { get; set; }

        [JsonIgnore]
        public List<MiniGamblePriceSet> MiniGamblePriceSetList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MarigoldModel.Model
{
    // 운영 목적을 위해 유저가 일정 시간에 접속시 보상 우편을 보내도록 한다.
    [Table("TimePost")]
    public partial class TimePost : BaseModel
    {
        // 보상을 받을 수 있는 시간
        public DateTime ReceiveBegin { get; set; }
        public DateTime ReceiveEnd { get; set; }

        // 받게될 우편에 대한 정보   
        public string PostCategory { get; set; }
        public string PostTitle { get; set; }
        public string PostMessage { get; set; }

        public AssetType PostAssetType { get; set; }
        public Int64 PostAssetId { get; set; }
        public int PostAssetCount { get; set; }

        public bool PostExpireUse { get; set; }
        public DateTime PostExpireBegin { get; set; }
        public DateTime PostExpireEnd { get; set; }
    }
}

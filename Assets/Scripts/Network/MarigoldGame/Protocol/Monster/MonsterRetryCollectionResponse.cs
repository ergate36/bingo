using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using MarigoldModel.Commands;

namespace MarigoldGame.Protocol.Monster
{
    public class MonsterRetryCollectionResponse : BaseResponse
    {
        public Command Command { get; internal set; }

        public MonsterRetryCollectionResponse() : base(MessageType.MonsterRetryCollectionResponse)
        {
        }
    }
}

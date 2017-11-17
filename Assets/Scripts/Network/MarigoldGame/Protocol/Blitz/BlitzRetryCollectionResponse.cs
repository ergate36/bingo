using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using MarigoldModel.Commands;

namespace MarigoldGame.Protocol.Blitz
{
    public class BlitzRetryCollectionResponse : BaseResponse
    {
        public Command Command { get; internal set; }

        public BlitzRetryCollectionResponse() : base(MessageType.BlitzRetryCollectionResponse)
        {
        }
    }
}

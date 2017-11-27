using MarigoldModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace MarigoldGame.Protocol
{
    class BlitzChattingResponse : BaseResponse
    {
        public BlitzChattingResponse() : base(MessageType.BlitzChattingResponse)
        {
        }

        public Command Command { get; set; }
    }
}

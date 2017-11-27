using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace MarigoldGame.Game.Blitz
{
    // 재시도 요청을 위해 서버에서 저장해둬야 하는 값들을 관리한다.
    class RetryCollection
    {
        public long CollectionId { get; set; }
    }
}

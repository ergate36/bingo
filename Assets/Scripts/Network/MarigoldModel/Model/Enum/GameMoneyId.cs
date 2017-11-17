using System;
using System.Collections.Generic;
using System.Text;

namespace MarigoldModel.Model
{
    // 게임머니의 ID를 숫자를 쓰지않고 표시하기위해 선언. 
    // DB 테이블의 ID와 일치해야하기 때문에 수정시 주의가 필요하다.
    public enum GameMoneyId : long
    {
        COIN = 1,
        CREDIT = 2,
    }
}

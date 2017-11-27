namespace MarigoldGame.Protocol
{
    // 패킷내용구성
    // 헤더(2byte) + 패킷사이즈(2byte) + 구조체내용
    // 헤더 : 0xffff
    // 패킷사이즈 : 헤더사이즈(2byte)+패킷사이즈의사이즈(2byte)+구조체사이즈

    public enum MessageType : ushort
    {
        HeartBeatRequest                = 97,       //하트비트
        HeartBeatResponse               = 98,       //하트비트

        ServerEnterRequest              = 198,	    //게임서버 입장 요청
        ServerEnterResponse             = 199,      //게임서버 입장 응답     
        QuickMatchRequest               = 200,	    //매치 요청
        QuickMatchResponse              = 201,	    //매치 응답     
        MatchSuccessAlarm               = 202,	    //매치성공 요청
        MatchCancelAlarm                = 203,	    //매치 취소됨 알림?
        MatchCancelRequest              = 204,	    //매치취소 요청
        MatchCancelResponse             = 205,	    //매치취소 응답  
        GameStartRequest                = 206,	    //게임스타트 요청
        GameStartResponse               = 207,	    //게임스타트 응답  
        GameStartAlarm                  = 208,	    //게임스타트 응답  
        ReadySuccessRequest             = 209,	    //준비완료 요청
        ReadySuccessResponse            = 210,	    //준비완료 응답  
        ReadySuccessAlarm               = 211,	    //준비완료 알림 응답  
        GameMessageRequest              = 212,	    //게임 메시지 요청
        GameMessageResponse             = 213,	    //게임 메시지 응답  
        GameMessageAlarm                = 214,	    //게임 메시지 알림 응답  
        ItemMessageRequest              = 215,	    //아이템 메시지 요청
        ItemMessageResponse             = 216,	    //아이템 메시지 응답
        ItemMessageAlarm                = 217,	    //아이템 메시지 알림 응답
        GameCalRequest                  = 218,	    //빙고결과 요청
        GameCalResponse                 = 219,	    //빙고결과 응답  
        GameCalAlarm                    = 220,	    //게임결과 알림 응답  
        UserExitAlarm                   = 221,	    //상대방 나감 알림 응답  
        MatchFailAlarm                  = 222,	    //매치실패 알림 응답         
        ConnectExitRequest              = 223,	    //접속종료 요청
        BingoRankingAlarm               = 224,	    //빙고랭킹 알림 응답
        ScoreUpdateRequest              = 225,	    //빙고결과 요청
        ScoreUpdateResponse             = 226,	    //빙고결과 응답  
        // 블리츠 모드 (메인 스테이지 모드)
        BlitzWaitRoomStatusAlarm        = 1002,     // 대기룸 상태 알림
        BlitzEnterGameRequest           = 1003,     // 게임 입장 요청
        BlitzEnterGameResponse          = 1004,     // 게임 입장 요청 응답
        BlitzStartGameAlarm             = 1005,     // 게임 시작 알림
        BlitzCallNumberAlarm            = 1006,     // 빙고 번호 알림
        BlitzCompleteBingoRequest       = 1007,     // 빙고 완성 요청
        BlitzCompleteBingoResponse      = 1008,     // 빙고 완성 요청 응답
        BlitzCompleteBingoAlarm         = 1009,     // 빙고 완성 알림
        BlitzEndGameAlarm               = 1010,     // 게임 끝 알림
        BlitzUsePowerUpRequest          = 1011,     // 아이템 사용 요청
        BlitzUsePowerUpResponse         = 1012,     // 아이템 사용 결과
        // (게임중 아이템이 떨어지면 람다 서버에 요청을 보내 구매하게 하고, 구매후 재확인 요청을 클라에서 보낸다.)
        // 재확인 요청을 보내지 않으면 새로 얻은 아이템을 사용할 수 없음.(게임서버에서는 바뀐걸 알 수 없음)
        BlitzRefreshPowerUpRequest      = 1013,     // 아이템 재확인 요청
        BlitzRefreshPowerUpResponse     = 1014,     // 아이템 재확인 응답
        BlitzGambleRequest              = 1015,     // 미니 게임 요청
        BlitzGambleResponse             = 1016,     // 미니 게임 응답
        BlitzCheckNumberRequest         = 1017,     // 빙고 숫자 체크 요청
        BlitzCheckNumberResponse        = 1018,     // 빙고 숫자 체크 응답
        BlitzRobotStateAlarm            = 1019,     // DEBUG용으로 로봇의 상태를 전달함.
        BlitzClearRewardAlarm           = 1020,     // 게임 끝날을때 보상정보를 알려준다.
        BlitzRetryCollectionRequest     = 1021,     // 못한 컬렉션 카드를 다시 시도할 수 있게 저장한다.
        BlitzRetryCollectionResponse    = 1022,     // 못한 컬렉션 카드 저장 응답
        BlitzChattingRequest            = 1023,     // 채팅 메시지 보내기
        BlitzChattingResponse           = 1024,     // 채팅 메시지 응답(요청에 대한 응답. 채팅 메시지는 들어있지 않음)
        BlitzChattingAlarm              = 1025,     // 채팅 메시지 알림
        // GameLift
        GameLiftConnectRequest          = 1101,     // 게임 리프트 인증 요청
        GameLiftConnectResponse         = 1102,     // 게임 리프트 인증 응답
        // Monster (서브 스테이지 모드)
        MonsterEnterGameRequest = 1203,     // 게임 입장 요청
        MonsterEnterGameResponse = 1204,     // 게임 입장 요청 응답
        MonsterStartGameAlarm = 1205,     // 게임 시작 알림
        MonsterCallNumberAlarm = 1206,     // 빙고 번호 알림
        MonsterCompleteBingoRequest = 1207,     // 빙고 완성 요청
        MonsterCompleteBingoResponse = 1208,     // 빙고 완성 요청 응답
        MonsterCompleteBingoAlarm = 1209,     // 빙고 완성 알림
        MonsterEndGameAlarm = 1210,     // 게임 끝 알림
        MonsterUsePowerUpRequest = 1211,     // 아이템 사용 요청
        MonsterUsePowerUpResponse = 1212,     // 아이템 사용 결과
        // 
        MonsterRefreshPowerUpRequest = 1213,     // 아이템 재확인 요청
        MonsterRefreshPowerUpResponse = 1214,     // 아이템 재확인 응답
        MonsterGambleRequest = 1215,     // 미니 게임 요청
        MonsterGambleResponse = 1216,     // 미니 게임 응답
        MonsterCheckNumberRequest = 1217,     // 빙고 숫자 체크 요청
        MonsterCheckNumberResponse = 1218,     // 빙고 숫자 체크 응답
        MonsterRobotStateAlarm = 1219,     // DEBUG용으로 로봇의 상태를 전달함.
        MonsterClearRewardAlarm = 1220,     // 게임 끝날을때 보상정보를 알려준다.
        MonsterRetryCollectionRequest = 1221,     // 못한 컬렉션 카드를 다시 시도할 수 있게 저장한다.
        MonsterRetryCollectionResponse = 1222,     // 못한 컬렉션 카드 저장 응답
        MonsterOpponentStateAlarm = 1223, // 상대방 정보 알림
        // DEBUG
        KillServerRequest = 9777,
        KillServerResponse = 9778,
    };
}


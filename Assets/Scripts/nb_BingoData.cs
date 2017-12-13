using MarigoldModel.Model;
public struct nb_gameData
{
    public long userID;
    public string nickname;
    public int[,] sheet;

    public int[] bingoState;
    public int itemIndex;
    public int itemTarget;
    public int sheetIndex;
    public int[] sheildState;

}

//public struct nb_bingoItem

public class nb_SocketClass
{
    public enum MsgType
    {
        HeartBeatRequest = 97,       //하트비트
        HeartBeatResponse = 98,       //하트비트

        ServerEnterRequest = 198,	    //게임서버 입장 요청
        ServerEnterResponse = 199,      //게임서버 입장 응답     
        QuickMatchRequest = 200,	    //매치 요청
        QuickMatchResponse = 201,	    //매치 응답     
        MatchSuccessAlarm = 202,	    //매치성공 요청
        MatchCancelAlarm = 203,	    //매치 취소됨 알림?
        MatchCancelRequest = 204,	    //매치취소 요청
        MatchCancelResponse = 205,	    //매치취소 응답  
        GameStartRequest = 206,	    //게임스타트 요청
        GameStartResponse = 207,	    //게임스타트 응답  
        GameStartAlarm = 208,	    //게임스타트 응답  
        ReadySuccessRequest = 209,	    //준비완료 요청
        ReadySuccessResponse = 210,	    //준비완료 응답  
        ReadySuccessAlarm = 211,	    //준비완료 알림 응답  
        GameMessageRequest = 212,	    //게임 메시지 요청
        GameMessageResponse = 213,	    //게임 메시지 응답  
        GameMessageAlarm = 214,	    //게임 메시지 알림 응답  
        ItemMessageRequest = 215,	    //아이템 메시지 요청
        ItemMessageResponse = 216,	    //아이템 메시지 응답
        ItemMessageAlarm = 217,	    //아이템 메시지 알림 응답
        GameCalRequest = 218,	    //빙고결과 요청
        GameCalResponse = 219,	    //빙고결과 응답  
        GameCalAlarm = 220,	    //게임결과 알림 응답  
        UserExitAlarm = 221,	    //상대방 나감 알림 응답  
        MatchFailAlarm = 222,	    //매치실패 알림 응답         
        ConnectExitRequest = 223,	    //접속종료 요청
        BingoRankingAlarm = 224,	    //빙고랭킹 알림 응답
        ScoreUpdateRequest = 225,	    //빙고결과 요청
        ScoreUpdateResponse = 226,	    //빙고결과 응답  
        // 블리츠 모드
        //BlitzEnterWaitRoomRequest = 1000,     // 대기룸 입장 요청
        //BlitzEnterWaitRoomResponse = 1001,     // 대기룸 입장 요청 응답
        BlitzWaitRoomStatusAlarm = 1002,     // 대기룸 상태 알림
        BlitzEnterGameRequest = 1003,     // 게임 입장 요청
        BlitzEnterGameResponse = 1004,     // 게임 입장 요청 응답
        BlitzStartGameAlarm = 1005,     // 게임 시작 알림
        BlitzBingoNumberAlarm = 1006,     // 빙고 번호 알림
        BlitzCompleteBingoRequest = 1007,     // 빙고 완성 요청
        BlitzCompleteBingoResponse = 1008,     // 빙고 완성 요청 응답
        BlitzCompleteBingoAlarm = 1009,     // 빙고 완성 알림
        BlitzEndGameAlarm = 1010,     // 게임 끝 알림
        
        BlitzUsePowerUpRequest = 1011,  //아이템 사용 요청
        BlitzUsePowerUpResponse = 1012, //아이템 사용 응답
        BlitzRefreshPowerUpRequest = 1013,  //아이템 재확인 요청
        BlitzRefreshPowerUpResponse = 1014, //아이템 재확인 응답
        BlitzGambleRequest = 1015,  //미니 게임 요청
        BlitzGambleResponse = 1016, //미니 게임 응답
        BlitzCheckNumberRequest = 1017, //빙고 숫자 체크 요청
        BlitzCheckNumberResponse = 1018,    //빙고 숫자 체크 응답
        BlitzRobotStateAlarm = 1019,    //로봇 상태 알림. AI의 현재 상태를 알려준다.
        BlitzClearRewardAlarm = 1020,     // 게임 끝날을때 보상정보를 알려준다.
        BlitzRetryCollectionRequest = 1021,     // 못한 컬렉션 카드를 다시 시도할 수 있게 저장한다.
        BlitzRetryCollectionResponse = 1022,     // 못한 컬렉션 카드 저장 응답

        BlitzChattingRequest = 1023,     // 채팅 메시지 보내기
        BlitzChattingResponse = 1024,     // 채팅 메시지 응답(요청에 대한 응답. 채팅 메시지는 들어있지 않음)
        BlitzChattingAlarm = 1025,     // 채팅 메시지 알림
        
        // GameLift
        GameLiftConnectRequest = 1101,     // 게임 리프트 인증 요청
        GameLiftConnectResponse = 1102,     // 게임 리프트 인증 응답

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

        KillServerRequest = 9777,   //서버 강제 종료
        KillServerResponse = 9778,
    };
    public enum STATE
    {
        excessTime = (short)888,
        connectionFail = (short)666,
        NaverconnectionFail = (short)667,

        waitSign = (short)999,

        mHeartBeat = (short)97,                //하트비트

        mGameServerJoinRequest = (short)198,
        mGameServerJoinIng = (short)199,
        mGameServerJoinIngComplete = (short)399,

        mMatchRequest = (short)200,
        mMatchIng = (short)201,
        mMatchIngComplete = (short)401,

        mMatchSuccessNoticeComplete = (short)402,
        mMatchCancelNoticeResponse = (short)403,

        mMatchCancelRequest = (short)204,
        mMatchCancelIng = (short)205,
        mMatchCancelIngComplete = (short)405,

        GameMessageRequest_End = (short)212,	    //게임 메시지 요청
        GameMessageResponse_End = (short)213,	    //게임 메시지 응답
        GameMessageAlarm_End = (short)214,	    //게임 메시지 알림 응답  

        ItemMessageRequest_End = (short)215,	    //아이템 메시지 요청
        ItemMessageResponse_End = (short)216,	    //아이템 메시지 응답
        ItemMessageAlarm_Ing = (short)217,	    //아이템 메시지 알림
        ItemMessageAlarm_End = (short)10217,	    //아이템 메시지 알림 응답

        GameCalRequest_End = (short)218,	    //빙고결과 요청
        GameCalResponse_End = (short)219,	    //빙고결과 응답  
        GameCalAlarm_End = (short)220,	    //게임결과 알림 응답  
        
        UserExitAlarm_End = (short)221,	    //상대방 나감 알림 응답  

        mClose = (short)223,
        mCloseIng = (short)433,
        mCloseComplete = (short)423,

        BingoRankingAlarm_End = (short)224,     //빙고 랭킹 알람

        /////////////////////////////////////////////
        
        //BlitzEnterWaitRoomRequest_Start = (short)1000,  //대기실 매칭 요청
        //BlitzEnterWaitRoomResponse_End = (short)1001, //대기실 매칭 됨
        BlitzWaitRoomStatusAlarm_End = (short)1002,      //대기실에서 대기중

        BlitzEnterGameRequest_End = (short)1003,
        BlitzEnterGameResponse_End = (short)1004,
        mGameJoinIng = (short)11004,

        BlitzStartGameAlarm_End = (short)1005,

        BlitzBingoNumberAlarm_End = (short)1006,

        BlitzCompleteBingoRequest_End = (short)1007,
        BlitzCompleteBingoResponse_End = (short)1008,
        BlitzCompleteBingoAlarm_End = (short)1009,
        BlitzEndGameAlarm_End = (short)1010,

        GameLiftConnectResponse_End = (short)1102,

        BlitzUsePowerUpRequest_End = 1011,
        BlitzUsePowerUpResponse_End = 1012,

        BlitzRefreshPowerUpRequest_End = 1013,
        BlitzRefreshPowerUpResponse_End = 1014,

        BlitzCheckNumberRequest_End = 1017,
        BlitzCheckNumberResponse_End = 1018,
        BlitzRobotStateAlarm_End = 1019,
        BlitzClearRewardAlarm_End = 1020,
        BlitzRetryCollectionRequest_End = 1021,
        BlitzRetryCollectionResponse_End = 1022,

        // Monster (서브 스테이지 모드)
        MonsterEnterGameRequest_End = 1203,     // 게임 입장 요청
        MonsterEnterGameResponse_End = 1204,     // 게임 입장 요청 응답
        MonsterStartGameAlarm_End = 1205,     // 게임 시작 알림
        MonsterCallNumberAlarm_End = 1206,     // 빙고 번호 알림
        MonsterCompleteBingoRequest_End = 1207,     // 빙고 완성 요청
        MonsterCompleteBingoResponse_End = 1208,     // 빙고 완성 요청 응답
        MonsterCompleteBingoAlarm_End = 1209,     // 빙고 완성 알림
        MonsterEndGameAlarm_End = 1210,     // 게임 끝 알림
        MonsterUsePowerUpRequest_End = 1211,     // 아이템 사용 요청
        MonsterUsePowerUpResponse_End = 1212,     // 아이템 사용 결과
        // 
        MonsterRefreshPowerUpRequest_End = 1213,     // 아이템 재확인 요청
        MonsterRefreshPowerUpResponse_End = 1214,     // 아이템 재확인 응답
        MonsterGambleRequest_End = 1215,     // 미니 게임 요청
        MonsterGambleResponse_End = 1216,     // 미니 게임 응답
        MonsterCheckNumberRequest_End = 1217,     // 빙고 숫자 체크 요청
        MonsterCheckNumberResponse_End = 1218,     // 빙고 숫자 체크 응답
        MonsterRobotStateAlarm_End = 1219,     // DEBUG용으로 로봇의 상태를 전달함.
        MonsterClearRewardAlarm_End = 1220,     // 게임 끝날을때 보상정보를 알려준다.
        MonsterRetryCollectionRequest_End = 1221,     // 못한 컬렉션 카드를 다시 시도할 수 있게 저장한다.
        MonsterRetryCollectionResponse_End = 1222,     // 못한 컬렉션 카드 저장 응답
        
        MonsterOpponentStateAlarm_End = 1223,


        KillServerRequest_End = 9777,   //서버 강제 종료
        KillServerResponse_End = 9778,
    };
}

public class nb_CardEffect
{
    public enum CardType
    {
        Free_daub = 0,
        Double_daub,
        Shop_BingoTicket,
        Shop_Card,

        Shop_Item,

        Shop_Max,
    }

    static public string[] itemImagePath =
    {
        "item_buy_5",
        "item_buy_10",
        "item_buy_20",
        "item_buy_40",
    };
}

public struct nb_MyCard
{
    public int cardno;
	public int cardId;
	public int cardexp;
    public int cardlevel;
    public int cardused;
    public int cardRank;
    public int cardRare;

	public int AT1;
    public float ATv1;
    public int AT2;
    public float ATv2;
    public int AT3;
    public float ATv3;
}

public struct nb_friendData
{
    public string FriendName;
    public int FriendId;
    public string FriendKey;

    public int flag;
}

public struct nb_friendInfo
{
    public int score_rank;
    public int coin_rank;
    public int score;
    public int coin;

    public int[] slot;

    public int battle_rank;
    public int win;
    public int lose;


    public int flag;
}

public struct nb_MonsterCardXML
{
    public int Id;
    public string Name;
    public string Img;
    public int Rank;
    public int Rare;

    public int Ability1;
    public float Lv1ability1value;
    public float AT1Add;

    public int Ability2;
    public float Lv1ability2value;
    public float AT2Add;

    public int Ability3;
    public float Lv1ability3value;
    public float AT3Add;
}

public struct nb_MonsterRankXML
{
    public int Id;
    public int Rank;
    public int Level;
    public int Exp;
    public int Sell;

    public int Enchant;
    public int MaterialExp;
    public int MixGacha;
}

public struct nb_RankXML
{
    public int Id;
    public int Type;
    public int Value;
    public int Bscore;
    public int Bgold;
    public int Gacha;
}

public struct nb_GachaXML
{
    public int Id;
    public int Group; 
    public int Value;
    public int LookValue;
    public int Type;
    public int TypeValue;
}

public struct nb_urlInfo
{
    public int notice_Num;
    public string img_url;
    public string move_url;
    public int todayNotView;
}

public struct nb_MyInfo
{
    public string deviceId;
    public string bandKey;
    public string userKey;
    public long userID;
    public string nickName;
    public int win;
    public int lose;

    public int gemCount;
    public int coinAmount;
    public int ticketCount;
    public int goldTicket;
    public int ItemCount;
    public int waitTime;

    public int GameScore;
    public int RankScore;
    public int MonsterScore;

    public int game_gold;
    public int game_ticket;
    public int game_goldticket;

    public int week_bestScore;
    public int week_bestCoin;
}

public struct nb_PlayerInfo
{
    public long userID;
    public string nickName;
    public bool bReady;
}

public struct nb_SheetInfo
{
    public long     userID;
    public string nickname;
    public string guid;
    public int monsterId;
    public long     roomkey;
    public int      activeSheetCount;
    public bool[]   bingoSheet;
    public int[,] sheet;
    public bool[,] sheetDaub;
    public int shield;
    public int betting_index;
}

public struct nb_BandInfo
{
    public string bandKey;
    public string bandName;
}

public class nb_Sound
{
    public enum TutorialSoundLIst
    { 
        Step_0 = 0,
        Step_1,
        Step_2,
        Step_3,
        Step_4,
        Step_5,
        Step_6,
        Step_7,
        Step_8,
        Step_9,
        Step_10,
        Step_MAX,
    }


    public enum EffSoundList
    { 
        Attack_Blind = 0,
        Attack_Frozen,
        Attack_Shuffle,
        Attack_Swap,
        BadBingo,

        bingo,
        click,
        Damage_Blind,
        Damage_Frozen,
        Damage_Shuffle,

        Damage_Swap,
        daub,
        itemeffect,
        nodaub,
        reward_bomb,
        reward_gold,
        reward_ticket,
        shieldOn,

        Attack_Hammer,
        cleardaub,
        tour_pass,
        selectClick,

        result_start,

        gacha_loop,
        alarm,
        itemeffect_bomb,
        itemeffect_ticket,

        shieldOff,
        yabawe_loop,
        yabawe_end,
        itemeffect_coin,
        Damage_Frozen_loop,
        EffectSound_MAX,
    }

    public enum TalkList
    {
        bad_bingo = 0,
        bingo ,
        bingo_d,
        bingo_monster,
        bingo_p,
        bingo_q,
        bingo_t,
        bingo_tonament_join,
        game_count,
        game_over,
        item_blind,
        item_freeze,
        item_getcoin,
        item_itemready,
        item_numbermix,
        item_shield,
        item_sidedaub,
        item_swap,
        EffectTalk_MAX,
    }
    

    public enum BGSoundLIst
    {
        EndingMusic = 0,
        IntroMusic,
        PlayMusic1,
        PlayMusic2,

        BGSound_MAX,
    }
}

public class nb_Shop
{
    static public int[] coinBonus =
    {
        500,
        2000,
        10000,
        50000,
        100000,
        300000,
    };

    static public int[] ticketBonus =
    {
        0,
        5,
        15,
        40,
    };

    static public int[] jamBonus =
    {
        0,
        5,
        30,
        120,
        260,
        840,
    };



    public enum TapType
    {         
        Shop_Gem = 0,
        Shop_Gold,                                                                                                                                                  
        Shop_BingoTicket,
        Shop_Card,

        Shop_Item,

        Shop_Max,
    }

    static public string[] itemImagePath =
    {
        "item_buy_5",
        "item_buy_10",
        "item_buy_20",
        "item_buy_40",
    };  

    public enum Item
    {
        item_5 = 0,
        item_10,
        item_20,
        item_40,
    };

    static public int[] itemCondition =
    {
        500,
        900,
        2500,
        4000,
    };

    static public string[] tapImagePath =
    {
        "Gem_button",
        "coin_button",
        "ticket_button",
        "card_button",
    };

    public enum Coin
    {
        coinItem_5000 = 0,
        coinItem_11000,
        coinItem_35000,
        coinItem_68000,
        coinItem_150000,
        coinItem_500000,
        coinItem_MAX,
    }

    static public int[] coinAmount =
    {
        5000,
        15000,
        50000,
        150000,
        250000,
        500000,
    };
    static public int[] gemAmount =
    {
        10,
        30,
        100,
        300,
        500,
        1000,
    };
    static public int[] ticketAmount =
    {
        5,
        30,
        60,
        120,
    };

    static public int[] coinCondition =
    {
        10,
        30,
        100,
        300,
        500,
        1000,
    };

    static public int[] gemCondition =
    {
        1000,
        3000,
        10000,
        30000,
        50000,
        100000,
    };


    static public string[] coinImagePath =
    {
        "5000coin",
        "11000coin",
        "35000coin",
        "68000coin",
        "150000coin",
        "500000coin",
    };

    public enum Gem
    {
        Gem_10 = 0,
        Gem_35,
        Gem_68,
        Gem_150,
        Gem_320,
        Gem_680,
        Gem_MAX,
    }

   
    static public string[] gemImagePath =
    {
        "10gem",
        "35gem",
        "68gem",
        "150gem",
        "320gem",
        "680gem",
    };

    public enum Ticket
    {
        Ticket_10 = 0,
        Ticket_25,
        Ticket_55,
        Ticket_120,

        Ticket_MAX,
    }

    static public int[] ticketCondition =
    {
        500,
        5,
        10,
        20,
    };

    static public string[] ticketImagePath =
    {
        "10ticket",
        "25ticket",
        "55ticket",
        "120ticket",

    };

    static public int[] cardCondition =
    {
        3000,
        30,
        250,
    };

    static public string[] cardConditionType =
    {
        "shop_coin",
        "card_gem",
        "card_gem",
    };


    public enum Card
    {
        Card_3000 = 0,
        Card_30,
        Card_250,
        Card_MAX,
    }

    static public string[] cardImagePath =
    {
        "card",
        "card_2",
        "card_3",
    };
    static public string[] cardEx =
    {
        "1-3",
        "2-5",
        "5-6",
    };


    // have to remove
    public enum ShopType
    {
        Shop_Gem = 0,
        Shop_Gold,
        Shop_BingoTicket,
        Shop_GoldTicket,

        Shop_Item,
        Shop_Card,

        Shop_Max,
    }

    static public int[] goldItemCount =
    {
        5000,
        15000,
        50000,
        150000,
        250000,
        500000,
    };

    static public int[] gemItemCount =
    {
        10,
        30,
        100,
        300,
        500,
        1000,
    };


    static public int[] itemItemCount =
    {
        5,
        10,
        30,
        50,
    };


    static public int[] bingoTicketItemCount =
    {
        5,
        30,
        60,
        120,
    };

    static public int[] cardItemCount =
    {
        11,
        12,
        13,
    };
}

public class nb_Item
{
    public enum nb_ItemType
    {
        Item_None = 0,
	
        Item_1_Daub = 1,            //1.single daub
	    Item_2_Coin = 2,            //2.coin reward
	    Item_3_Chest = 3,           //3.chest
	    Item_4_DoubleDaub = 4,      //4.double daubs
	    Item_5_DoubleExp = 5,       //5.double exp
	    Item_6_DoubleReward = 6,    //6.double reward
	    Item_7_Bomb = 7,            //7.bomb
	    Item_8_InstantWin = 8,      //8.instant win
	    Item_9_Booster = 9,         //9.booster
	    Item_10_TripleDaub = 10,    //10.triple daubs

        Item_11_SingleDust = 11,    //11.single dust
        Item_12_Fog = 12,           //12.fog
        Item_13_Blind = 13,         //13.blind
        Item_14_DoubleDust = 14,    //14.double dust
        Item_15_Shield = 15,        //15.shield
        Item_16_Mix = 16,           //16.mix
        Item_17_Jamming = 17,       //17.jamming
        Item_18_Freezing = 18,      //18.freezing
        Item_19_Avoid = 19,         //19.avoid
        Item_20_TripleDust = 20,    //20.triple dust
    }

    static public string[] nb_itemIconPath =
    {
        "ui_money_normal",
        "ui_item_normal1",
        "ui_item_normal2",
        "ui_item_normal3",
        "ui_item_normal4",
        "ui_item_normal5",
        "ui_item_normal6",
        "ui_item_normal7",
        "ui_item_normal8",
        "ui_item_normal9",
        "ui_item_normal10",
        "ui_item_battel1",  //battle 오타인데 일단 그냥 씀
        "ui_item_battel2",
        "ui_item_battel3",
        "ui_item_battel4",
        "ui_item_battel5",
        "ui_item_battel6",
        "ui_item_battel7",
        "ui_item_battel8",
        "ui_item_battel9",
        "ui_item_battel10",
    };

    static public string[] itemImageMent =
    {
        "item_unknown",
        "Immediately eliminates cells at random.",
        "Immediately eliminates randomized cells including ones at the both sides of them.",
        "Generates cells that enable you to get coins on the sheet.",
        "Generates cells that enable you to get coins on the sheet.",
        "Generates cells that enable you to get lots of coins on the sheet.",
        "Generates a cell that you can make Bingo with at one go. ",
        "Generates a cell that gives you a ticket on the sheet. ",
        "Hinders from controlling by freezing for 11 seconds.",
        "Hinders from hearing Bingo counter and other sounds for 7 seconds.",
        "Exchanges oppenent's sheet and mine.",
        "Change randomly the arrangement of sheets.",
        "Protects you once from the opponent's attack.",
        "item_goldticket"
    };



    static public string[] nb_daubItemImagePath =
    {
        "item_unknown",
        "item_unknown",
        "ui_item_normal2",
        "ui_item_normal3",
        "item_unknown",
        "ui_item_normal5",  //임시 이미지
        "ui_item_normal6",  //임시 이미지
        "ui_item_normal7",  //임시 이미지
        "ui_item_normal8",  //임시 이미지
        "ui_item_normal9",  //임시 이미지
        "item_unknown",
    };
}



// http://msdn.microsoft.com/ko-kr/library/0taef578(v=vs.90).aspx
// struct constructor ....
public struct nb_Cell
{
    public int index;
    public int number;
    public bool daub;
    public bool realDaub;
    public int itemEffectIndex;
}

public struct nb_BingoSheet
{
    public int sheetIndex;
    public nb_Cell[] cells;
}

public struct nb_otherBingoState
{
    public int userId;
    public bool[] bingo;
}


public struct nb_eventData
{
    public int productID;
    public int sell_Count;
    public int total_Count;
    public string img_url;
    public string name;
    public int[] bingo;
    public int bingoInfo;
}



public struct nb_UseItemInfo
{
    public long itemUserID;
    public long targetID;
    public int itemIndex;
    public int param1;
    public int param2;
};


public struct nb_RankInfo
{
    public string userKey;
    public string nickname;
    public int weekly_best;
    public int rank;
}

public struct nb_CoinRankInfo
{
    public string userKey;
    public string nickname;
    public int weekly_best;
    public int rank;
}

public struct nb_BattleRankInfo
{
    public string userKey;
    public string nickname;
    public int win;
    public int lose;
    public int rank;
}

public struct nb_BandRankInfo
{
    public string nickname;
    public int weekly_bestScore;
    public int rank;

}

public struct nb_MailInfo
{
    public string sender;
    public int giftIndex;
    public int giftCount;
    public string content;
    public int mailNo;
    public string eDate;
}


public struct nb_sWinner
{
    public string nickname;
    public int rank;
    public int monster_id;
    public int coin;
}


public struct nb_GachaInfo
{
    public int type;
    public int value;
    public int selected;
}
public struct nb_monsterSetInfo
{
    public int stat;
    public int card_num;
    public int sell_money;
    public int slot;
    public int card_id;

    public int card_level;
    public int card_exp;
    public int card_rank;
}

public struct nb_materialInfo
{
    public int material_Key;
    public int material_exp;
}

public struct nb_useItemData
{
    public int infoId;
    public int sheet;
    public int number;
}

public struct nb_userMoney
{
    public GameMoneyId id;
    public long value;
}
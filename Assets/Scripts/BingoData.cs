public class Ment
{
    static public string[] bingoTip =
    {
"TIP : If you want to use an attack item, click the oppenent and it'll be appllied.",
"TIP : Clicking Bingo cell will not activate the item when the mole is asleep.",
"TIP : Clicking Bingo will give you a win when there are one more Bingo lines in a horizontal, vertical, or diagonal row.",
"TIP : Items of Bingo at a breath can reverse your disadvantages.",
"TIP : Reinforcing card will lead to status improvement.",
"TIP : Withoust retained items no item comes out during the game.",
"TIP : You can browse abundant information in the official café of Bingo Monster.",
"TIP : Rewards vary according to the weekely ranking. Try to be ranked in the top.",
"TIP : The more tickets you consume, the more Bingo boards you can add.",
"TIP : Monster cards have respective statuses.",
"TIP : When you play 'BAD BINGO,' you can play it on a different Bingo board.",
"TIP : Shield items give a chance to protect the opponent's attack one time.",
"TIP : NumberMix items change the opponent's sheet at random!",
"TIP : Freeze items make the opponent's Bingo board frozen and uncontrollable!",
"TIP : Blind items hinder Bingo counter and sound of the opponent!",
"TIP : Swap items exchange your own Bingo board for your opponent's.",
"TIP : Free daub Items immediately eliminates cells at random!",
"TIP : Side daub Items immediately eliminates randomized cells including ones at the both sides of them.",
    };
}


public struct gameData
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

public class SocketClass
{
    public enum MsgType
    {
        mServerInfoRequest = (short)95,
        mServerInfoResponse = (short)96,
        mHeartBeat = (short)97,                //하트비트
        mServerTestRequest = (short)98,
        mServerTestResponse = (short)99,
        mNoticeRequest = (short)100,
        mNoticeResponse = (short)101,
        mLoginRequest = (short)102,
        mLoginResponse = (short)103,
        mMyGameInfoRequest = (short)104,
        mMyGameInfoResponse = (short)105,
        mBandkeySetRequest = (short)106,
        mBandkeySetResponse = (short)107,
        mBandListRequest = (short)108,
        mBandListResponse = (short)109,
        mTicketChargeRequest = (short)110,
        mTicketChargeResponse = (short)111,
        mTicketSendRequest = (short)112,
        mTicketSendResponse = (short)113,
        mTicketSettingRequest = (short)114,
        mTicketSettingResponse = (short)115,
        mItemShopRequest = (short)116,
        mItemShopResponse = (short)117,
        mBandRankingRequest = (short)118,
        mBandRankingResponse = (short)119,
        mWorldRankingRequest = (short)120,
        mWorldRankingResponse = (short)121,
        mCoinRankingRequest = (short)122,
        mCoinRankingResponse = (short)123,
        mMailGetRequest = (short)124,
        mMailGetResponse = (short)125,
        mMailCheckRequest = (short)126,
        mMailCheckResponse = (short)127,
        mDropRequest = (short)128,
        mDropResponse = (short)129,
        mMonsterGachaRequest = (short)130,
        mMonsterGachaResponse = (short)131,
        mPlayGachaRequest = (short)132,
        mPlayGachaResponse = (short)133,
        mMonsterCardListRequest = (short)134,
        mMonsterCardListResponse = (short)135,
        mMonsterCardUseRequest = (short)136,
        mMonsterCardUseResponse = (short)137,
        mMonsterCardMixRequest = (short)138,
        mMonsterCardMixResponse = (short)139,
        mEventListRequest = (short)140,
        mEventListResponse = (short)141,
        mEventGachaRequest = (short)142,
        mEventGachaResponse = (short)143,
        mEventMyInfoRequest = (short)144,
        mEventMyInfoResponse = (short)145,


        mGameServerJoinRequest = (short)198,
        mGameServerJoinResponse = (short)199,
        mMatchRequest = (short)200,
        mMatchResponse = (short)201,
        mMatchSuccessNoticeResponse = (short)202,
        mMatchCancelNoticeResponse = (short)203,
        mMatchCancelRequest = (short)204,
        mMatchCancelResponse = (short)205,
        mGameStartRequest = (short)206,
        mGameStartResponse = (short)207,
        mGameStartNoticeResponse = (short)208,
        mReadycompleteRequest = (short)209,
        mReadycompleteResponse = (short)210,
        mReadycompleteNoticeResponse = (short)211,
        mGameMessageRequest = (short)212,
        mGameMessageResponse = (short)213,
        mGameMessageNoticeResponse = (short)214,
        mItemMessageRequest = (short)215,
        mItemMessageResponse = (short)216,
        mItemMessageNoticeResponse = (short)217,
        mBingoResultRequest = (short)218,
        mBingoResultResponse = (short)219,

        mGameResultNoticeResponse = (short)220,

        mOtherOutResponse = (short)221,
        mMatchFailResponse = (short)222,
        mClose = (short)223,
        mBingoRankResponse = (short)224,
        mScoreUpdateRequest = (short)225,
        mScoreUpdateResponse = (short)226,
        mEventServerJoinRequest = (short)227,
        mEventServerJoinResponse = (short)228,
        mDailyInfoRequest = (short)229,
        mDailyInfoResponse = (short)230,
        mDailyCheckRequest = (short)231,
        mDailyCheckResponse = (short)232,

        mFriendConnectInfoRequest = (short)233,
        mFriendConnectInfoResponse = (short)234,

        mFriendInviteRequest = (short)235,
        mFriendInviteResponse = (short)236,

        mFriendInviteAgreeRequest = (short)237,
        mFriendInviteAgreeResponse = (short)238,

        mFriendInviteAgreeNoticeResponse = (short)239,

        mFriendInviteCancelRequest = (short)240,
        mFriendInviteCancelResponse = (short)241,
        mFriendInviteCancelNoticeResponse = (short)242,

        mFriendInfoRequest = (short)243,
        mFriendInfoResponse = (short)244,

        mFriendSuccessNoticeResponse = (short)245,
        mFriendPlayRequest = (short)246,
        mFriendPlayResponse = (short)247,

        mCouponRequest = (short)248,
        mCouponResponse = (short)249,

        mPushinfoRequest = (short)250,
        mPushinfoResponse = (short)251,

        mRankingRequest = (short)252,
        mRankingResponse = (short)253,
    };
    public enum STATE
    {
        connectComplete = (short)90,
        mServerInfoRequest = (short)95,
        mServerInfoIng = (short)96,
        mServerInfoComplete = (short)296,

        mEventGachaEnd = (short)777,
        mEventGachaFail = (short)776,

        excessTime = (short)888,
        connectionFail = (short)666,
        NaverconnectionFail = (short)667,

        waitSign = (short)999,

        mHeartBeat = (short)97,                //하트비트
        mServerTestRequest = (short)98,
        mServerTestIng = (short)99,
        mServerTestIngComplete = (short)299,

        mNoticeRequest = (short)100,
        mNoticeIng = (short)101,
        mNoticeIngComplete = (short)301,

        mLoginRequest = (short)102,
        mLoginIng = (short)103,
        mLoginIngComplete = (short)303,

        mMyGameInfoRequest = (short)104,
        mMyGameInfoIng = (short)105,
        mMyGameInfoIngComplete = (short)305,

        mBandkeySetRequest = (short)106,
        mBandkeySetIng = (short)107,
        mBandkeySetIngComplete = (short)307,

        mBandListRequest = (short)108,
        mBandListIng = (short)109,
        mBandListIngComplete = (short)309,

        mTicketChargeRequest = (short)110,
        mTicketChargeIng = (short)111,
        mTicketChargeIngComplete = (short)311,

        mTicketSendRequest = (short)112,
        mTicketSendIng = (short)113,
        mTicketSendIngComplete = (short)313,

        mTicketSettingRequest = (short)114,
        mTicketSettingIng = (short)115,
        mTicketSettingIngComplete = (short)315,

        mItemShopRequest = (short)116,
        mItemShopIng = (short)117,
        mItemShopIngComplete = (short)317,

        mBandRankingRequest = (short)118,
        mBandRankingIng = (short)119,
        mBandRankingIngComplete = (short)319,

        mWorldRankingRequest = (short)120,
        mWorldRankingIng = (short)121,
        mWorldRankingIngComplete = (short)321,

        mCoinRankingRequest = (short)122,
        mCoinRankinIng = (short)123,
        mCoinRankingIngComplete = (short)323,

        mMailGetRequest = (short)124,
        mMailGetIng = (short)125,
        mMailGetIngComplete = (short)325,

        mMailCheckRequest = (short)126,
        mMailCheckIng = (short)127,
        mMailCheckIngComplete = (short)327,

        mDropRequest = (short)128,
        mDropIng = (short)129,
        mDropIngComplete = (short)329,

        mMonsterGachaRequest = (short)130,
        mMonsterGachaIng = (short)131,
        mMonsterGachaIngComplete = (short)331,

        mPlayGachaRequest = (short)132,
        mPlayGachaIng = (short)133,
        mPlayGachaIngComplete = (short)333,

        mMonsterCardListRequest = (short)134,
        mMonsterCardListIng = (short)135,
        mMonsterCardListIngComplete = (short)335,

        mMonsterCardUseRequest = (short)136,
        mMonsterCardUseIng = (short)137,
        mMonsterCardUseIngComplete = (short)337,

        mMonsterCardMixRequest = (short)138,
        mMonsterCardMixIng = (short)139,
        mMonsterCardMixIngComplete = (short)339,

        mEventListRequest = (short)140,
        mEventListIng = (short)141,
        mEventListIngComplete = (short)341,

        mEventGachaRequest = (short)142,
        mEventGachaIng = (short)143,
        mEventGachaIngComplete = (short)343,

        mEventMyInfoRequest = (short)144,
        mEventMyInfoIng = (short)145,
        mEventMyInfoIngComplete = (short)345,

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


        mGameStartRequest = (short)206,
        mGameStartIng = (short)207,
        mGameStartIngComplete = (short)407,

        mGameStartNoticeIng = (short)208,
        mGameStartNoticeIngComplete = (short)408,

        mReadycompleteRequest = (short)209,

        mReadycompleteIng = (short)210,
        mReadycompleteIngComplete = (short)410,

        mReadycompleteNoticeIng = (short)211,
        mReadycompleteNoticeIngComplete = (short)411,

        mGameMessageRequest = (short)212,
        mGameMessageIng = (short)213,
        mGameMessageIngComplete = (short)413,

        mGameMessageNoticeIng = (short)214,
        mGameMessageNoticeIngComplete = (short)414,

        mItemMessageRequest = (short)215,
        mItemMessageIng = (short)216,
        mItemMessageIngComplete = (short)416,

        mItemMessageNoticeIng = (short)217,
        mItemMessageNoticeIngComplete = (short)417,

        mBingoResultRequest = (short)218,
        mBingoResultIng = (short)219,
        mBingoResultIngComplete = (short)419,

        mGameResultRequest = (short)220,
        mGameResultIng = (short)225,
        mGameResultIngComplete = (short)425,

        mGameResultNoticeIng = (short)501,
        mGameResultNoticeIngComplete = (short)502,

        mOtherOutIng = (short)221,
        mOtherOutIngComplete = (short)421,

        mMatchFailIng = (short)222,
        mMatchFailIngComplete = (short)422,

        mClose = (short)223,
        mCloseIng = (short)433,
        mCloseComplete = (short)423,

        mBingoRankIng = (short)224,
        mBingoRankIngComplete = (short)424,

        mScoreUpdateRequest = (short)225,
        mScoreUpdateIng = (short)425,
        mScoreUpdateIngComplete = (short)428,

        mEventServerJoinRequest = (short)226,
        mEventServerJoinIng = (short)426,
        mEventServerJoinComplete = (short)427,

        mDailyInfoRequest = (short)229,
        mDailyInfoIng = (short)230,
        mDailyInfoComplete = (short)430,

        mDailyCheckRequest = (short)231,
        mDailyCheckIng = (short)232,
        mDailyCheckComplete = (short)432,


        mFriendConnectInfoRequest = (short)233,
        mFriendConnectInfoIng = (short)234,
        mFriendConnectInfoComplete = (short)434,

        mFriendInviteRequest = (short)235,
        mFriendInviteIng = (short)236,
        mFriendInviteComplete = (short)436,

        mFriendInviteAgreeRequest = (short)237,
        mFriendInviteAgreeIng = (short)238,
        mFriendInviteAgreeComplete = (short)438,

        mFriendInviteAgreeNoticeIng = (short)239,
        mFriendInviteAgreeNoticeComplete = (short)439,

        mFriendInviteCancelRequest = (short)240,
        mFriendInviteCancelIng = (short)241,
        mFriendInviteCancelComplete = (short)442,
        mFriendInviteCancelNoticeComplete = (short)242,



        mFriendInfoRequest = (short)243,
        mFriendInfoIng = (short)244,
        mFriendInfoComplete = (short)444,


        mFriendSuccessNoticeComplete = (short)245,
        mFriendPlayRequest = (short)246,
        mFriendPlayIng = (short)247,
        mFriendPlayComplete = (short)447,

        mCouponRequest = (short)248,
        mCouponIng = (short)249,
        mCouponComplete = (short)449,

        mPushinfoRequest = (short)250,
        mPushinfoIng = (short)251,
        mPushinfoComplete = (short)451,

        mRankingRequest = (short)252,
        mRankingIng = (short)253,
        mRankingComplete = (short)453,



    };

    //170803:추가프로토콜
    public enum BingoMsg
    {
        mWaitingRoomJoinRequest = (short)1000,      //1000	대기룸에 입장  요청 C - > S
        mWaitingRoomJoinResponse = (short)1001,     //1001	대기룸 입장 응답 S -> C
        mWaitingRoomJoinIng = (short)1002,          //1002	대기룸 상태 알람 S -> C

        mGameJoinRequest = (short)1003,             //1003	게임 입장 요청 C -> S
        mGameJoinResponse = (short)1004,            //1004	게임 입장 요청 응답 S -> C

        mGameStartResponse = (short)1005,           //1005	게임 시작 알람 S -> C

        mGameBingoNumberIng = (short)1006,          //1006	빙고 숫자 알람 S -> C
        mGameBingoFinishRequest = (short)1007,      //1007	빙고 완성 요청 C -> S
        mGameBingoFinishResponse = (short)1008,     //1008	빙고 완성 응답 S -> C
        mGameBingoFinishOtherResponse = (short)1009,          //1009	빙고 완성 알림 S -> C

        mGameCloseResponse = (short)1010,           //1010	게임 종료 알림 S -> C

        mGameRobotStatusAlarm = (short)1011,        //1011  봇 상태 알람

        mGameLiftAuthRequest = (short)1101,         //1101  게임리프트 인증 요청 C -> S
        mGameLiftAuthResponse = (short)1102,        //1102  게임리프트 인증 응답 S -> C
    };

    public enum BingoState
    {
        mWaitingRoomJoinRequest = (short)1000,  //대기실 매칭 요청
        mWaitingRoomJoinComplete = (short)1001, //대기실 매칭 됨
        mWaitingRoomJoinIng = (short)1002,      //대기실에서 대기중
        mWaitingRoomJoinIngComplete = (short)11002, //대기 끝남

        mGameJoinRequest = (short)1003,
        mGameJoinIng = (short)1004,
        mGameJoinIngComplete = (short)11004,

        //mGameStartResponse = (short)1005,
        mGameStartIngComplete = (short)11005,

        //mGameBingoNumberIng = (short)1006,
        mGameBingoNumberIngComplete = (short)11006,

        mGameBingoFinishRequest = (short)1007,
        mGameBingoFinishComplete = (short)11008,
        //mGameBingoFinishIng = (short)1009,
        mGameBingoFinishOtherResponse = (short)11009,

        //mGameCloseResponse = (short)1010,

        mGameLiftAuthRequest = (short)1101,
    };

}

public class CardEffect
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

public struct MyCard
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

public struct friendData
{
    public string FriendName;
    public int FriendId;
    public string FriendKey;

    public int flag;
}

public struct friendInfo
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

public struct MonsterCardXML
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

public struct MonsterRankXML
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

public struct RankXML
{
    public int Id;
    public int Type;
    public int Value;
    public int Bscore;
    public int Bgold;
    public int Gacha;
}

public struct GachaXML
{
    public int Id;
    public int Group;
    public int Value;
    public int LookValue;
    public int Type;
    public int TypeValue;
}

public struct urlInfo
{
    public int notice_Num;
    public string img_url;
    public string move_url;
    public int todayNotView;
}

public struct MyInfo
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

public struct PlayerInfo
{
    public long userID;
    public string nickName;
    public bool bReady;
}

public struct SheetInfo
{
    public long userID;
    public string nickname;
    public int monsterId;
    public long roomkey;
    public int activeSheetCount;
    public bool[] bingoSheet;
    public int[,] sheet;
    public int shield;
    public int betting_index;
}

public struct BandInfo
{
    public string bandKey;
    public string bandName;
}

public class Sound
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
        bingo,
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
public enum MonsterTapType
{
    Monster_admin = 0,
    Monster_upgrade,
    Monster_Mix

}

public class Shop
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

public class Item
{
    public enum ItemType
    {
        Item_None = 0,

        Item_Daub,
        Item_Bomb,
        Item_Daub_10,
        Item_Gold_30,
        Item_Gold_50,
        Item_DirectBingo,
        Item_Ticket,
        Item_FrozenItem,
        Item_Blind_5,
        Item_SwapSheet,
        Item_ShuffleSheet,
        Item_Shield,
        kItemEffect_Max,
    }

    static public string[] itemImagePath =
    {
        "item_unknown",
        "item_bonusdub",
        "item_doubledub",
        "item_extracoin",
        "item_extracoin",
        "item_boxcoin",
        "item_bingo",
        "item_ticket",
        "item_freeze",
        "item_blind",
        "item_swap",
        "item_numbermix",
        "item_shield"
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



    static public string[] daubItemImagePath =
    {
        "item_unknown",
        "board_daub",
        "board_bomb",
        "board_gold30",
        "board_gold30",
        "board_gold50",
        "board_bingo",
        "board_ticket",
        "item_unknown",
        "item_unknown",
        "item_unknown",
        "item_unknown",
        "item_unknown"
    };
}



// http://msdn.microsoft.com/ko-kr/library/0taef578(v=vs.90).aspx
// struct constructor ....
public struct Cell
{
    public int index;
    public int number;
    public bool daub;
    public bool realDaub;
    public int itemEffectIndex;
}

public struct BingoSheet
{
    public int sheetIndex;
    public Cell[] cells;
}

public struct otherBingoState
{
    public int userId;
    public bool[] bingo;
}


public struct eventData
{
    public int productID;
    public int sell_Count;
    public int total_Count;
    public string img_url;
    public string name;
    public int[] bingo;
    public int bingoInfo;
}



public struct UseItemInfo
{
    public long itemUserID;
    public long targetID;
    public int itemIndex;
    public int param1;
    public int param2;
};


public struct RankInfo
{
    public string userKey;
    public string nickname;
    public int weekly_best;
    public int rank;
}

public struct CoinRankInfo
{
    public string userKey;
    public string nickname;
    public int weekly_best;
    public int rank;
}

public struct BattleRankInfo
{
    public string userKey;
    public string nickname;
    public int win;
    public int lose;
    public int rank;
}

public struct BandRankInfo
{
    public string nickname;
    public int weekly_bestScore;
    public int rank;

}

public struct MailInfo
{
    public string sender;
    public int giftIndex;
    public int giftCount;
    public string content;
    public int mailNo;
    public string eDate;
}


public struct sWinner
{
    public string nickname;
    public int rank;
    public int monster_id;
    public int coin;
}


public struct GachaInfo
{
    public int type;
    public int value;
    public int selected;
}
public struct monsterSetInfo
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

public struct materialInfo
{
    public int material_Key;
    public int material_exp;
}
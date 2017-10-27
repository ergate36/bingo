using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GlobalData : MonoBehaviour
{

    public static GlobalData g_global = null;

    [HideInInspector]
    public string srvURL;

    [HideInInspector]
    public string gameURL;
    [HideInInspector]
    public string lobbyURL;
    [HideInInspector]
    public string checkURL;

    [HideInInspector]
    public List<int> writeList = new List<int>();

    [HideInInspector]
    public MyInfo myInfo;
    [HideInInspector]
    public MyCard myCard;

    [HideInInspector]
    public friendInfo f_Info;

    [HideInInspector]
    public monsterSetInfo m_setInfo;


    [HideInInspector]
    public PlayerInfo[] playerInfo;
    [HideInInspector]
    public eventData[] m_eventData;


    [HideInInspector]
    public SheetInfo sheetInfo;
    [HideInInspector]
    public SheetInfo[] otherSheetInfo;
    [HideInInspector]
    public gameData g_gameData;
    [HideInInspector]
    public List<RankInfo> r_rankInfo;
    [HideInInspector]
    public List<CoinRankInfo> c_rankInfo;
    [HideInInspector]
    public List<BattleRankInfo> b_rankInfo;

    [HideInInspector]
    public List<MailInfo> m_mailInfo;
    [HideInInspector]
    public List<materialInfo> m_material;

    [HideInInspector]
    public int rank_state = 0;


    [HideInInspector]
    public int roomkey = 0;
    [HideInInspector]
    public bool bAutoSendReady = false;
    [HideInInspector]
    public int callCount = 1;
    [HideInInspector]
    public int serverCallCount = 1;
    [HideInInspector]
    public int bingoCount = 0;

    [HideInInspector]
    public int itemIndex = 0;
    [HideInInspector]
    public float waitTime = 0.0f;
    [HideInInspector]
    public long[] bingoRankId;
    [HideInInspector]
    public int tourPlayer_count = 0;
    [HideInInspector]
    public int lobby_count = 0;
    [HideInInspector]
    public int lobby_waitTime = 0;

    [HideInInspector]
    public int admission = 0;


    [HideInInspector]
    public int[] dailyInfo;

    [HideInInspector]
    public int detail_index = 0;

    
    [HideInInspector]
    public int item_count = 0;

    [HideInInspector]
    public int flag = 0;

    [HideInInspector]
    public int getCoin = 0;


    [HideInInspector]
    public int couponResult = 0;


    [HideInInspector]
    public string couponNum = "";


    [HideInInspector]
    public int[] bingoball;

    // sounds
    [HideInInspector]
    public AudioClip[] BGSound;
    [HideInInspector]
    public AudioClip[] EffectSound;
    [HideInInspector]
    public AudioClip[] BallSound;
    [HideInInspector]
    public AudioClip[] TalkSound;
    [HideInInspector]
    public AudioClip[] TutorialSound;
    [HideInInspector]
    public List<MyCard> myCardList;

    [HideInInspector]
    public List<friendData> myFriendList;


    // shop previous scene
    [HideInInspector]
    public string sceneBeforeShop;
    // gotoShop from where
    [HideInInspector]
    public Shop.ShopType gotoShopType;

    [HideInInspector]
    public bool isFirstLogin = true;

    [HideInInspector]
    public int selectedCardNum = 0;
    [HideInInspector]
    public int selectedCardId = 0;
    [HideInInspector]
    public int listCardindex = 0;
    [HideInInspector]
    public int bingoFlag = 0;

    [HideInInspector]
    public int CardId = 0;

    [HideInInspector]
    public int selectedCardflag = 0;

    [HideInInspector]
    public int selectedCardNumber = 0;
    [HideInInspector]
    public int monsterFlag = 0;

    [HideInInspector]
    public int listType = 1;

    [HideInInspector]
    public int gameType;
    [HideInInspector]
    public int myrank;


    [HideInInspector]
    public int soundFlg = 0;

    [HideInInspector]
    public int pushFlg = 1;

    [HideInInspector]
    public int myBlockinfo = 0;

    [HideInInspector]
    public int socketState = 0;

    [HideInInspector]
    public int serverFlag = 0;

    [HideInInspector]
    public int popupstate = 0;


    [HideInInspector]
    public sWinner[] m_winnerList;
    [HideInInspector]
    public int rankCount = 0;
    [HideInInspector]
    public GachaInfo[] gachaInfo;
    [HideInInspector]
    public otherBingoState[] otherBingoS;


    [HideInInspector]
    public int monsterGachaId = 0;
    [HideInInspector]
    public int monsterGachaKey = 0;

    [HideInInspector]
    public int shopIndex = 0;
    [HideInInspector]
    public int shopTapindex = 0;

    [HideInInspector]
    public int getMailType = 0;
    [HideInInspector]
    public int getMailindex = 0;

    [HideInInspector]
    public int mixResultId = 0;

    [HideInInspector]
    public int myShield = 0;

    [HideInInspector]
    public int attack_id = 0;
    [HideInInspector]
    public int attack_index = 0;
    [HideInInspector]
    public int attack_sheetindex = 0;
    [HideInInspector]
    public int attacker_sheetindex = 0;


    [HideInInspector]
    public int target_id = 0;
    [HideInInspector]
    public int target_index = 0;
    [HideInInspector]
    public int target_sheetindex = 0;
    [HideInInspector]
    public int targeter_sheetindex = 0;

    [HideInInspector]
    public int outer_id = 0;


    [HideInInspector]
    public int urlCount = 0;

    [HideInInspector]
    public bool callBingo = false ;
    [HideInInspector]
    public string event_period;

    [HideInInspector]
    public string invite_name;


    [HideInInspector]
    public int event_join = 0;

    [HideInInspector]
    public int rand_index = 0;


    [HideInInspector]
    public int friendRoom = 0;


    [HideInInspector]
    public bool invite_able = true;
    

    [HideInInspector]
    public int event_index;
    [HideInInspector]
    public int event_btnindex;
    [HideInInspector]
    public string event_phoneNumber;
    [HideInInspector]
    public int shop_flag=0;
    [HideInInspector]
    public int event_flag = 0;

    [HideInInspector]
    public int closeType = 0;

    [HideInInspector]
    public int tutorialPage = 1;

    [HideInInspector]
    public int shopshop = 0;

    [HideInInspector]
    public int currentScene = 0;

    [HideInInspector]
    public int sendNum = 0;

    [HideInInspector]
    public int resultData = 0;

    [HideInInspector]
    public int eventtttt = 0;

    [HideInInspector]
    public int myPlayCard = 0;

    [HideInInspector]
    public int currentItemindex = 0;

    [HideInInspector]
    public int cardCount = 0;

    [HideInInspector]
    public int afterCardCount = 0;


    [HideInInspector]
    public string[] serverip;
    [HideInInspector]
    public int[] serverport;

    [HideInInspector]
    public string[] itemip;
    [HideInInspector]
    public int[] itemport;

    [HideInInspector]
    public string lobbyip;
    [HideInInspector]
    public int lobbyport;

    [HideInInspector]
    public int selectSheet = 0;

    [HideInInspector]
    public int reviewIndex = 0;


    [HideInInspector]
    public int serverIndex = 0;
    [HideInInspector]
    public int modeIndex = 0;

    [HideInInspector]
    public int betting_index = 0;
    

    [HideInInspector]
    public int useItemCount = 0;


    [HideInInspector]
    public int connect_index = 0;

    [HideInInspector]
    public int login_num = 0;

    [HideInInspector]
    public int just_one = 0;


    [HideInInspector]
    public bool servertesting = false;

    [HideInInspector]
    public int loginState = 0;


    [HideInInspector]
    public int game_over = 0;


    [HideInInspector]
    public int mail_index = 0;

    [HideInInspector]
    public string myFriend = "";


    [HideInInspector]
    public int playCardIndex = 0;

    [HideInInspector]
    public int dailyIndex = 0;

    [HideInInspector]
    public int friendIndex = 0;

    [HideInInspector]
    public int selectFriendId = 0;

    [HideInInspector]
    public string selectFriendKey;

    [HideInInspector]
    public string selectFriendName;


    [HideInInspector]
    public int selectInvite = 0;

    [HideInInspector]
    public int win = 0;

    [HideInInspector]
    public int friend_count = 0;

    [HideInInspector]
    public int Invite_result = 0;


    [HideInInspector]
    public string updateUrl;
    [HideInInspector]
    public string testMessage;

    [HideInInspector]
    public string myEmail;



    [HideInInspector]
    public bool cardflag=false;

    [HideInInspector]
    public bool ItemUseState = false;
    [HideInInspector]
    public bool soundFlag = false;

    [HideInInspector]
    public bool myRankFlag = false;


    [HideInInspector]
    public List<urlInfo> mUrlInfo;

    //170804:추가 변수
    [HideInInspector]
    public int mWaitingRoomID = 0;

    [HideInInspector]
    public int mWaitingRoomJoinGameRemainSec = 0;

    [HideInInspector]
    public int mWaitingRoomEndGameRemainBingo = 0;

    [HideInInspector]
    public int mSelectBingoButtonIndex = -1;

    [HideInInspector]
    public int BingoNumberCount = 0;


    [HideInInspector]
    public int BingoRoomUserCount = 0;

    [HideInInspector]
    public string[] BingoRoomUserNameList;


    [HideInInspector]
    public string BingoRankingUserName1;

    [HideInInspector]
    public string BingoRankingUserName2;

    [HideInInspector]
    public string BingoRankingUserName3;

    [HideInInspector]
    public string BingoRankingUserNameX;

    [HideInInspector]
    public int BingoTotalFinishCount = 0;

    [HideInInspector]
    public string BingoLastFinishUserName = "";


    void Awake()
    {
        couponResult = 100;
        friendIndex = 0;
        reviewIndex = 0;
        servertesting = false;
        srvURL = "http://211.110.140.69:1225/bingoweb-1.0-SNAPSHOT/api/";
        lobbyURL = "http://211.110.140.69:1225/bingoweb-1.0-SNAPSHOT/api/";
        //lobbyURL = "http://lobbyelb-927068723.ap-southeast-2.elb.amazonaws.com:8080/bingoweb-1.0-SNAPSHOT/api/";
        //lobbyURL = "http://BingoELB-480217038.ap-northeast-1.elb.amazonaws.com:8080/bingoweb-1.0-SNAPSHOT/api/";
        gameURL = "http://175.126.111.103:1225/bingoweb-1.0-SNAPSHOT/api/";
        //   checkURL = "http://54.252.200.6:8080/bingoweb-1.0-SNAPSHOT/api/";
        checkURL = "http://211.110.140.155:8080/bingoweb-1.0-SNAPSHOT/api/";
        /*
        srvURL = "http://211.110.140.155:8080/bingoweb-1.0-SNAPSHOT/api/";
        lobbyURL = "http://lobbyelb-927068723.ap-southeast-2.elb.amazonaws.com:8080/bingoweb-1.0-SNAPSHOT/api/";
        //lobbyURL = "http://BingoELB-480217038.ap-northeast-1.elb.amazonaws.com:8080/bingoweb-1.0-SNAPSHOT/api/";
        gameURL = "http://211.110.140.155:8080/bingoweb-1.0-SNAPSHOT/api/";
        checkURL = "http://54.252.200.6:8080/bingoweb-1.0-SNAPSHOT/api/";
        */
        myInfo = new MyInfo();
        playerInfo = new PlayerInfo[5];
        sheetInfo = new SheetInfo();
        m_setInfo = new monsterSetInfo();
        otherSheetInfo = new SheetInfo[4];
        otherBingoS = new otherBingoState[4];
        m_eventData = new eventData[3];


        myInfo.deviceId = "null";

        f_Info = new friendInfo();
        f_Info.slot = new int[4];

        dailyInfo = new int[7];
        for (int i = 0; i < 7; i++ )
        {
            dailyInfo[i] = 0;
        }
        serverIndex = 0;
        modeIndex = 0;
        useItemCount = 0;
        serverip = new string[5];
        serverport = new int[5];
        itemip = new string[4];
        itemport = new int[4];
        myInfo.waitTime = 100;
        betting_index = 0;
        for (int i = 0; i < 5; i++)
        {
            serverip[i] = "14.63.172.176";
            if(i ==0){
                serverport[i] = 50111;
            }
            else if (i == 1)
            {
                serverport[i] = 50112;
            }
            else
            {
                serverport[i] = 50113; 
            }
        }
        serverIndex = Random.Range(0, 4);

        for (int i = 0; i < 4; i++)
        {
            otherBingoS[i].bingo = new bool[4];
            for (int j = 0; j < 4; j++)
            {
                otherBingoS[i].userId = 0;
                otherBingoS[i].bingo[j] = false;
            }
        }

        for (int i = 0; i < 3; i++ )
        {
            m_eventData[i].bingo = new int[25];
        }


        g_gameData = new gameData();
        g_gameData.bingoState = new int[4];
        g_gameData.sheildState = new int[5];
        r_rankInfo = new List<RankInfo>();
        b_rankInfo = new List<BattleRankInfo>();
        c_rankInfo = new List<CoinRankInfo>();

        m_mailInfo = new List<MailInfo>();
        m_material = new List<materialInfo>();
        mUrlInfo = new List<urlInfo>();
        myFriendList = new List<friendData>();

        bingoball = new int[75];
        urlCount = 0;
        //for (int i = 0; i < 75; i++)
        //{
        //    if (i < 10)
        //    {
        //        bingoball[i] = i + 1;
        //    }
        //    else if (i < 20)
        //    {
        //        bingoball[i] = i + 6;
        //    }
        //    else if (i < 30)
        //    {
        //        bingoball[i] = i + 11;
        //    }
        //    else if (i < 40)
        //    {
        //        bingoball[i] = i + 16;
        //    }
        //    else if (i < 50)
        //    {
        //        bingoball[i] = i + 21;
        //    }
        //}

        for (int i = 0; i < 4; i++)
        {
            g_gameData.bingoState[i] = 0;
        }
        for (int i = 0; i < 5; i++)
        {
            g_gameData.sheildState[i] = 0;
        }

        m_winnerList = new sWinner[100];

        myCardList = new List<MyCard>();
        gameType = 0;
        bingoRankId = new long[3];
        gachaInfo = new GachaInfo[3];


        myInfo.userKey = "0";

        for (int i = 0; i < 3; ++i)
        {
            bingoRankId[i] = 0;
        }

        sheetInfo.bingoSheet = new bool[4];
        for (int i = 0; i < 4; ++i)
        {
            sheetInfo.bingoSheet[i] = false;
        }
        sheetInfo.sheet = new int[4, 25];

        for (int i = 0; i < 4; ++i)
        {
            otherSheetInfo[i].bingoSheet = new bool[4];
            for (int k = 0; k < 4; ++k)
            {
                otherSheetInfo[i].bingoSheet[k] = false;
            }
            otherSheetInfo[i].sheet = new int[4, 25];
        }

        BGSound = new AudioClip[(int)Sound.BGSoundLIst.BGSound_MAX];
        EffectSound = new AudioClip[(int)Sound.EffSoundList.EffectSound_MAX];
        BallSound = new AudioClip[75];
        TalkSound = new AudioClip[(int)Sound.TalkList.EffectTalk_MAX];
        TutorialSound = new AudioClip[(int)Sound.TutorialSoundLIst.Step_MAX];

        preloadSounds();

        DontDestroyOnLoad(gameObject);

        gotoShopType = Shop.ShopType.Shop_Gold;

    }

    void Start()
    {
        if (g_global == null)
        {
            //g_global = GameObject.Find("GlobalObject").GetComponent<GlobalData>();
            g_global = GameObject.Find("nb_GlobalObject").GetComponent<GlobalData>();
        }
        bingoCount = 0;
        tourPlayer_count = 0;
        sceneBeforeShop = "MainScene";
        gotoShopType = Shop.ShopType.Shop_Gold;
    }

    void onRequestLogin()
    {
        string url = srvURL;
        url += "login.go?";
        url += "userID=0&";
    }

    public void ResetInfo()
    {
        roomkey = 0;
        callCount = 1;
        serverCallCount = 1;
        bAutoSendReady = false;
        waitTime = 0;


        bingoCount = 0;
        itemIndex = 0;
        tourPlayer_count = 0;
        item_count = 0;
        for (int i = 0; i < 3; ++i)
        {
            bingoRankId[i] = 0;
        }

        for (int i = 0; i < 4; ++i)
        {
            sheetInfo.bingoSheet[i] = false;
        }

        for (int i = 0; i < 4; ++i)
        {
            for (int k = 0; k < 4; ++k)
            {
                otherSheetInfo[i].bingoSheet[k] = false;
            }
        }
    }


    private void preloadSounds()
    {
        //bg
        BGSound[(int)Sound.BGSoundLIst.EndingMusic] = (AudioClip)Resources.Load("Sounds/BGM/EndingMusic", typeof(AudioClip));
        BGSound[(int)Sound.BGSoundLIst.IntroMusic] = (AudioClip)Resources.Load("Sounds/BGM/bingomonster_maineventBGM", typeof(AudioClip));
        BGSound[(int)Sound.BGSoundLIst.PlayMusic1] = (AudioClip)Resources.Load("Sounds/BGM/bingomonster_VSplayBGM", typeof(AudioClip));
        BGSound[(int)Sound.BGSoundLIst.PlayMusic2] = (AudioClip)Resources.Load("Sounds/BGM/bingomonster_tournamentplayBGM", typeof(AudioClip));

        // tutorial
        for (int i = 0; i < (int)Sound.TutorialSoundLIst.Step_MAX; ++i)
        {
            string path = "Sounds/tutorial/tutorial_sound_step";

            if (i < 10)
            {
                path += "0" + i.ToString();
            }
            else
            {
                path += i.ToString();
            }

            TutorialSound[i] = (AudioClip)Resources.Load(path, typeof(AudioClip));
        }

        // effect

        // EffectSound[(int)Sound.EffSoundList.Attack_Hammer] = (AudioClip)Resources.Load("Sounds/Eff/Attack_Blind", typeof(AudioClip));

        EffectSound[(int)Sound.EffSoundList.Attack_Blind] = (AudioClip)Resources.Load("Sounds/Eff/Attack_Blind", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.Attack_Frozen] = (AudioClip)Resources.Load("Sounds/Eff/Attack_Frozen", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.Attack_Shuffle] = (AudioClip)Resources.Load("Sounds/Eff/Attack_Shuffle", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.Attack_Swap] = (AudioClip)Resources.Load("Sounds/Eff/Attack_Swap", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.BadBingo] = (AudioClip)Resources.Load("Sounds/Eff/badBingo", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.bingo] = (AudioClip)Resources.Load("Sounds/Eff/bingo", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.click] = (AudioClip)Resources.Load("Sounds/Eff/click", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.Damage_Blind] = (AudioClip)Resources.Load("Sounds/Eff/Damage_Blind", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.Damage_Frozen] = (AudioClip)Resources.Load("Sounds/Eff/Damage_Frozen", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.Damage_Shuffle] = (AudioClip)Resources.Load("Sounds/Eff/Damage_Shuffle", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.Damage_Swap] = (AudioClip)Resources.Load("Sounds/Eff/Damage_Swap", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.daub] = (AudioClip)Resources.Load("Sounds/Eff/daub", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.itemeffect] = (AudioClip)Resources.Load("Sounds/Eff/itemeffect", typeof(AudioClip));

        EffectSound[(int)Sound.EffSoundList.cleardaub] = (AudioClip)Resources.Load("Sounds/Eff/nodaub", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.nodaub] = (AudioClip)Resources.Load("Sounds/Eff/nodaub", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.reward_bomb] = (AudioClip)Resources.Load("Sounds/Eff/reward_bomb", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.reward_gold] = (AudioClip)Resources.Load("Sounds/Eff/reward_gold", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.reward_ticket] = (AudioClip)Resources.Load("Sounds/Eff/reward_ticket", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.shieldOn] = (AudioClip)Resources.Load("Sounds/Eff/shieldOn", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.tour_pass] = (AudioClip)Resources.Load("Sounds/Eff/tour_pass", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.selectClick] = (AudioClip)Resources.Load("Sounds/Eff/select", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.result_start] = (AudioClip)Resources.Load("Sounds/Eff/result_sound", typeof(AudioClip));

        EffectSound[(int)Sound.EffSoundList.gacha_loop] = (AudioClip)Resources.Load("Sounds/Eff/gacha_loop", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.alarm] = (AudioClip)Resources.Load("Sounds/Eff/alarm", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.itemeffect_bomb] = (AudioClip)Resources.Load("Sounds/Eff/itemeffect_bomb", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.itemeffect_ticket] = (AudioClip)Resources.Load("Sounds/Eff/getticket", typeof(AudioClip));
        
        EffectSound[(int)Sound.EffSoundList.shieldOff] = (AudioClip)Resources.Load("Sounds/Eff/shieldOff", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.yabawe_loop] = (AudioClip)Resources.Load("Sounds/Eff/yabawe_loop", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.yabawe_end] = (AudioClip)Resources.Load("Sounds/Eff/yabawe_end", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.itemeffect_coin] = (AudioClip)Resources.Load("Sounds/Eff/itemeffect_coin", typeof(AudioClip));
        EffectSound[(int)Sound.EffSoundList.Damage_Frozen_loop] = (AudioClip)Resources.Load("Sounds/Eff/Damage_Frozen_loop", typeof(AudioClip));

        //Talk
        TalkSound[(int)Sound.TalkList.bad_bingo] = (AudioClip)Resources.Load("Sounds/talk/bad_bingo", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.bingo] = (AudioClip)Resources.Load("Sounds/talk/bingo", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.bingo_d] = (AudioClip)Resources.Load("Sounds/talk/bingo_d", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.bingo_monster] = (AudioClip)Resources.Load("Sounds/talk/bingo_monster", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.bingo_p] = (AudioClip)Resources.Load("Sounds/talk/bingo_p", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.bingo_q] = (AudioClip)Resources.Load("Sounds/talk/bingo_q", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.bingo_t] = (AudioClip)Resources.Load("Sounds/talk/bingo_t", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.bingo_tonament_join] = (AudioClip)Resources.Load("Sounds/talk/bingo_tonament_join", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.game_count] = (AudioClip)Resources.Load("Sounds/talk/game_count", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.game_over] = (AudioClip)Resources.Load("Sounds/talk/game_over", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.item_blind] = (AudioClip)Resources.Load("Sounds/talk/item_blind", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.item_freeze] = (AudioClip)Resources.Load("Sounds/talk/item_freeze", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.item_getcoin] = (AudioClip)Resources.Load("Sounds/talk/item_getcoin", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.item_itemready] = (AudioClip)Resources.Load("Sounds/talk/item_itemready", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.item_numbermix] = (AudioClip)Resources.Load("Sounds/talk/item_numbermix", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.item_shield] = (AudioClip)Resources.Load("Sounds/talk/item_shield", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.item_sidedaub] = (AudioClip)Resources.Load("Sounds/talk/item_sidedaub", typeof(AudioClip));
        TalkSound[(int)Sound.TalkList.item_swap] = (AudioClip)Resources.Load("Sounds/talk/item_swap", typeof(AudioClip));

        for (int i = 0; i < 75; ++i)
        {
            string path = "Sounds/BallEff/";

            if (i < 15)
            {
                if (i < 9)
                {
                    path += "B_0" + (i + 1).ToString();
                }
                else
                {
                    path += "B_" + (i + 1).ToString();
                }

            }
            else if (i < 30)
            {
                path += "I_" + (i + 1).ToString();
            }
            else if (i < 45)
            {
                path += "N_" + (i + 1).ToString();
            }
            else if (i < 60)
            {
                path += "G_" + (i + 1).ToString();
            }
            else
            {
                path += "O_" + (i + 1).ToString();
            }
            BallSound[i] = (AudioClip)Resources.Load(path, typeof(AudioClip));

        }
    }
}

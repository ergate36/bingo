using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MarigoldModel.Model;
using MarigoldGame.Protocol;


public class nb_GlobalData : MonoBehaviour
{

    public static nb_GlobalData g_global = null;
    public static AudioSource g_global_bgm1 = null;
    public static AudioSource g_global_bgm2 = null;
    public static AudioSource g_global_bgm3 = null;
    public static AudioSource g_global_bgm4 = null;

    // model
    [HideInInspector]
    public BaseUserModel baseUserModel;

    [HideInInspector]
    public BaseModel baseModel;

    [HideInInspector]
    public UserAccount userAccount;

    [HideInInspector]
    public UserSession userSession;

    [HideInInspector]
    public List<UserPowerUp> userPowerUpList;

    [HideInInspector]
    public List<Stage> stageList;

    // protocol 
    [HideInInspector]
    public GameLiftConnectResponse gameLiftConnectResponse;
    
    [HideInInspector]
    public BlitzWaitRoomStatusAlarm blitzWaitRoomStatusAlarm;

    [HideInInspector]
    public BlitzEnterGameResponse blitzEnterGameResponse;

    [HideInInspector]
    public BlitzStartGameAlarm blitzStartGameAlarm;
    
    [HideInInspector]
    public BlitzCallNumberAlarm blitzCallNumberAlarm;
    
    [HideInInspector]
    public BlitzCompleteBingoAlarm blitzCompleteBingoAlarm;

    [HideInInspector]
    public BlitzCompleteBingoResponse blitzCompleteBingoResponse;

    [HideInInspector]
    public BlitzCheckNumberResponse blitzCheckNumberResponse;
    
    [HideInInspector]
    public BlitzRefreshPowerUpResponse blitzRefreshPowerUpResponse;
    
    [HideInInspector]
    public BlitzUsePowerUpResponse blitzUsePowerUpResponse;

    //protocol battle mode
    [HideInInspector]
    public MonsterStartGameAlarm monsterStartGameAlarm;

    [HideInInspector]
    public MonsterCallNumberAlarm monsterCallNumberAlarm;

    [HideInInspector]
    public MonsterCompleteBingoResponse monsterCompleteBingoResponse;

    [HideInInspector]
    public MonsterCompleteBingoAlarm monsterCompleteBingoAlarm;

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
    public int soundFlg = 0;

    // global
    [HideInInspector]
    public int socketState = 0; //게임서버 소켓 상태

    [HideInInspector]
    public int serverFlag = 0;

    [HideInInspector]
    public nb_SheetInfo sheetInfo;


    [HideInInspector]
    public nb_MyInfo myInfo;
    [HideInInspector]
    public nb_MyCard myCard;

    [HideInInspector]
    public int myShield = 0;

    [HideInInspector]
    public int resultData = 0;
    
    [HideInInspector]
    public int[] bingoball;


    [HideInInspector]
    public int admission = 0;


    [HideInInspector]
    public int game_over = 0;

    [HideInInspector]
    public int gameType = 0;
    [HideInInspector]
    public int myrank;

    [HideInInspector]
    public int bingoCount = 0;


    [HideInInspector]
    public int itemIndex = 0;

    [HideInInspector]
    public bool callBingo = false;


    [HideInInspector]
    public bool ItemUseState = false;
    [HideInInspector]
    public bool soundFlag = false;

    [HideInInspector]
    public bool myRankFlag = false;


    [HideInInspector]
    public bool invite_able = true;

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

    [HideInInspector]
    public int[] BingoMyBlitzRanking;

    [HideInInspector]
    public bool CallMyBingo = false;

    [HideInInspector]
    public int SelectWorldStage;

    //[HideInInspector]
    //public bool WorldStageSpineRefresh;

    //[HideInInspector]
    //public bool WorldStageSpineSelectAni;

    //[HideInInspector]
    //public string WorldStageSpineAnimation;



    //login
    [HideInInspector]
    public string InputSessionLoginKey;

    [HideInInspector]
    public string InputSocialNickname;

    //main
    [HideInInspector]
    public int maxStage = 4;
    [HideInInspector]
    public int selectStageId = 1;

    [HideInInspector]
    public string gl_playerSessionId;
    [HideInInspector]
    public string gl_ipAddress;
    [HideInInspector]
    public int gl_port;

    [HideInInspector]
    public bool MainMenuActive = false;
    [HideInInspector]
    public bool MainShopActive = false;
    [HideInInspector]
    public bool MainStageMove = false;

    //lobby
    [HideInInspector]
    public bool LobbyMenuActive = false;

    [HideInInspector]
    public bool LobbyShopActive = false;

    //check number
    [HideInInspector]
    public int CheckNumCardIndex = 0;
    [HideInInspector]
    public int CheckNumNumber = 0;

    //select Item
    [HideInInspector]
    public int selectItemId = 0;

    [HideInInspector]
    public List<nb_useItemData> useItemDataList;


    void Awake()
    {
        Application.runInBackground = true;


        baseUserModel = new BaseUserModel();
        baseModel = new BaseModel();
        userAccount = new UserAccount();
        userSession = new UserSession();

        gameLiftConnectResponse = new GameLiftConnectResponse();
        blitzWaitRoomStatusAlarm = new BlitzWaitRoomStatusAlarm();
        blitzEnterGameResponse = new BlitzEnterGameResponse();
        blitzStartGameAlarm = new BlitzStartGameAlarm();
        blitzCallNumberAlarm = new BlitzCallNumberAlarm();
        blitzCompleteBingoAlarm = new BlitzCompleteBingoAlarm();
        blitzCompleteBingoResponse = new BlitzCompleteBingoResponse();
        blitzCheckNumberResponse = new BlitzCheckNumberResponse();
        blitzRefreshPowerUpResponse = new BlitzRefreshPowerUpResponse();
        blitzUsePowerUpResponse = new BlitzUsePowerUpResponse();
        monsterStartGameAlarm = new MonsterStartGameAlarm();
        monsterCallNumberAlarm = new MonsterCallNumberAlarm();
        monsterCompleteBingoResponse = new MonsterCompleteBingoResponse();
        monsterCompleteBingoAlarm = new MonsterCompleteBingoAlarm();


        BGSound = new AudioClip[(int)Sound.BGSoundLIst.BGSound_MAX];
        EffectSound = new AudioClip[(int)Sound.EffSoundList.EffectSound_MAX];
        BallSound = new AudioClip[75];
        TalkSound = new AudioClip[(int)Sound.TalkList.EffectTalk_MAX];
        TutorialSound = new AudioClip[(int)Sound.TutorialSoundLIst.Step_MAX];


        blitzWaitRoomStatusAlarm.RemainBingo = 0;
        blitzWaitRoomStatusAlarm.RemainSecond = 0;

        myInfo = new nb_MyInfo();
        myInfo.deviceId = "null";
        myInfo.waitTime = 100;
        myInfo.userKey = "0";

        sheetInfo = new nb_SheetInfo();
        sheetInfo.bingoSheet = new bool[4];
        for (int i = 0; i < 4; ++i)
        {
            sheetInfo.bingoSheet[i] = false;
        }
        sheetInfo.sheet = new int[4, 25];


        bingoball = new int[75];


        BingoMyBlitzRanking = new int[4];
        resetMyBlitzRanking();


        //스테이지 스파인
        SelectWorldStage = 1;
        //WorldStageSpineRefresh = false;
        //WorldStageSpineSelectAni = false;
        //WorldStageSpineAnimation = "wait1";

        userPowerUpList = new List<UserPowerUp>();
        stageList = new List<Stage>();

        useItemDataList = new List<nb_useItemData>();


        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (g_global == null)
        {
            g_global = GameObject.Find("nb_GlobalObject").GetComponent<nb_GlobalData>();
        }

        if (g_global_bgm1 == null)
        {
            g_global_bgm1 = GameObject.Find("nb_GlobalObject/bgm1").GetComponent<AudioSource>();
        }
        if (g_global_bgm2 == null)
        {
            g_global_bgm2 = GameObject.Find("nb_GlobalObject/bgm2").GetComponent<AudioSource>();
        }
        if (g_global_bgm3 == null)
        {
            g_global_bgm3 = GameObject.Find("nb_GlobalObject/bgm3").GetComponent<AudioSource>();
        }
        if (g_global_bgm4 == null)
        {
            g_global_bgm4 = GameObject.Find("nb_GlobalObject/bgm4").GetComponent<AudioSource>();
        }
    }


    public void ResetInfo()
    {
        for (int i = 0; i < 4; ++i)
        {
            sheetInfo.bingoSheet[i] = false;
        }
    }

    public void resetMyBlitzRanking()
    {
        for (int i = 0; i < 4; ++i)
        {
            BingoMyBlitzRanking[i] = 0;
        }
    }

    public void addMyBlitzRanking(int rank)
    {
        for (int i = 0; i < 4; ++i)
        {
            if (BingoMyBlitzRanking[i] == 0)
            {
                BingoMyBlitzRanking[i] = rank;
                return;
            }
        }
    }

    public int getTotalNormalPowerUpCount()
    {
        int size = userPowerUpList.Count;
        int result = 0;
        for (int i = 0; i < size; ++i)
        {
            result += (int)(userPowerUpList[i].Count);
        }

        return result;
    }

    public int getNormalItemIndex(int infoId)
    {
        //1.single daub
        //2.coin reward
        //3.chest
        //4.double daubs
        //5.double exp
        //6.double reward
        //7.bomb
        //8.instant win
        //9.booster
        //10.triple daubs
        int result = 0;

        switch(infoId)
        {
            case 5:
                result = 1;
                break;
            case 6:
                result = 2;
                break;
            case 3:
                result = 3;
                break;
            case 8:
                result = 4;
                break;
            case 9:
                result = 5;
                break;
            case 11:
                result = 6;
                break;
            case 1:
                result = 7;
                break;
            case 13:
                result = 8;
                break;
            case 15:
                result = 9;
                break;
            case 17:
                result = 10;
                break;
        }

        return result;
    }
}

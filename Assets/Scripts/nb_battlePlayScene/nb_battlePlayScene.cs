using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class nb_battlePlayScene : MonoBehaviour
{

    public enum BingoType
    {
        kBingoType_NotBingo = 0,

        kBingoType_Col1st,
        kBingoType_Col2nd,
        kBingoType_Col3rd,
        kBingoType_Col4th,
        kBingoType_Col5th,

        kBingoType_Row1st,
        kBingoType_Row2nd,
        kBingoType_Row3rd,
        kBingoType_Row4th,
        kBingoType_Row5th,

        kBingoType_LT2RB,
        kBingoType_RT2LB,
        kBingoType_Corner,

        kBingoType_DirectBingoItem,
        kBingoType_MAX,
    }

    //private float ballTime = 7.0f;
    private int ballCount = 1;

    public GameObject resultPopupRoot;

    private bool[] m_calledBallNumber;

    [HideInInspector]
    public bool[] m_reloadSheets;

    [HideInInspector]
    public nb_BingoSheet[] m_myLocalSheets;

    [HideInInspector]
    public GameObject[,] daubObjects;

    List<GameObject> m_existBallList;
    List<GameObject> m_blindList;
    List<GameObject> daubItemList;
    List<GameObject> otherDaubList;
    private float lastUseItemTime = 0;
    private int m_itemGaugeCount = 0;
    private bool m_bItemReady = false;

    [HideInInspector]
    public bool m_onFrozen = false;
    private bool m_onBlind = false;
    private bool m_onSheild = false;
    private bool m_sendUseItem = false;

    [HideInInspector]
    public bool m_bGameEnd = false;
    
    [HideInInspector]
    public int temp_index = 0;


    [HideInInspector]
    public int m_swapMySheetIndex = -1;

    [HideInInspector]
    public BingoType m_bingoType = BingoType.kBingoType_NotBingo;

    private nb_battlePlayScene_UI playScene_ui;
    private Transform allbtn;
    private GameObject shield_start;
    [HideInInspector]
    public Vector3 m_positionAtDefence;

    private GameObject m_shield_idle;

    [HideInInspector]
    public int m_badbingoSheetId = 0;
    [HideInInspector]
    public bool[] m_badBingo;
    private float[] m_bingoCooltime;

    private bool bingoflag = false;

    [HideInInspector]
    public string attacker;
    [HideInInspector]
    public string defender;
    [HideInInspector]
    public string skill;
    [HideInInspector]
    public string gameMessage;

    public Texture tex;

    [HideInInspector]
    public string logString = "";
    [HideInInspector]
    public string itemString = "";

    [HideInInspector]
    public bool isSceneEnd = false;

    [HideInInspector]
    public int rankInfo = 0;

    [HideInInspector]
    public bool myrankFlag = true;

    private int bingoCount = 0;

    //참가 유저 리스트
    [HideInInspector]
    public GameObject[] m_playerList;


    //페이드인아웃
    float alpha = 0;
    float FadeSpeed = 0.5f;
    float StartTime;
    float fadeOutTimer;
    bool fadeIn;
    bool fadeOut;

    //아이템
    float itemGaugeRate = 0;
    bool itemCoolDown = false;
    int chestItemCount = 0;
    bool itemBoosterOn = false;


    void Awake()
    {
        m_badBingo = new bool[4];
        //m_bingoCooltime = new float[4];

        for (int i = 0; i < 4; ++i)
        {
            m_badBingo[i] = false;
            //m_bingoCooltime[i] = 5.0f;
        }

        nb_GlobalData.g_global_bgm1.Stop();
        nb_GlobalData.g_global_bgm2.Stop();
        nb_GlobalData.g_global_bgm4.Stop();

        StartCoroutine("playGameBGM");
    }
    
    private IEnumerator playGameBGM()
    {
        yield return new WaitForSeconds(2f);

        nb_GlobalData.g_global_bgm3.Play();
    }

    private IEnumerator callHeartbeat()
    {
        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.HeartBeatRequest);
        yield return new WaitForSeconds(5f);
        StartCoroutine("callHeartbeat");
    }

    void Start()
    {
        StartTime = Time.time;

        fadeIn = true;
        fadeOut = false;

        //StartCoroutine("callHeartbeat");
        rankInfo = 0;
        makeItem();
        
        //nb_GlobalData.g_global.itemIndex = 10;
        bingoCount = 0;

        isSceneEnd = false;
        // introMusic off
        //nb_GlobalData.g_global.GetComponent<AudioSource>().Pause();
        resultPopupRoot.gameObject.SetActive(false);
        playScene_ui = gameObject.GetComponent<nb_battlePlayScene_UI>();
        playScene_ui.label_bingoCount.GetComponent<UILabel>().text = nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainBingo.ToString();
        iTween.ScaleTo(playScene_ui.label_bingoCount.gameObject, iTween.Hash("x", 1f, "y", 1, "z", 1f, "easeType", "easeOutElastic", "time", 0.8f));

        m_bGameEnd = false;
        m_bItemReady = false;
        m_swapMySheetIndex = -1;
        m_onBlind = false;
        m_onFrozen = false;

        lastUseItemTime -= 20.0f;
        m_itemGaugeCount = 0;
        m_bingoType = BingoType.kBingoType_NotBingo;

        m_reloadSheets = new bool[4];
        for (int i = 0; i < 4; ++i)
        {
            m_reloadSheets[i] = false;
        }

        m_calledBallNumber = new bool[76];
        for (int i = 0; i < 76; ++i)
        {
            m_calledBallNumber[i] = false;
        }

        m_existBallList = new List<GameObject>();
        m_blindList = new List<GameObject>();
        daubObjects = new GameObject[4, 25];       // initialized by null;
        daubItemList = new List<GameObject>();
        otherDaubList = new List<GameObject>();

        m_positionAtDefence = Vector3.zero;
        if (m_shield_idle != null)
        {
            DestroyImmediate(m_shield_idle);
            m_shield_idle = null;
        }

        initBingoSheet();
        for (int i = 0; i < 4; ++i)
        {
            //activeBingoSheet(i, true);
        }

        //StartCoroutine("addBall", nb_GlobalData.g_global.bingoball[ballCount]);
//        int count = 1;
//        StartCoroutine("addBall", nb_GlobalData.g_global.bingoball[count]);
        //

        ballCount = 1;

        drawStageBg();
    }

    void Update()
    {
        if (fadeIn)
        {
            alpha = Mathf.Lerp(1.0f, 0.0f, (Time.time - StartTime) * FadeSpeed);
            if (alpha <= 0f)
            {
                fadeIn = false;
            }
        }

        if (fadeOut)
        {
            alpha = Mathf.Lerp(0.0f, 1.0f, (Time.time - fadeOutTimer) * FadeSpeed);
            if (alpha >= 1f)
            {
                fadeOut = false;
                fadeOutEnd();
            }
        }

        //for (int i = 0; i < 4; ++i)
        //{
        //    if (m_badBingo[i])
        //    {
        //        m_bingoCooltime[i] -= Time.deltaTime;

        //        if (playScene_ui.m_badBingoSheetBG[i].gameObject.activeSelf)
        //        {
        //            playScene_ui.m_badBingoSheetBG[i].GetComponentInChildren<UILabel>().text = "00 : 0" + ((int)m_bingoCooltime[i]).ToString();
        //        }

        //        if (m_bingoCooltime[i] <= 0)
        //        {
        //            m_badBingo[i] = false;
        //            m_bingoCooltime[i] = 5f;

        //            if (playScene_ui.m_badBingoSheetBG[i].gameObject.activeSelf)
        //            {
        //                playScene_ui.m_badBingoSheetBG[i].gameObject.SetActive(false);
        //                setActiveNumberButtons(true);
        //            }
        //        }
        //    }
        //}

        /*
        if (bingoCount != nb_GlobalData.g_global.bingoCount)
        {
            bingoCount = nb_GlobalData.g_global.bingoCount;
            playScene_ui.label_bingoCount.GetComponent<UILabel>().text = bingoCount.ToString();
        }
        */

        if (nb_GlobalData.g_global.game_over == 1)
        {
            playScene_ui.label_bingoCount.GetComponent<UILabel>().text = nb_GlobalData.g_global.bingoCount.ToString();

            if (nb_GlobalData.g_global.myRankFlag == false)
            {
                nb_GlobalData.g_global.myrank = 4;
            }
            // 빙고 카운트  = 0 
            bingoflag = true;
            StartCoroutine(activeGameOver());
        }



        if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.MonsterEndGameAlarm_End)
        {
            //게임오버
            runGameOver();

            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
        }

        //else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.BingoRankingAlarm_End)
        //{
        //    nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
        //    playScene_ui.label_bingoCount.GetComponent<UILabel>().text = nb_GlobalData.g_global.bingoCount.ToString();
        //    bingoCount++;
        //}

        //else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.GameCalAlarm_End)
        //{
        //    nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
        //    if (nb_GlobalData.g_global.myRankFlag == false)
        //    {
        //        nb_GlobalData.g_global.myrank = 4;
        //    }
        //    // 빙고 카운트  = 0 
        //    bingoflag = true;
        //    if (m_bGameEnd == false)
        //    {
        //        StartCoroutine(activeGameOver());
        //    }
        //}

        //else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.UserExitAlarm_End)
        //{
        //    //나간 유저 처리
        //    nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
        //    playScene_ui.label_bingoCount.GetComponent<UILabel>().text = nb_GlobalData.g_global.bingoCount.ToString();

            //for (int i = 0; i < 4; i++)
            //{
            //    if (nb_GlobalData.g_global.outer_id == nb_GlobalData.g_global.otherSheetInfo[i].userID)
            //    {
            //        playScene_ui.m_othersName[i].GetComponent<UILabel>().text = "";
            //        nb_GlobalData.g_global.otherSheetInfo[i].activeSheetCount = 0;
            //    }
            //}
            //for (int i = 0; i < 4; i++)
            //{

            //    if (nb_GlobalData.g_global.otherSheetInfo[i].activeSheetCount > 0)
            //    {
            //        break;
            //    }

            //    if (i == 3)
            //    {
            //        playScene_ui.m_item.gameObject.SetActive(false);
            //        playScene_ui.m_itemCooltime.gameObject.SetActive(false);
            //        playScene_ui.m_itemGauge[0].gameObject.SetActive(false);
            //        playScene_ui.m_itemGauge[1].gameObject.SetActive(false);
            //        playScene_ui.m_itemGauge[2].gameObject.SetActive(false);

            //        if (m_shield_idle != null)
            //        {
            //            DestroyImmediate(m_shield_idle);
            //            m_shield_idle = null;
            //        }

            //        for (int otherIndex = 0; otherIndex < 4; ++otherIndex)
            //        {
            //            string targetPath = "target0" + otherIndex.ToString();
            //            string otherBingoPath = "bingo0" + otherIndex.ToString();
            //            playScene_ui.m_targets[otherIndex].gameObject.SetActive(false);
            //            for (int sheetIndex = 0; sheetIndex < 4; ++sheetIndex)
            //            {
            //                string targetSheetPath = targetPath + "_0" + sheetIndex.ToString();
            //                playScene_ui.m_targetSheets[otherIndex, sheetIndex].gameObject.SetActive(false);
            //                string bingoSheetPath = otherBingoPath + "_0" + sheetIndex.ToString();
            //                playScene_ui.m_otherBingoSheets[otherIndex, sheetIndex].gameObject.SetActive(false);
            //            }
            //        }

            //        playScene_ui.m_damageBoard.gameObject.SetActive(false);
            //        foreach (GameObject blind in m_blindList)
            //        {
            //            Destroy(blind.gameObject);
            //        }

            //        if (m_shield_idle != null)
            //        {
            //            DestroyImmediate(m_shield_idle);
            //            m_shield_idle = null;

            //        }

            //        int buttonCount = playScene_ui.buttons.Length;
            //        int resultButtonCount = playScene_ui.resultButtons.Length;

            //        playScene_ui.m_ItemBoard.gameObject.SetActive(false);
            //        playScene_ui.m_targetBoard.gameObject.SetActive(false);

            //        if (m_onSheild)
            //        {
            //            DestroyImmediate(m_shield_idle);
            //            m_shield_idle = null;

            //        }
            //        StopCoroutine("addBall");
            //        if (bingoflag == true)
            //        {
            //            return;
            //        }

            //        playScene_ui.m_otherOut.gameObject.SetActive(true);
            //    }
            //}
        //}

        //else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.GameMessageAlarm_End)
        //{
        //    //for (int i = 0; i < 4; i++)
        //    //{
        //    //    for (int j = 0; j < 4; j++)
        //    //    {
        //    //        if (nb_GlobalData.g_global.otherSheetInfo[i].bingoSheet[j] != nb_GlobalData.g_global.otherBingoS[i].bingo[j])
        //    //        {
        //    //            nb_GlobalData.g_global.otherBingoS[i].bingo[j] = true;

        //    //            rankInfo++;
        //    //            StartCoroutine(setOtherBingo(i, j));

        //    //            processOtherSheet();
        //    //            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;

        //    //            return;
        //    //        }
        //    //    }
        //    //}

        //    processOtherSheet();
        //    nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
        //}

        //else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.ItemMessageAlarm_End)
        //{
        //    nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.ItemMessageAlarm_Ing;
        //    //아이템 받았을경우
        //    if (nb_GlobalData.g_global.myShield == 1)
        //    {
        //        StartCoroutine(unactiveShield());
        //        m_onSheild = false;

        //        nb_GlobalData.g_global.myShield = 0;
        //        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.GameMessageRequest);
        //        nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.GameMessageRequest_End;
        //    }

        //    else
        //    {
        //        if (m_shield_idle != null)
        //        {
        //            DestroyImmediate(m_shield_idle.gameObject);
        //            m_shield_idle = null;
        //        }

        //        //getAttackItem(nb_GlobalData.g_global.attack_id, nb_GlobalData.g_global.attack_index, nb_GlobalData.g_global.attack_sheetindex, nb_GlobalData.g_global.attacker_sheetindex);
        //    }
        //}

        else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.MonsterCompleteBingoAlarm_End ||
            nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.MonsterCompleteBingoResponse_End)
        {
            //순위 갱신
            if (nb_GlobalData.g_global.callBingo)
            {
                //내가 부른 빙고
                nb_GlobalData.g_global.addMyBlitzRanking(
                    nb_GlobalData.g_global.BingoTotalFinishCount);
            }

            //if (nb_GlobalData.g_global.BingoTotalFinishCount == 1)
            //{
            //    playScene_ui.m_rankBoard.Find("player_ranking_1/t_player_name").GetComponent<UILabel>().text =
            //        nb_GlobalData.g_global.BingoLastFinishUserName;
            //}
            //else if (nb_GlobalData.g_global.BingoTotalFinishCount == 2)
            //{
            //    playScene_ui.m_rankBoard.Find("player_ranking_2/t_player_name").GetComponent<UILabel>().text =
            //        nb_GlobalData.g_global.BingoLastFinishUserName;
            //}
            //else if (nb_GlobalData.g_global.BingoTotalFinishCount == 3)
            //{
            //    playScene_ui.m_rankBoard.Find("player_ranking_3/t_player_name").GetComponent<UILabel>().text =
            //        nb_GlobalData.g_global.BingoLastFinishUserName;
            //}
            //else if (nb_GlobalData.g_global.BingoTotalFinishCount >= 4)
            //{
            //    playScene_ui.m_rankBoard.Find("player_ranking_x/t_player_name").GetComponent<UILabel>().text =
            //        nb_GlobalData.g_global.BingoLastFinishUserName;

            //    playScene_ui.m_rankBoard.Find("player_ranking_x/t_rank_text").GetComponent<UILabel>().text =
            //        nb_GlobalData.g_global.BingoTotalFinishCount.ToString() + "th";
            //}
            //else
            //{
            //    nb_GlobalData.g_global.BingoLastFinishUserName = "-";
            //}

            if (nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainBingo <= 5)
            {
                playScene_ui.label_bingoCount.GetComponent<UILabel>().color = Color.red;
            }

            //남은 빙고 숫자 갱신
            playScene_ui.label_bingoCount.GetComponent<UILabel>().text = nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainBingo.ToString();
            iTween.ScaleTo(playScene_ui.label_bingoCount.gameObject, 
                iTween.Hash("x,", 1.2f, "y", 1.3f, "oncompletetarget", gameObject,
                "oncomplete", "completeScaling", "time", 0.3f));


            nb_GlobalData.g_global.callBingo = false;
            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
        }

        else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.MonsterCallNumberAlarm_End)
        {
            StartCoroutine("addBall", nb_GlobalData.g_global.bingoball[ballCount]);

            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
        }


        //if (nb_GlobalData.g_global.BingoNumberCount == 1 && ballCount == 1 &&
        //    nb_GlobalData.g_global.bingoball[1] != 0)
        //{
        //    StartCoroutine("addBall", nb_GlobalData.g_global.bingoball[1]);
        //}
        //else if (ballCount == 1)
        //{
        //    Debug.Log("\"addball\" fail!! " + nb_GlobalData.g_global.BingoNumberCount.ToString() + " "
        //        + ballCount.ToString() + " " + nb_GlobalData.g_global.bingoball[1].ToString());
        //}

        else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.MonsterCheckNumberResponse_End)
        {
            //빙고 번호 선택
            if (nb_GlobalData.g_global.selectItemId != 0)
            {
                int index = nb_GlobalData.g_global.getBattleItemIndex(
                    nb_GlobalData.g_global.selectItemId);
                if (index != 0)
                {
                    playScene_ui.m_itemBtn.Find("i_icon").GetComponent<UISprite>().spriteName
                        = nb_Item.nb_itemIconPath[index + 10];
                    playScene_ui.m_itemBtn.Find("i_icon").GetComponent<UISprite>().MakePixelPerfect();

                    playScene_ui.m_itemGauge.GetComponent<UISprite>().fillAmount = 0;

                    lastUseItemTime = Time.time;
                }
            }

            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
        }

        else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.MonsterUsePowerUpResponse_End)
        {
            //아이템 사용 응답
            runUseItemAction();

            nb_GlobalData.g_global.selectItemId = 0;
            itemCoolDown = true;
            m_itemGaugeCount = 0;

            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
        }

        if (itemCoolDown == true)
        {
            float checkTime = 20;
            if (itemBoosterOn)
            {
                checkTime = 10;
            }
            float coolTime = Time.time - lastUseItemTime;
            if (coolTime > checkTime)
            {
                //쿨타임 끝
                playScene_ui.m_itemBtn.Find("t_label").GetComponent<UILabel>().text =
                    "Charging...";
                itemCoolDown = false;
            }
            else
            {
                //쿨타임
                playScene_ui.m_itemBtn.Find("t_label").GetComponent<UILabel>().text =
                    (checkTime - (int)coolTime).ToString() + " second";
            }
        }
        else
        {
            if (itemBoosterOn)
            {
                if (m_itemGaugeCount == 2)
                {
                    playScene_ui.m_itemBtn.Find("t_label").GetComponent<UILabel>().text =
                        "Ready";
                }
                else
                {
                    playScene_ui.m_itemBtn.Find("t_label").GetComponent<UILabel>().text =
                        "Charging...";
                }
            }
            else
            {
                if (m_itemGaugeCount == 3)
                {
                    playScene_ui.m_itemBtn.Find("t_label").GetComponent<UILabel>().text =
                        "Ready";
                }
                else
                {
                    playScene_ui.m_itemBtn.Find("t_label").GetComponent<UILabel>().text =
                        "Charging...";
                }
            }
        }
    }

    public void makeItem()
    {

        //if (nb_GlobalData.g_global.myCardList.Count != 0)
        //{
        //    int range = 1200;
        //    int freedaub_range = 0;
        //    int doubledaub_range = 0;
        //    int gold30_range = 0;
        //    int gold50_range = 0;
        //    int directBingo_range = 0;
        //    int ticket_range = 0;

        //    int freeze_range = 0;
        //    int blind_range = 0;
        //    int swap_range = 0;
        //    int mix_range = 0;
        //    int defence_range = 0;
        //    int goldticket_range = 0;

        //    for (int i = 0; i < nb_GlobalData.g_global.myCardList.Count; i++)
        //    {
        //        if (nb_GlobalData.g_global.myCardList[i].cardused != 0)
        //        {
        //            for (int k = 0; k < 3; k++)
        //            {
        //                int taggg = 0;
        //                int addValue = 0;
        //                int cardid = 0;
        //                if (k == 0)
        //                {
        //                    taggg = nb_GlobalData.g_global.myCardList[i].AT1;
        //                    cardid = nb_GlobalData.g_global.myCardList[i].cardId;
        //                    addValue = XmlCtrl.x_xml.MCxml[i].Ability1 + (nb_GlobalData.g_global.myCardList[i].cardlevel * (int)XmlCtrl.x_xml.MCxml[cardid].AT1Add);
        //                }
        //                else if (k == 1)
        //                {
        //                    taggg = nb_GlobalData.g_global.myCardList[i].AT2;
        //                    addValue = XmlCtrl.x_xml.MCxml[i].Ability2 + (nb_GlobalData.g_global.myCardList[i].cardlevel * (int)XmlCtrl.x_xml.MCxml[cardid].AT2Add);

        //                }
        //                else
        //                {
        //                    taggg = nb_GlobalData.g_global.myCardList[i].AT3;
        //                    addValue = XmlCtrl.x_xml.MCxml[i].Ability3 + (nb_GlobalData.g_global.myCardList[i].cardlevel * (int)XmlCtrl.x_xml.MCxml[cardid].AT3Add);

        //                }

        //                switch (taggg)
        //                {
        //                    case 1:
        //                        freedaub_range = addValue;
        //                        range = range + freedaub_range;
        //                        break;
        //                    case 2:
        //                        doubledaub_range = addValue;
        //                        range = range + doubledaub_range;
        //                        break;
        //                    case 4:
        //                        gold30_range = addValue;
        //                        range = range + gold30_range;
        //                        break;
        //                    case 5:
        //                        gold50_range = addValue;
        //                        range = range + gold50_range;
        //                        break;
        //                    case 6:
        //                        directBingo_range = addValue;
        //                        range = range + directBingo_range;
        //                        break;
        //                    case 7:
        //                        ticket_range = addValue;
        //                        range = range + ticket_range;
        //                        break;
        //                    case 8:
        //                        freeze_range = addValue;
        //                        range = range + freeze_range;
        //                        break;
        //                    case 9:
        //                        blind_range = addValue;
        //                        range = range + blind_range;
        //                        break;
        //                    case 10:
        //                        swap_range = addValue;
        //                        range = range + swap_range;
        //                        break;
        //                    case 11:
        //                        mix_range = addValue;
        //                        range = range + mix_range;
        //                        break;
        //                    case 12:
        //                        defence_range = addValue;
        //                        range = range + defence_range;
        //                        break;
        //                }
        //            }
        //        }
        //    }

        //    int nextItemIndex = Random.Range(0, range);

        //    if (nextItemIndex > 0 && nextItemIndex <= (96 + freedaub_range))
        //    {
        //        nb_GlobalData.g_global.itemIndex = 1;
        //    }
        //    else if (nextItemIndex > (96 + freedaub_range) && nextItemIndex <= (192 + doubledaub_range + freedaub_range))
        //    {
        //        nb_GlobalData.g_global.itemIndex = 2;
        //    }
        //    else if (nextItemIndex > (192 + doubledaub_range + freedaub_range) && nextItemIndex <= (336 + gold30_range + doubledaub_range + freedaub_range))
        //    {
        //        nb_GlobalData.g_global.itemIndex = 4;
        //    }
        //    else if (nextItemIndex > (336 + gold30_range + doubledaub_range + freedaub_range) && nextItemIndex <= (480 + gold30_range + doubledaub_range + freedaub_range + gold50_range))
        //    {
        //        nb_GlobalData.g_global.itemIndex = 5;
        //    }
        //    else if (nextItemIndex > (480 + gold30_range + doubledaub_range + freedaub_range + gold50_range) && nextItemIndex <= (540 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range))
        //    {
        //        if (nb_GlobalData.g_global.useItemCount < 7)
        //        {
        //            nb_GlobalData.g_global.itemIndex = 5;
        //        }
        //        else
        //        {
        //            nb_GlobalData.g_global.itemIndex = 6;
        //        }
        //    }
        //    else if (nextItemIndex > (540 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range) && nextItemIndex <= (684 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range + freeze_range))
        //    {
        //        nb_GlobalData.g_global.itemIndex = 7;
        //    }
        //    else if (nextItemIndex > (684 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range + freeze_range) && nextItemIndex <= (780 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range + freeze_range + blind_range))
        //    {
        //        nb_GlobalData.g_global.itemIndex = 8;
        //    }
        //    else if (nextItemIndex > (780 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range + freeze_range + blind_range) && nextItemIndex <= (876 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range + freeze_range + blind_range + swap_range))
        //    {
        //        nb_GlobalData.g_global.itemIndex = 9;
        //    }
        //    else if (nextItemIndex > (876 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range + freeze_range + blind_range + swap_range) && nextItemIndex <= (972 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range + freeze_range + blind_range + swap_range + mix_range))
        //    {
        //        nb_GlobalData.g_global.itemIndex = 10;
        //    }
        //    else if (nextItemIndex > (972 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range + freeze_range + blind_range + swap_range + mix_range) && nextItemIndex <= (1068 + directBingo_range + gold30_range + doubledaub_range + freedaub_range + gold50_range + freeze_range + blind_range + swap_range + mix_range + defence_range))
        //    {
        //        nb_GlobalData.g_global.itemIndex = 11;
        //    }
        //    else
        //    {
        //        nb_GlobalData.g_global.itemIndex = 12;
        //    }

        //    if (nb_GlobalData.g_global.itemIndex == 0 || nb_GlobalData.g_global.itemIndex > 12)
        //    {
        //        nb_GlobalData.g_global.itemIndex = 3;

        //    }


        //    return;
        //}

        //int nextItemIndex2 = Random.Range(0, 1200);

        //if (nextItemIndex2 >= 0 && nextItemIndex2 <= 96)
        //{
        //    nb_GlobalData.g_global.itemIndex = 1;
        //}
        //else if (nextItemIndex2 > 96 && nextItemIndex2 <= 192)
        //{
        //    nb_GlobalData.g_global.itemIndex = 2;
        //}
        //else if (nextItemIndex2 > 192 && nextItemIndex2 <= 336)
        //{
        //    nb_GlobalData.g_global.itemIndex = 4;
        //}
        //else if (nextItemIndex2 > 336 && nextItemIndex2 <= 480)
        //{
        //    nb_GlobalData.g_global.itemIndex = 5;
        //}
        //else if (nextItemIndex2 > 480 && nextItemIndex2 <= 540)
        //{
        //    if (nb_GlobalData.g_global.useItemCount < 7)
        //    {
        //        nb_GlobalData.g_global.itemIndex = 5;
        //    }
        //    else
        //    {
        //        nb_GlobalData.g_global.itemIndex = 6;
        //    }
        //}
        //else if (nextItemIndex2 > 540 && nextItemIndex2 <= 684)
        //{
        //    nb_GlobalData.g_global.itemIndex = 7;
        //}
        //else if (nextItemIndex2 > 684 && nextItemIndex2 <= 780)
        //{
        //    nb_GlobalData.g_global.itemIndex = 8;
        //}
        //else if (nextItemIndex2 > 780 && nextItemIndex2 <= 876)
        //{
        //    nb_GlobalData.g_global.itemIndex = 9;
        //}
        //else if (nextItemIndex2 > 876 && nextItemIndex2 <= 972)
        //{
        //    nb_GlobalData.g_global.itemIndex = 10;
        //}
        //else if (nextItemIndex2 > 972 && nextItemIndex2 <= 1068)
        //{
        //    nb_GlobalData.g_global.itemIndex = 11;
        //}
        //else
        //{
        //    nb_GlobalData.g_global.itemIndex = 12;
        //}
        //// nb_GlobalData.g_global.itemIndex = 12;
        //if(nb_GlobalData.g_global.itemIndex == 0 || nb_GlobalData.g_global.itemIndex>12){
        //    nb_GlobalData.g_global.itemIndex = 3;

        //}


        return;
    }

    public void onSendBingo(int sheetIndex)
    {
        /*
        nb_GlobalData.g_global.sheetInfo.bingoSheet[m_currentSheetIndex] = true;
        StartCoroutine(activeBingoEffect());
        nb_GlobalData.g_global.bingoCount--;
        nb_GlobalData.g_global.callBingo = true;
        setBingo(m_currentSheetIndex);

        nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mGameMessageIng;
        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mGameMessageRequest);

        nb_GlobalData.g_global.m_winnerList[rankInfo].nickname = nb_GlobalData.g_global.myInfo.nickName;
        nb_GlobalData.g_global.m_winnerList[rankInfo].monster_id = nb_GlobalData.g_global.sheetInfo.monsterId;
        nb_GlobalData.g_global.m_winnerList[rankInfo].rank = rankInfo+1;
        myrankFlag = false;
        nb_GlobalData.g_global.myrank = rankInfo+1;
        rankInfo++;
        StartCoroutine(bingoResult());
        */
        /*
         StartCoroutine(activeBingoEffect());
         nb_GlobalData.g_global.callBingo = true;
         setBingo(m_currentSheetIndex);
         nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mGameMessageIng;
         nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mGameMessageRequest);
         StartCoroutine(bingoResult());
      */

        if (nb_GlobalData.g_global.sheetInfo.bingoSheet[sheetIndex] == true)
        {
            return;
        }

        bool bingo = checkBingo(sheetIndex);

        if (bingo)
        {
            playScene_ui.m_effectLayer02Board.Find(
                "sound_bingo").GetComponent<AudioSource>().Play();

            nb_GlobalData.g_global.sheetInfo.bingoSheet[sheetIndex] = true;
            playScene_ui.m_bingoButton[sheetIndex].GetComponent<BoxCollider>().enabled = false;
            /*
            if (myrankFlag)
            {
                nb_GlobalData.g_global.m_winnerList[rankInfo].nickname = nb_GlobalData.g_global.myInfo.nickName;
                nb_GlobalData.g_global.m_winnerList[rankInfo].monster_id = nb_GlobalData.g_global.sheetInfo.monsterId;
                nb_GlobalData.g_global.m_winnerList[rankInfo].rank = rankInfo+1;
                
                nb_GlobalData.g_global.myrank = rankInfo+1;
                rankInfo++;
                myrankFlag = false;
            }
            */

            StartCoroutine(activeBingoEffect(sheetIndex));
            /*
            nb_GlobalData.g_global.bingoCount--;
            if (nb_GlobalData.g_global.bingoCount <= 0)
            {
                nb_GlobalData.g_global.bingoCount = 0;
            }
             * */
            nb_GlobalData.g_global.callBingo = true;
            setBingo(sheetIndex);
            //nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.BlitzCompleteBingoRequest);
            //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzCompleteBingoRequest_End;

            StartCoroutine(bingoResult());

        }
        else
        {
            //todo:오답 사운드 필요
            //playScene_ui.m_effectLayer02Board.Find(
            //    "sound_bingo").GetComponent<AudioSource>().Play();

            StartCoroutine(activeBadBingoEffect(sheetIndex));
        }
    }

    private IEnumerator bingoResult()
    {
        yield return new WaitForSeconds(0.5f);

        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.MonsterCompleteBingoRequest);
        nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.MonsterCompleteBingoRequest_End;

    }

    public bool checkBingo(int sheetIndex)
    {
        if (checkCornerBingo(sheetIndex))
            return true;

        if (checkDiagonalBingo(sheetIndex))
            return true;

        for (int i = 0; i < 5; ++i)
        {
            if (checkColBingo(sheetIndex, i) || checkRowBingo(sheetIndex, i))
                return true;
        }
        return false;
    }
    bool checkRowBingo(int sheetIndex, int row)
    {
        for (int index = 0; index < 5; ++index)
        {
            if (m_myLocalSheets[sheetIndex].cells[row * 5 + index].realDaub == false)
                return false;
        }
        m_bingoType = BingoType.kBingoType_Row1st + row;
        return true;
    }
    bool checkColBingo(int sheetIndex, int col)
    {
        for (int index = 0; index < 5; ++index)
        {
            if (m_myLocalSheets[sheetIndex].cells[5 * index + col].realDaub == false)
                return false;
        }
        m_bingoType = BingoType.kBingoType_Col1st + col;
        return true;
    }
    bool checkCornerBingo(int sheetIndex)
    {
        bool cell00 = m_myLocalSheets[sheetIndex].cells[0].realDaub;
        bool cell04 = m_myLocalSheets[sheetIndex].cells[4].realDaub;
        bool cell20 = m_myLocalSheets[sheetIndex].cells[20].realDaub;
        bool cell24 = m_myLocalSheets[sheetIndex].cells[24].realDaub;

        if (cell00 && cell04 && cell20 && cell24)
        {
            m_bingoType = BingoType.kBingoType_Corner;
            return true;
        }
        return false;
    }
    bool checkDiagonalBingo(int sheetIndex)
    {
        if (m_myLocalSheets[sheetIndex].cells[12].realDaub == false)
            return false;

        bool cell00 = m_myLocalSheets[sheetIndex].cells[0].realDaub;
        bool cell06 = m_myLocalSheets[sheetIndex].cells[6].realDaub;
        bool cell18 = m_myLocalSheets[sheetIndex].cells[18].realDaub;
        bool cell24 = m_myLocalSheets[sheetIndex].cells[24].realDaub;

        if (cell00 && cell06 && cell18 && cell24)
        {
            m_bingoType = BingoType.kBingoType_LT2RB;
            return true;
        }
        bool cell04 = m_myLocalSheets[sheetIndex].cells[4].realDaub;
        bool cell08 = m_myLocalSheets[sheetIndex].cells[8].realDaub;
        bool cell16 = m_myLocalSheets[sheetIndex].cells[16].realDaub;
        bool cell20 = m_myLocalSheets[sheetIndex].cells[20].realDaub;

        if (cell04 && cell08 && cell16 && cell20)
        {
            m_bingoType = BingoType.kBingoType_RT2LB;
            return true;
        }
        return false;
    }

    public void visibleScoreGauge(Vector3 pos)
    {
        //int score = 100;

        //nb_GlobalData.g_global.myInfo.GameScore += score;
        //playScene_ui.m_gameScore.GetComponent<UILabel>().text = nb_GlobalData.g_global.myInfo.GameScore.ToString();

        //GameObject scoreEffect = Instantiate(Resources.Load("common/score")) as GameObject;

        //scoreEffect.transform.parent = playScene_ui.m_ItemBoard;
        //scoreEffect.transform.position = pos;
        //scoreEffect.transform.localScale = Vector3.one;
        //StartCoroutine(scoreEffect.GetComponent<scoreCtrl>().setEffect(score));
    }
    public void visibleScoreBingo(int sheetIndex, Vector3 pos)
    {
        //int score = 9000;

        //nb_GlobalData.g_global.myInfo.GameScore += score;
        //playScene_ui.m_gameScore.GetComponent<UILabel>().text = nb_GlobalData.g_global.myInfo.GameScore.ToString();

        //GameObject scoreEffect = Instantiate(Resources.Load("common/score")) as GameObject;

        //scoreEffect.transform.parent = playScene_ui.m_bingoButton[sheetIndex];
        //scoreEffect.transform.position = pos;
        //scoreEffect.transform.localScale = Vector3.one;
        //StartCoroutine(scoreEffect.GetComponent<scoreCtrl>().setEffect(score));

    }

    public int surround_Check(int sheetIndex, int index)
    {
        //모서리
        int score = 0;

        if (index == 0)
        {
            score += surround_Down_Check(sheetIndex, index);
            score += surround_Right_Check(sheetIndex, index);
        }
        else if (index == 4)
        {
            score += surround_Down_Check(sheetIndex, index);
            score += surround_Left_Check(sheetIndex, index);

        }
        else if (index == 20)
        {
            score += surround_Up_Check(sheetIndex, index);
            score += surround_Right_Check(sheetIndex, index);

        }
        else if (index == 24)
        {
            score += surround_Up_Check(sheetIndex, index);
            score += surround_Left_Check(sheetIndex, index);

        }
        //위
        else if (index == 1 || index == 2 || index == 3)
        {
            score += surround_Down_Check(sheetIndex, index);
            score += surround_Left_Check(sheetIndex, index);
            score += surround_Right_Check(sheetIndex, index);

        }
        //아래
        else if (index == 21 || index == 22 || index == 23)
        {
            score += surround_Up_Check(sheetIndex, index);
            score += surround_Right_Check(sheetIndex, index);
            score += surround_Left_Check(sheetIndex, index);

        }
        //오른쪽
        else if (index == 9 || index == 14 || index == 19)
        {
            score += surround_Up_Check(sheetIndex, index);
            score += surround_Down_Check(sheetIndex, index);
            score += surround_Left_Check(sheetIndex, index);

        }
        //왼쪽
        else if (index == 5 || index == 10 || index == 15)
        {
            score += surround_Up_Check(sheetIndex, index);
            score += surround_Down_Check(sheetIndex, index);
            score += surround_Right_Check(sheetIndex, index);

        }
        //나머지
        else
        {
            score += surround_Up_Check(sheetIndex, index);
            score += surround_Down_Check(sheetIndex, index);
            score += surround_Right_Check(sheetIndex, index);
            score += surround_Left_Check(sheetIndex, index);
        }
        return score;
    }
    public int surround_Up_Check(int sheetIndex, int index)
    {
        if (nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, index - 5] == 255)
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }
    public int surround_Down_Check(int sheetIndex, int index)
    {
        if (nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, index + 5] == 255)
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }
    public int surround_Right_Check(int sheetIndex, int index)
    {
        if (nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, index + 1] == 255)
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }
    public int surround_Left_Check(int sheetIndex, int index)
    {
        if (nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, index - 1] == 255)
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }



    public void visibleScore(Vector3 pos, int sheetIndex, int index)
    {
        //int score = 100;


        //score += surround_Check(sheetIndex, index);

        //nb_GlobalData.g_global.myInfo.GameScore += score;

        //playScene_ui.m_gameScore.GetComponent<UILabel>().text = nb_GlobalData.g_global.myInfo.GameScore.ToString();

        //GameObject scoreEffect = Instantiate(Resources.Load("common/score")) as GameObject;
        //scoreEffect.transform.parent = playScene_ui.m_effectLayer02Board;
        //scoreEffect.transform.position = pos;
        //scoreEffect.transform.localScale = Vector3.one;
        //StartCoroutine(scoreEffect.GetComponent<scoreCtrl>().setEffect(score));

    }
    public void visibleMinusScore(Vector3 pos)
    {
        //int score = 2;

        //nb_GlobalData.g_global.myInfo.GameScore -= score;

        //if (nb_GlobalData.g_global.myInfo.GameScore < 1)
        //{
        //    nb_GlobalData.g_global.myInfo.GameScore = 0;
        //}

        //playScene_ui.m_gameScore.GetComponent<UILabel>().text = nb_GlobalData.g_global.myInfo.GameScore.ToString();

        //GameObject scoreEffect = Instantiate(Resources.Load("common/score")) as GameObject;
        //scoreEffect.transform.parent = playScene_ui.m_effectLayer02Board;
        //scoreEffect.transform.position = pos;
        //scoreEffect.transform.localScale = Vector3.one;
        //StartCoroutine(scoreEffect.GetComponent<scoreCtrl>().setMinusEffect(score));

    }

    public void onUseItem()
    {
        //아이템 게이지가 차야됨
        if (m_itemGaugeCount <= 2)
            return;

        if (nb_GlobalData.g_global.selectItemId == 0)
        {
            //선택된 아이템이 없음
            return;
        }

        if (itemCoolDown == true)
        {
            //쿨타임 중
            return;
        }

        //사용
        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.MonsterUsePowerUpRequest);


        playScene_ui.m_itemBtn.Find("i_icon").GetComponent<UISprite>().spriteName
            = "ui_money_normal";
        playScene_ui.m_itemBtn.Find("i_icon").GetComponent<UISprite>().SetDimensions(92, 98);

        //if (nb_GlobalData.g_global.itemIndex == (int)Item.ItemType.Item_None)
        //    return;
        //bool needTarget = false;

        //nb_GlobalData.g_global.g_gameData.itemIndex = nb_GlobalData.g_global.itemIndex;

        //long targetID = nb_GlobalData.g_global.sheetInfo.userID;
        //Item.ItemType itemtype = (Item.ItemType)nb_GlobalData.g_global.itemIndex;

        //visibleScoreGauge(playScene_ui.m_itemGauge[0].position);

        //StartCoroutine(activeGaugeClickEffect());

        //switch (itemtype)
        //{
        //    case Item.ItemType.Item_Shield:
        //        {
        //            targetID = nb_GlobalData.g_global.sheetInfo.userID;

        //        } break;
        //    case Item.ItemType.Item_FrozenItem:
        //        {

        //            needTarget = true;
        //            setTargetUI(true, false);
        //        } break;
        //    case Item.ItemType.Item_Blind_5:
        //        {

        //            needTarget = true;
        //            setTargetUI(true, false);
        //        } break;
        //    case Item.ItemType.Item_SwapSheet:
        //        {

        //            //if (nb_GlobalData.g_global.sheetInfo.bingoSheet[m_currentSheetIndex] == true)
        //            //{
        //            //    nb_GlobalData.g_global.ItemUseState = false;

        //            //    resetGauge();

        //            //    return;
        //            //}

        //            //needTarget = true;
        //            //setTargetUI(true, true);

        //        } break;
        //    case Item.ItemType.Item_ShuffleSheet:
        //        {
        //            //setTargetUI(true, true);
        //            //needTarget = true;
        //        } break;
        //}

        //int[] applyCellIndex2 = new int[nb_GlobalData.g_global.sheetInfo.activeSheetCount];
        //int tempint = 0;
        //for (int i = 0; i < nb_GlobalData.g_global.sheetInfo.activeSheetCount; i++)
        //{
        //    tempint = Random.Range(0, 24);
        //    int whileCount = 0;
        //    while (m_myLocalSheets[i].cells[tempint].itemEffectIndex != 0 || m_myLocalSheets[i].cells[tempint].realDaub == true)
        //    {
        //        whileCount++;
        //        tempint = Random.Range(0, 24);
        //        if (whileCount == 500)
        //        {
        //            resetGauge();
        //            nb_GlobalData.g_global.ItemUseState = false;
        //            if (nb_GlobalData.g_global.myInfo.ItemCount <= 0)
        //            {
        //                playScene_ui.noItemMsg.gameObject.SetActive(true);
        //            }
        //            return;
        //        }
        //    }
        //    applyCellIndex2[i] = tempint;
        //}

        //if (needTarget)
        //{
        //    resetGauge();
        //    if (nb_GlobalData.g_global.myInfo.ItemCount <= 0)
        //    {
        //        playScene_ui.noItemMsg.gameObject.SetActive(true);
        //    }
        //    return;
        //}

        //nb_GlobalData.g_global.ItemUseState = false;
        //Item.ItemType type = (Item.ItemType)nb_GlobalData.g_global.itemIndex;

        //applyItem(type, applyCellIndex2);

        //if (nb_GlobalData.g_global.myInfo.ItemCount <= 0)
        //{
        //    playScene_ui.noItemMsg.gameObject.SetActive(true);
        //}
    }

    public void setGameMent(string attacker, bool attack, bool attacked, bool defence, bool bingo)
    {
        //playScene_ui.m_gameMessage_target.GetComponent<UILabel>().text = attacker;

        string action = "";

        if (attack)
        {
            action = "ATTACK  " + attacker;
        }
        else if (attacked)
        {
            action = "You're attacked by " + attacker;
        }
        else if (defence)
        {
            action = "You defend attack of " + attacker;
        }
        else if (bingo)
        {
            action = attacker + " made a bingo";
        }

        playScene_ui.m_gameMessage_action.GetComponent<UILabel>().text = action;
        StartCoroutine(waitTime());
    }

    public void onEndGame()
    {
        resultPopupRoot.gameObject.SetActive(true);

        int buttonCount = playScene_ui.buttons.Length;
        int resultButtonCount = playScene_ui.resultButtons.Length;

        for (int i = 0; i < buttonCount; ++i)
        {
            playScene_ui.buttons[i].enabled = false;
        }

        for (int k = 0; k < resultButtonCount; ++k)
        {
            playScene_ui.resultButtons[k].enabled = true;
        }

        playScene_ui.m_ItemBoard.gameObject.SetActive(false);
        playScene_ui.m_targetBoard.gameObject.SetActive(false);

        if (m_shield_idle != null)
        {
            DestroyImmediate(m_shield_idle);
            m_shield_idle = null;
        }
        StopCoroutine("addBall");
        StopCoroutine("callHeartbeat");
        if (nb_GlobalData.g_global.gameType == 1)
        {
            StartCoroutine(resultPopupRoot.GetComponent<gameRanking>().initRanking());
        }

        else
        {
            StartCoroutine(resultPopupRoot.GetComponent<battingRanking>().initRanking());
        }

        //        StartCoroutine(resultPopupRoot.GetComponent<ResultPopupCtrl>().initResut());
    }
    private IEnumerator waitMessage()
    {
        yield return new WaitForSeconds(0.5f);

        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.ItemMessageRequest);
        nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.ItemMessageRequest_End;
    }

    //아이템 공격 할때
    public void setItemTarget(int objectId, int sheetIndex, int sheetId)
    {
        //if (nb_GlobalData.g_global.itemIndex == (int)Item.ItemType.Item_SwapSheet || nb_GlobalData.g_global.itemIndex == (int)Item.ItemType.Item_ShuffleSheet)
        //{
        //    sheetId = sheetId - 1;
        //}

        //nb_GlobalData.g_global.target_id = (int)nb_GlobalData.g_global.otherSheetInfo[objectId].userID;
        //nb_GlobalData.g_global.target_index = nb_GlobalData.g_global.itemIndex;
        //nb_GlobalData.g_global.target_sheetindex = sheetId;
        //nb_GlobalData.g_global.targeter_sheetindex = sheetIndex;
        //Vector3 pos = Vector3.zero;
        //int temp = 0;
        //if (nb_GlobalData.g_global.otherSheetInfo[objectId].shield == 1)
        //{
        //    /// 상대방 쉴드 있음
        //    nb_GlobalData.g_global.otherSheetInfo[objectId].shield = 0;

        //    nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mGameMessageIng;
        //    nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mGameMessageRequest);

        //    StartCoroutine(waitMessage());

        //    setTargetUI(false, false);

        //    return;
        //}

        //else
        //{
        //    if (nb_GlobalData.g_global.itemIndex == (int)Item.ItemType.Item_SwapSheet)
        //    {
        //        clearSheet(sheetIndex);
        //        for (int k = 0; k < 25; k++)
        //        {
        //            temp = nb_GlobalData.g_global.otherSheetInfo[objectId].sheet[sheetId, k];
        //            nb_GlobalData.g_global.otherSheetInfo[objectId].sheet[sheetId, k] = nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, k];
        //            nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, k] = temp;
        //        }

        //        //activeBingoSheet(sheetIndex, false);
        //        activeItemNoticeEffect(Item.ItemType.Item_SwapSheet);
        //        string nickname = nb_GlobalData.g_global.otherSheetInfo[objectId].nickname;

        //        pos = playScene_ui.m_targets[objectId].localPosition;

        //        reloadSheet(sheetIndex);
        //        processMySheet(sheetIndex);
        //        processOtherSheet();
        //    }
        //}

        //long otherUserId = nb_GlobalData.g_global.otherSheetInfo[objectId].userID;

        //setGameMent(nb_GlobalData.g_global.otherSheetInfo[objectId].nickname, true, false, false, false);

        //Item.ItemType itemtype = (Item.ItemType)nb_GlobalData.g_global.itemIndex;

        //m_positionAtDefence = playScene_ui.m_targetSheets[objectId, sheetId].localPosition;

        //pos = playScene_ui.m_targetSheets[objectId, sheetId].localPosition;
        //if (nb_GlobalData.g_global.itemIndex == (int)Item.ItemType.Item_Blind_5 || nb_GlobalData.g_global.itemIndex == (int)Item.ItemType.Item_FrozenItem)
        //{
        //    pos = playScene_ui.m_targets[objectId].localPosition;
        //}
        ////121212
        //StartCoroutine(activeAttackEffect(itemtype, pos));


        //setTargetUI(false, false);

        //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mItemMessageIng;
        //nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mItemMessageRequest);
    }

    public void applyItem(Item.ItemType itemtype, int[] applyCellIndex)
    {
        //nb_GlobalData.g_global.ItemUseState = false;

        //int activeCount = nb_GlobalData.g_global.sheetInfo.activeSheetCount;

        //switch (itemtype)
        //{
        //    case Item.ItemType.Item_Daub:
        //        {
        //            for (int sheetIndex = 0; sheetIndex < activeCount; ++sheetIndex)
        //            {
        //                int cellIndex = applyCellIndex[sheetIndex];
        //                if (cellIndex < 250)
        //                {
        //                    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.itemeffect_bomb];
        //                    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Play();
        //                    setDaub(sheetIndex, cellIndex, true, true);
        //                }

        //            }
        //            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mGameMessageIng;   // onsendDaub
        //            nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mGameMessageRequest);
        //        } break;
        //    case Item.ItemType.Item_DirectBingo:
        //    case Item.ItemType.Item_Bomb:
        //    case Item.ItemType.Item_Daub_10:
        //    case Item.ItemType.Item_Gold_30:
        //    case Item.ItemType.Item_Gold_50:
        //    case Item.ItemType.Item_Ticket:
        //        {
        //            // item sound
        //            playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.itemeffect];
        //            playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Play();

        //            for (int sheetIndex = 0; sheetIndex < activeCount; ++sheetIndex)
        //            {
        //                int cellIndex = applyCellIndex[sheetIndex];

        //                if (cellIndex < 200)
        //                {
        //                    //effect
        //                    m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex = (int)itemtype;

        //                }

        //                //if (sheetIndex == m_currentSheetIndex)
        //                {
        //                    if (cellIndex < 200)
        //                    {
        //                        // effect
        //                        m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex = (int)itemtype;
        //                        // daub item effect
        //                        StartCoroutine(activeDaubItemCreate(playScene_ui.m_cells[sheetIndex, cellIndex].position));

        //                        setItemDaubUI(sheetIndex, cellIndex, (int)itemtype);
        //                    }
        //                }
        //            }
        //        } break;

        //    case Item.ItemType.Item_Shield:
        //        {
        //            if (nb_GlobalData.g_global.myShield == 0)
        //            {
        //                m_onSheild = true;
        //                nb_GlobalData.g_global.myShield = 1;

        //                StartCoroutine(activeShield());
        //            }
        //        } break;
        //    case Item.ItemType.Item_SwapSheet:
        //        {
        //            for (int sheetIndex = 0; sheetIndex < activeCount; ++sheetIndex)
        //            {
        //                clearSheet(sheetIndex);
        //                m_swapMySheetIndex = -1;
        //            }

        //        } break;
        //    case Item.ItemType.Item_ShuffleSheet:
        //        {
        //        } break;
        //}
        //resetGauge();
    }

    public void processDaub(int sheetIndex, int index)
    {
        int cellNumber = getDaubNumber(sheetIndex, index);
        bool bDaubed = m_myLocalSheets[sheetIndex].cells[index].daub;

        if (bDaubed == false)
        {
            m_myLocalSheets[sheetIndex].cells[index].daub = true;

            daubObjects[sheetIndex, index].SetActive(true);
            daubObjects[sheetIndex, index].GetComponentInChildren<UILabel>().text = cellNumber.ToString();
            daubObjects[sheetIndex, index].GetComponent<UISprite>().spriteName = "ui_daub1";
            //if (m_calledBallNumber[m_myLocalSheets[sheetIndex].cells[index].number] == true)
            if (checkDaubNumber(cellNumber) == true)
                //
            {
                //덮되면 번호  변경

                nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, index] = 255;

                m_myLocalSheets[sheetIndex].cells[index].realDaub = true;
                //playScene_ui.m_cells[sheetIndex, index].GetComponent<UILabel>().text = "";
                //
                daubObjects[sheetIndex, index].GetComponent<UISprite>().spriteName = "ui_daub1";
                daubObjects[sheetIndex, index].transform.Find("num").GetComponent<UILabel>().color = Color.white;
                //playScene_ui.m_sound_daubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.daub];
                //playScene_ui.m_sound_daubEffect.GetComponent<AudioSource>().Play();
                int tempitem = m_myLocalSheets[sheetIndex].cells[index].itemEffectIndex;

                //visibleScore(playScene_ui.m_cells[sheetIndex, index].position, sheetIndex, index);

                int itemIndex = m_myLocalSheets[sheetIndex].cells[index].itemEffectIndex;
                if (itemIndex > 0)   //아이템 있는 칸 처리
                {
                    if (itemIndex == 3)
                    {
                        //상자
                        ++chestItemCount;
                        playScene_ui.m_baseBoard.Find("i_icon2").GetComponent<UISprite>().spriteName = "ui_info_chest1";
                        playScene_ui.m_baseBoard.Find("t_bonus_count").GetComponent<UILabel>().text = "x" + chestItemCount.ToString();
                    }
                    else if (itemIndex == 6)
                    {
                        //따블 리워드
                        playScene_ui.m_baseBoard.Find("i_icon3").GetComponent<UISprite>().spriteName = "ui_info_all1";
                    }
                    else if (itemIndex == 5)
                    {
                        //따블 경험치
                        playScene_ui.m_baseBoard.Find("i_icon4").GetComponent<UISprite>().spriteName = "ui_info_xp1";
                    }

                    //if (m_myLocalSheets[sheetIndex].cells[index].itemEffectIndex != (int)Item.ItemType.Item_DirectBingo)
                    //{
                    //    int itemIndex = m_myLocalSheets[sheetIndex].cells[index].itemEffectIndex;
                    //    m_myLocalSheets[sheetIndex].cells[index].itemEffectIndex = 0;
                    //    StartCoroutine(activeDaubItemClick(sheetIndex, itemIndex, index));
                    //    //playScene.visibleScore((Item.ItemType)itemIndex, playSceneUI.m_cells[m_cellIndexAtSendDaub].position);
                    //    setItemDaubUI(sheetIndex, index, -1);
                    //}
                }

                //170817: 아이템 나중에 다시 구현
                //if (tempitem == 4 || tempitem == 5)
                //{
                //    playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_getcoin];
                //    playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().Play();

                //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.itemeffect_coin];
                //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Play();
                //}
                //else if (tempitem == 2)
                //{
                //    playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_sidedaub];
                //    playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().Play();

                //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.itemeffect_bomb];
                //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Play();
                //}
                //else if (tempitem == 7)
                //{
                //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.itemeffect_ticket];
                //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Play();
                //}

                //if (checkItemBingo(sheetIndex, index) == true)
                //{
                //    nb_GlobalData.g_global.sheetInfo.bingoSheet[sheetIndex] = true;
                //    /*
                //    if (myrankFlag)
                //    {
                //        nb_GlobalData.g_global.m_winnerList[rankInfo].nickname = nb_GlobalData.g_global.myInfo.nickName;
                //        nb_GlobalData.g_global.m_winnerList[rankInfo].monster_id = nb_GlobalData.g_global.sheetInfo.monsterId;
                //        nb_GlobalData.g_global.m_winnerList[rankInfo].rank = rankInfo+1;

                //        nb_GlobalData.g_global.myrank = rankInfo+1;
                //        rankInfo++;
                //        myrankFlag = false;
                //    }
                //    */
                //    StartCoroutine(activeBingoEffect(sheetIndex));
                //    /*
                //    nb_GlobalData.g_global.bingoCount--;
                //    if (nb_GlobalData.g_global.bingoCount <= 0)
                //    {
                //        nb_GlobalData.g_global.bingoCount = 0;
                //    }
                //    */
                //    //nb_GlobalData.g_global.callBingo = true;
                //    setBingo(sheetIndex);

                //    StartCoroutine(bingoResult());
                //}

                increaseGauge();

                nb_GlobalData.g_global.CheckNumCardIndex = sheetIndex;
                nb_GlobalData.g_global.CheckNumNumber = cellNumber;

                nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.MonsterCheckNumberRequest);

                //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mGameMessageIng;   // onsendDaub
                //nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mGameMessageRequest);

                //
            }
            else
            {
                //playScene_ui.m_sound_daubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.nodaub];
                //playScene_ui.m_sound_daubEffect.GetComponent<AudioSource>().Play();

                //visibleMinusScore(playScene_ui.m_cells[sheetIndex, index].position);
            }
        }
        else // daub == true
        {
            if (m_myLocalSheets[sheetIndex].cells[index].realDaub == false)
            {
                //틀린답은 클릭하면 원래대로
                m_myLocalSheets[sheetIndex].cells[index].daub = false;

                daubObjects[sheetIndex, index].SetActive(false);
                daubObjects[sheetIndex, index].transform.Find("num").GetComponent<UILabel>().color = Color.black;

                int itemType = m_myLocalSheets[sheetIndex].cells[index].itemEffectIndex;
                if (itemType != 0)
                {
                    setCellItem(sheetIndex, index, (nb_Item.nb_ItemType)itemType);
                    //playScene_ui.m_cells[sheetIndex, index].GetComponent<UILabel>().text = "";
                }

                //changeCellColor(index, false);

                //playScene_ui.m_sound_cleardaubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.cleardaub];
                //playScene_ui.m_sound_cleardaubEffect.GetComponent<AudioSource>().Play();
            }
        }

        // daub effect
        StartCoroutine(activeDaubEffect(playScene_ui.m_cells[sheetIndex, index]));
    }

    private IEnumerator activeDaubEffect(Transform tr)
    {
        GameObject effect = Instantiate(Resources.Load("effects/eff_daub_click_particle")) as GameObject;

        effect.transform.parent = tr;

        effect.transform.localPosition = Vector3.zero;
        effect.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(1.0f);

        if (effect)
        {
            Destroy(effect.gameObject);
        }
    }

    public IEnumerator activeGameOver()
    {
        if (m_bGameEnd == false)
        {
            m_bGameEnd = true;
            playScene_ui.m_bingoButton[0].gameObject.SetActive(false);
            playScene_ui.m_bingoButton[1].gameObject.SetActive(false);
            playScene_ui.m_bingoButton[2].gameObject.SetActive(false);
            playScene_ui.m_bingoButton[3].gameObject.SetActive(false);
            //playScene_ui.m_item.gameObject.SetActive(false);
            playScene_ui.m_itemCooltime.gameObject.SetActive(false);
            //playScene_ui.m_itemGauge[0].gameObject.SetActive(false);
            //playScene_ui.m_itemGauge[1].gameObject.SetActive(false);
            //playScene_ui.m_itemGauge[2].gameObject.SetActive(false);
            if (m_shield_idle != null)
            {
                DestroyImmediate(m_shield_idle);
                m_shield_idle = null;
            }
            //yield return new WaitForSeconds(1.5f);

            for (int otherIndex = 0; otherIndex < 4; ++otherIndex)
            {
                string targetPath = "target0" + otherIndex.ToString();
                string otherBingoPath = "bingo0" + otherIndex.ToString();

                playScene_ui.m_targets[otherIndex].gameObject.SetActive(false);

                for (int sheetIndex = 0; sheetIndex < 4; ++sheetIndex)
                {
                    string targetSheetPath = targetPath + "_0" + sheetIndex.ToString();
                    playScene_ui.m_targetSheets[otherIndex, sheetIndex].gameObject.SetActive(false);

                    string bingoSheetPath = otherBingoPath + "_0" + sheetIndex.ToString();

                    playScene_ui.m_otherBingoSheets[otherIndex, sheetIndex].gameObject.SetActive(false);
                }
            }

            playScene_ui.m_damageBoard.gameObject.SetActive(false);
            foreach (GameObject blind in m_blindList)
            {
                Destroy(blind.gameObject);
            }
            playScene_ui.m_resultBoard.gameObject.SetActive(true);

            GameObject gameoverEff = Instantiate(Resources.Load("effects/eff_notice_gameover")) as GameObject;
            Vector3 scale = gameoverEff.transform.localScale;
            Vector3 pos = gameoverEff.transform.localPosition;

            gameoverEff.transform.parent = playScene_ui.m_resultBoard;
            gameoverEff.transform.localPosition = pos;
            gameoverEff.transform.localScale = scale;

            //playScene_ui.m_talk_gameover.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.game_over];
            //playScene_ui.m_talk_gameover.GetComponent<AudioSource>().Play();

            nb_GlobalData.g_global.ItemUseState = true;

            yield return new WaitForSeconds(2f);

            if (gameoverEff)
            {
                Destroy(gameoverEff);
            }
            onEndGame();
        }
    }

    public void activeBingoSheet(int sheetIndex, bool force)
    {
        //if (force == false)
        //{
        //    if (sheetIndex == m_currentSheetIndex)
        //        return;
        //}

        //int activeSheetCount = nb_GlobalData.g_global.sheetInfo.activeSheetCount;

        //if (sheetIndex >= activeSheetCount)
        //{
        //    return;
        //}

        //if (nb_GlobalData.g_global.sheetInfo.bingoSheet[sheetIndex] == true)
        //{
        //    playScene_ui.m_bingoButton[sheetIndex].GetComponent<BoxCollider>().enabled = false;
        //    playScene_ui.m_bingoSheetBG[sheetIndex].gameObject.SetActive(true);
        //    return;
        //}

        //else
        //{
        //    playScene_ui.m_bingoButton[sheetIndex].GetComponent<BoxCollider>().enabled = true;
        //    playScene_ui.m_bingoSheetBG[sheetIndex].gameObject.SetActive(false);
        //}


        //resetDaubObjects();

        //foreach (GameObject obj in daubItemList)
        //{
        //    Destroy(obj);
        //}
        //daubItemList.Clear();

        //for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
        //{
        //    playScene_ui.m_cells[sheetIndex, cellIndex].GetComponent<UILabel>().text = m_myLocalSheets[sheetIndex].cells[cellIndex].number.ToString();

        //    int daubItemIndex = m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex;
        //    bool daub = m_myLocalSheets[sheetIndex].cells[cellIndex].daub;
        //    bool realDaub = m_myLocalSheets[sheetIndex].cells[cellIndex].realDaub;
        //    int number = m_myLocalSheets[sheetIndex].cells[cellIndex].number;

        //    if (daub)
        //    {
        //        if (realDaub == true)
        //        {
        //            //playScene_ui.m_cells[sheetIndex, cellIndex].GetComponent<UILabel>().text = "";

        //            daubObjects[sheetIndex, cellIndex].gameObject.SetActive(true);
        //            daubObjects[sheetIndex, cellIndex].GetComponent<UISprite>().spriteName = "daub_o";
        //        }
        //        else
        //        {
        //            if (daubItemIndex > 0)
        //            {
        //                setItemDaubUI(sheetIndex, cellIndex, daubItemIndex);
        //            }

        //            //changeCellColor(cellIndex, true);
        //            daubObjects[sheetIndex, cellIndex].gameObject.SetActive(true);
        //            daubObjects[sheetIndex, cellIndex].GetComponent<UISprite>().spriteName = "daub_x";
        //        }
        //    }
        //    else
        //    {
        //        if (daubItemIndex > 0)
        //        {
        //            setItemDaubUI(sheetIndex, cellIndex, daubItemIndex);
        //        }

        //        daubObjects[sheetIndex, cellIndex].SetActive(false);

        //        //changeCellColor(cellIndex, false);
        //    }
        //}

        //if (m_badBingo[sheetIndex])
        //{
        //    playScene_ui.m_badBingoSheetBG[sheetIndex].gameObject.SetActive(true);
        //    setActiveNumberButtons(false);
        //}
        //else
        //{
        //    playScene_ui.m_badBingoSheetBG[sheetIndex].gameObject.SetActive(false);
        //    setActiveNumberButtons(true);
        //}
    }

    public IEnumerator activeGaugeClickEffect()
    {
        GameObject effect = Instantiate(Resources.Load("effects/eff_gauge_fire")) as GameObject;
        Vector3 pos = effect.transform.localPosition;
        Vector3 scale = effect.transform.localScale;

        effect.transform.parent = playScene_ui.m_effectLayer02Board;
        effect.transform.localPosition = pos;
        effect.transform.localScale = scale;

        yield return new WaitForSeconds(1.0f);

        if (effect)
        {
            Destroy(effect);
        }
    }

    public void setBingo(int sheetIndex)
    {
        if (nb_GlobalData.g_global.sheetInfo.bingoSheet[sheetIndex] == false)// && m_currentSheetIndex == sheetIndex)
        {

        }

        //if (sheetIndex == m_currentSheetIndex)
        {
            playScene_ui.m_bingoSheetBG[sheetIndex].gameObject.SetActive(true);

            if (nb_GlobalData.g_global.sheetInfo.bingoSheet[sheetIndex] == false)
            {

                int bingoIndex = nb_GlobalData.g_global.bingoCount - 1;
                //nb_GlobalData.g_global.bingoRankId[bingoIndex] = nb_GlobalData.g_global.sheetInfo.userID;

            }
        }
        //StartCoroutine(activeBingoEffect());
        //playScene_ui.setSheetButton_Bingo(sheetIndex); //시트 전환 버튼 삭제함
    }

    public void setItemDaubUI(int sheetIndex, int cellIndex, int itemIndex)
    {
        if (itemIndex == -1)
        {
            if (playScene_ui.m_cells[sheetIndex, cellIndex].childCount > 0)
            {
                Transform daubItemTr = playScene_ui.m_cells[sheetIndex, cellIndex].GetChild(0);
                daubItemList.Remove(daubItemTr.gameObject);
                Destroy(daubItemTr.gameObject);
            }
        }
        else
        {

            // focus effect

            StartCoroutine(activeDaubItemFocus(playScene_ui.m_cells[sheetIndex, cellIndex].position));

            GameObject daubItemObject = Instantiate(Resources.Load("game/daubItem")) as GameObject;
            string path = nb_Item.nb_daubItemImagePath[itemIndex].ToString();
            //path += "_b";
            daubItemObject.GetComponent<UISprite>().spriteName = path;


            // Destroy(target_cell);
            daubItemObject.transform.parent = playScene_ui.m_cells[sheetIndex, cellIndex];
            daubItemObject.transform.localPosition = Vector3.zero;
            daubItemObject.transform.localScale = Vector3.one;

            daubItemList.Add(daubItemObject);

        }
    }

    public IEnumerator waitTime()
    {
        yield return new WaitForSeconds(5.0f);
        playScene_ui.m_gameMessage_target.GetComponent<UILabel>().text = "";
    }

    public void increaseGauge()
    {
        //if (m_onFrozen || m_bItemReady)
        //{
        //    return;
        //}

        if (nb_GlobalData.g_global.getTotalNormalPowerUpCount() == 0)
        {
            //가진 아이템이 없음
            Debug.Log("increaseGauge fail : not enough item");
            return;
        }

        float coolTime = Time.time - lastUseItemTime;

        if (itemBoosterOn && coolTime < 10.0f)
        {
            //아이템 쿨타임 중임 : 부스터 상태
            Debug.Log("increaseGauge fail : CoolTime(boost)");
            return;
        }
        else if (itemBoosterOn == false && coolTime < 20.0f)
        {
            //아이템 쿨타임 중임
            Debug.Log("increaseGauge fail : CoolTime");
            return;
        }

        if (itemBoosterOn && m_itemGaugeCount > 1)
        {
            //게이지가 가득참 : 부스터 상태
            return;
        }
        else if (itemBoosterOn == false && m_itemGaugeCount > 2)
        {
            //게이지가 가득참
            return;
        }

        ++m_itemGaugeCount;

        if (m_itemGaugeCount == 1)
        {
            //한칸
            playScene_ui.m_itemGauge.GetComponent<UISprite>().fillAmount = 0.33f;

            if (itemBoosterOn)
            {
                playScene_ui.m_itemGauge.GetComponent<UISprite>().fillAmount = 0.5f;
            }
            //Debug.Log("Item Debug Msg : Gauge 1/3");
        }
        else if (m_itemGaugeCount == 2)
        {
            //두칸
            playScene_ui.m_itemGauge.GetComponent<UISprite>().fillAmount = 0.66f;

            if (itemBoosterOn)
            {
                playScene_ui.m_itemGauge.GetComponent<UISprite>().fillAmount = 1.0f;
            }
            //Debug.Log("Item Debug Msg : Gauge 2/3");
        }
        else if (m_itemGaugeCount == 3)
        {
            //만땅
            playScene_ui.m_itemGauge.GetComponent<UISprite>().fillAmount = 1.0f;
            activeItem(nb_GlobalData.g_global.itemIndex);

            //Debug.Log("Item Debug Msg : Gauge 3/3");

           // nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.BlitzRefreshPowerUpRequest);
        }


    }

    public IEnumerator activeItemReady()
    {
        playScene_ui.m_itemCooltime.gameObject.SetActive(true);
        //playScene_ui.m_itemGauge[0].gameObject.SetActive(false);
        playScene_ui.m_itemGauge.GetComponent<UISprite>().fillAmount = 1.0f;

        m_bItemReady = true;

        float wait = 20.0f;
        if (itemBoosterOn)
        {
            wait = 10.0f;
        }
        yield return new WaitForSeconds(wait);

        if (nb_GlobalData.g_global.ItemUseState == true)
        {
            StartCoroutine(activeItemReady());
        }

        else
        {
            makeItem();
            nb_GlobalData.g_global.ItemUseState = false;
            Item.ItemType type = (Item.ItemType)nb_GlobalData.g_global.itemIndex;

            playScene_ui.m_itemCooltime.gameObject.SetActive(false);
            //playScene_ui.m_itemGauge[0].gameObject.SetActive(true);
            playScene_ui.m_itemGauge.GetComponent<UISprite>().fillAmount = 0.0f;
            m_bItemReady = false;
        }
    }

    public void reloadSheet(int sheetIndex)
    {
        for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
        {
            int number = nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, cellIndex];

            m_myLocalSheets[sheetIndex].cells[cellIndex].number = number;

            if (nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, cellIndex] > 200)
            {
                setDaub(sheetIndex, cellIndex, true, true);
            }
        }

        //if (sheetIndex == m_currentSheetIndex)
        {
            for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
            {
                int number = m_myLocalSheets[sheetIndex].cells[cellIndex].number;
                playScene_ui.m_cells[sheetIndex, cellIndex].GetComponent<UILabel>().text = number.ToString();
            }

            //activeBingoSheet(sheetIndex, true);
        }
        //playScene_network.onGetItemSheet(sheetIndex);
    }

    public void getAttackItem(int attacker, int itemIndex, int sheetIndex, int attacker_sheet)
    {
        //switch (itemIndex)
        //{
        //    case (int)Item.ItemType.Item_FrozenItem:
        //        {
        //            lastUseItemTime = Time.time - 5.0f;
        //            activeItemNoticeEffect(Item.ItemType.Item_FrozenItem);
        //            m_onFrozen = true;
        //            string nickname = "";

        //            for (int i = 0; i < 4; i++)
        //            {
        //                if (nb_GlobalData.g_global.otherSheetInfo[i].userID == attacker)
        //                {
        //                    nickname = nb_GlobalData.g_global.otherSheetInfo[i].nickname;
        //                    break;
        //                }
        //            }
        //            setGameMent(nickname, false, true, false, false);

        //        } break;

        //    case (int)Item.ItemType.Item_Blind_5:
        //        {
        //            activeItemNoticeEffect(Item.ItemType.Item_Blind_5);
        //            string nickname = "";

        //            for (int i = 0; i < 4; i++)
        //            {
        //                if (nb_GlobalData.g_global.otherSheetInfo[i].userID == attacker)
        //                {
        //                    nickname = nb_GlobalData.g_global.otherSheetInfo[i].nickname;
        //                    break;
        //                }
        //            }
        //            setGameMent(nickname, false, true, false, false);
        //        } break;
        //    case (int)Item.ItemType.Item_SwapSheet:
        //        {
        //            string nickname = "";
        //            int temp = 0;
        //            clearSheet(sheetIndex);
        //            activeItemNoticeEffect(Item.ItemType.Item_SwapSheet);
        //            for (int i = 0; i < 4; i++)
        //            {
        //                if (nb_GlobalData.g_global.otherSheetInfo[i].userID == attacker)
        //                {
        //                    for (int k = 0; k < 25; k++)
        //                    {
        //                        temp = nb_GlobalData.g_global.otherSheetInfo[i].sheet[attacker_sheet, k];
        //                        nb_GlobalData.g_global.otherSheetInfo[i].sheet[attacker_sheet, k] = nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, k];
        //                        nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, k] = temp;
        //                    }

        //                    //activeBingoSheet(sheetIndex, false);
        //                    //Vector3 pos = playScene_ui.m_sheetbuttons[sheetIndex].localPosition;
        //                    //activeItemNoticeEffect(Item.ItemType.Item_SwapSheet);
        //                    nickname = nb_GlobalData.g_global.otherSheetInfo[i].nickname;
        //                    break;
        //                }
        //            }

        //            setGameMent(nickname, false, true, false, false);

        //            reloadSheet(sheetIndex);
        //            processMySheet(sheetIndex);
        //            processOtherSheet();
        //        } break;
        //    case (int)Item.ItemType.Item_ShuffleSheet:
        //        {
        //            //activeBingoSheet(sheetIndex, false);
        //            activeItemNoticeEffect(Item.ItemType.Item_ShuffleSheet);
        //            clearSheet(sheetIndex);

        //            for (int i = 0; i < 25; i++)
        //            {
        //                if (m_myLocalSheets[sheetIndex].cells[i].realDaub == true)
        //                {
        //                    continue;
        //                }
        //                int rand = Random.Range(0, 4);
        //                int sum = rand * 5;
        //                sum = sum + (i % 5);

        //                int whileCount = 0;

        //                while (m_myLocalSheets[sheetIndex].cells[sum].realDaub == true)
        //                {
        //                    if (whileCount == 100)
        //                    {
        //                        break;
        //                    }
        //                    whileCount++;
        //                    rand = Random.Range(0, 4);
        //                    sum = rand * 5;
        //                    sum = sum + (i % 5);
        //                }

        //                if (whileCount == 100)
        //                {
        //                    continue;
        //                }

        //                int temp33 = nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, i];
        //                nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, i] = nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, sum];
        //                nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, sum] = temp33;

        //            }

        //            string nickname = "";
        //            for (int i = 0; i < 4; i++)
        //            {
        //                if (nb_GlobalData.g_global.otherSheetInfo[i].userID == attacker)
        //                {
        //                    nickname = nb_GlobalData.g_global.otherSheetInfo[i].nickname;
        //                    break;
        //                }
        //            }

        //            setGameMent(nickname, false, true, false, false);

        //            reloadSheet(sheetIndex);
        //            processMySheet(sheetIndex);
        //            processOtherSheet();

        //        } break;
        //}
        //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mGameMessageIng;
        //nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mGameMessageRequest);
    }

    public void processMySheet(int sheetIndex)
    {
        for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
        {
            int number = nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, cellIndex];

            if (number > 200)
            {
                setDaub(sheetIndex, cellIndex, true, true);

                daubObjects[sheetIndex, cellIndex].gameObject.SetActive(true);
                daubObjects[sheetIndex, cellIndex].GetComponent<UISprite>().spriteName = "ui_daub1";

            }
        }
    }

    public IEnumerator setOtherBingo(int index, int sheetIndex)
    {
        playScene_ui.m_otherBingoSheets[index, sheetIndex].gameObject.SetActive(true);

        GameObject effect = Instantiate(Resources.Load("effects/eff_bingo_button")) as GameObject;

        AudioClip clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.bingo];

        effect.GetComponent<AudioSource>().PlayOneShot(clip);

        string spritename = "gold_bingo";

        if (bingoCount == 1)
        {
            spritename = "gold_bingo";
        }
        else if (bingoCount == 2)
        {
            spritename = "silver_bingo";
        }
        else if (bingoCount == 3)
        {
            spritename = "bronze_bingo";
        }
        playScene_ui.m_otherBingoSheets[index, sheetIndex].GetComponent<UISprite>().spriteName = spritename;
        Vector3 pos = playScene_ui.m_otherBingoSheets[index, sheetIndex].position;
        Vector3 scale = effect.transform.localScale;
        effect.transform.parent = playScene_ui.m_baseBoard;
        effect.transform.position = pos;
        effect.transform.localScale = scale;

        yield return new WaitForSeconds(2.0f);

        if (effect)
        {
            Destroy(effect);
        }

    }

    public void processOtherSheet()
    {
        //foreach (GameObject otherDaub in otherDaubList)
        //{
        //    DestroyImmediate(otherDaub);
        //}
        //otherDaubList.Clear();

        //for (int index = 0; index < 4; ++index)
        //{
        //    int activeSheetCount = nb_GlobalData.g_global.otherSheetInfo[index].activeSheetCount;

        //    for (int sheetIndex = 0; sheetIndex < activeSheetCount; ++sheetIndex)
        //    {
        //        for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
        //        {
        //            if (nb_GlobalData.g_global.otherSheetInfo[index].sheet[sheetIndex, cellIndex] > 200)
        //            {
        //                if (playScene_ui.m_otherSheets[index, sheetIndex, cellIndex].childCount == 0)
        //                {
        //                    GameObject daubObj = Instantiate(Resources.Load("game/other_daub")) as GameObject;
        //                    daubObj.transform.parent = playScene_ui.m_otherSheets[index, sheetIndex, cellIndex];
        //                    daubObj.transform.position = playScene_ui.m_otherSheets[index, sheetIndex, cellIndex].position;
        //                    daubObj.transform.localScale = Vector3.one;

        //                    otherDaubList.Add(daubObj);
        //                }
        //            }
        //        }
        //    }
        //}
    }

    public bool checkItemBingo(int sheetIndex, int cellIndex)
    {
        if (m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex == (int)Item.ItemType.Item_DirectBingo)
        {
            m_bingoType = BingoType.kBingoType_DirectBingoItem;
            return true;
        }
        return false;
    }

    private void clearSheet(int sheetIndex)
    {
        m_reloadSheets[sheetIndex] = true;

        for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
        {
            m_myLocalSheets[sheetIndex].cells[cellIndex].number = 0;
            m_myLocalSheets[sheetIndex].cells[cellIndex].daub = false;
            m_myLocalSheets[sheetIndex].cells[cellIndex].realDaub = false;
            m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex = 0;
        }
    }

    public IEnumerator activeDaubItemClick(int sheetIndex, int daubItemIdex, int index)
    {
        GameObject effect = null;

        switch (daubItemIdex)
        {
            case (int)Item.ItemType.Item_Bomb:
                {
                    effect = Instantiate(Resources.Load("effects/eff_daubItem_bomb")) as GameObject;
                    doubleDaub(sheetIndex, index);
                } break;
            case (int)Item.ItemType.Item_Daub:
                {
                    effect = Instantiate(Resources.Load("effects/eff_daub_click_particle")) as GameObject;
                } break;
            case (int)Item.ItemType.Item_Daub_10:
                {
                    effect = Instantiate(Resources.Load("effects/eff_daubItem_coin")) as GameObject;
                    nb_GlobalData.g_global.myInfo.game_gold += 10;
                } break;
            case (int)Item.ItemType.Item_Gold_30:
                {
                    effect = Instantiate(Resources.Load("effects/eff_daubItem_coins")) as GameObject;
                    nb_GlobalData.g_global.myInfo.game_gold += 10;
                } break;
            case (int)Item.ItemType.Item_Gold_50:
                {
                    effect = Instantiate(Resources.Load("effects/eff_daubItem_coinbox")) as GameObject;
                    nb_GlobalData.g_global.myInfo.game_gold += 30;
                } break;
            case (int)Item.ItemType.Item_Ticket:
                {
                    effect = Instantiate(Resources.Load("effects/eff_daubItem_bingoticket")) as GameObject;
                    nb_GlobalData.g_global.myInfo.game_ticket += 1;
                } break;
        }

        if (effect)
        {
            Vector3 scale = effect.transform.localScale;

            effect.transform.parent = playScene_ui.m_baseBoard;
            effect.transform.position = playScene_ui.m_cells[sheetIndex, index].position;
            effect.transform.localScale = scale;
        }

        yield return new WaitForSeconds(1.5f);

        if (effect)
        {
            Destroy(effect);
        }
    }

    private IEnumerator activeDaubItemCreate(Vector3 pos)
    {
        GameObject daubEffect = Instantiate(Resources.Load("effects/eff_daub_item_create")) as GameObject;
        Vector3 scale = daubEffect.transform.localPosition;

        daubEffect.transform.parent = playScene_ui.m_baseBoard;
        daubEffect.transform.position = pos;
        daubEffect.transform.localScale = scale;

        yield return new WaitForSeconds(1.0f);

        if (daubEffect)
        {
            Destroy(daubEffect);
        }
    }

    private IEnumerator activeDaubItemFocus(Vector3 pos)
    {
        GameObject daubEffect = Instantiate(Resources.Load("effects/eff_daub_item_focus")) as GameObject;
        Vector3 scale = daubEffect.transform.localPosition;

        daubEffect.transform.parent = playScene_ui.m_baseBoard;
        daubEffect.transform.position = pos;
        daubEffect.transform.localScale = scale;

        yield return new WaitForSeconds(1.0f);

        if (daubEffect)
        {
            Destroy(daubEffect);
        }
    }

    public IEnumerator acvtiveDamageNotice(Item.ItemType attackType, float deleteTime)
    {
        GameObject effect = null;
        switch (attackType)
        {
            case Item.ItemType.Item_FrozenItem:
                {
                    effect = Instantiate(Resources.Load("effects/eff_notice_freeze")) as GameObject;

                    //for (int i = 0; i < 25; ++i)
                    //{
                    //    playScene_ui.m_cells[i].GetComponent<BoxCollider>().enabled = false;

                    //}
                    //for (int i = 0; i < 4; i++)
                    //{
                    //    playScene_ui.m_sheetbuttons[i].GetComponent<BoxCollider>().enabled = false;
                    //}
                    for (int s = 0; s < 4; s++)
                    {
                        //playScene_ui.m_sheetbuttons[i].GetComponent<BoxCollider>().enabled = false;
                        for (int i = 0; i < 25; ++i)
                        {
                            playScene_ui.m_cells[s, i].GetComponent<BoxCollider>().enabled = false;

                        }

                        GameObject.Find("bingo_btn_" + s).GetComponent<BoxCollider>().enabled = false;
                    }


                } break;
            case Item.ItemType.Item_Blind_5:
                {
                    effect = Instantiate(Resources.Load("effects/eff_notice_blind")) as GameObject;
                } break;
            case Item.ItemType.Item_SwapSheet:
                {
                    effect = Instantiate(Resources.Load("effects/eff_notice_swap")) as GameObject;

                } break;
            case Item.ItemType.Item_ShuffleSheet:
                {
                    effect = Instantiate(Resources.Load("effects/eff_notice_numbermix")) as GameObject;
                } break;
            case Item.ItemType.Item_Shield:
                {
                    effect = Instantiate(Resources.Load("effects/eff_notice_shield")) as GameObject;
                } break;
        }

        Vector3 pos = effect.transform.localPosition;
        Vector3 scale = effect.transform.localScale;
        effect.transform.parent = playScene_ui.m_damageBoard;
        effect.transform.localPosition = pos;
        effect.transform.localScale = scale;

        yield return new WaitForSeconds(deleteTime);

        if (effect)
        {
            Destroy(effect);
        }
        if (attackType == Item.ItemType.Item_FrozenItem)
        {
            m_onFrozen = false;

            //for (int i = 0; i < 25; ++i)
            //{
            //    playScene_ui.m_cells[i].GetComponent<BoxCollider>().enabled = true;
            //}
            //for (int i = 0; i < 4; i++)
            //{
            //    playScene_ui.m_sheetbuttons[i].GetComponent<BoxCollider>().enabled = true;
            //}
            for (int s = 0; s < 4; s++)
            {
                //playScene_ui.m_sheetbuttons[i].GetComponent<BoxCollider>().enabled = true;
                for (int i = 0; i < 25; ++i)
                {
                    playScene_ui.m_cells[s, i].GetComponent<BoxCollider>().enabled = true;
                }
                GameObject.Find("bingo_btn_" + s).GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    private IEnumerator activeShield()
    {
        //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mGameMessageIng;
        //nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mGameMessageRequest);

        StartCoroutine(acvtiveDamageNotice(Item.ItemType.Item_Shield, 2.5f));

        yield return new WaitForSeconds(1.0f);

        shield_start = Instantiate(Resources.Load("effects/eff_shield_start")) as GameObject;

        Vector3 pos = shield_start.transform.localPosition;
        Vector3 scale = shield_start.transform.localScale;

        shield_start.transform.parent = playScene_ui.m_effectLayer02Board;
        shield_start.transform.localPosition = pos;
        shield_start.transform.localScale = scale;

        yield return new WaitForSeconds(shield_start.GetComponent<AudioSource>().clip.length);

        Destroy(shield_start);

        m_shield_idle = Instantiate(Resources.Load("effects/eff_shield_idle")) as GameObject;
        pos = shield_start.transform.localPosition;
        scale = m_shield_idle.transform.localScale;

        m_shield_idle.transform.parent = playScene_ui.m_effectLayer02Board;
        m_shield_idle.transform.localPosition = pos;
        m_shield_idle.transform.localScale = scale;

    }

    private IEnumerator unactiveShield()
    {
        nb_GlobalData.g_global.myShield = 0;

        if (m_shield_idle != null)
        {
            DestroyImmediate(m_shield_idle.gameObject);
            m_shield_idle = null;
        }

        GameObject shield_end = Instantiate(Resources.Load("effects/eff_shield_end")) as GameObject;
        Vector3 pos = shield_end.transform.localPosition;
        Vector3 scale = shield_end.transform.localScale;
        shield_end.transform.parent = playScene_ui.m_effectLayer02Board;
        shield_end.transform.localPosition = pos;
        shield_end.transform.localScale = scale;

        yield return new WaitForSeconds(0.5f);

        Destroy(shield_end.gameObject);
    }

    private IEnumerator activeAttackEffect(Item.ItemType itemtype, Vector3 pos)
    {
        GameObject effect = null;

        // sound
        if (itemtype == Item.ItemType.Item_FrozenItem)
        {
            effect = Instantiate(Resources.Load("effects/eff_attack_freeze")) as GameObject;
            Vector3 localscale = effect.transform.localScale;

            effect.transform.parent = playScene_ui.m_baseBoard;
            effect.transform.localPosition = pos;
            effect.transform.localScale = localscale;


            //playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.Attack_Frozen];
            //playScene_ui.m_talk_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_freeze];

        }
        else if (itemtype == Item.ItemType.Item_ShuffleSheet)
        {

            effect = Instantiate(Resources.Load("effects/eff_attack_numbermix")) as GameObject;
            Vector3 localscale = effect.transform.localScale;

            effect.transform.parent = playScene_ui.m_baseBoard;
            effect.transform.localPosition = pos;
            effect.transform.localScale = localscale;

            //playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.Attack_Shuffle];
            //playScene_ui.m_talk_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_numbermix];

        }
        else if (itemtype == Item.ItemType.Item_Blind_5)
        {
            effect = Instantiate(Resources.Load("effects/eff_attack_blind")) as GameObject;
            Vector3 localscale = effect.transform.localScale;

            effect.transform.parent = playScene_ui.m_baseBoard;
            effect.transform.localPosition = pos;
            effect.transform.localScale = localscale;

            //playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.Attack_Blind];
            //playScene_ui.m_talk_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_blind];

        }
        else if (itemtype == Item.ItemType.Item_SwapSheet)
        {
            effect = Instantiate(Resources.Load("effects/eff_attck_swap")) as GameObject;
            Vector3 localscale = effect.transform.localScale;

            effect.transform.parent = playScene_ui.m_baseBoard;
            effect.transform.localPosition = pos;
            effect.transform.localScale = localscale;

            //playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.Attack_Swap];
            //playScene_ui.m_talk_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_swap];

        }

        //playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Play();
        //playScene_ui.m_talk_attackEffect.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(2.0f);

        if (itemtype == Item.ItemType.Item_SwapSheet)
        {
            //StartCoroutine(activeSwapCellEffect(m_currentSheetIndex));
        }
        else if (itemtype == Item.ItemType.Item_ShuffleSheet)
        {
            // StartCoroutine( activeShuffleCellEffect()  );
        }

        //playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Stop();

        if (effect)
        {
            Destroy(effect.gameObject);
        }
    }

    private IEnumerator activeBlindEffect()
    {
        m_onBlind = true;

        foreach (GameObject ball in m_existBallList)
        {
            GameObject effect = Instantiate(Resources.Load("effects/ball_blind_effect")) as GameObject;

            Vector3 pos = effect.transform.localPosition;
            Vector3 scale = effect.transform.localScale;

            effect.transform.parent = ball.transform;
            effect.transform.localPosition = pos;
            effect.transform.localScale = scale;


            m_blindList.Add(effect);

            // ball.transform.localScale = Vector3.one * 0.01f;
        }

        // sound
        //playScene_ui.m_sound_damageEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.Damage_Blind];
        //playScene_ui.m_sound_damageEffect.GetComponent<AudioSource>().Play();
        //playScene_ui.m_sound_damageEffect.GetComponent<AudioSource>().loop = true;

        yield return new WaitForSeconds(11.0f);
        //playScene_ui.m_sound_damageEffect.GetComponent<AudioSource>().loop = false;
        //playScene_ui.m_sound_damageEffect.GetComponent<AudioSource>().Stop();

        if (m_onBlind == true)
        {
            m_onBlind = false;
        }

        foreach (GameObject blind in m_blindList)
        {
            Destroy(blind.gameObject);
        }
        m_blindList.Clear();

        foreach (GameObject ball in m_existBallList)
        {
            ball.transform.localScale = Vector3.one;
        }
    }

    private IEnumerator activeBingoEffect(int sheetIndex)
    {
        //GameObject effect = Instantiate(Resources.Load("effects/eff_bingo_finish")) as GameObject;
        //AudioClip clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.bingo];

        //effect.GetComponent<AudioSource>().PlayOneShot(clip);

        //Vector3 scale = effect.transform.localScale;
        //Vector3 pos = effect.transform.localPosition;

        //effect.transform.parent = playScene_ui.m_baseBoard;
        //effect.transform.localPosition = pos;
        //effect.transform.localScale = scale;

        //
        //visibleScoreBingo(sheetIndex, playScene_ui.m_bingoButton[sheetIndex].position);

        StartCoroutine(activeBingoMessageEffect());

        yield return new WaitForSeconds(2.0f);

        //if (effect)
        //{
        //    Destroy(effect.gameObject);
        //}

        //
    }

    private IEnumerator activeBingoMessageEffect()
    {

        // bingo medal
        GameObject effect = Instantiate(Resources.Load("effects/eff_bingo_medal_gold")) as GameObject;
        Vector3 scale = effect.transform.localScale;
        Vector3 pos = effect.transform.localPosition;

        if (bingoCount == 0)
        {
            Sprite sprite = Resources.Load("images/gold_mark", typeof(Sprite)) as Sprite;
            effect.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        }
        else if (bingoCount == 1)
        {
            Sprite sprite = Resources.Load("images/silver_mark", typeof(Sprite)) as Sprite;
            effect.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        }

        effect.transform.parent = playScene_ui.m_baseBoard;
        effect.transform.localPosition = pos;
        effect.transform.localScale = scale;

        yield return new WaitForSeconds(1.5f);

        if (effect)
            Destroy(effect);
    }

    public IEnumerator activeBadBingoEffect(int sheetIndex)
    {
        //GameObject effect = Instantiate(Resources.Load("effects/eff_badbingo")) as GameObject;
        //Vector3 scale = effect.transform.localScale;
        //Vector3 pos = effect.transform.localPosition;

        //effect.transform.parent = playScene_ui.m_baseBoard;
        //effect.transform.localPosition = pos;
        //effect.transform.localScale = scale;

        playScene_ui.m_bingoButton[sheetIndex].GetComponent<BoxCollider>().enabled = false;

        //playScene_ui.m_talk_bingoEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.bad_bingo];
        //playScene_ui.m_talk_bingoEffect.GetComponent<AudioSource>().Play();


        playScene_ui.m_badBingoSheetBG[sheetIndex].gameObject.SetActive(true);
        setActiveNumberButtons(false, sheetIndex);
        m_badBingo[sheetIndex] = true;

        yield return new WaitForSeconds(5f);
        if (bingoflag == false)
        {
            playScene_ui.m_bingoButton[sheetIndex].GetComponent<BoxCollider>().enabled = true;
        }
        //if (effect)
        //{
        //    Destroy(effect.gameObject);
        //}
    }

    private IEnumerator activeSwapCellEffect(int sheetIndex)
    {
        setActiveSheetButton(false);

        for (int i = 0; i < 25; ++i)
        {
            playScene_ui.m_cells[sheetIndex, i].localScale = Vector3.one * 0.01f;
            iTween.ScaleTo(playScene_ui.m_cells[sheetIndex, i].gameObject, iTween.Hash("x", 1f,
                                                                            "y", 1f,
                //"easetype", "easeOutBack",
                                                                            "time", 0.25f + 0.1f * i));
        }
        yield return new WaitForSeconds(1.0f);
        setActiveSheetButton(true);
    }

    private void setActiveSheetButton(bool active)
    {

        //for (int i = 0; i < playScene_ui.m_sheetbuttons.Length; ++i)
        //{
        //    playScene_ui.m_sheetbuttons[i].GetComponent<BoxCollider>().enabled = active;
        //}
    }

    private void activeItemNoticeEffect(Item.ItemType itemIndex)
    {
        switch (itemIndex)
        {
            case Item.ItemType.Item_Blind_5:
                {
                    StartCoroutine(acvtiveDamageNotice(Item.ItemType.Item_Blind_5, 11f));  //123
                    StartCoroutine(activeBlindEffect());

                } break;
            case Item.ItemType.Item_FrozenItem:
                {
                    StartCoroutine(acvtiveDamageNotice(Item.ItemType.Item_FrozenItem, 11.5f));
                    // sound
                    //playScene_ui.m_sound_damageEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.Damage_Frozen];
                    //playScene_ui.m_sound_damageEffect.GetComponent<AudioSource>().Play();
                    //StartCoroutine(activeDamageEffect());
                    //StartCoroutine(activeFrozenEffect());

                } break;
            case Item.ItemType.Item_SwapSheet:
                {
                    StartCoroutine(acvtiveDamageNotice(Item.ItemType.Item_SwapSheet, 3.5f));

                } break;
            case Item.ItemType.Item_ShuffleSheet:
                {

                    StartCoroutine(acvtiveDamageNotice(Item.ItemType.Item_ShuffleSheet, 3.5f));


                } break;
            // case Item.ItemType.Item_Shield:
            //     {
            //         StartCoroutine(acvtiveDamageNotice(Item.ItemType.Item_Shield, 2.5f));
            //    } break;
            default:
                break;
        }
    }

    private void setTargetUI(bool active, bool needSheet)
    {
        //if (active == false)
        //{
        //    for (int otherIndex = 0; otherIndex < 4; ++otherIndex)
        //    {
        //        playScene_ui.m_targets[otherIndex].gameObject.SetActive(false);

        //        for (int sheetIndex = 0; sheetIndex < 4; ++sheetIndex)
        //        {
        //            playScene_ui.m_targetSheets[otherIndex, sheetIndex].gameObject.SetActive(false);
        //        }
        //    }
        //}
        //else
        //{
        //    for (int otherIndex = 0; otherIndex < 4; ++otherIndex)
        //    {
        //        int activeSheetCount = nb_GlobalData.g_global.otherSheetInfo[otherIndex].activeSheetCount;

        //        if (needSheet == true)
        //        {
        //            for (int sheetIndex = 0; sheetIndex < activeSheetCount; ++sheetIndex)
        //            {
        //                if (nb_GlobalData.g_global.otherSheetInfo[otherIndex].bingoSheet[sheetIndex] == false)
        //                {
        //                    playScene_ui.m_targetSheets[otherIndex, sheetIndex].gameObject.SetActive(true);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 0; i < 4; i++)
        //            {
        //                if (nb_GlobalData.g_global.otherSheetInfo[i].activeSheetCount == 0)
        //                {
        //                    playScene_ui.m_targets[i].gameObject.SetActive(false);
        //                }
        //                else
        //                {
        //                    playScene_ui.m_targets[i].gameObject.SetActive(true);
        //                }
        //            }
        //        }
        //    }

        //}
    }

    private void setDaub(int sheetIndex, int cellIndex, bool val, bool correct)
    {
        m_myLocalSheets[sheetIndex].cells[cellIndex].daub = val;
        m_myLocalSheets[sheetIndex].cells[cellIndex].realDaub = correct;

        if (m_myLocalSheets[sheetIndex].cells[cellIndex].number != -2)
        {
            int daubItemIndex = m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex;

            if (daubItemIndex != 0)
            {
                //if (sheetIndex == m_currentSheetIndex)
                {
                    StartCoroutine(activeDaubItemClick(sheetIndex, daubItemIndex, cellIndex));
                    setItemDaubUI(sheetIndex, cellIndex, -1);
                }

                if (daubItemIndex == 4 || daubItemIndex == 5)
                {
                    //playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_getcoin];
                    //playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().Play();
                }
                else if (daubItemIndex == 2)
                {
                    //playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_sidedaub];
                    //playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().Play();
                }
                else if (daubItemIndex == 6)
                {
                    m_bingoType = BingoType.kBingoType_DirectBingoItem;
                }
                m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex = 0;
            }
        }
        //1414
        m_myLocalSheets[sheetIndex].cells[cellIndex].number = 255;
        nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, cellIndex] = 255;

        //if (sheetIndex == m_currentSheetIndex)
        {
            if (correct)
            {
                daubObjects[sheetIndex, cellIndex].SetActive(true);
                daubObjects[sheetIndex, cellIndex].GetComponent<UISprite>().spriteName = "ui_daub1";

                playScene_ui.m_cells[sheetIndex, cellIndex].GetComponent<UILabel>().text = "";
            }
        }
    }

    private void activeItem(int item_type)
    {
        //if (item_type == 0 || item_type > 12)
        //{
        //    item_type = 3;
        //}
        ////playScene_ui.m_item.gameObject.SetActive(true);
        ////playScene_ui.m_item.GetComponent<UISprite>().spriteName = Item.itemImagePath[item_type];
    }

    public void resetGauge()
    {
        m_sendUseItem = false;
        lastUseItemTime = Time.time;
        m_itemGaugeCount = 0;

        playScene_ui.m_itemGauge.GetComponent<UISprite>().fillAmount = 0.0f;
        //playScene_ui.m_itemGauge[0].gameObject.SetActive(true);
        //playScene_ui.m_itemGauge[1].gameObject.SetActive(false);
        //playScene_ui.m_itemGauge[2].gameObject.SetActive(false);
        //playScene_ui.m_item.gameObject.SetActive(false);
        StartCoroutine(activeItemReady());
    }

    private void resetDaubObjects()
    {
        for (int s = 0; s < 4; ++s)
        {
            for (int i = 0; i < 25; ++i)
            {
                daubObjects[s, i].SetActive(false);
            }
        }
    }

    private int getDaubNumber(int sheet, int index)
    {
        return m_myLocalSheets[sheet].cells[index].number;
    }

    private void initBingoSheet()
    {
        int activeSheetCount = nb_GlobalData.g_global.sheetInfo.activeSheetCount;

        m_myLocalSheets = new nb_BingoSheet[activeSheetCount];

        // local Sheet init;
        for (int sheetIndex = 0; sheetIndex < activeSheetCount; ++sheetIndex)
        {
            m_myLocalSheets[sheetIndex].cells = new nb_Cell[25];
            //playScene_ui.m_sheetbuttons[sheetIndex].GetComponent<UIImageButton>().isEnabled = true;

            for (int index = 0; index < 25; ++index)
            {
                int number = nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, index];
                m_myLocalSheets[sheetIndex].cells[index].number = number;
                m_myLocalSheets[sheetIndex].cells[index].daub = false;
                m_myLocalSheets[sheetIndex].cells[index].realDaub = false;
                m_myLocalSheets[sheetIndex].cells[index].itemEffectIndex = 0;
            }
        }

        // daub object setting
        for (int sheetIndex = 0; sheetIndex < activeSheetCount; ++sheetIndex)
        {
            for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
            {
                int number = nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, cellIndex];
                playScene_ui.m_cells[sheetIndex, cellIndex].GetComponent<UILabel>().text = number.ToString();

                daubObjects[sheetIndex, cellIndex] = Instantiate(Resources.Load("game/nb_daub")) as GameObject;
                //daubObjects[sheetIndex, cellIndex].transform.parent = playScene_ui.m_daubBoard;
                daubObjects[sheetIndex, cellIndex].transform.parent = playScene_ui.m_cells[sheetIndex, cellIndex];
                daubObjects[sheetIndex, cellIndex].transform.position = playScene_ui.m_cells[sheetIndex, cellIndex].position;
                daubObjects[sheetIndex, cellIndex].transform.localScale = Vector3.one;
                daubObjects[sheetIndex, cellIndex].SetActive(false);

                if (sheetIndex < activeSheetCount)
                {
                    if (number == 0 || number > 200)    //가운데는 정답뚫려있음
                    {
                        //Debug.Log("@@ center check sheet index " + sheetIndex.ToString()
                        //    + " : " + number.ToString());

                        m_myLocalSheets[sheetIndex].cells[cellIndex].daub = true;
                        m_myLocalSheets[sheetIndex].cells[cellIndex].realDaub = true;

                        m_myLocalSheets[sheetIndex].cells[cellIndex].number = 0;
                        //nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, cellIndex] = 255;
                        //setDaub(sheetIndex, cellIndex, true, true);

                        daubObjects[sheetIndex, cellIndex].gameObject.SetActive(true);
                        daubObjects[sheetIndex, cellIndex].GetComponent<UISprite>().spriteName = "ui_card_center";
                        daubObjects[sheetIndex, cellIndex].GetComponentInChildren<UILabel>().text = "";
                    }
                }
            }
        }

        for (int i = activeSheetCount; i < 4; ++i)
        {
            playScene_ui.m_bingoBoard.Find("sheet_" + i.ToString()).gameObject.SetActive(false);
        }

        // other name setting
        //for (int otherIndex = 0; otherIndex < 4; ++otherIndex)
        //{
        //    playScene_ui.m_othersName[otherIndex].GetComponent<UILabel>().text = nb_GlobalData.g_global.otherSheetInfo[otherIndex].nickname;
        //}

        // 참가한 유저 리스트
        //int userCount = nb_GlobalData.g_global.BingoRoomUserCount;
        //m_playerList = new GameObject[userCount];

        //UIGrid grid = playScene_ui.m_userList.Find("scrollview/Grid").GetComponent<UIGrid>();
        //UIScrollView sv = playScene_ui.m_userList.Find("scrollview").GetComponent<UIScrollView>();

        //for (int i = 0; i < userCount; ++i)
        //{
        //    m_playerList[i] = Instantiate(Resources.Load("game/nb_playerForm")) as GameObject;

        //    Vector3 pos = new Vector3(0, (-50 * i) + 100);
        //    Vector3 scale = m_playerList[i].transform.localScale;
        //    m_playerList[i].transform.parent = grid.transform;
        //    m_playerList[i].transform.localPosition = pos;
        //    m_playerList[i].transform.localScale = scale;

        //    m_playerList[i].GetComponent<UIDragScrollView>().scrollView = sv;

        //    m_playerList[i].transform.Find("t_username").GetComponent<UILabel>().text =
        //        nb_GlobalData.g_global.BingoRoomUserNameList[i];
        //}

        //grid.Reposition();
        //sv.ResetPosition();
        //grid.repositionNow = true;

        ////순위 초기화
        //nb_GlobalData.g_global.BingoRankingUserName1 = "-";
        //nb_GlobalData.g_global.BingoRankingUserName2 = "-";
        //nb_GlobalData.g_global.BingoRankingUserName3 = "-";
        //nb_GlobalData.g_global.BingoRankingUserNameX = "-";

        //nb_GlobalData.g_global.BingoTotalFinishCount = 0;

        //playScene_ui.m_rankBoard.Find("player_ranking_1/t_player_name").GetComponent<UILabel>().text =
        //    nb_GlobalData.g_global.BingoRankingUserName1;
        //playScene_ui.m_rankBoard.Find("player_ranking_2/t_player_name").GetComponent<UILabel>().text =
        //    nb_GlobalData.g_global.BingoRankingUserName2;
        //playScene_ui.m_rankBoard.Find("player_ranking_3/t_player_name").GetComponent<UILabel>().text =
        //    nb_GlobalData.g_global.BingoRankingUserName3;
        //playScene_ui.m_rankBoard.Find("player_ranking_x/t_player_name").GetComponent<UILabel>().text =
        //    nb_GlobalData.g_global.BingoRankingUserNameX;

        //playScene_ui.m_rankBoard.Find("player_ranking_x/t_rank_text").GetComponent<UILabel>().text = "4th";
    }

    public IEnumerator addBall(int number)
    {
        //Debug.Log("@@ run addBall : " + number.ToString());

        if (ballCount >= 75)
        {
            //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mBingoResultIng;
            //nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mBingoResultRequest);

            //

            yield return false;
        }
        else
        {
            //지금까지 나온 답 리스트
            if (number != 0)
            {
                playScene_ui.m_daubBoardNumber[number - 1].GetComponent<UILabel>().color = Color.white;
                playScene_ui.m_daubBoardNumber[number - 1].GetComponent<UISprite>().enabled = true;
            }

            GameObject ball;
            string ball_ResourcePath = "";
            if (number > 0 && number <= 15)
            {
                ball_ResourcePath = "ball/nb_ball_B";
            }
            else if (number > 15 && number <= 30)
            {
                ball_ResourcePath = "ball/nb_ball_I";
            }
            else if (number > 30 && number <= 45)
            {
                ball_ResourcePath = "ball/nb_ball_N";
            }
            else if (number > 45 && number <= 60)
            {
                ball_ResourcePath = "ball/nb_ball_G";
            }
            else if (number > 60 && number <= 75)
            {
                ball_ResourcePath = "ball/nb_ball_O";
            }
            else
            {
                Debug.Log("addball fail number : " + number.ToString());
            }

            if (number > 0 && number <= 75)
            {
                //번호별 사운드 재생
                playScene_ui.m_effectLayer02Board.Find(
                    "sound_num_" + number.ToString()).GetComponent<AudioSource>().Play();
            }

            ball = Instantiate(Resources.Load(ball_ResourcePath)) as GameObject;
            ball.transform.GetComponentInChildren<UILabel>().text = number.ToString();
            ball.transform.parent = playScene_ui.m_baseBoard;
            ball.transform.position = playScene_ui.m_ballStart_pos.position;
            ball.transform.localScale = Vector3.one * 0.01f;

            ball.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.BallSound[number - 1];

            if (!m_onBlind)
            {
                ball.GetComponent<AudioSource>().Play();
            }


            iTween.ScaleTo(ball, Vector3.one, 0.4f);


            if (m_onBlind)
            {
                GameObject effect = Instantiate(Resources.Load("effects/ball_blind_effect")) as GameObject;

                Vector3 pos = effect.transform.localPosition;
                Vector3 scale = effect.transform.localScale;

                effect.transform.parent = ball.transform;
                effect.transform.localPosition = pos;
                effect.transform.localScale = scale;

                m_blindList.Add(effect);

                ball.transform.localScale = Vector3.one * 0.01f;
            }

            m_calledBallNumber[number] = true;

            ballCount++;

            //
            //int existBallCount = m_existBallList.Count;

            foreach (GameObject existball in m_existBallList)
            {
                iTween.MoveBy(existball, Vector3.right * 0.2f, 0.5f);
            }

            m_existBallList.Add(ball);

            if (m_existBallList.Count > 6)
            {
                GameObject temp = m_existBallList[0];
                if (temp)
                {
                    m_existBallList.RemoveAt(0);
                    Destroy(temp);
                }
            }


           // yield return new WaitForSeconds(ballTime);

            //int count = nb_GlobalData.g_global.BingoNumberCount;
            //StartCoroutine("addBall", nb_GlobalData.g_global.bingoball[count]);
            if (ballCount < 75)
            {
                //StartCoroutine("addBall", nb_GlobalData.g_global.bingoball[ballCount]);
            }
            //
        }
    }

    private void setActiveNumberButtons(bool active, int sheet)
    {
        //for (int s = 0; s < 4; ++s)
        {
            for (int i = 0; i < 25; ++i)
            {
                playScene_ui.m_cells[sheet, i].GetComponent<BoxCollider>().enabled = active;
            }
        }
    }

    public void doubleDaub(int sheetIndex, int dbCellIndex)
    {

        if (dbCellIndex == 0 || dbCellIndex == 5 || dbCellIndex == 10 || dbCellIndex == 15 || dbCellIndex == 20)
        {
            if (m_myLocalSheets[sheetIndex].cells[dbCellIndex + 1].realDaub == false)
            {
                processDaub_D(sheetIndex, dbCellIndex + 1);
            }
        }
        else if (dbCellIndex == 4 || dbCellIndex == 9 || dbCellIndex == 14 || dbCellIndex == 19 || dbCellIndex == 24)
        {
            if (m_myLocalSheets[sheetIndex].cells[dbCellIndex - 1].realDaub == false)
            {
                processDaub_D(sheetIndex, dbCellIndex - 1);
            }
        }
        else
        {
            if (m_myLocalSheets[sheetIndex].cells[dbCellIndex - 1].realDaub == false)
            {
                processDaub_D(sheetIndex, dbCellIndex - 1);
            }
            if (m_myLocalSheets[sheetIndex].cells[dbCellIndex + 1].realDaub == false)
            {
                processDaub_D(sheetIndex, dbCellIndex + 1);
            }
        }
    }

    public void processDaub_D(int sheetIndex, int cellIndex)
    {
        m_myLocalSheets[sheetIndex].cells[cellIndex].daub = true;

        daubObjects[sheetIndex, cellIndex].SetActive(true);

        nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, cellIndex] = 255;

        m_myLocalSheets[sheetIndex].cells[cellIndex].realDaub = true;
        playScene_ui.m_cells[sheetIndex, cellIndex].GetComponent<UILabel>().text = "";
        daubObjects[sheetIndex, cellIndex].GetComponent<UISprite>().spriteName = "ui_daub1";
        //playScene_ui.m_sound_daubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.daub];
        //playScene_ui.m_sound_daubEffect.GetComponent<AudioSource>().Play();
        int tempitem = m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex;


        //visibleScore(playScene_ui.m_cells[sheetIndex, cellIndex].position, sheetIndex, cellIndex);

        if (m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex > 0)
        {
            if (m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex != (int)Item.ItemType.Item_DirectBingo)
            {
                int itemIndex = m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex;
                m_myLocalSheets[sheetIndex].cells[cellIndex].itemEffectIndex = 0;
                StartCoroutine(activeDaubItemClick(sheetIndex, itemIndex, cellIndex));

                setItemDaubUI(sheetIndex, cellIndex, -1);

            }
        }

        //if (tempitem == 4)
        //{
        //    playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_getcoin];
        //    playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().Play();

        //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.itemeffect_coin];
        //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Play();

        //}
        //else if (tempitem == 5)
        //{
        //    playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_getcoin];
        //    playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().Play();

        //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.itemeffect_coin];
        //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Play();
        //}
        //else if (tempitem == 2)
        //{
        //    playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.TalkSound[(int)Sound.TalkList.item_sidedaub];
        //    playScene_ui.m_talk_daubEffect.GetComponent<AudioSource>().Play();

        //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.itemeffect_bomb];
        //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Play();
        //}

        //else if (tempitem == 7)
        //{
        //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().clip = nb_GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.itemeffect_ticket];
        //    playScene_ui.m_sound_attackEffect.GetComponent<AudioSource>().Play();
        //}


        //if (checkItemBingo(sheetIndex, cellIndex) == true)
        //{
        //    nb_GlobalData.g_global.sheetInfo.bingoSheet[sheetIndex] = true;
        //    /*
        //    if (myrankFlag)
        //    {
        //        nb_GlobalData.g_global.m_winnerList[rankInfo].nickname = nb_GlobalData.g_global.myInfo.nickName;
        //        nb_GlobalData.g_global.m_winnerList[rankInfo].monster_id = nb_GlobalData.g_global.sheetInfo.monsterId;
        //        nb_GlobalData.g_global.m_winnerList[rankInfo].rank = rankInfo+1;

        //        nb_GlobalData.g_global.myrank = rankInfo+1;
        //        rankInfo++;
        //        myrankFlag = false;
        //    }
        //    */
        //    StartCoroutine(activeBingoEffect(sheetIndex));
        //    //nb_GlobalData.g_global.bingoCount = System.Math.Max(0, nb_GlobalData.g_global.bingoCount--);
        //    //nb_GlobalData.g_global.callBingo = true;
        //    setBingo(sheetIndex);
        //    //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mGameMessageRequest;
        //    StartCoroutine(bingoResult());

        //}

        StartCoroutine(activeDaubEffect(playScene_ui.m_cells[sheetIndex, cellIndex]));
        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.GameMessageRequest);
        nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.GameMessageRequest_End;   // onsendDaub

    }

    private bool checkDaubNumber(int number)
    {
        //string dd = "";
        //foreach (var pair in nb_GlobalData.g_global.bingoball)
        //{
        //    dd += pair.ToString() + ",";
        //}
        //Debug.Log("DAUB : " + dd);

        int count = 1;
        foreach (var pair in nb_GlobalData.g_global.bingoball)
        {
            if (count > ballCount)
            {
                return false;   //서버에서 이미 받긴했지만 안보여줬으니 오답처리
            }

            if (pair == number)
            {
                return true;
            }

            ++count;
        }

        return false;
    }

    private void runGameOver()
    {
        //playScene_ui.popup_gameEnd.gameObject.SetActive(true);
        //Transform textObj = playScene_ui.popup_gameEnd.Find("Label");

        //bool noRank = true;
        //string resultText = "Your ranking : ";
        //for(int i = 0; i < 4; ++i)
        //{
        //    int rank = nb_GlobalData.g_global.BingoMyBlitzRanking[i];
        //    if(rank != 0)
        //    {
        //        noRank = false;

        //        if(rank == 1)
        //        {
        //            resultText += "1st";
        //        }
        //        else if(rank ==2)
        //        {
        //            resultText += "2nd";
        //        }
        //        else if(rank == 3)
        //        {
        //            resultText += "3rd";
        //        }
        //        else
        //        {
        //            resultText += rank.ToString() + "th";
        //        }
        //        resultText += " ";
        //    }
        //}

        //if (noRank)
        //{
        //    resultText += "norank";
        //}
        //textObj.GetComponent<UILabel>().text = resultText;

        //Debug.Log("runGameOver() : " + resultText);

        playScene_ui.effect_end.gameObject.SetActive(true);

        playScene_ui.m_effectLayer02Board.Find(
            "sound_roundover").GetComponent<AudioSource>().Play();

        StartCoroutine("ShowResultSpine");
    }

    private IEnumerator ShowResultSpine()
    {
        nb_GlobalData.g_global_bgm3.Stop();
        nb_GlobalData.g_global_bgm4.Play();

        setRoundOverUiDisable();

        yield return new WaitForSeconds(1.4f);

        playScene_ui.effect_end.gameObject.SetActive(false);
        playScene_ui.popup_gameEnd.gameObject.SetActive(true);

        StartCoroutine("EndResultSpine");
    }

    private IEnumerator EndResultSpine()
    {
        yield return new WaitForSeconds(18.0f);

        //메인메뉴로 보내버려

        fadeOut = true;
        fadeOutTimer = Time.time;
    }

    void setRoundOverUiDisable()
    {
        //게임끝날때 ui 비활성화

        //메뉴버튼
        playScene_ui.playUI.Find("menu_btn").GetComponent<BoxCollider>().enabled = false;
        
        //시트
        for (int i = 0; i < 4; ++i)
        {
            playScene_ui.m_bingoBoard.Find("sheet_" + i.ToString()).gameObject.SetActive(false);
        }

        //빙고,배드빙고ui
        playScene_ui.m_effectLayer01Board.gameObject.SetActive(false);
    }

    private void completeScaling()
    {
        iTween.ScaleTo(playScene_ui.label_bingoCount.gameObject, Vector3.one, 0.3f);
    }

    private void fadeOutEnd()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();

        Application.LoadLevel("nb_battleLobbyScene");
    }

    void OnGUI()
    {
        if (tex)
        {
            Color color = Color.black;
            color.a = alpha;
            GUI.color = color;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);
        }

    }

    public void setCellItem(int sheetIndex, int cellIndex, nb_Item.nb_ItemType type)
    {
        if (nb_Item.nb_daubItemImagePath[(int)type] == "item_unknown")
        {
            daubObjects[sheetIndex, cellIndex].gameObject.SetActive(false);
            return;
        }

        daubObjects[sheetIndex, cellIndex].gameObject.SetActive(true);
        daubObjects[sheetIndex, cellIndex].GetComponent<UISprite>().spriteName =
            nb_Item.nb_daubItemImagePath[(int)type];
    }

    public void activeCellItem(int sheetIndex, int cellIndex)
    {
        //nb_GlobalData.g_global.sheetInfo.sheet[sheetIndex, cellIndex]
        //m_myLocalSheets[sheetIndex].cells[index].itemEffectIndex;
    }

    private void runUseItemAction()
    {
        foreach(var data in nb_GlobalData.g_global.useItemDataList)
        {
            int type = nb_GlobalData.g_global.getBattleItemIndex(data.infoId);
            int cellIndex = findCellIndex(data.sheet, data.number);

            //if (type == 1 || type == 4 || type == 10)
            //{
            //    //daub action
            //    m_myLocalSheets[data.sheet].cells[cellIndex].realDaub = true;
            //    m_myLocalSheets[data.sheet].cells[cellIndex].daub = true;
            //    daubObjects[data.sheet, cellIndex].SetActive(true);
            //    daubObjects[data.sheet, cellIndex].GetComponent<UISprite>().spriteName = "ui_daub1";
            //    daubObjects[data.sheet, cellIndex].transform.Find("num").GetComponent<UILabel>().text = data.number.ToString();
            //}
            //else
            //{
            //    //sheet action

            //    m_myLocalSheets[data.sheet].cells[cellIndex].itemEffectIndex = type;
            //    setCellItem(data.sheet, cellIndex, (nb_Item.normal_ItemType)type);
            //    daubObjects[data.sheet, cellIndex].transform.Find("num").GetComponent<UILabel>().text = data.number.ToString();
            //    daubObjects[data.sheet, cellIndex].transform.Find("num").GetComponent<UILabel>().color = Color.black;
            //}

            //if (type == 9)
            //{
            //    //부스터
            //    itemBoosterOn = true;
            //}
        }

        nb_GlobalData.g_global.useItemDataList.Clear();
    }

    private int findCellIndex(int sheet, int number)
    {
        for(int i = 0; i < 25; ++i)
        {
            if (m_myLocalSheets[sheet].cells[i].number == number)
            {
                return i;
            }
        }

        return 99;
    }

    private void drawStageBg()
    {
        Transform bg = playScene_ui.playUI.Find("layer0/bg");

        Texture texture = Resources.Load("nb_images/stage/stage" +
            nb_GlobalData.g_global.selectStageId.ToString() + "_sub", typeof(Texture)) as Texture;
        bg.GetComponent<UITexture>().mainTexture = texture;
        bg.GetComponent<UITexture>().MakePixelPerfect();
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Spine.Unity;
using MarigoldModel.Model;

public class nb_MainScene : MonoBehaviour
{

    private GameObject popup;
    private Transform popup_start;
    private Transform popup_ctrl;
    private GameObject mainSceneUI;
    private GameObject baseUI;
    [HideInInspector]
    public nb_RankInfo myInfo;
    public nb_BandRankInfo b_myInfo;

    private Transform[] bingoTickets;
    private BoxCollider[] buttons;

    bool bGetRoomkey = false;

    private int tutoflag = 0;

    private int waitTime2;
    private UILabel label;
    private UILabel label2;

    private Transform[] dailyCheck;

    private int tempJoin;
    private int temptuto;
    private int openTuto;

    public GameObject progress_popup;

    public Transform parent_current;
    public Transform parent_prev;
    public Transform parent_next;

    private GameObject bgLayer;

    private bool selectBattleMode;

    public float bgLayerMoveTime;

    private List<Transform> disableStageList;

    void Awake()
    {
        bgLayer = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase/layer_bg") as GameObject;

        nb_GlobalData.g_global_bgm1.Play();
        nb_GlobalData.g_global_bgm2.Stop();
        nb_GlobalData.g_global_bgm3.Stop();
        nb_GlobalData.g_global_bgm4.Stop();

        openTuto = 0;
        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;

        baseUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        mainSceneUI = GameObject.Find("mainSceneUI");

        disableStageList = new List<Transform>();

        //닉네임
        if (nb_GlobalData.g_global.fb_active == false)
        {
            nb_GlobalData.g_global.InputSocialNickname = nb_GlobalData.g_global.userAccount.Name;
        }
        Debug.Log("Nickname : " + nb_GlobalData.g_global.InputSocialNickname);
        baseUI.transform.Find("layer_base/player_info/profile_name").GetComponent<UILabel>().text
            = nb_GlobalData.g_global.InputSocialNickname;

        //경험치
        redrawPlayerLevel();

        //선택한 스테이지 + 앞뒤 스테이지 로드
        refreshStageView();
        refreshStageEffect();

        //ticket test
        nb_GlobalData.g_global.myInfo.ticketCount = 999;
        //
        
    }

    void Update()
    {
        //http
        if (nbHttp.state == nbHttp.nbHttpState.ConnectStageStart ||
            nbHttp.state == nbHttp.nbHttpState.ConnectBattleStageStart)
        {
            if(progress_popup.active == false)
            {
                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                progress_popup.SetActive(true);
                //progress_popup.transform.Find("text").GetComponent<UILabel>().text = "stage connecting";
                progress_popup.transform.Find("text").GetComponent<UILocalize>().key = "MainMsg_StageConnecting";
            }
        }
        else if (nbHttp.state == nbHttp.nbHttpState.ConnectStageSuccess)
        {
            //일반모드
            Debug.Log("mainScene Update : ConnectStage Success, nb_lobbyScene Load");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            Resources.UnloadUnusedAssets();
            System.GC.Collect();

            Application.LoadLevel("nb_LobbyScene");
        }
        else if (nbHttp.state == nbHttp.nbHttpState.ConnectBattleStageSuccess)
        {
            //배틀모드
            Debug.Log("mainScene Update : ConnectBattleStage Success, nb_battleLobbyScene Load");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            Resources.UnloadUnusedAssets();
            System.GC.Collect();

            Application.LoadLevel("nb_BattleLobbyScene");
        }
        else if (nbHttp.state == nbHttp.nbHttpState.ConnectStageFail)
        {
            Debug.Log("mainScene Update : ConnectStage failed");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            progress_popup.SetActive(false);
        }
        else if (nbHttp.state == nbHttp.nbHttpState.ConnectBattleStageFail)
        {
            Debug.Log("mainScene Update : ConnectBattleStage failed");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            progress_popup.SetActive(false);
        }
        else if (nbHttp.state == nbHttp.nbHttpState.NetworkFailTimeout)
        {
            nbHttp.state = nbHttp.nbHttpState.Wait;

            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            progress_popup.SetActive(false);
        }


    }


    public IEnumerator GetWebImage(string friendKey)
    {

        WWW www = new WWW("https://graph.facebook.com/" + friendKey + "/picture");
        yield return www;
        GameObject profile = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase/myProfile");
        if (www.error == null)
        {
            profile.GetComponent<UITexture>().mainTexture = www.texture;
        }
    }

    private IEnumerator refreshImage()
    {
        yield return new WaitForSeconds(2f);

        GameObject mainUIRoot2 = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;
        Transform mail_glo = mainUIRoot2.transform.Find("layer_Item/mail_glo");
        mail_glo.gameObject.SetActive(false);

        //if (GlobalData.g_global.m_mailInfo.Count > 0)
        //{
        //    mail_glo.gameObject.SetActive(true);
        //}

        //else
        //{
        //    mail_glo.gameObject.SetActive(false);
        //}

        //GameObject mainUIRoot = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;
        //Transform btn_monster = mainUIRoot.transform.Find("layer_Item/card_btn/new");

        //if (GlobalData.g_global.cardCount != GetMyCardCountFromPrefs())
        //{
        //    btn_monster.gameObject.SetActive(true);
        //}
        //else
        //{
        //    btn_monster.gameObject.SetActive(false);
        //}
        StartCoroutine("refreshImage");
    }

    void Start()
    {
        //GlobalData.g_global.currentScene = 1;
        //GlobalData.g_global.mail_index = 0;
        //GlobalData.g_global.reviewIndex = 0;
        //GlobalData.g_global.invite_able = true;

        initGameData();
        //GlobalData.g_global.rankCount = 0;
        //GlobalData.g_global.bingoFlag = 0;

        //nb_GlobalData.g_global.serverFlag = 1;
        //nbSocket.sCtrl.StartClient();
        //GlobalData.g_global.socketState = (int)SocketClass.STATE.mLoginIng;
        //nbSocket.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mLoginRequest);
        
//#if UNITY_ANDROID
//        AndroidManager.Instance.RequestFriends_info();
//#endif
        
        //if (GlobalData.g_global.GetComponent<AudioSource>().isPlaying == false)
        //    GlobalData.g_global.GetComponent<AudioSource>().Play();


        drawGameMoney();
        drawPowerUpMoney();
    }

    private void diff_item()
    {
        GameObject mainUIRoot2 = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;
        Transform mail_glo = mainUIRoot2.transform.Find("layer_Item/mail_glo");
        mail_glo.gameObject.SetActive(false);

        if (GlobalData.g_global.m_mailInfo.Count > 0)
        {
            mail_glo = mainUIRoot2.transform.Find("layer_Item/mail_glo");
            mail_glo.gameObject.SetActive(true);
        }
        else
        {
            mail_glo = mainUIRoot2.transform.Find("layer_Item/mail_glo");
            mail_glo.gameObject.SetActive(false);
        }

        //GameObject mainUIRoot = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;
        //Transform btn_monster = mainUIRoot.transform.Find("layer_Item/card_btn/new");

        //if (GlobalData.g_global.cardCount != GetMyCardCountFromPrefs())
        //{
        //    btn_monster.gameObject.SetActive(true);
        //}
        //else
        //{
        //    btn_monster.gameObject.SetActive(false);
        //}


        //Transform popup_ctrl = popup.transform.Find("popup_tuto");

        //if (GetTutoData() == 0)
        //{
        //    mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        //    buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        //    for (int i = 0; i < buttons.Length; ++i)
        //    {
        //        buttons[i].enabled = false;
        //    }
        //    popup_ctrl.gameObject.SetActive(true);
        //}

        //if (GetReviewData() == 10)
        //{
        //    GameObject mainUIRoot3 = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        //    Transform popup_goEvent = mainUIRoot3.transform.Find("popup_review");

        //    popup_goEvent.gameObject.SetActive(true);

        //    mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        //    buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

        //    for (int i = 0; i < buttons.Length; ++i)
        //    {
        //        buttons[i].enabled = false;
        //    }
        //}
    }

    int GetReviewData()
    {
        return PlayerPrefs.GetInt("Review");
    }

    int GetMyCardCountFromPrefs()
    {
        return PlayerPrefs.GetInt("cardCount");
    }

    void SaveCardCountData(int cardCount)
    {
        PlayerPrefs.SetInt("cardCount", cardCount);
    }


    int GetMyDailyCheck()
    {
        GlobalData.g_global.dailyIndex = 0;
        return PlayerPrefs.GetInt("dailyCheck");
    }

    void SaveMyDailyCheck(int daily)
    {
        PlayerPrefs.SetInt("dailyCheck", daily);
    }


    int GetTutoData()
    {

        return PlayerPrefs.GetInt("Tuto");
    }


    private void initGameData()
    {
        //GlobalData.g_global.invite_able = true;

        //GlobalData.g_global.win = 0;

        //GlobalData.g_global.ItemUseState = false;
        //GlobalData.g_global.useItemCount = 0;
        //GlobalData.g_global.serverFlag = 1;
        //GlobalData.g_global.selectSheet = 0;
        //GlobalData.g_global.myInfo.game_ticket = 0;
        //GlobalData.g_global.myInfo.game_gold = 0;
        //GlobalData.g_global.myInfo.game_goldticket = 0;
        //GlobalData.g_global.myInfo.GameScore = 0;
        //GlobalData.g_global.myRankFlag = false;
        //GlobalData.g_global.myrank = 0;
        //GlobalData.g_global.bingoCount = 0;
        //GlobalData.g_global.rankCount = 0;
        //GlobalData.g_global.sheetInfo.activeSheetCount = 0;
        //GlobalData.g_global.sheetInfo.monsterId = 0;
        //GlobalData.g_global.connect_index = 0;
        //GlobalData.g_global.game_over = 0;
        //GlobalData.g_global.betting_index = 0;
        //GlobalData.g_global.friendRoom = 0;
        //GlobalData.g_global.gameType = 0;
        //GlobalData.g_global.admission = 0;
        //for (int i = 0; i < 100; i++)
        //{
        //    GlobalData.g_global.m_winnerList[i].monster_id = 0;
        //    GlobalData.g_global.m_winnerList[i].nickname = "";
        //    GlobalData.g_global.m_winnerList[i].rank = 0;
        //    GlobalData.g_global.m_winnerList[i].coin = 0;
        //}

        //for (int i = 0; i < 4; i++)
        //{
        //    GlobalData.g_global.sheetInfo.bingoSheet[i] = false;
        //    GlobalData.g_global.sheetInfo.userID = 0;

        //    for (int j = 0; j < 4; j++)
        //    {
        //        GlobalData.g_global.otherSheetInfo[i].bingoSheet[j] = false;
        //        GlobalData.g_global.otherBingoS[i].bingo[j] = false;
        //    }

        //    GlobalData.g_global.otherSheetInfo[i].monsterId = 0;
        //    GlobalData.g_global.otherSheetInfo[i].nickname = "";
        //    GlobalData.g_global.otherSheetInfo[i].roomkey = 0;
        //    GlobalData.g_global.otherSheetInfo[i].shield = 0;
        //    GlobalData.g_global.otherSheetInfo[i].userID = 0;
        //    GlobalData.g_global.otherSheetInfo[i].activeSheetCount = 0;
        //    GlobalData.g_global.otherSheetInfo[i].betting_index = 0;
        //}
    }

    private void callCharge()
    {
        //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mTicketChargeIng;
        //nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mTicketChargeRequest);
        //StartCoroutine(time(nb_GlobalData.g_global.myInfo.waitTime));
    }

    private IEnumerator time(int waitTime)
    {
        yield return new WaitForSeconds(1);

        label2 = GameObject.Find("chargeTime_label").GetComponent<UILabel>();
        int minute = nb_GlobalData.g_global.myInfo.waitTime / 60;
        int second = nb_GlobalData.g_global.myInfo.waitTime % 60;
        string secondstr = second.ToString();
        string minutestr = minute.ToString();

        if (second < 10)
        {
            secondstr = "0" + second.ToString();
        }
        if (minute < 10)
        {
            minutestr = "0" + minute.ToString();
        }

        label2.text = minutestr + ":" + secondstr;

        nb_GlobalData.g_global.myInfo.waitTime = nb_GlobalData.g_global.myInfo.waitTime - 1;

        //8분당 티켓 1개 480초로 세팅 추후 밸런스 테스트후에 바뀔수도 있음

        //if (nb_GlobalData.g_global.myInfo.waitTime < 1)
        //{
        //    //nbSocket.sCtrl.closeSocket();

        //    //GlobalData.g_global.myInfo.ticketCount++;
        //    nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mTicketChargeIng;
        //    nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.mTicketChargeRequest);

        //    GlobalData.g_global.myInfo.waitTime = 480;
        //}


        //if (GlobalData.g_global.myInfo.ticketCount >= 20)
        //{
        //    label2.text = "MAX";
        //}

        //else
        //{
        //    StartCoroutine(time(GlobalData.g_global.myInfo.waitTime));
        //}
    }

    public void drawGameMoney()
    {
        GameObject moneyGroup = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase/money_group") as GameObject;
        if (moneyGroup == null)
        {
            Debug.Log("moneyGroup null");
        }
        UILabel textLabel1 = moneyGroup.transform.Find("gold_group/t_value").GetComponent<UILabel>();
        UILabel textLabel2 = moneyGroup.transform.Find("credit_group/t_value").GetComponent<UILabel>();

        long value1 = nb_GlobalData.g_global.util.getGameMoney(GameMoneyId.COIN);
        long value2 = nb_GlobalData.g_global.util.getGameMoney(GameMoneyId.CREDIT);

        textLabel1.text = nb_GlobalData.g_global.util.GetCommaNumber((int)value1);
        textLabel2.text = nb_GlobalData.g_global.util.GetCommaNumber((int)value2);
    }

    public void drawPowerUpMoney()
    {
        GameObject moneyGroup = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase/money_group") as GameObject;
        if (moneyGroup == null)
        {
            Debug.Log("moneyGroup null");
        }
        UILabel textLabel3 = moneyGroup.transform.Find("ticket_n_group/t_value").GetComponent<UILabel>();
        UILabel textLabel4 = moneyGroup.transform.Find("ticket_b_group/t_value").GetComponent<UILabel>();

        textLabel3.text = nb_GlobalData.g_global.getTotalNormalPowerUpCount().ToString();
        textLabel4.text = nb_GlobalData.g_global.getTotalBattlePowerUpCount().ToString();
    }

    public void moveStageNext()
    {
        if (nb_GlobalData.g_global.selectStageIndex + 1 > nb_GlobalData.g_global.maxStage)
        {
            //Debug.Log("StageNextNull Return");
            nb_GlobalData.g_global.MainStageMove = false;
            return;
        }

        nb_GlobalData.g_global.selectStageIndex = nb_GlobalData.g_global.selectStageIndex + 1;

        Vector3 vec = new Vector3();
        vec.x = parent_prev.position.x;

        iTween.MoveBy(bgLayer, vec, bgLayerMoveTime);

        StartCoroutine("waitStageMoveNext");
    }

    public void moveStagePrev()
    {
        if (nb_GlobalData.g_global.selectStageIndex - 1 < 1)
        {
            //Debug.Log("StagePrevNull Return");
            nb_GlobalData.g_global.MainStageMove = false;
            return;
        }

        nb_GlobalData.g_global.selectStageIndex = nb_GlobalData.g_global.selectStageIndex - 1;

        Vector3 vec = new Vector3();
        vec.x = parent_next.position.x;

        iTween.MoveBy(bgLayer, vec, bgLayerMoveTime);

        StartCoroutine("waitStageMovePrev");
    }

    IEnumerator waitStageMoveNext()
    {
        Vector3 vec = new Vector3();
        vec.x = 500;

        Transform child = parent_next.transform.GetChild(0);
        
        if (child != null)
        {
            iTween.MoveBy(child.Find("spine").gameObject, vec, bgLayerMoveTime * 0.4f);
            //iTween.MoveBy(child.Find("icon").gameObject, vec, bgLayerMoveTime * 0.4f);
            child.Find("collection_info").gameObject.SetActive(false);
            child.Find("gamestart_btn").gameObject.SetActive(false);
            child.Find("name").gameObject.SetActive(false);     
                    
            //if (child.Find("icon_sub") != null)
            if (child.Find("spine_sub") != null)
            {
                iTween.MoveBy(child.Find("spine_sub").gameObject, vec, bgLayerMoveTime * 0.4f);
                //iTween.MoveBy(child.Find("icon_sub").gameObject, vec, bgLayerMoveTime * 0.4f);
                child.Find("collection_info_sub").gameObject.SetActive(false);
                child.Find("gamestart_btn_sub").gameObject.SetActive(false);
                child.Find("name_sub").gameObject.SetActive(false);     
            }
        }

        yield return new WaitForSeconds(bgLayerMoveTime * 0.4f);

        if (child != null)
        {
            iTween.MoveBy(child.Find("spine").gameObject, -vec, bgLayerMoveTime * 0.4f);
            //iTween.MoveBy(child.Find("icon").gameObject, -vec, bgLayerMoveTime * 0.4f);
        }

        yield return new WaitForSeconds(bgLayerMoveTime * 0.4f);
        iTween.Stop(child.Find("spine").gameObject);
        child.Find("spine").transform.localPosition = child.Find("spine_pos").localPosition;

        if (child != null)
        {
            if (child.Find("spine_sub") != null)
            //if (child.Find("icon_sub") != null)
            {
                iTween.MoveTo(child.Find("spine_sub").gameObject, child.Find("spine_sub_pos").position, bgLayerMoveTime * 0.4f);
                //iTween.MoveBy(child.Find("icon_sub").gameObject, -vec, bgLayerMoveTime * 0.4f);
            }
        }

        yield return new WaitForSeconds(bgLayerMoveTime * 0.4f);

        if (child != null)
        {
            child.Find("collection_info").gameObject.SetActive(true);
            child.Find("gamestart_btn").gameObject.SetActive(true);
            child.Find("name").gameObject.SetActive(true);

            if (child.Find("icon_sub") != null)
            {
                child.Find("collection_info_sub").gameObject.SetActive(true);
                child.Find("gamestart_btn_sub").gameObject.SetActive(true);
                child.Find("name_sub").gameObject.SetActive(true);
            }
        }

        refreshStageNext();
    }


    IEnumerator waitStageMovePrev()
    {
        Vector3 vec = new Vector3();
        vec.x = 500;

        Transform child = parent_prev.transform.GetChild(0);

        if (child != null)
        {
            iTween.MoveBy(child.Find("spine").gameObject, -vec, bgLayerMoveTime * 0.4f);
            //iTween.MoveBy(child.Find("icon").gameObject, -vec, bgLayerMoveTime * 0.4f);
            child.Find("collection_info").gameObject.SetActive(false);
            child.Find("gamestart_btn").gameObject.SetActive(false);
            child.Find("name").gameObject.SetActive(false);

            if (child.Find("spine_sub") != null)
            //if (child.Find("icon_sub") != null)
            {
                iTween.MoveBy(child.Find("spine_sub").gameObject, -vec, bgLayerMoveTime * 0.4f);
                //iTween.MoveBy(child.Find("icon_sub").gameObject, -vec, bgLayerMoveTime * 0.4f);
                child.Find("collection_info_sub").gameObject.SetActive(false);
                child.Find("gamestart_btn_sub").gameObject.SetActive(false);
                child.Find("name_sub").gameObject.SetActive(false);
            }
        }

        yield return new WaitForSeconds(bgLayerMoveTime * 0.4f);

        if (child != null)
        {
            iTween.MoveBy(child.Find("spine").gameObject, vec, bgLayerMoveTime * 0.4f);
            //iTween.MoveBy(child.Find("icon").gameObject, vec, bgLayerMoveTime * 0.4f);
        }

        yield return new WaitForSeconds(bgLayerMoveTime * 0.4f);
        iTween.Stop(child.Find("spine").gameObject);
        child.Find("spine").transform.localPosition = child.Find("spine_pos").localPosition;

        if (child != null)
        {
            if (child.Find("spine_sub") != null)
            //if (child.Find("icon_sub") != null)
            {
                iTween.MoveTo(child.Find("spine_sub").gameObject, child.Find("spine_sub_pos").position, bgLayerMoveTime * 0.4f);
                //iTween.MoveBy(child.Find("icon_sub").gameObject, vec, bgLayerMoveTime * 0.4f);
            }
        }

        yield return new WaitForSeconds(bgLayerMoveTime * 0.4f);

        if (child != null)
        {
            child.Find("collection_info").gameObject.SetActive(true);
            child.Find("gamestart_btn").gameObject.SetActive(true);
            child.Find("name").gameObject.SetActive(true);

            if (child.Find("spine_sub") != null)
            //if (child.Find("icon_sub") != null)
            {
                child.Find("collection_info_sub").gameObject.SetActive(true);
                child.Find("gamestart_btn_sub").gameObject.SetActive(true);
                child.Find("name_sub").gameObject.SetActive(true);
            }
        }

        refreshStagePrev();
    }

    private void refreshStageNext()
    {
        int selectStage = nb_GlobalData.g_global.selectStageIndex;
        int maxStage = nb_GlobalData.g_global.maxStage;
        GameObject baseObj = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;
        GameObject prevBtn = baseObj.transform.Find("layer_base/left_btn").gameObject;
        GameObject nextBtn = baseObj.transform.Find("layer_base/right_btn").gameObject;
        
        for (int i = 0; i < parent_prev.transform.childCount; ++i)
        {
            GameObject.Destroy(parent_prev.transform.GetChild(i).gameObject);
        }

        parent_current.transform.GetChild(0).SetParent(parent_prev);
        parent_next.transform.GetChild(0).SetParent(parent_current);

        for (int i = 0; i < parent_prev.transform.childCount; ++i)
        {
            parent_prev.transform.GetChild(i).localPosition = Vector3.zero;
        }
        for (int i = 0; i < parent_current.transform.childCount; ++i)
        {
            parent_current.transform.GetChild(i).localPosition = Vector3.zero;
        }
        for (int i = 0; i < parent_next.transform.childCount; ++i)
        {
            parent_next.transform.GetChild(i).localPosition = Vector3.zero;
        }

        GameObject stage_next = null;
        if (selectStage < maxStage)
        {
            stage_next = Instantiate(Resources.Load("game/stage" + (selectStage + 1).ToString())) as GameObject;
            stage_next.transform.parent = parent_next;
            stage_next.transform.localPosition = Vector3.zero;
            stage_next.transform.localScale = Vector3.one;
            stage_next.SetActive(true);
            nextBtn.SetActive(true);
        }
        else
        {
            stage_next = null;
            nextBtn.SetActive(false);
        }
        prevBtn.SetActive(true);

        bgLayer.transform.position = Vector3.zero;

        nb_GlobalData.g_global.MainStageMove = false;

        refreshStageEffect();
    }

    private void refreshStagePrev()
    {
        int selectStage = nb_GlobalData.g_global.selectStageIndex;
        int maxStage = nb_GlobalData.g_global.maxStage;
        GameObject baseObj = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;
        GameObject prevBtn = baseObj.transform.Find("layer_base/left_btn").gameObject;
        GameObject nextBtn = baseObj.transform.Find("layer_base/right_btn").gameObject;

        for (int i = 0; i < parent_next.transform.childCount; ++i)
        {
            GameObject.Destroy(parent_next.transform.GetChild(i).gameObject);
        }

        parent_current.transform.GetChild(0).SetParent(parent_next);
        parent_prev.transform.GetChild(0).SetParent(parent_current);

        for (int i = 0; i < parent_prev.transform.childCount; ++i)
        {
            parent_prev.transform.GetChild(i).localPosition = Vector3.zero;
        }
        for (int i = 0; i < parent_current.transform.childCount; ++i)
        {
            parent_current.transform.GetChild(i).localPosition = Vector3.zero;
        }
        for (int i = 0; i < parent_next.transform.childCount; ++i)
        {
            parent_next.transform.GetChild(i).localPosition = Vector3.zero;
        }

        GameObject stage_prev = null;
        if (selectStage > 1)
        {
            stage_prev = Instantiate(Resources.Load("game/stage" + (selectStage - 1).ToString())) as GameObject;
            stage_prev.transform.parent = parent_prev;
            stage_prev.transform.localPosition = Vector3.zero;
            stage_prev.transform.localScale = Vector3.one;
            stage_prev.SetActive(true);
            prevBtn.SetActive(true);
        }
        else
        {
            stage_prev = null;
            prevBtn.SetActive(false);
        }
        nextBtn.SetActive(true);

        bgLayer.transform.position = Vector3.zero;

        nb_GlobalData.g_global.MainStageMove = false;

        refreshStageEffect();
    }

    private void refreshStageView()
    {
        int selectStage = nb_GlobalData.g_global.selectStageIndex;
        int maxStage = nb_GlobalData.g_global.maxStage;
        GameObject baseObj = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;
        GameObject prevBtn = baseObj.transform.Find("layer_base/left_btn").gameObject;
        GameObject nextBtn = baseObj.transform.Find("layer_base/right_btn").gameObject;

        bgLayer.transform.position = Vector3.zero;

        for (int i = 0; i < parent_current.transform.childCount; ++i)
        {
            GameObject.Destroy(parent_current.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < parent_prev.transform.childCount; ++i)
        {
            GameObject.Destroy(parent_prev.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < parent_next.transform.childCount; ++i)
        {
            GameObject.Destroy(parent_next.transform.GetChild(i).gameObject);
        }

        GameObject stage_current = null;
        GameObject stage_prev = null;
        GameObject stage_next = null;

        stage_current = Instantiate(Resources.Load("game/stage" + selectStage.ToString())) as GameObject;
        stage_current.transform.parent = parent_current;
        stage_current.transform.localPosition = Vector3.zero;
        stage_current.transform.localScale = Vector3.one;
        stage_current.SetActive(true);

        if (selectStage > 1)
        {
            stage_prev = Instantiate(Resources.Load("game/stage" + (selectStage - 1).ToString())) as GameObject;
            stage_prev.transform.parent = parent_prev;
            stage_prev.transform.localPosition = Vector3.zero;
            stage_prev.transform.localScale = Vector3.one;
            stage_prev.SetActive(true);
            prevBtn.SetActive(true);
        }
        else
        {
            stage_prev = null;
            prevBtn.SetActive(false);
        }

        if (selectStage < maxStage)
        {
            Debug.Log("next stage refresh selectStage : " + selectStage + ", max : " + maxStage);
            stage_next = Instantiate(Resources.Load("game/stage" + (selectStage + 1).ToString())) as GameObject;
            stage_next.transform.parent = parent_next;
            stage_next.transform.localPosition = Vector3.zero;
            stage_next.transform.localScale = Vector3.one;
            stage_next.SetActive(true);
            nextBtn.SetActive(true);
        }
        else
        {
            stage_next = null;
            nextBtn.SetActive(false);
        }

        nb_GlobalData.g_global.MainStageMove = false;
    }

    private void refreshStageEffect()
    {
        //Debug.Log("refreshStageEffect() start");
        //Debug.Log("refreshStageEffect() parent_current.childCount : " + parent_current.childCount);
        //Debug.Log("refreshStageEffect() parent_prev.childCount : " + parent_prev.childCount);
        //Debug.Log("refreshStageEffect() parent_next.childCount : " + parent_next.childCount);
        disableStageList.Clear();
        int selectStage = nb_GlobalData.g_global.selectStageIndex;

        if (parent_current.childCount != 0)
        {
            var current = parent_current.GetChild(0).Find("stage_effect");
            if (current != null)
            {
                activeStageEffect(current, true);
            }
        }

        for (int i = 0; i < parent_prev.childCount; ++i)
        {
            disableStageList.Add(parent_prev.GetChild(i).Find("stage_effect"));
        }
        for (int i = 0; i < parent_next.childCount; ++i)
        {
            disableStageList.Add(parent_next.GetChild(i).Find("stage_effect"));
        }

        foreach (var obj in disableStageList)
        {
            activeStageEffect(obj, false);
        }

        //if (parent_prev.childCount != 0)
        //{
        //    var stage = parent_prev.Find("stage" + (selectStage - 1));
        //    if (stage != null)
        //    {
        //        var prev = stage.Find("stage_effect");
        //        if (prev != null)
        //        {
        //            activeStageEffect(prev, false);
        //        }
        //    }
        //}

        //if (parent_next.childCount != 0)
        //{
        //    var stage = parent_next.Find("stage" + (selectStage + 1));
        //    if (stage != null)
        //    {
        //        var next = stage.Find("stage_effect");
        //        if (next != null)
        //        {
        //            activeStageEffect(next, false);
        //        }
        //    }
        //}
    }

    private void activeStageEffect(Transform parent, bool isEnable)
    {
        if (parent != null)
        {
            parent.gameObject.SetActive(isEnable);
            if (isEnable)
            {
                var twr = parent.GetComponentsInChildren<TweenRotation>();
                foreach (var tw in twr)
                {
                    tw.ResetToBeginning();
                    tw.PlayForward();
                }
                var twp = parent.GetComponentsInChildren<TweenPosition>();
                foreach (var tw in twp)
                {
                    tw.ResetToBeginning();
                    tw.PlayForward();
                }
            }
        }
    }

    private void redrawPlayerLevel()
    {
        var levelObj = baseUI.transform.Find("layer_base/player_info/profile_star_text");
        var gaugeObj = baseUI.transform.Find("layer_base/player_info/profile_gauge");
        var gaugeText = baseUI.transform.Find("layer_base/player_info/profile_gauge_text");

        levelObj.GetComponent<UILabel>().text = nb_GlobalData.g_global.userAccount.Level.ToString();

        int level = nb_GlobalData.g_global.userAccount.Level;
        int nowExp = nb_GlobalData.g_global.userAccount.Experience;
        int nextLevelExp = nb_GlobalData.g_global.util.getLevelRequireExp(level + 1);

        Debug.Log("redrawPlayerLevel() level : " + level + ", exp : " + nowExp + ", next : " + nextLevelExp);

        float rate = (float)nowExp / (float)nextLevelExp;
        int intRate = (int)(rate * 100);

        Debug.Log("redrawPlayerLevel() rate : " + rate + ", intRate : " + intRate);

        rate = rate > 1 ? 1 : rate;
        intRate = intRate > 100 ? 100 : intRate;

        gaugeObj.GetComponent<UISprite>().fillAmount = rate;
        gaugeText.GetComponent<UILabel>().text = intRate.ToString() + "%";        
    }
}

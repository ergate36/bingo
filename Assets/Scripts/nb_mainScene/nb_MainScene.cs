using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Spine.Unity;

public class nb_MainScene : MonoBehaviour
{

    private GameObject popup;
    private Transform popup_start;
    private Transform popup_ctrl;
    private GameObject mainSceneUI;
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
    
    public GameObject bgSpine;
    private SkeletonAnimation sa;

    public GameObject progress_popup;


    void Awake()
    {
        nb_GlobalData.g_global_bgm1.Play();
        nb_GlobalData.g_global_bgm2.Stop();
        nb_GlobalData.g_global_bgm3.Stop();
        nb_GlobalData.g_global_bgm4.Stop();

        openTuto = 0;
        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            //buttons[i].enabled = false;
        }

        int check = 0;
        if (check == 1)
        {
            check = 0;
            mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                //buttons[i].enabled = false;
            }
        }
        mainSceneUI = GameObject.Find("mainSceneUI");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        GameObject mainUIRoot = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;

        sa = bgSpine.GetComponent<SkeletonAnimation>();

        sa.AnimationName = nb_GlobalData.g_global.WorldStageSpineAnimation;
        sa.loop = true;
    }

    void Update()
    {
        //http
        if (nbHttp.state == nbHttp.nbHttpState.ConnectStageStart)
        {
            if(progress_popup.active == false)
            {
                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                progress_popup.SetActive(true);
                progress_popup.transform.Find("text").GetComponent<UILabel>().text = "stage connecting";
            }
        }
        else if (nbHttp.state == nbHttp.nbHttpState.ConnectStageSuccess)
        {
            Debug.Log("mainScene Update : ConnectStage Success, nb_lobbyScene Load");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            Resources.UnloadUnusedAssets();
            System.GC.Collect();

            Application.LoadLevel("nb_LobbyScene");
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


        if (nb_GlobalData.g_global.WorldStageSpineRefresh == true)
        {
            nb_GlobalData.g_global.WorldStageSpineRefresh = false;

            sa.AnimationState.SetAnimation(0,
                nb_GlobalData.g_global.WorldStageSpineAnimation, false);

            StartCoroutine("PlayBgWait");
        }
        
        if (nb_GlobalData.g_global.WorldStageSpineSelectAni == true)
        {
            nb_GlobalData.g_global.WorldStageSpineSelectAni = false;

            //sa.AnimationState.SetAnimation(0,
            //    nb_GlobalData.g_global.WorldStageSpineAnimation, false);

            //StartCoroutine("PlayBgWait");


            //
            Application.LoadLevel("nb_LobbyScene");

            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }
    }

    IEnumerator PlayBgWait()
    {
        yield return new WaitForSeconds(0.5f);

        nb_GlobalData.g_global.WorldStageSpineAnimation = 
            "wait" + nb_GlobalData.g_global.SelectWorldStage.ToString();

        sa.AnimationState.SetAnimation(0,
            nb_GlobalData.g_global.WorldStageSpineAnimation, true);
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

    public void drawPowerUpMoney()
    {
        GameObject moneyGroup = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase/money_group") as GameObject;
        if (moneyGroup == null)
        {
            Debug.Log("moneyGroup null");
        }
        UILabel textLabel = moneyGroup.transform.Find("ticket_n_group/t_value").GetComponent<UILabel>();

        textLabel.text = nb_GlobalData.g_global.getTotalNormalPowerUpCount().ToString();
    }
}
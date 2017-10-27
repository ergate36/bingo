using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MainScene : MonoBehaviour
{

    private GameObject popup;
    private Transform popup_start;
    private Transform popup_mail;
    private Transform popup_ctrl;
    private GameObject mainSceneUI;
    [HideInInspector]
    public RankInfo myInfo;
    public BandRankInfo b_myInfo;

    private Transform[] bingoTickets;
    private BoxCollider[] buttons;

    public UIGrid grid;
    public UIScrollView sv;
    public UIPanel panel;

    public Transform gridRoot;
    bool bGetRoomkey = false;

    private int tutoflag = 0;

    private int waitTime2;
    private UILabel label;
    private UILabel label2;

    private Transform[] dailyCheck;

    private int tempJoin;
    private int temptuto;
    private int openTuto;

    void Awake()
    {
        openTuto = 0;
        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        int check = 0;
        if (check == 1)
        {
            check = 0;
            mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
        }
        mainSceneUI = GameObject.Find("mainSceneUI");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        GameObject mainUIRoot = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;
    }

    void Update()
    {
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mLoginIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mServerTestIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mServerTestRequest);
        }

        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mServerTestIngComplete)
        {
            if (GlobalData.g_global.servertesting == false)
            {
                GlobalData.g_global.socketState = (int)SocketClass.STATE.mMyGameInfoIng;
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMyGameInfoRequest);
            }
        }
        else if(GlobalData.g_global.reviewIndex == 222){
            // 광고 코인 != 0
            //GlobalData.g_global.reviewIndex = 555;
            //GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            //mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");

            //buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

            //for (int i = 0; i < buttons.Length; ++i)
            //{
            //    buttons[i].enabled = false;
            //}

            //popup_ctrl = popup.transform.Find("popup_getTapjoy");
            //popup_ctrl.gameObject.SetActive(true);
            //popup_ctrl.transform.localScale = Vector3.one * 0.7f;
            //iTween.ScaleTo(popup_ctrl.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            //iTween.ValueTo(popup_ctrl.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup_ctrl.gameObject));

            //Transform label3 = popup_ctrl.transform.Find("Label");
            //Debug.Log("COIT : " + GlobalData.g_global.getCoin);
            //label3.GetComponent<UILabel>().text = "Congratulations!\nYou’ve just earned "+GlobalData.g_global.getCoin+" COIN!";

        }
        else if (GlobalData.g_global.reviewIndex == 333)
        {
            //광고 코인 ==0
            //GlobalData.g_global.reviewIndex = 555;

            //GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            //mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");

            //buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

            //for (int i = 0; i < buttons.Length; ++i)
            //{
            //    buttons[i].enabled = false;
            //}

            //popup_ctrl = popup.transform.Find("popup_nullTapjoy");
            //popup_ctrl.gameObject.SetActive(true);
            //popup_ctrl.transform.localScale = Vector3.one * 0.7f;
            //iTween.ScaleTo(popup_ctrl.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            //iTween.ValueTo(popup_ctrl.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup_ctrl.gameObject));

        }

        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mFriendInviteAgreeNoticeComplete)
        {
            if (GlobalData.g_global.invite_able == true)
            {
                GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

                mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");

                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }

                popup_ctrl = popup.transform.Find("popup_invited");
                popup_ctrl.gameObject.SetActive(true);
                popup_ctrl.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(popup_ctrl.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(popup_ctrl.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup_ctrl.gameObject));

                popup_ctrl.Find("Label").GetComponent<UILabel>().text = GlobalData.g_global.invite_name.ToString();  
                for (int i = 1; i < 5; i++)
                {
                    GameObject check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_invited/Sprite" + i);
                    check.transform.GetComponent<UISprite>().spriteName = "";
                    
                    if (i == 1)
                    {
                        check.transform.GetComponent<UISprite>().spriteName = "check_big";
                        GlobalData.g_global.selectSheet = 1;
                    }
                }
            }

            else
            {
                GlobalData.g_global.selectInvite = 1;
                GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteAgreeRequest;
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mFriendInviteAgreeRequest);
        
            }
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMyGameInfoIngComplete && GlobalData.g_global.connect_index != 33)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mEventServerJoinIng;
            ItemLayer_Ctrl.ictrl.SetItem();

            GameObject check = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase/layer_myinfo/nickname");
            check.transform.GetComponent<UILabel>().text = GlobalData.g_global.myInfo.nickName;

            check = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase/layer_myinfo/score");
            check.transform.GetComponent<UILabel>().text = (GlobalData.g_global.myInfo.win + GlobalData.g_global.myInfo.lose).ToString();
            
            check = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase/layer_myinfo/win");
            check.transform.GetComponent<UILabel>().text = GlobalData.g_global.myInfo.win.ToString();
            
            check = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase/layer_myinfo/lose");
            check.transform.GetComponent<UILabel>().text = GlobalData.g_global.myInfo.lose.ToString();
            
            check = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase/layer_myinfo/rate");
            if (GlobalData.g_global.myInfo.win == 0 && GlobalData.g_global.myInfo.lose == 0)
            {
                check.transform.GetComponent<UILabel>().text = "0 %";
            }
            else
            {
                check.transform.GetComponent<UILabel>().text = ((GlobalData.g_global.myInfo.win * 100) / (GlobalData.g_global.myInfo.win + GlobalData.g_global.myInfo.lose)).ToString() + " %";
            }
            StartCoroutine(GetWebImage(GlobalData.g_global.myInfo.userKey));

            GlobalData.g_global.socketState = (int)SocketClass.STATE.mEventServerJoinIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mEventServerJoinRequest);
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mEventServerJoinComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardListIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMonsterCardListRequest);
        }

        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMonsterCardListIngComplete && GlobalData.g_global.currentScene == 1)
        {
            //121212
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (GlobalData.g_global.myCardList[i].cardused == 1)
                {
                    GlobalData.g_global.playCardIndex = 1;
                    GlobalData.g_global.myPlayCard = GlobalData.g_global.myCardList[i].cardId;
                    break;
                }
                else if (GlobalData.g_global.myCardList[i].cardused == 2)
                {
                    if (GlobalData.g_global.playCardIndex == 1)
                    {
                        break;
                    }
                    GlobalData.g_global.playCardIndex = 2;
                    GlobalData.g_global.myPlayCard = GlobalData.g_global.myCardList[i].cardId;
                }
                else if (GlobalData.g_global.myCardList[i].cardused == 3)
                {
                    if (GlobalData.g_global.playCardIndex != 0 && GlobalData.g_global.playCardIndex < 3)
                    {
                        continue;
                    }
                    GlobalData.g_global.playCardIndex = 3;
                    GlobalData.g_global.myPlayCard = GlobalData.g_global.myCardList[i].cardId;
                }
                else if (GlobalData.g_global.myCardList[i].cardused == 4)
                {
                    if (GlobalData.g_global.playCardIndex != 0 && GlobalData.g_global.playCardIndex < 4)
                    {
                        continue;
                    }
                    GlobalData.g_global.playCardIndex = 4;
                    GlobalData.g_global.myPlayCard = GlobalData.g_global.myCardList[i].cardId;
                }
                if (i == GlobalData.g_global.myCardList.Count - 1 && GlobalData.g_global.playCardIndex == 0)
                {
                    GlobalData.g_global.myPlayCard = 0;
                }
            }

            GlobalData.g_global.socketState = (int)SocketClass.STATE.mDailyInfoIng;
            //makeRanking();
            if (GetMyDailyCheck() != System.Convert.ToInt32(DateTime.Now.ToString("dd")))
            {
                PlayerPrefs.SetInt("facebook", 0);
                SaveMyDailyCheck(System.Convert.ToInt32(DateTime.Now.ToString("dd")));
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mDailyInfoRequest);
            }
            else
            {
                GlobalData.g_global.socketState = (int)SocketClass.STATE.mMailGetIng;
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMailGetRequest);
            }
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMailGetIngComplete && GlobalData.g_global.mail_index == 0)
        {
            callCharge();
        }

        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mDailyCheckComplete && GlobalData.g_global.reviewIndex == 0)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mMailGetIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMailGetRequest);


            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
            popup_ctrl = popup.transform.Find("popup_daily");
            popup_ctrl.gameObject.SetActive(true);
            
            mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");

            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
            dailyCheck = new Transform[7];

            for (int i = 0; i < 7; i++)
            {
                dailyCheck[i] = popup_ctrl.transform.Find("Sprite" + (i + 1));
                dailyCheck[i].GetComponent<UISprite>().spriteName = "";
            }

            for (int i = 0; i < 7; i++)
            {
                dailyCheck[i].GetComponent<UISprite>().spriteName = "get";

                if (GlobalData.g_global.dailyInfo[i] == 0)
                {
                    break;
                }
            }

            //callCharge();
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mDailyInfoComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mDailyCheckIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mDailyCheckRequest);
        }


        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mTicketChargeIngComplete)
        {
            
            ItemLayer_Ctrl.ictrl.SetItem();

            //Socket_Ctrl.sCtrl.closeSocket();
            StartCoroutine("refreshImage");
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");

            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }

            GlobalData.g_global.event_join = tempJoin;
            popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
            if (temptuto == 1)
            {
                temptuto = 0;
                tutoflag = 1;
                mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
            }
            diff_item();
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

        if (GlobalData.g_global.m_mailInfo.Count > 0)
        {
            mail_glo.gameObject.SetActive(true);
        }

        else
        {
            mail_glo.gameObject.SetActive(false);
        }

        GameObject mainUIRoot = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;
        Transform btn_monster = mainUIRoot.transform.Find("layer_Item/card_btn/new");

        if (GlobalData.g_global.cardCount != GetMyCardCountFromPrefs())
        {
            btn_monster.gameObject.SetActive(true);
        }
        else
        {
            btn_monster.gameObject.SetActive(false);
        }
        StartCoroutine("refreshImage");
    }

    void Start()
    {
        GlobalData.g_global.currentScene = 1;
        GlobalData.g_global.mail_index = 0;
        GlobalData.g_global.reviewIndex = 0;
        GlobalData.g_global.invite_able = true;
        
        initGameData();
        GlobalData.g_global.rankCount = 0;
        GlobalData.g_global.bingoFlag = 0;

        GlobalData.g_global.serverFlag = 1;
        Socket_Ctrl.sCtrl.StartClient();
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mLoginIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mLoginRequest);
        
#if UNITY_ANDROID
        AndroidManager.Instance.RequestFriends_info();
#endif
        
        if (GlobalData.g_global.GetComponent<AudioSource>().isPlaying == false)
            GlobalData.g_global.GetComponent<AudioSource>().Play();

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

        GameObject mainUIRoot = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase") as GameObject;
        Transform btn_monster = mainUIRoot.transform.Find("layer_Item/card_btn/new");

        if (GlobalData.g_global.cardCount != GetMyCardCountFromPrefs())
        {
            btn_monster.gameObject.SetActive(true);
        }
        else
        {
            btn_monster.gameObject.SetActive(false);
        }


        Transform popup_ctrl = popup.transform.Find("popup_tuto");

        if (GetTutoData() == 0)
        {
            mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
            popup_ctrl.gameObject.SetActive(true);
        }

        if (GetReviewData() == 10)
        {
            GameObject mainUIRoot3 = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
            Transform popup_goEvent = mainUIRoot3.transform.Find("popup_review");

            popup_goEvent.gameObject.SetActive(true);

            mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
        }
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
        GlobalData.g_global.invite_able = true;

        GlobalData.g_global.win = 0;

        GlobalData.g_global.ItemUseState = false;
        GlobalData.g_global.useItemCount = 0;
        GlobalData.g_global.serverFlag = 1;
        GlobalData.g_global.selectSheet = 0;
        GlobalData.g_global.myInfo.game_ticket = 0;
        GlobalData.g_global.myInfo.game_gold = 0;
        GlobalData.g_global.myInfo.game_goldticket = 0;
        GlobalData.g_global.myInfo.GameScore = 0;
        GlobalData.g_global.myRankFlag = false;
        GlobalData.g_global.myrank = 0;
        GlobalData.g_global.bingoCount = 0;
        GlobalData.g_global.rankCount = 0;
        GlobalData.g_global.sheetInfo.activeSheetCount = 0;
        GlobalData.g_global.sheetInfo.monsterId = 0;
        GlobalData.g_global.connect_index = 0;
        GlobalData.g_global.game_over = 0;
        GlobalData.g_global.betting_index = 0;
        GlobalData.g_global.friendRoom = 0;
        GlobalData.g_global.gameType = 0;
        GlobalData.g_global.admission = 0;
        for (int i = 0; i < 100; i++)
        {
            GlobalData.g_global.m_winnerList[i].monster_id = 0;
            GlobalData.g_global.m_winnerList[i].nickname = "";
            GlobalData.g_global.m_winnerList[i].rank = 0;
            GlobalData.g_global.m_winnerList[i].coin = 0;
        }

        for (int i = 0; i < 4; i++)
        {
            GlobalData.g_global.sheetInfo.bingoSheet[i] = false;
            GlobalData.g_global.sheetInfo.userID = 0;

            for (int j = 0; j < 4; j++)
            {
                GlobalData.g_global.otherSheetInfo[i].bingoSheet[j] = false;
                GlobalData.g_global.otherBingoS[i].bingo[j] = false;
            }

            GlobalData.g_global.otherSheetInfo[i].monsterId = 0;
            GlobalData.g_global.otherSheetInfo[i].nickname = "";
            GlobalData.g_global.otherSheetInfo[i].roomkey = 0;
            GlobalData.g_global.otherSheetInfo[i].shield = 0;
            GlobalData.g_global.otherSheetInfo[i].userID = 0;
            GlobalData.g_global.otherSheetInfo[i].activeSheetCount = 0;
            GlobalData.g_global.otherSheetInfo[i].betting_index = 0;
        }
    }

    private void callCharge()
    {
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mTicketChargeIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mTicketChargeRequest);
        StartCoroutine(time(GlobalData.g_global.myInfo.waitTime));
    }

    private IEnumerator time(int waitTime)
    {
        yield return new WaitForSeconds(1);

        label2 = GameObject.Find("chargeTime_label").GetComponent<UILabel>();
        int minute = GlobalData.g_global.myInfo.waitTime / 60;
        int second = GlobalData.g_global.myInfo.waitTime % 60;
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

        GlobalData.g_global.myInfo.waitTime = GlobalData.g_global.myInfo.waitTime - 1;

        //8분당 티켓 1개 480초로 세팅 추후 밸런스 테스트후에 바뀔수도 있음

        if (GlobalData.g_global.myInfo.waitTime < 1)
        {
            //Socket_Ctrl.sCtrl.closeSocket();

            //GlobalData.g_global.myInfo.ticketCount++;
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mTicketChargeIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mTicketChargeRequest);

            GlobalData.g_global.myInfo.waitTime = 480;
        }


        if (GlobalData.g_global.myInfo.ticketCount >= 20)
        {
            label2.text = "MAX";
        }

        else
        {
            StartCoroutine(time(GlobalData.g_global.myInfo.waitTime));
        }
    }

}
﻿using UnityEngine;
using System.Collections;
public class nb_BattleLobbyScene : MonoBehaviour
{

    private nb_BattleLobbySceneUI m_nbLobbySceneUI;
    private float alpha;
    private GameObject decrease_eff;

    public bool m_bReady = false;
    public bool m_enterLobby = false;
    public bool m_bGetRoomkey = false;
    private UILabel label2;
    private UILabel label;
    public GameObject popup;
    public Transform popup_ticket;

    private int waitTime2 = 60;

    public int m_startCountDown = 5;
    // notice sound
    private GameObject sound_notice;
    private bool readyflag = false;

    [HideInInspector]
    public bool killingServer = false;

    void Awake()
    {
        readyflag = false;
        //sound_notice = GameObject.Find("sound_notice");

        nb_GlobalData.g_global_bgm1.Stop();
        nb_GlobalData.g_global_bgm2.Play();
        nb_GlobalData.g_global_bgm3.Stop();
        nb_GlobalData.g_global_bgm4.Stop();
    }
    // Use this for initialization

    private void OnGUI()
    {
        //var ipname = GlobalData.g_global.itemip[GlobalData.g_global.modeIndex];
        //var portname = GlobalData.g_global.itemport[GlobalData.g_global.modeIndex];
        //GUI.Label(new Rect(0.0f, 0.0f, 100.0f, 100.0f), "ipname:" + ipname);
        //GUI.Label(new Rect(0.0f, 20.0f, 100.0f, 100.0f), "portname:" + portname);
    }

    void Start()
    {
        m_nbLobbySceneUI = gameObject.GetComponent<nb_BattleLobbySceneUI>();
        callCharge();
        m_bReady = false;
        m_enterLobby = false;

        m_startCountDown = 5;

        // 게임리프트 인증 요청
        nb_GlobalData.g_global.serverFlag = 2;
        nbSocket.sCtrl.StartClient();

        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.GameLiftConnectRequest);

        drawPowerUpMoney();
        drawStageInfo();
    }



    void Update()
    {
        //socket
        if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.GameLiftConnectResponse_End)
        {
            readyflag = true;
        }

        if (readyflag == false) //게임리프트 인증 상태에서만 동작
        {
            return;
        }

        if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.MonsterEnterGameResponse_End)
        {
            Debug.Log("=== WAIT START ===");
            //입장 요청 응답 - 대기열 상태
            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
            
            onReady(nb_GlobalData.g_global.sheetInfo.activeSheetCount);

            playStart();    //카운트 다운 코루틴 동작
        }
        else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.MonsterStartGameAlarm_End)
        {
            //게임시작 알람
            //Debug.Log("=== GAME START ===");
            //m_nbLobbySceneUI.cancel_btn.GetComponent<BoxCollider>().enabled = false;

            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
            //StartCoroutine(waitStart());
        }
        //else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.BlitzWaitRoomStatusAlarm_End)
        //{
        //}

        else if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.KillServerResponse_End)
        {
            //서버 주금 완료
            Debug.Log("KillServerResponse_End");
            killingServer = false;

            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
        }

        if (nb_GlobalData.g_global.PlaySceneChange)
        {
            nb_GlobalData.g_global.PlaySceneChange = false;

            Debug.Log("=== GAME START ===");
            m_nbLobbySceneUI.cancel_btn.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(waitStart());
        }
    }

    private IEnumerator waitStart()
    {
        StopCoroutine("activeStartCountDown");
        yield return new WaitForSeconds(1f);

        Resources.UnloadUnusedAssets();
        System.GC.Collect();

        Application.LoadLevel("nb_BattlePlayScene");
    }

    public void onReady(int sheetCount)
    {
        if (readyflag == false)
        {
            Debug.Log("readyflag is False");
            return;
        }

        m_bReady = true;

        activeReadyPopup(); //대기중 ui 가져옴
    }
    private void activeReadyPopup()
    {
        BoxCollider[] buttons = m_nbLobbySceneUI.uiRoot.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }   //대기중 뒤쪽 화면 ui 버튼 모두 비활성화

        BoxCollider[] popupButtons = m_nbLobbySceneUI.m_waitPopup.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < popupButtons.Length; ++i)
        {
            popupButtons[i].enabled = true;
        }   //wait ui 버튼 활성화

        //sound_notice.GetComponent<AudioSource>().Play();
        
        iTween.MoveTo(m_nbLobbySceneUI.m_waitPopup.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", "easeOutElastic", "time", 1));
    }


    private void activeStart()
    {
        //m_nbLobbySceneUI.mentWait.gameObject.SetActive(false);
        //m_nbLobbySceneUI.mentRetry.gameObject.SetActive(false);
        //m_nbLobbySceneUI.mentStart.gameObject.SetActive(true);

        //StartCoroutine(activeStartCountDown());
    }


    private IEnumerator activeStartCountDown()
    {

        //대기타임이 2초 이상일때는 나가는 것이 가능함
        if (nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainSecond > 2)
        {
            m_nbLobbySceneUI.cancel_btn.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            m_nbLobbySceneUI.cancel_btn.GetComponent<BoxCollider>().enabled = false;
        }
        
        if (nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainSecond > 0)
        {
            --nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainSecond;

            nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainBingo = 0;
        }
        
        if (nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainSecond > 0)
        {
            m_nbLobbySceneUI.waitTextImageSecond.gameObject.SetActive(true);
            m_nbLobbySceneUI.waitTextImageBingo.gameObject.SetActive(false);
            m_nbLobbySceneUI.countDown.GetComponent<UILabel>().text = nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainSecond.ToString();
            m_nbLobbySceneUI.countDown2.GetComponent<UILabel>().text = nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainBingo.ToString();

            iTween.ScaleTo(m_nbLobbySceneUI.countDown.gameObject, iTween.Hash("x,", 1.2f, "y", 1.3f, "oncompletetarget", gameObject, "oncomplete", "completeScaling", "time", 0.3f));
        }
        else if (nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainBingo > 0)
        {
            m_nbLobbySceneUI.waitTextImageSecond.gameObject.SetActive(false);
            m_nbLobbySceneUI.waitTextImageBingo.gameObject.SetActive(true);
            m_nbLobbySceneUI.countDown.GetComponent<UILabel>().text = nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainBingo.ToString();
            m_nbLobbySceneUI.countDown2.GetComponent<UILabel>().text = nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainBingo.ToString();

            iTween.ScaleTo(m_nbLobbySceneUI.countDown2.gameObject, iTween.Hash("x,", 1.2f, "y", 1.3f, "oncompletetarget", gameObject, "oncomplete", "completeScaling", "time", 0.1f));
        }
        yield return new WaitForSeconds(1.0f);

        StartCoroutine("activeStartCountDown");

    }

    private void callCharge()
    {
        //StartCoroutine(time(GlobalData.g_global.myInfo.waitTime));
    }
    private IEnumerator time(int waitTime)
    {
        yield return new WaitForSeconds(1);
        label2 = GameObject.Find("chargeTime_label").GetComponent<UILabel>();
        int minute = waitTime / 60;
        int second = waitTime % 60;
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

        nb_GlobalData.g_global.myInfo.waitTime = waitTime - 1;

        if (nb_GlobalData.g_global.myInfo.waitTime < 1)
        {
            nb_GlobalData.g_global.myInfo.waitTime = 480;

            //GlobalData.g_global.myInfo.ticketCount++;
            //GlobalData.g_global.socketState = (int)SocketClass.STATE.mTicketChargeIng;
            //nbSocket.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mTicketChargeRequest);
        }

        if (nb_GlobalData.g_global.myInfo.ticketCount >= 20)
        {
            label2.text = "MAX";
        }
        else
        {
            //StartCoroutine(time(GlobalData.g_global.myInfo.waitTime));
        }
    }

    private void playStart()
    {
        StopCoroutine("activeStartCountDown");
        StartCoroutine("activeStartCountDown");

        m_nbLobbySceneUI.m_waitPopup.gameObject.SetActive(true);  //wait ui 활성화
        m_nbLobbySceneUI.cardSelectGroup.gameObject.SetActive(false);
    }

    private void completeScaling()
    {
        iTween.ScaleTo(m_nbLobbySceneUI.countDown.gameObject, Vector3.one, 0.3f);
        iTween.ScaleTo(m_nbLobbySceneUI.countDown2.gameObject, Vector3.one, 0.3f);
    }

    public void drawPowerUpMoney()
    {
        Transform moneyGroup = m_nbLobbySceneUI.uiRoot.transform.Find("layer1/money_group");
        if (moneyGroup == null)
        {
            Debug.Log("moneyGroup null");
        }
        UILabel textLabel3 = moneyGroup.transform.Find("ticket_n_group/t_value").GetComponent<UILabel>();
        UILabel textLabel4 = moneyGroup.transform.Find("ticket_b_group/t_value").GetComponent<UILabel>();

        textLabel3.text = nb_GlobalData.g_global.getTotalNormalPowerUpCount().ToString();
        textLabel4.text = nb_GlobalData.g_global.getTotalBattlePowerUpCount().ToString();
    }

    private void drawStageInfo()
    {
        Transform icon = m_nbLobbySceneUI.waitRemainRoot.Find("i_icon");
        Transform name = m_nbLobbySceneUI.waitRemainRoot.Find("i_stage_name");

        string iconPath = "stageicon" + nb_GlobalData.g_global.selectStageIndex.ToString() + "_sub";
        string namePath = "stagename" + nb_GlobalData.g_global.selectStageIndex.ToString() + "_sub";
        
        icon.GetComponent<UISprite>().spriteName = iconPath;
        name.GetComponent<UISprite>().spriteName = namePath;
        icon.GetComponent<UISprite>().MakePixelPerfect();
        name.GetComponent<UISprite>().MakePixelPerfect();

        icon.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        name.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        Transform bg = m_nbLobbySceneUI.uiRoot.transform.Find("layer0/bg");

        Texture texture = Resources.Load("nb_images/stage/stage" +
            nb_GlobalData.g_global.selectStageIndex.ToString() + "_sub", typeof(Texture)) as Texture;
        bg.GetComponent<UITexture>().mainTexture = texture;
        bg.GetComponent<UITexture>().MakePixelPerfect();
    }
}

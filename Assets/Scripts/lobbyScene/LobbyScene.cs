using UnityEngine;
using System.Collections;
public class LobbyScene : MonoBehaviour
{

    private LobbySceneUI m_lobbySceneUI;
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

    void Awake()
    {
        readyflag = false;
        sound_notice = GameObject.Find("sound_notice");
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
        int itemindex = 3;
        GlobalData.g_global.itemIndex = itemindex;


        GlobalData.g_global.sheetInfo.monsterId = GlobalData.g_global.myPlayCard;

        m_lobbySceneUI = gameObject.GetComponent<LobbySceneUI>();
        callCharge();
        m_bReady = false;
        m_enterLobby = false;

        m_startCountDown = 5;
    }



    void Update()
    {
        //if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMatchIngComplete)
        if (GlobalData.g_global.socketState == (int)SocketClass.BingoState.mGameJoinIngComplete)
            //
        {
            //GlobalData.g_global.socketState = (int)SocketClass.STATE.mMatchIng;
            GlobalData.g_global.socketState = (int)SocketClass.BingoState.mGameJoinIng;
            //

            playStart();
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mFriendInviteAgreeNoticeComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteCancelRequest;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mFriendInviteCancelRequest);
        }
        //else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMatchSuccessNoticeComplete)
        //{
        //    m_lobbySceneUI.btn_test.GetComponent<BoxCollider>().enabled = false;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (GlobalData.g_global.tourPlayer_count == (i + 1))
        //        {
        //            break;
        //        }
        //    }
        //    GlobalData.g_global.socketState = (int)SocketClass.STATE.mGameStartIng;
        //    Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mGameStartRequest);
        //}
        else if (GlobalData.g_global.socketState == (int)SocketClass.BingoState.mGameStartIngComplete)
        {
            m_lobbySceneUI.btn_test.GetComponent<BoxCollider>().enabled = false;

            //GlobalData.g_global.socketState = (int)SocketClass.STATE.mGameStartIng;
            //Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mGameStartRequest);

            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            StartCoroutine(waitStart());
        }
            //
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMatchFailIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mMatchIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMatchRequest);
        }

        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mGameStartNoticeIngComplete)
        {
            //makeOtherSheet();
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mReadycompleteIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mReadycompleteRequest);
        }

        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mReadycompleteNoticeIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            StartCoroutine(waitStart());
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mOtherOutIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            Application.LoadLevel("LobbyScene");
        }
    }

    private IEnumerator waitStart()
    {
        yield return new WaitForSeconds(2f);

        Resources.UnloadUnusedAssets();
        System.GC.Collect();

        //Application.LoadLevel("PlayScene");
        Application.LoadLevel("PlayBlitzScene");
    }

    public void onReady(int sheetCount)
    {
        m_bReady = true;
        //makeMySheet();

        activeReadyPopup();

        //GlobalData.g_global.socketState = (int)SocketClass.STATE.mMatchIng;
        //Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMatchRequest);
        GlobalData.g_global.socketState = (int)SocketClass.BingoState.mGameJoinIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.BingoState.mGameJoinRequest);
        //
    }
    private void activeReadyPopup()
    {
        BoxCollider[] buttons = m_lobbySceneUI.uiRoot.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        BoxCollider[] itemButtons = m_lobbySceneUI.m_layerItem.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < itemButtons.Length; ++i)
        {
            itemButtons[i].enabled = false;
        }

        m_lobbySceneUI.m_waitPopup.gameObject.SetActive(true);

        m_lobbySceneUI.tip_label.GetComponent<UILabel>().text = Ment.bingoTip[Random.Range(0, 19)];

        BoxCollider[] popupButtons = m_lobbySceneUI.m_waitPopup.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < popupButtons.Length; ++i)
        {
            popupButtons[i].enabled = true;
        }

        sound_notice.GetComponent<AudioSource>().Play();


        iTween.MoveTo(m_lobbySceneUI.m_waitPopup.gameObject, iTween.Hash("x", 0, "y", 0, "easeType", "easeOutElastic", "time", 1));
    }


    private void activeStart()
    {
        m_lobbySceneUI.mentWait.gameObject.SetActive(false);
        m_lobbySceneUI.mentRetry.gameObject.SetActive(false);
        m_lobbySceneUI.mentStart.gameObject.SetActive(true);

        StartCoroutine(activeStartCountDonw());
    }


    private IEnumerator activeStartCountDonw()
    {

        //대기타임이 2초 이상일때는 나가는 것이 가능함
        //if (GlobalData.g_global.lobby_waitTime > 2)
        if (GlobalData.g_global.mWaitingRoomJoinGameRemainSec > 2)
            //
        {
            m_lobbySceneUI.btn_test.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            m_lobbySceneUI.btn_test.GetComponent<BoxCollider>().enabled = false;
        }

        //--GlobalData.g_global.lobby_waitTime;
        //if (GlobalData.g_global.lobby_waitTime <= 0)
        //{
        //    GlobalData.g_global.lobby_waitTime = 0;
        //}
        //m_lobbySceneUI.countDown.GetComponent<UILabel>().text = GlobalData.g_global.lobby_waitTime.ToString();
        if (GlobalData.g_global.mWaitingRoomJoinGameRemainSec <= 0)
        {
            GlobalData.g_global.mWaitingRoomJoinGameRemainSec = 0;
        }
        m_lobbySceneUI.countDown.GetComponent<UILabel>().text = GlobalData.g_global.mWaitingRoomJoinGameRemainSec.ToString();
        //

        iTween.ScaleTo(m_lobbySceneUI.countDown.gameObject, iTween.Hash("x,", 1.2f, "y", 1.3f, "oncompletetarget", gameObject, "oncomplete", "completeScaling", "time", 0.3f));
        yield return new WaitForSeconds(1.0f);

        StartCoroutine("activeStartCountDonw");

    }

    private void callCharge()
    {
        StartCoroutine(time(GlobalData.g_global.myInfo.waitTime));
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

        GlobalData.g_global.myInfo.waitTime = waitTime - 1;

        if (GlobalData.g_global.myInfo.waitTime < 1)
        {
            GlobalData.g_global.myInfo.waitTime = 480;

            //GlobalData.g_global.myInfo.ticketCount++;
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mTicketChargeIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mTicketChargeRequest);
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

    private void makeMySheet()
    {
        int[] Blist = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] Ilist = { 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
        int[] Nlist = { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
        int[] Glist = { 46, 47, 48, 49, 50, 51, 52, 53, 54, 55 };
        int[] Olist = { 61, 62, 63, 64, 65, 66, 67, 68, 69, 70 };

        int emp = 0;

        // 시트 초기화
        for (int i = 0; i < 25; i++)
        {
            GlobalData.g_global.sheetInfo.sheet[0, i] = 0;
            GlobalData.g_global.sheetInfo.sheet[1, i] = 0;
            GlobalData.g_global.sheetInfo.sheet[2, i] = 0;
            GlobalData.g_global.sheetInfo.sheet[3, i] = 0;
        }

        GlobalData.g_global.sheetInfo.shield = 0;
        GlobalData.g_global.myShield = 0;
        for (int i = 0; i < 4; i++)
        {
            GlobalData.g_global.sheetInfo.bingoSheet[i] = false;
        }


        //생성

        for (int k = 0; k < GlobalData.g_global.sheetInfo.activeSheetCount; k++)
        {

            for (int i = 0; i < 10; i++)
            {
                int temp = Random.Range(0, 10);
                int temp2 = Blist[i];
                Blist[i] = Blist[temp];
                Blist[temp] = temp2;

                temp2 = Ilist[i];
                Ilist[i] = Ilist[temp];
                Ilist[temp] = temp2;

                temp2 = Nlist[i];
                Nlist[i] = Nlist[temp];
                Nlist[temp] = temp2;

                temp2 = Glist[i];
                Glist[i] = Glist[temp];
                Glist[temp] = temp2;

                temp2 = Olist[i];
                Olist[i] = Olist[temp];
                Olist[temp] = temp2;
            }


            for (int i = 0; i < 25; i++)
            {
                emp = i / 5;
                //////  BBBBBBBBBBBBBBB
                if (i == 0 || i == 5 || i == 10 || i == 15 || i == 20)
                {
                    GlobalData.g_global.sheetInfo.sheet[k, i] = Blist[emp];
                }
                /////// iiiiiiiiiiiiiiii
                else if (i == 1 || i == 6 || i == 11 || i == 16 || i == 21)
                {
                    GlobalData.g_global.sheetInfo.sheet[k, i] = Ilist[emp];
                }
                /////// NNNNNNNNNNNNNNNN
                else if (i == 2 || i == 7 || i == 12 || i == 17 || i == 22)
                {
                    GlobalData.g_global.sheetInfo.sheet[k, i] = Nlist[emp];
                }
                /////// GGGGGGGGGGGGGGGG
                else if (i == 3 || i == 8 || i == 13 || i == 18 || i == 23)
                {
                    GlobalData.g_global.sheetInfo.sheet[k, i] = Glist[emp];
                }
                /////// OOOOOOOOOOOOOOOOO
                else if (i == 4 || i == 9 || i == 14 || i == 19 || i == 24)
                {
                    GlobalData.g_global.sheetInfo.sheet[k, i] = Olist[emp];
                }
            }
        }

    }

    private void makeOtherSheet()
    {
        // 시트 세팅후
    }

    private void playStart()
    {
        StopCoroutine("activeStartCountDonw");
        StartCoroutine("activeStartCountDonw");
    }

    private void completeScaling()
    {
        iTween.ScaleTo(m_lobbySceneUI.countDown.gameObject, Vector3.one, 0.3f);
    }

}

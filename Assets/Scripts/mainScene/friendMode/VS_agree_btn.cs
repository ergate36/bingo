using UnityEngine;
using System.Collections;

public class VS_agree_btn : MonoBehaviour
{
    public Transform vsFriend;
    public Transform detailFriend;
    public Transform invitePanel;
    public Transform waitPanel;
    public Transform failMatch;
    public Transform failMatch_label;

    public Transform btn_test;
    public Transform countDown;
    public Transform btn_fail;

    // Use this for initialization
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mFriendInviteComplete)
        {
            GlobalData.g_global.lobby_waitTime = 10;

            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            if (GlobalData.g_global.Invite_result == 0)
            {
                GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
                iTween.ScaleTo(invitePanel.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

                GameObject mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
                BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }

                waitPanel.gameObject.SetActive(true);
                waitPanel.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(waitPanel.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(waitPanel.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", waitPanel.gameObject));
                mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_wait");
                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }
                StopCoroutine("activeStartCountDonw");
                StartCoroutine("activeStartCountDonw");
            }

            else
            {
                friendCancel();
            }

        }
            /*
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mFriendInviteCancelComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            iTween.ScaleTo(waitPanel.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

            friendCancel();
        }
            */
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mFriendInviteCancelNoticeComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            iTween.ScaleTo(waitPanel.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

            friendCancel();
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mFriendSuccessNoticeComplete)
        {
            btn_test.GetComponent<BoxCollider>().enabled = false;
            
            GlobalData.g_global.modeIndex = 3;
            GlobalData.g_global.betting_index = 0;
            GlobalData.g_global.gameType = 1;
            GlobalData.g_global.serverFlag = 2;
            Socket_Ctrl.sCtrl.StartClient();

            GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendPlayIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mFriendPlayRequest);
        }

        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMatchFailIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            // exit

            btn_test.GetComponent<BoxCollider>().enabled = true;

            iTween.ScaleTo(waitPanel.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        }

        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMatchSuccessNoticeComplete)
        {
            StopCoroutine("activeStartCountDonw");
            btn_test.GetComponent<BoxCollider>().enabled = false;

            Debug.Log("readyC");

            GlobalData.g_global.socketState = (int)SocketClass.STATE.mGameStartIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mGameStartRequest);
        }

        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mGameStartNoticeIngComplete)
        {
            Debug.Log("gameC");
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mReadycompleteIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mReadycompleteRequest);
        }

        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mReadycompleteNoticeIngComplete)
        {
            Debug.Log("readyC");
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            StartCoroutine(waitStart());
        }
    }

    private IEnumerator waitStart()
    {
        yield return new WaitForSeconds(2f);
        Application.LoadLevel("PlayScene");
    }

    void OnClick()
    {
        if(GlobalData.g_global.myInfo.ticketCount < GlobalData.g_global.selectSheet){

            failMatch.gameObject.SetActive(true);
            failMatch_label.GetComponent<UILabel>().text = "Ticket is low.you must buy ticket.";
            btn_fail.GetComponent<BoxCollider>().enabled = true;
            failMatch.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(failMatch.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(failMatch.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", failMatch.gameObject));
                
            return;
        }
        if (GlobalData.g_global.selectSheet != 0)
        {
            GlobalData.g_global.sheetInfo.activeSheetCount = GlobalData.g_global.selectSheet;
            makeMySheet();
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mFriendInviteRequest);
        }
    }
    

    private IEnumerator activeStartCountDonw()
    {

        //대기타임이 2초 이상일때는 나가는 것이 가능함
        if (GlobalData.g_global.lobby_waitTime > 2)
        {
            btn_test.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            btn_test.GetComponent<BoxCollider>().enabled = false;
        }

        --GlobalData.g_global.lobby_waitTime;
        if (GlobalData.g_global.lobby_waitTime <= 0)
        {
            StopCoroutine("activeStartCountDonw");
            GlobalData.g_global.lobby_waitTime = 1000;
            countDown.GetComponent<UILabel>().text = "";
            if (GlobalData.g_global.popupstate ==0)
            {
                iTween.ScaleTo(waitPanel.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

                GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteCancelIng;
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mFriendInviteCancelRequest);

                friendCancel();
            }

        }
        else
        {
            countDown.GetComponent<UILabel>().text = GlobalData.g_global.lobby_waitTime.ToString();

            iTween.ScaleTo(countDown.gameObject, iTween.Hash("x,", 1.2f, "y", 1.3f, "oncompletetarget", gameObject, "oncomplete", "completeScaling", "time", 0.3f));
            yield return new WaitForSeconds(1.0f);

            StartCoroutine("activeStartCountDonw");
        }
    }

    private void friendCancel()
    {
        StopCoroutine("activeStartCountDonw");

        GlobalData.g_global.popupstate = 1;
        GlobalData.g_global.Invite_result = 0;
        
        failMatch.gameObject.SetActive(true);

        failMatch_label.GetComponent<UILabel>().text = "match failed. please try agin.";
        btn_fail.GetComponent<BoxCollider>().enabled = true;
        failMatch.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(failMatch.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(failMatch.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", failMatch.gameObject));
                
    }
    private void completeScaling()
    {
        iTween.ScaleTo(countDown.gameObject, Vector3.one, 0.3f);
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


}

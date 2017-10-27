using UnityEngine;
using System.Collections;

public class Reward_myclose_btn : MonoBehaviour {

	// Use this for initialization
    private GameObject popup;
    private Transform popupup;
    private GameObject mainSceneUI;
    private BoxCollider[] buttons;
    void Awake()
    {
    }

    void Start()
    {
    }
    void OnClick()
    {
        GlobalData.g_global.invite_able = true;

        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_daily");
        iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
        callCharge();
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

        UILabel label2 = mainSceneUI.transform.Find("layer_base/chargeTime_label").GetComponent<UILabel>();
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

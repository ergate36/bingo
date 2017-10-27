using UnityEngine;
using System.Collections;

public class LobbySheet_btn : MonoBehaviour {

    private LobbyScene lobbyScene;
    public GameObject popup;
    public Transform popup_ticket;
    private GameObject obj;
    public enum eSheetCount
    {
        SheetCount_01 = 1,
        SheetCount_02,
        SheetCount_03,
        SheetCount_04,
    }

    public eSheetCount sheetCount;

    void Update()
    {
        //if(GlobalData.g_global.socketState ==(int)SocketClass.STATE.mGameServerJoinIngComplete){
        //    GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
        //    GameObject obj = GameObject.Find("lobbyBlitzScene") as GameObject;
        //    obj.GetComponent<LobbyScene>().onReady((int)sheetCount);
        //}
        if (GlobalData.g_global.socketState == (int)SocketClass.BingoState.mWaitingRoomJoinIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            GameObject obj = GameObject.Find("lobbyBlitzScene") as GameObject;
            obj.GetComponent<LobbyScene>().onReady((int)sheetCount);
        }
        //
    }

    void Start()
    {
        GameObject obj = GameObject.Find("lobbyBlitzScene") as GameObject;
        lobbyScene = obj.GetComponent<LobbyScene>();
    }

    void OnClick()
    {

        if (lobbyScene.m_bReady)
           return;

        GameObject obj = GameObject.Find("lobbyBlitzScene") as GameObject;

        if (GlobalData.g_global.myInfo.ticketCount >= (int)sheetCount)
        {
            if (GlobalData.g_global.myInfo.coinAmount < GlobalData.g_global.admission)
            {
                GameObject mainSceneUI = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/lobby_layer_Item");

                BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }


                mainSceneUI = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/lobbyBase");

                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }

                popup = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor") as GameObject;
                popup_ticket = popup.transform.Find("popup_coin");
                popup_ticket.gameObject.SetActive(true);
            }

            else
            {
                GlobalData.g_global.serverFlag = 2;
                Socket_Ctrl.sCtrl.StartClient();
                GlobalData.g_global.selectSheet = (int)sheetCount;
                GlobalData.g_global.sheetInfo.activeSheetCount = (int)sheetCount;
                //GlobalData.g_global.socketState = (int)SocketClass.STATE.mGameServerJoinIng;
                //Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mGameServerJoinRequest);
                GlobalData.g_global.socketState = (int)SocketClass.BingoState.mWaitingRoomJoinIng;
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.BingoMsg.mWaitingRoomJoinRequest);
                //
            }
        }

        else
        {

            GameObject mainSceneUI = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/lobby_layer_Item");

            BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }


            mainSceneUI = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/lobbyBase");

            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }


            popup = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor") as GameObject;
            popup_ticket = popup.transform.Find("popup_ticket");
            popup_ticket.gameObject.SetActive(true);
        }
    }
}

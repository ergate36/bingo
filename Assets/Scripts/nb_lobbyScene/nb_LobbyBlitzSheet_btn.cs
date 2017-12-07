using UnityEngine;
using System.Collections;

public class nb_LobbyBlitzSheet_btn : MonoBehaviour {

    private nb_LobbyBlitzScene lobbyScene;
    public GameObject popup;
    public GameObject soundObj;
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

    //void Update()
    //{
    //    if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.BlitzEnterGameResponse_End)
    //    {
    //        nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
    //        GameObject obj = GameObject.Find("lobbyBlitzScene") as GameObject;
    //        obj.GetComponent<LobbyBlitzScene>().onReady((int)sheetCount);
    //    }
    //    //
    //}

    void Start()
    {
        GameObject obj = GameObject.Find("lobbyScene") as GameObject;
        lobbyScene = obj.GetComponent<nb_LobbyBlitzScene>();
    }

    void OnClick()
    {

        if (lobbyScene.m_bReady)    //대기열 등록 중 일땐 클릭 안됨
        {
            Debug.Log("is Waitting...");
            return;
        }

        var sound = soundObj.GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.Play();
        }

        GameObject obj = GameObject.Find("lobbyScene") as GameObject;

        if (nb_GlobalData.g_global.myInfo.ticketCount >= (int)sheetCount)
        {
            if (nb_GlobalData.g_global.myInfo.coinAmount < nb_GlobalData.g_global.admission)
            {
                GameObject mainSceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/lobby_layer_Item");

                BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }


                mainSceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/nb_lobbyBase");

                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }

                popup = GameObject.Find("lobbySceneUI/Camera/Anchor") as GameObject;
                popup_ticket = popup.transform.Find("popup_coin");
                popup_ticket.gameObject.SetActive(true);
            }

            else
            {
                nb_GlobalData.g_global.serverFlag = 2;
                nbSocket.sCtrl.StartClient();
                nb_GlobalData.g_global.sheetInfo.activeSheetCount = (int)sheetCount;
                nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.BlitzEnterGameRequest);
                nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzEnterGameRequest_End;
            }
        }

        else
        {
            //todo: 입장 재화 부족



            //GameObject mainSceneUI = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/lobby_layer_Item");

            //BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

            //for (int i = 0; i < buttons.Length; ++i)
            //{
            //    buttons[i].enabled = false;
            //}

            GameObject mainSceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/nb_lobbyBase");
            //mainSceneUI = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/lobbyBase");

            //buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

            //for (int i = 0; i < buttons.Length; ++i)
            //{
            //    buttons[i].enabled = false;
            //}


            //popup = GameObject.Find("lobbySceneUI/Camera/Anchor") as GameObject;
            //popup_ticket = popup.transform.Find("popup_ticket");
            //popup_ticket.gameObject.SetActive(true);
        }
    }
}

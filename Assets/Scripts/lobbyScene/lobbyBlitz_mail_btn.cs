using UnityEngine;
using System.Collections;

public class lobbyBlitz_mail_btn : MonoBehaviour {

    private GameObject popup;
    private Transform popupup;
    private GameObject lobbyUI;
    private BoxCollider[] buttons;
    private GameObject root;
    private Lobby_MailBoxCtrl mailboxctrl;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMailGetIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            mailboxctrl.mailClear();
            mailboxctrl.mailling();

        }
    }
    void OnClick()
    {
        GlobalData.g_global.serverFlag = 1;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMailGetIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMailGetRequest);

        popup = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_mail");
        popupup.gameObject.SetActive(true);
        popupup.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(popupup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popupup.gameObject));
        
        root = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/popup_mail/mailList");
        mailboxctrl = root.GetComponent<Lobby_MailBoxCtrl>();
        lobbyUI = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/lobby_layer_Item");
        buttons = lobbyUI.GetComponentsInChildren<BoxCollider>();
        
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }
        
        lobbyUI = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/lobbyBase");
        buttons = lobbyUI.GetComponentsInChildren<BoxCollider>();
        
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }
    }
}

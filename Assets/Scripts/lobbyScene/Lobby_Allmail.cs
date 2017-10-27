using UnityEngine;
using System.Collections;
public class Lobby_Allmail : MonoBehaviour {

    Lobby_MailBoxCtrl mailboxctrl;
    private GameObject root;
    private GameObject root2;

    // Use this for initialization
    void Start()
    {
        root = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_mail/mailList/Grid");
        root2 = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_mail/mailList");
        mailboxctrl = root2.GetComponent<Lobby_MailBoxCtrl>();
    }

    void OnClick()
    {
       
        getMail();
    }

    void getMail()
    {
        GlobalData.g_global.getMailType = 1;

        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMailCheckIng;

        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMailCheckRequest);
        GlobalData.g_global.m_mailInfo.Clear();
        mailboxctrl.mailClear();
        mailboxctrl.reFresh();
    }  
}

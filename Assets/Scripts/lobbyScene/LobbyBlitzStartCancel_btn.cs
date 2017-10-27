using UnityEngine;
using System.Collections;

public class LobbyBlitzStartCancel_btn : MonoBehaviour {

    void Update()
    {
        if (nb_GlobalData.g_global.socketState == (int)nb_SocketClass.STATE.mMatchCancelIng)
        {
            nb_GlobalData.g_global.serverFlag = 1;
            nbSocket.sCtrl.StartClient();

            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;

            Resources.UnloadUnusedAssets();
            System.GC.Collect(); 

            Application.LoadLevel("LobbyBlitzScene");
        }
    }


    void OnClick()
    {
        //GlobalData.g_global.socketState = (int)SocketClass.STATE.mMatchCancelRequest;
        nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mMatchCancelIng;
        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.MatchCancelRequest);
    }
}

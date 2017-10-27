using UnityEngine;
using System.Collections;

public class LobbyStartCancel_btn : MonoBehaviour {

    void Update()
    {
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMatchCancelIng)
        {
            GlobalData.g_global.serverFlag = 1;
            Socket_Ctrl.sCtrl.StartClient();

            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            Resources.UnloadUnusedAssets();
            System.GC.Collect(); 

            Application.LoadLevel("LobbyScene");
        }
    }


    void OnClick()
    {
        //GlobalData.g_global.socketState = (int)SocketClass.STATE.mMatchCancelRequest;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMatchCancelIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMatchCancelRequest);
    }
}

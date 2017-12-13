using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nb_killserver_btn : MonoBehaviour
{
    bool killingServer = false;

    void OnClick()
    {
        if (killingServer == true)
        {
            Debug.Log("killingServer Progress");
            return;
        }

        //서버 주거라
        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.KillServerRequest);
        killingServer = true;
    }
}

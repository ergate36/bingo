using UnityEngine;
using System.Collections;

public class nb_BattleLobbyFullScreen_btn : MonoBehaviour
{
    GameObject lobbyScene;

    void Start()
    {
        lobbyScene = GameObject.Find("battleLobbyScene") as GameObject;
        
    }

    void OnClick()
    {
        //if (Screen.fullScreen)
        //{
        //    Screen.SetResolution(Screen.height, Screen.width, false);
        //}
        //else
        //{
        //    Screen.SetResolution(Screen.height, Screen.width, true);
        //}
        //Debug.Log("fullScreen button Click! h : " + Screen.height
        //    + ", w : " + Screen.width + ", isFull : " + Screen.fullScreen.ToString());

        if (lobbyScene.GetComponent<nb_BattleLobbyScene>().killingServer == true)
        {
            Debug.Log("killingServer Progress");
            return;
        }

        //서버 주거라
        nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.KillServerRequest);
        lobbyScene.GetComponent<nb_BattleLobbyScene>().killingServer = true;
    }
}

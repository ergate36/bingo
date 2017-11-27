using UnityEngine;
using System.Collections;

public class nb_SoundCtrl : MonoBehaviour {
	// Use this for initialization
    //public GameObject root;
    //public GameObject root2;
	void Start () {
        //root2.gameObject.SetActive(false);
        //root.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        //if(GlobalData.g_global.servertesting == true){
        //    GlobalData.g_global.servertesting = false;
        //    root2.gameObject.SetActive(true);
        //    root2.transform.Find("Label").GetComponent<UILabel>().text = "A CHECK OF the server";
        //}
        if (nb_GlobalData.g_global.socketState == (int)SocketClass.STATE.connectionFail
            || nb_GlobalData.g_global.socketState == (int)SocketClass.STATE.excessTime)
        {
            //root2.gameObject.SetActive(true);
            nb_GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
        }
        if (nb_GlobalData.g_global.socketState == (int)SocketClass.STATE.NaverconnectionFail)
        {
            //root2.gameObject.SetActive(true);
            //Transform label = root2.transform.Find("Label");
            //label.GetComponent<UILabel>().text = "Failed to login with Facebook.";
            nb_GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
        }
        nb_GlobalData.g_global.soundFlg = PlayerPrefs.GetInt("soundFlg");

        if (nb_GlobalData.g_global.soundFlg == 1)
        {
            AudioListener.volume = 0;        
        }
        else
        {
            AudioListener.volume = 1f;            
        }

        if(Input.GetKeyUp(KeyCode.Escape)){
            // popup 
            string sceneName = Application.loadedLevelName;
            if (sceneName == "TournamentScene" || sceneName == "PlayScene" ||
                sceneName == "PlayBlitzScene")
            {
            }
            else
            {
                nb_GlobalData.g_global.invite_able = false;
                //root.SetActive(true);
            }
            
        }
	}
}

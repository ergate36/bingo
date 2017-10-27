using UnityEngine;
using System.Collections;

public class SoundCtrl : MonoBehaviour {
	// Use this for initialization
    public GameObject root;
    public GameObject root2;
	void Start () {
        root2.gameObject.SetActive(false);
        root.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(GlobalData.g_global.servertesting == true){
            GlobalData.g_global.servertesting = false;
            root2.gameObject.SetActive(true);
            root2.transform.Find("Label").GetComponent<UILabel>().text = "A CHECK OF the server";
        }
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.connectionFail 
            || GlobalData.g_global.socketState == (int)SocketClass.STATE.excessTime)
        {
            root2.gameObject.SetActive(true);
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
        }
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.NaverconnectionFail)
        {
            root2.gameObject.SetActive(true);
            Transform label = root2.transform.Find("Label");
            label.GetComponent<UILabel>().text = "Failed to login with Facebook.";
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
        }
        GlobalData.g_global.soundFlg=PlayerPrefs.GetInt("soundFlg");
        
        if (GlobalData.g_global.soundFlg == 1)
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
                GlobalData.g_global.invite_able = false;
                root.SetActive(true);
            }
            
        }
	}
}

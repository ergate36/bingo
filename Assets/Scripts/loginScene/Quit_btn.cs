using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Quit_btn : MonoBehaviour {
    public int flag;
    public GameObject root;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        if (flag == 1)
        {
            ProcessThreadCollection pt = Process.GetCurrentProcess().Threads;

            foreach (ProcessThread p in pt)
            {
                p.Dispose();
            }

            System.Diagnostics.Process.GetCurrentProcess().Kill();

            Application.Quit();
        }
        else if(flag == 2)
        {

            ProcessThreadCollection pt = Process.GetCurrentProcess().Threads;

            foreach (ProcessThread p in pt)
            {
                p.Dispose();
            }

            System.Diagnostics.Process.GetCurrentProcess().Kill();

            Application.Quit();
        }
        else if (flag == 3)
        {
            root.gameObject.SetActive(false);

            GlobalData.g_global.socketState = (int)SocketClass.STATE.mBingoResultIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mBingoResultRequest);
        }
        else
        {
            root.SetActive(false);
        }
    }
}

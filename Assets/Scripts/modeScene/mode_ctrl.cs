using UnityEngine;
using System.Collections;

public class mode_ctrl : MonoBehaviour {
    public int mode;
    public Transform root;
    public Transform closeRoot;
    private BoxCollider[] buttons;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        GlobalData.g_global.invite_able = false;

        if (mode == 1)
        {
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            //Application.LoadLevel("LobbyScene");
            Application.LoadLevel("LobbyBlitzScene");
            //
        }
        else if(mode ==2){
            //취소
            buttons = root.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            iTween.ScaleTo(closeRoot.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        }
    }
}

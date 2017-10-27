using UnityEngine;
using System.Collections;

public class UI_JoinBtn : MonoBehaviour {
    private GameObject popup;
    private Transform popupup;

    private Transform item_score;
    private Transform notem_score;

    private GameObject mainSceneUI;
    private BoxCollider[] buttons;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
	void OnClick()
    {
        GlobalData.g_global.invite_able = false;

        //popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        //popupup = popup.transform.Find("popup_start");
        //popupup.gameObject.SetActive(true);

        //popupup.transform.localScale = Vector3.one * 0.7f;
        //iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        //iTween.ValueTo(popupup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popupup.gameObject));
        
        //mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        //buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        //for (int i = 0; i < buttons.Length; ++i)
        //{
        //    buttons[i].enabled = false;
        //}

        GlobalData.g_global.modeIndex = 3;
        GlobalData.g_global.betting_index = 0;
        GlobalData.g_global.gameType = 1;
        Application.LoadLevel("LobbyBlitzScene");

        //
        

        Resources.UnloadUnusedAssets();
        System.GC.Collect();

        //GlobalData.g_global.modeIndex = 3;

       // Application.LoadLevel("LobbyScene");
    }
}

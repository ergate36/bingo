using UnityEngine;
using System.Collections;

public class ranking_popup_close : MonoBehaviour {
    private GameObject popup;
    private Transform popupup;
    private GameObject mainSceneUI;
    private BoxCollider[] buttons;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        GlobalData.g_global.invite_able = true;
        GlobalData.g_global.detail_index = 0;
        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_ranking");
        iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }

    }
}

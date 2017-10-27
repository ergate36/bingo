using UnityEngine;
using System.Collections;

public class failMatch : MonoBehaviour {
    public Transform closePopup;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        GlobalData.g_global.popupstate = 0;
        GlobalData.g_global.invite_able = true;
        GameObject mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor");
        BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
        iTween.ScaleTo(closePopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
    }
}

using UnityEngine;
using System.Collections;

public class openGacha : MonoBehaviour {
    public Transform openPop;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        openPop.gameObject.SetActive(true);
        GlobalData.g_global.invite_able = false;
        openPop.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(openPop.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(openPop.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", openPop.gameObject));
        GameObject mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        // popup open
    }
}

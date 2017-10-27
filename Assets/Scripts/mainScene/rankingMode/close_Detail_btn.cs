using UnityEngine;
using System.Collections;

public class close_Detail_btn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        GameObject popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        Transform vsFriend = popup.transform.Find("popup_ranking");
        BoxCollider[] buttons = vsFriend.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
        vsFriend = popup.transform.Find("popup_ranking/popup_frienddetail");
        iTween.ScaleTo(vsFriend.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
    }
}

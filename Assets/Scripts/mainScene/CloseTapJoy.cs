using UnityEngine;
using System.Collections;

public class CloseTapJoy : MonoBehaviour {

    public Transform ClosePopup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        GameObject mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");

        BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }

        iTween.ScaleTo(ClosePopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

        ClosePopup.gameObject.SetActive(false);
    }
}

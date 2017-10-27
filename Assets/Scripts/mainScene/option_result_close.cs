using UnityEngine;
using System.Collections;

public class option_result_close : MonoBehaviour {
    public Transform popup;
    public Transform popupup;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        iTween.ScaleTo(popup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
       BoxCollider[] buttons = popupup.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
    }
}

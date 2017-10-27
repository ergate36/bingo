using UnityEngine;
using System.Collections;

public class facebookCancel : MonoBehaviour {

    public Transform closeFace;
    public Transform btnBase;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {

        BoxCollider[] buttons = btnBase.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
        iTween.ScaleTo(closeFace.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

    }
}

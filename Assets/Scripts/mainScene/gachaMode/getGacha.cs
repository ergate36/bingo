using UnityEngine;
using System.Collections;

public class getGacha : MonoBehaviour {

    public Transform close1;
    public Transform close2;
    public Transform main;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        GlobalData.g_global.invite_able = true;

        iTween.ScaleTo(close1.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

        BoxCollider[] buttons = main.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }

        close2.gameObject.SetActive(false);
    }
}

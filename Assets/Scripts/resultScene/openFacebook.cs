using UnityEngine;
using System.Collections;

public class openFacebook : MonoBehaviour {

    public Transform openFace;
    public Transform btnBase;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        openFace.gameObject.SetActive(true);
        openFace.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(openFace.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(openFace.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", openFace.gameObject));

        BoxCollider[] buttons = btnBase.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        buttons = openFace.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }


    }
}

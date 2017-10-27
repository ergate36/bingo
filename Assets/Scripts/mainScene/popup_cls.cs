using UnityEngine;
using System.Collections;

public class popup_cls : MonoBehaviour {
    public Transform root;
    public Transform rootbox;
    public Transform popup_back;
    private BoxCollider[] buttons;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {

        root.gameObject.SetActive(false);
        popup_back.gameObject.SetActive(false);
        buttons = rootbox.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }


    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class nb_login_poup_btn : MonoBehaviour
{
    public GameObject loginPopup;

    UILabel inputTextLabel;

	// Use this for initialization
	void Start ()
    {
        inputTextLabel = loginPopup.transform.Find("input").Find("input_text").GetComponent<UILabel>();
	}
	
	// Update is called once per frame
    void Update () 
    {
	}

    void OnClick()
    {
        if (inputTextLabel.text == "" ||
            inputTextLabel.text == "click here")
        {
            Debug.Log("nb_login_poup_btn : empty text");
            return;
        }
        loginPopup.SetActive(false);

        Debug.Log("nb_login_poup_btn : CreateUserSession Start");

        nb_GlobalData.g_global.InputSessionLoginKey = inputTextLabel.text;

        nbHttp.http.CreateUserSession();
    }
}

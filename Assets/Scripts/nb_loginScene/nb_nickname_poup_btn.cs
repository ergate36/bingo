using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class nb_nickname_poup_btn : MonoBehaviour
{
    public GameObject nicknamePopup;

    UILabel inputTextLabel;

	// Use this for initialization
	void Start ()
    {
        inputTextLabel = nicknamePopup.transform.Find("input").FindChild("input_text").GetComponent<UILabel>();
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
            Debug.Log("nb_nickname_poup_btn : empty text");
            return;
        }

        nb_GlobalData.g_global.InputSocialNickname = inputTextLabel.text;

        nicknamePopup.SetActive(false);

        Debug.Log("nb_nickname_poup_btn : CreateUserAccount Start");

        //입력한 닉네임으로 계정 생성
        nbHttp.http.CreateUserAccount(
            nb_GlobalData.g_global.userSession.SessionKey,
            nb_GlobalData.g_global.InputSocialNickname);
    }
}

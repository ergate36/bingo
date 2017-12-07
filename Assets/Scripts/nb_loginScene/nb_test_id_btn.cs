using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class nb_test_id_btn : MonoBehaviour
{
    private GameObject uiRoot;
    public GameObject selectPopup;

	// Use this for initialization
	void Start () {
        uiRoot = GameObject.Find("loginSceneUI/Camera/Anchor/Panel") as GameObject;
	}

    void OnClick()
    {
        nb_GlobalData.g_global.fb_active = false;

        GameObject popup = GameObject.Find("loginSceneUI/Camera/Anchor/popup_login");
        popup.SetActive(true);

        selectPopup.SetActive(false);

        //ticker test
        //nb_GlobalData.g_global.myInfo.ticketCount = 999;
        //
    }
}

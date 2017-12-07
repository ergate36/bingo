using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class nb_test_fb_btn : MonoBehaviour
{
    public GameObject selectPopup;
    public nbFB nbFBObject;

	// Use this for initialization
	void Start () {
	}

    void OnClick()
    {
        nb_GlobalData.g_global.fb_active = true;

        nbFBObject.startFB();

        selectPopup.SetActive(false);

        //ticker test
        //nb_GlobalData.g_global.myInfo.ticketCount = 999;
        //
    }
}

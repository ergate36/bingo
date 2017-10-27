using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class loginStart_btn : MonoBehaviour {

    private Transform progress;
    private GameObject pro;
    private int flag;
    private Transform click;
    private nb_MyCard myCard;

    private GameObject uiRoot;

	// Use this for initialization
	void Start () {
        uiRoot = GameObject.Find("loginSceneUI/Camera/Anchor/Panel") as GameObject;
        //click = uiRoot.transform.Find("click");
        flag = 0;
	}
	
	// Update is called once per frame
	/*
    void Update () {
        if(flag == 1){
            pro.GetComponent<UISlider>().value = GlobalData.g_global.progress;
            if (pro.GetComponent<UISlider>().value == 1)
            {
                flag = 0;
                getMyCard();
            }
        }
	}
    */

    void OnClick()
    {
        //nbHttp.http.CreateUserSession();  //팝업에서 처리

        GameObject popup = GameObject.Find("loginSceneUI/Camera/Anchor/popup_login");
        popup.SetActive(true);

        uiRoot.transform.Find("bg").GetComponent<BoxCollider>().enabled = false;

        //ticker test
        nb_GlobalData.g_global.myInfo.ticketCount = 999;
        //
    }
}

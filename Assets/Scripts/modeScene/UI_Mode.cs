using UnityEngine;
using System.Collections;

public class UI_Mode : MonoBehaviour {
    public int mode;
    private GameObject mainSceneUI;
    private BoxCollider[] buttons;
    private Transform popup_other;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        GlobalData.g_global.invite_able = false;

        GameObject popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        Transform popupup = popup.transform.Find("popup_startmode");

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }
        StopAllCoroutines();

        Resources.UnloadUnusedAssets();
        System.GC.Collect(); 

        if( mode ==1 ){
            GlobalData.g_global.modeIndex = 3;
            GlobalData.g_global.betting_index = 0;
            GlobalData.g_global.gameType = 1;
            //Application.LoadLevel("LobbyScene");
            Application.LoadLevel("LobbyBlitzScene");
            //
        }

        else if(mode == 2)
        {
            if (GlobalData.g_global.myInfo.coinAmount < 500)
            {

                    mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode");
                    buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = false;
                    }

                    popup_other = popupup.transform.Find("popup_link");
                   
                    popup_other.gameObject.SetActive(true);
                    popup_other.transform.localScale = Vector3.one * 0.7f;
                    iTween.ScaleTo(popup_other.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                    iTween.ValueTo(popup_other.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup_other.gameObject));

                    mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode/popup_link");
                    buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = true;
                    }

                    return;
            }


            else
            {
                GlobalData.g_global.admission = 500;
                mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode");
                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }

                popup_other = popupup.transform.Find("popup_check");
                popup_other.gameObject.SetActive(true);
                popup_other.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(popup_other.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(popup_other.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup_other.gameObject));

                popupup.transform.Find("popup_check/Label").GetComponent<UILabel>().text = "Admission 500 COIN is spent\nwhen participating in the game.\nWould you like to participate\nin the game?";

                mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode/popup_check");
                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }
                GlobalData.g_global.modeIndex = 2;
                GlobalData.g_global.gameType = 2;
                GlobalData.g_global.betting_index = 0;

                return;
            }
        }

        else if (mode == 3)
        {
            if (GlobalData.g_global.myInfo.coinAmount < 1000)
            {
                    mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode");
                    buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = false;
                    }

                    popup_other = popupup.transform.Find("popup_link");
                    popup_other.gameObject.SetActive(true);

                    popup_other.transform.localScale = Vector3.one * 0.7f;
                    iTween.ScaleTo(popup_other.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                    iTween.ValueTo(popup_other.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup_other.gameObject));

                    mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode/popup_link");
                    buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = true;
                    }
                    return;


            }
            else
            {
                GlobalData.g_global.admission = 1000;
                mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode");
                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                popup_other = popupup.transform.Find("popup_check");
                popup_other.gameObject.SetActive(true);
                popup_other.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(popup_other.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(popup_other.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup_other.gameObject));

                popupup.transform.Find("popup_check/Label").GetComponent<UILabel>().text = "Admission 1000 COIN is spent\nwhen participating in the game.\nWould you like to participate\nin the game?";

                mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode/popup_check");
                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }
                GlobalData.g_global.modeIndex = 2;
                GlobalData.g_global.gameType = 2;
                GlobalData.g_global.betting_index = 1;

                return;
            }
        }

        else if (mode == 4)
        {
            if (GlobalData.g_global.myInfo.coinAmount < 1500)
            {
                
                    mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode");
                    buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = false;
                    }

                    popup_other = popupup.transform.Find("popup_link");
                    popup_other.gameObject.SetActive(true);

                    popup_other.transform.localScale = Vector3.one * 0.7f;
                    iTween.ScaleTo(popup_other.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                    iTween.ValueTo(popup_other.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup_other.gameObject));

                    mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode/popup_link");
                    buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = true;
                    }
                    return;
            }

            else
            {
                GlobalData.g_global.admission = 1500;
                mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode");
                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }

                popup_other = popupup.transform.Find("popup_check");
                popup_other.gameObject.SetActive(true);
                popup_other.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(popup_other.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(popup_other.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup_other.gameObject));

                popupup.transform.Find("popup_check/Label").GetComponent<UILabel>().text = "Admission 1500 COIN is spent\nwhen participating in the game.\nWould you like to participate\nin the game?";
                mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode/popup_check");
                buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }
                GlobalData.g_global.modeIndex = 2;
                GlobalData.g_global.gameType = 2;
                GlobalData.g_global.betting_index = 2;
                return;
            }
        }

        else if (mode == 5)
        {
            GlobalData.g_global.modeIndex = 2;
            GlobalData.g_global.gameType = 2;
            GlobalData.g_global.betting_index = 0;

            // open popup
            popupup = popup.transform.Find("popup_startmode");
            popupup.gameObject.SetActive(true);
            buttons = popupup.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            popupup.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(popupup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popupup.gameObject));
        
            popupup = popup.transform.Find("popup_start");
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        }
    }

    void SaveToDaily(int t_day)
    {
        PlayerPrefs.SetInt("facebook", t_day);
    }

    int GetDaily()
    {
        return PlayerPrefs.GetInt("facebook");
    }
}

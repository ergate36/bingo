using UnityEngine;
using System.Collections;

public class week_reward_btn : MonoBehaviour {

    private GameObject popup;
    private Transform popupup;
    private GameObject mainSceneUI;
    private BoxCollider[] buttons;
    void Awake()
    {
    }

    void Start()
    {
    }
    void OnClick()
    {
        GlobalData.g_global.invite_able = false;

        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_rank");
        popupup.gameObject.SetActive(false);
        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
    }
}

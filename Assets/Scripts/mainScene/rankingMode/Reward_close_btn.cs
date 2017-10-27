using UnityEngine;
using System.Collections;

public class Reward_close_btn : MonoBehaviour {

	// Use this for initialization

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
        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_ranking/popup_rank");
        iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
    }
}

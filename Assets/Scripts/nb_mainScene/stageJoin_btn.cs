using UnityEngine;
using System.Collections;

public class stageJoin_btn : MonoBehaviour
{
    private GameObject mainSceneUI;
    private BoxCollider[] buttons;

    public int stageId;
    public bool battleMode = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
	void OnClick()
    {
        if (nb_GlobalData.g_global.MainMenuActive == true)
        {
            return;
        }

        if (nb_GlobalData.g_global.MainShopActive == true)
        {
            return;
        }

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        var sound = this.GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.Play();
        }

        Debug.Log("click stage " + stageId.ToString() + " connect button");

        nb_GlobalData.g_global.selectStageId = stageId;
        nbHttp.http.ConnectStage(
            nb_GlobalData.g_global.userSession.SessionKey, stageId, battleMode);
    }
}

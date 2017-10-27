using UnityEngine;
using System.Collections;

public class openTapjoy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        GameObject mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");

        BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        GlobalData.g_global.invite_able = false;

        Socket_Ctrl.sCtrl.closeSocket();
#if UNITY_ANDROID
        AndroidManager.Instance.openOfferWall();
        AndroidManager.Instance.initFlag();
#endif
    }
}

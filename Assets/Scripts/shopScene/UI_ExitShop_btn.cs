using UnityEngine;
using System.Collections;

public class UI_ExitShop_btn : MonoBehaviour {

    public GameObject sceneManager;
    public GameObject panelRoot;
    public ItemLayer_Ctrl itemLayerCtrl;
    public GameObject btnCtrl;
    private BoxCollider[] buttons;

    void OnClick()
    {
        if (btnCtrl != null)
        {
            buttons = btnCtrl.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
        }

        GlobalData.g_global.invite_able = true;

        itemLayerCtrl.SetItem();
        GlobalData.g_global.eventtttt = 1;
        StartCoroutine(panelRoot.GetComponent<Popup_Effector>().unactivePopup(0.5f));

    }
}

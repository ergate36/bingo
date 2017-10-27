using UnityEngine;
using System.Collections;

public class UI_ConfirmExit_btn : MonoBehaviour {

    void OnClick()
    {
        GlobalData.g_global.eventtttt = 0;
        StartCoroutine( transform.parent.GetComponent<Popup_Effector>().unactivePopup( 0.5f ) );

    }
}

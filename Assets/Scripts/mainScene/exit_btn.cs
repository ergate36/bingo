using UnityEngine;
using System.Collections;

public class exit_btn : MonoBehaviour {

    public GameObject popupRoot;

    void OnClick()
    {
        StartCoroutine(popupRoot.GetComponent<Popup_Effector>().unactivePopup(0.5f));

    }
}

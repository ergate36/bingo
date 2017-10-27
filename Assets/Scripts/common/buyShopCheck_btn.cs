using UnityEngine;
using System.Collections;

public class buyShopCheck_btn : MonoBehaviour {

    public Transform root;

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
        StartCoroutine(root.GetComponent<Popup_Effector>().unactivePopup(0.5f));
    }
}

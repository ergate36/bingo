using UnityEngine;
using System.Collections;

public class CloseErrorPopup : MonoBehaviour {

    public GameObject errorRoot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        errorRoot.gameObject.SetActive(false);
    }
}

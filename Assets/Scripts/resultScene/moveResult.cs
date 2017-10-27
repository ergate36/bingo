using UnityEngine;
using System.Collections;

public class moveResult : MonoBehaviour {

    public GameObject resultPopupRoot;
    public GameObject falsepopup;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        StartCoroutine(resultPopupRoot.GetComponent<ResultPopupCtrl>().initResut());
        falsepopup.gameObject.SetActive(false);

    }
}

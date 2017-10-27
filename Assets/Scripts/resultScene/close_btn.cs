using UnityEngine;
using System.Collections;

public class close_btn : MonoBehaviour {
	private GameObject popup;
	private Transform popupup;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnClick()
	{
		popup = GameObject.Find("resultSceneUI/Camera/Anchor") as GameObject;
		popupup = popup.transform.Find("result_popup");
		popupup.gameObject.SetActive(false);
		
	}
}

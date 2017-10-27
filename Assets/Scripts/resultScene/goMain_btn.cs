using UnityEngine;
using System.Collections;

public class goMain_btn : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnClick()
	{
        Resources.UnloadUnusedAssets();
        System.GC.Collect(); 

		Application.LoadLevel("MainScene");
	}
}

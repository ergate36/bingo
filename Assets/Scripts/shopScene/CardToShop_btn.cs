using UnityEngine;
using System.Collections;

public class CardToShop_btn : MonoBehaviour {


    MainScene mainScene;

	// Use this for initialization
	void Start () {
	
        GameObject obj = GameObject.Find("mainScene") as GameObject;
        mainScene = obj.GetComponent<MainScene>();
    }
	
	// Update is called once per frame
	void OnClick () {
        //mainScene.closeCardShopAndOpenShop();

	}
}

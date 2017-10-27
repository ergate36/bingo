using UnityEngine;
using System.Collections;

public class Option_id : MonoBehaviour {


	// Use this for initialization
	void Start () {
        transform.GetComponent<UILabel>().text = GlobalData.g_global.myInfo.userKey;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

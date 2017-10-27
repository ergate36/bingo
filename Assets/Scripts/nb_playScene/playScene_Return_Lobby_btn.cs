using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class playScene_Return_Lobby_btn : MonoBehaviour
{
    public GameObject root;

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

        Application.LoadLevel("nb_LobbyScene");
    }
}

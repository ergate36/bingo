using UnityEngine;
using System.Collections;

public class buyTicket_close_btn : MonoBehaviour {

    private LobbyScene lobbyScene;
    public GameObject popup;
    public Transform popup_ticket;

    void Start()
    {
        GameObject obj = GameObject.Find("lobbyScene") as GameObject;
        lobbyScene = obj.GetComponent<LobbyScene>();

    }

    void OnClick()
    {
        GameObject mainSceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/lobby_layer_Item");

        BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }


        mainSceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/lobbyBase");

        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
            popup_ticket.gameObject.SetActive(false);
    }
}

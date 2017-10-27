using UnityEngine;
using System.Collections;

public class Lobby_ticket_shop : MonoBehaviour {

    public GameObject popupRoot;
    public GameObject popup_ticket;

    public Shop.TapType taptype;
    private LobbyScene lobbyScene;
    private GameObject popup;
    void Start()
    {
    }

    void OnClick()
    {
        popup_ticket.gameObject.SetActive(false);
        popupRoot.SetActive(true);
        GlobalData.g_global.shopshop = 1;
        popupRoot.GetComponent<ShopCtrl>().popupShop(true, (int)taptype);

    }
}

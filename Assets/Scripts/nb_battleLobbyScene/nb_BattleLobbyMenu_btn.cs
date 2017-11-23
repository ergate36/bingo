using UnityEngine;
using System.Collections;

public class nb_BattleLobbyMenu_btn : MonoBehaviour
{
    public GameObject menuObj;

    void Start()
    {
        if (menuObj != null)
        {
            menuObj.SetActive(false);
            nb_GlobalData.g_global.LobbyMenuActive = false;
        }
    }

    void OnClick()
    {
        if (nb_GlobalData.g_global.MainMenuActive == true)
        {
            return;
        }

        if (nb_GlobalData.g_global.LobbyMenuActive == false)
        {
            nb_GlobalData.g_global.LobbyMenuActive = true;
            menuObj.SetActive(true);
            this.GetComponent<BoxCollider>().enabled = false;

            var sound = this.GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.Play();
            }
        }
    }
}

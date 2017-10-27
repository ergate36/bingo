using UnityEngine;
using System.Collections;

public class nb_LobbyBlitzShopOpen_btn : MonoBehaviour
{
    public GameObject shopObj;
    public GameObject mainLayer;
    public GameObject soundObj;

    void OnClick()
    {
        if (nb_GlobalData.g_global.LobbyShopActive == false)
        {
            nb_GlobalData.g_global.LobbyShopActive = true;
            mainLayer.SetActive(false);
            shopObj.SetActive(true);

            var sound = soundObj.GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.Play();
            }
        }
    }
}

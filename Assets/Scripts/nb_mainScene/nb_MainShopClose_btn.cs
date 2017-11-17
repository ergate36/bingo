using UnityEngine;
using System.Collections;

public class nb_MainShopClose_btn : MonoBehaviour
{
    public GameObject shopObj;
    public GameObject mainLayer;
    public GameObject soundObj;

    void OnClick()
    {
        if (nb_GlobalData.g_global.MainShopActive == true)
        {
            nb_GlobalData.g_global.MainShopActive = false;
            mainLayer.SetActive(true);
            shopObj.SetActive(false);

            var sound = soundObj.GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.Play();
            }
        }
    }
}

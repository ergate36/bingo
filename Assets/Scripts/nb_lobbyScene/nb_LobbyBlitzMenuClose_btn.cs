using UnityEngine;
using System.Collections;

public class nb_LobbyBlitzMenuClose_btn : MonoBehaviour
{
    public GameObject menuObj;
    public GameObject menuBtn;
    public GameObject soundObj;

    void OnClick()
    {
        if (nb_GlobalData.g_global.LobbyMenuActive == true)
        {
            nb_GlobalData.g_global.LobbyMenuActive = false;
            menuObj.SetActive(false);
            menuBtn.GetComponent<BoxCollider>().enabled = true;

            var sound = soundObj.GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.Play();
            }
        }
    }
}

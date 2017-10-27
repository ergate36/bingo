using UnityEngine;
using System.Collections;

public class nb_LobbyBlitzMenuBack_btn : MonoBehaviour
{
    public GameObject soundObj;

    void OnClick()
    {
        if (nb_GlobalData.g_global.LobbyMenuActive == true)
        {
            var sound = soundObj.GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.Play();
            }

            nb_GlobalData.g_global.LobbyMenuActive = false;

            Resources.UnloadUnusedAssets();
            System.GC.Collect();

            Application.LoadLevel("nb_MainScene");
        }
    }
}

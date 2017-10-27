using UnityEngine;
using System.Collections;

public class nb_LobbyBlitzFullScreen_btn : MonoBehaviour
{
    //public GameObject menuObj;

    void Start()
    {
        
    }

    void OnClick()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(Screen.height, Screen.width, false);
        }
        else
        {
            Screen.SetResolution(Screen.height, Screen.width, true);
        }
        Debug.Log("fullScreen button Click! h : " + Screen.height
            + ", w : " + Screen.width + ", isFull : " + Screen.fullScreen.ToString());
    }
}

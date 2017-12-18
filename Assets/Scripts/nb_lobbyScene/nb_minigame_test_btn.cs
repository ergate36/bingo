using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nb_minigame_test_btn : MonoBehaviour
{
    public GameObject miniGameLayer;
    public GameObject cardSelectLayer;
    public GameObject waitLayer;
    
    void OnClick()
    {
        nb_GlobalData.g_global.miniGameState = MiniGameState.TEST;
        miniGameLayer.SetActive(true);
        cardSelectLayer.SetActive(false);
        waitLayer.SetActive(false);
    }
}

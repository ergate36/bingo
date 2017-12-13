using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nb_minigame_shuffle_btn : MonoBehaviour
{
    public nb_minigame minigame;


    void OnClick()
    {
        if (minigame.bShuffle == true || minigame.bCardOpen == true)
        {
            return;
        }

        bool result = minigame.runShuffleCard();

        if (result == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
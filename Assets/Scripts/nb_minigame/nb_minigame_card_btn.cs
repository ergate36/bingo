using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nb_minigame_card_btn : MonoBehaviour
{
    public nb_minigame minigame;

    public int cardIndex;

    void OnClick()
    {
        if (minigame.bShuffle == false || minigame.bCardOpen == true)
        {
            return;
        }

        minigame.bCardOpen = true;

        minigame.runOpenCard(cardIndex);
    }
}
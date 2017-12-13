using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nb_minigame_bet_ctrl_btn : MonoBehaviour
{
    public bool isUp;
    public nb_minigame minigame;


    void OnClick()
    {
        if (minigame.priceSetList.Count <= 0)
        {
            Debug.Log("priceSetList data null");
            return;
        }


        if (isUp)
        {
            //올려
            if (minigame.priceSetList.Count > minigame.selectPriceIndex + 1)
            {
                ++minigame.selectPriceIndex;
            }
        }
        else
        {
            //내려
            if (0 < minigame.selectPriceIndex)
            {
                --minigame.selectPriceIndex;
            }
        }
        minigame.drawSelectPrice();
        minigame.drawSelectGroup(false);


        //this.gameObject.SetActive(false);
    }
}
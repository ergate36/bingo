using UnityEngine;
using System.Collections;

public class ResultBonusCtrl : MonoBehaviour {

    Transform[] spr_pos;

    void Awake()
    {
        spr_pos = new Transform[3];
        spr_pos[0] = transform.Find("spr_pos0");
        spr_pos[1] = transform.Find("spr_pos1");
        spr_pos[2] = transform.Find("spr_pos2");
    }

    public void setBonus(bool[] rankInfo, int rankCount, int gold)
    {
        transform.Find("amount").GetComponent<UILabel>().text = "+" + gold.ToString();

        if (rankCount == 3)
        {
            spr_pos[0].gameObject.SetActive(true);
            spr_pos[1].gameObject.SetActive(true);
            spr_pos[2].gameObject.SetActive(true);
        }
        else if (rankCount == 2)
        {
            spr_pos[0].gameObject.SetActive(false);
            spr_pos[1].gameObject.SetActive(true);
            spr_pos[2].gameObject.SetActive(true);

            if (rankInfo[0])
            {
                spr_pos[1].GetComponent<UISprite>().spriteName = "gold_mark";
                
                if(rankInfo[1])
                {
                    spr_pos[2].GetComponent<UISprite>().spriteName = "silver_mark";
                }
                else if (rankInfo[2])
                {
                    spr_pos[2].GetComponent<UISprite>().spriteName = "bronze_mark";
                }
            }
            else if (rankInfo[1])
            {
                spr_pos[1].GetComponent<UISprite>().spriteName = "silver_mark";
                spr_pos[2].GetComponent<UISprite>().spriteName = "bronze_mark";
            }
        }
        else if (rankCount == 1)
        {
            spr_pos[0].gameObject.SetActive(false);
            spr_pos[1].gameObject.SetActive(true);
            spr_pos[2].gameObject.SetActive(false);

            if (rankInfo[0])
            {
                spr_pos[1].GetComponent<UISprite>().spriteName = "gold_mark";
            }
            else if (rankInfo[1])
            {
                spr_pos[1].GetComponent<UISprite>().spriteName = "silver_mark";
            }
            else
            {
                spr_pos[1].GetComponent<UISprite>().spriteName = "bronze_mark";
            }
        }
        else {
            spr_pos[0].gameObject.SetActive(false);
            spr_pos[1].gameObject.SetActive(false);
            spr_pos[2].gameObject.SetActive(false);
        }

    }
}

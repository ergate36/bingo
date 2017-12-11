using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class nb_minigame : MonoBehaviour
{
    [HideInInspector]
    public Transform[] card;

    [HideInInspector]
    public bool bShuffle = false;

    private Transform betParent;

    [HideInInspector]
    public int betIndex = 0;

    [HideInInspector]
    public bool bCardOpen = false;

    void Awake()
    {
        card = new Transform[5];
        for (int i = 0; i < 5; ++i)
        {
            card[i] = this.transform.Find("card" + (i + 1).ToString());
        }

        betParent = this.transform.Find("i_betbar");
    }
    
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void runShuffleCard()
    {
        betParent.gameObject.SetActive(false);

        for (int i = 0; i < 5; ++i)
        {
            StartCoroutine(miniGameEffectWait(i));
        }        
    }

    private IEnumerator miniGameEffectWait(int cardIndex)
    {
        Vector3 center = card[2].position;

        if (card[cardIndex] != null)
        {
            card[cardIndex].Find("i_card").gameObject.SetActive(false);
            card[cardIndex].Find("spine").gameObject.SetActive(true);
            card[cardIndex].Find("spine").GetComponent<SkeletonAnimation>().AnimationName = "card_close";

            yield return new WaitForSeconds(0.8f);

            iTween.MoveTo(card[cardIndex].Find("spine").gameObject, center, 0.2f);

            yield return new WaitForSeconds(0.8f);

            iTween.MoveTo(card[cardIndex].Find("spine").gameObject, card[cardIndex].position, 0.2f);

            yield return new WaitForSeconds(0.2f);

            card[cardIndex].Find("i_card").gameObject.SetActive(true);
            card[cardIndex].Find("i_card").GetComponent<UISprite>().spriteName = "ui_minigame_card1";
            card[cardIndex].Find("i_card/icon").gameObject.SetActive(false);
            card[cardIndex].Find("i_card/value").gameObject.SetActive(false);
            card[cardIndex].Find("spine").gameObject.SetActive(false);

            card[cardIndex].Find("spine2").gameObject.SetActive(true);

            bShuffle = true;
        }

        yield return null;
    }

    public void runOpenCard(int cardIndex)
    {
        StartCoroutine(miniGameCardOpen(cardIndex));
    }

    private IEnumerator miniGameCardOpen(int cardIndex)
    {
        if (card[cardIndex] != null)
        {
            card[cardIndex].Find("i_card").gameObject.SetActive(false);
            card[cardIndex].Find("spine").gameObject.SetActive(true);
            card[cardIndex].Find("spine").GetComponent<SkeletonAnimation>().AnimationName = "card_open";
            yield return new WaitForSeconds(0.2f);

            card[cardIndex].Find("i_card").gameObject.SetActive(true);
            card[cardIndex].Find("i_card").GetComponent<UISprite>().spriteName = "ui_minigame_card2";
            card[cardIndex].Find("i_card/icon").gameObject.SetActive(true);
            card[cardIndex].Find("i_card/value").gameObject.SetActive(true);
            card[cardIndex].Find("spine").gameObject.SetActive(false);

            StartCoroutine(miniGameRewardAction(cardIndex));
        }

        yield return null;
    }

    private IEnumerator miniGameRewardAction(int cardIndex) //todo:보상 종류, 수량 추가 해야됨
    {
        //Debug.Log("miniGameRewardAction start");
        if (card[cardIndex] != null)
        {
            GameObject pref = Instantiate(Resources.Load("game/minigame_reward")) as GameObject;
            if(pref == null)
            {
                Debug.Log("reward pref null");
                yield return null;
            }

            for (int i = 0; i < 5; ++i)
            {
                card[i].Find("spine2").gameObject.SetActive(false);
            }

            pref.transform.parent = card[cardIndex].Find("effect");
            pref.transform.localPosition = new Vector3(0, 30, 0);
            pref.transform.localScale = Vector3.one;

            Vector3 pos = new Vector3(0, 0.24f, 0);
            iTween.MoveBy(pref, pos, 1.0f);

            yield return new WaitForSeconds(2.0f);

            for (int i = 0; i < card[cardIndex].Find("effect").childCount; ++i)
            {
                //이펙트 삭제
                GameObject.Destroy(card[cardIndex].Find("effect").GetChild(i).gameObject);
            }

            //초기화
            card[cardIndex].Find("i_card").gameObject.SetActive(false);
            card[cardIndex].Find("spine").gameObject.SetActive(true);
            card[cardIndex].Find("spine").GetComponent<SkeletonAnimation>().AnimationName = "card_close";

            yield return new WaitForSeconds(0.8f);

            for (int i = 0; i < 5; ++i)
            {
                card[i].Find("i_card").gameObject.SetActive(false);
                card[i].Find("spine").gameObject.SetActive(true);
                card[i].Find("spine").GetComponent<SkeletonAnimation>().AnimationName = "card_open";
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 5; ++i)
            {
                card[i].Find("i_card").gameObject.SetActive(true);
                card[i].Find("i_card/icon").gameObject.SetActive(true);
                card[i].Find("i_card/value").gameObject.SetActive(true);
                card[i].Find("spine").gameObject.SetActive(false);
                card[i].Find("i_card").GetComponent<UISprite>().spriteName = "ui_minigame_card2";
            }

            bShuffle = false;
            bCardOpen = false;

            betParent.gameObject.SetActive(true);
            this.transform.Find("i_shuffle_btn").gameObject.SetActive(true);
        }
        yield return null;
    }
}
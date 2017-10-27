using UnityEngine;
using System.Collections;
using System;
public class Reward_Ctrl : MonoBehaviour {

    Transform[] yabaweMonsters;
    public Transform[] yabawePos;
    Transform[] yabaweMonsters_back;
    Transform[] yabaweMonsters_yabawe;
    Transform[] yabaweMonsters_reward_label;

    Animation[] anim;

    Transform gotoMainBtn;

    Transform rewardFocus;
    Transform rewardFocus_click;
    Transform rewardFocus_btn;
    Transform rewardFocus_item;
    Transform rewardFocus_card;

    [HideInInspector]
    public bool isRetry = false;

    private int selectedIndex = 0;

    [HideInInspector]
    public int selectedMonster = -1;
    [HideInInspector]
    public int dropMonster = -1;
    [HideInInspector]
    public bool bMustSelect = false;

    private int click_flag = 0;
    private int alphaFlag = 0;
    private int colorValue = 250;
    
    public struct RewardInfo
    {
        public int grade;
        public int rewardKind;
        public int amount;
        public int selected;
    }
    public struct GachaInfo
    {
        public int type;
        public int value;
        public int selected;
    }



    [HideInInspector]
    public RewardInfo[] rewardInfo;

    void Update()
    {
        if(click_flag == 1){
            if (colorValue < 5)
            {
                alphaFlag = 1;
            }
            else if (colorValue > 250)
            {
                alphaFlag = 0;
            }

            if(alphaFlag==0){
                colorValue-=6;
                rewardFocus_click.GetComponent<UILabel>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, colorValue / 255f); ;
            }
            else if (alphaFlag == 1)
            {
                colorValue+=6;
                rewardFocus_click.GetComponent<UILabel>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f, colorValue / 255f); ;
            }
        }
    }

    void Awake()
    {
        click_flag = 0;
        anim = new Animation[3];
        yabaweMonsters = new Transform[3];
        yabaweMonsters_back = new Transform[3];
        yabaweMonsters_yabawe = new Transform[3];
        yabaweMonsters_reward_label = new Transform[3];
        for (int i = 0; i < 3; ++i)
        {
            string name = "yabawe_monster_" + i.ToString();
            
            yabaweMonsters[i] = transform.Find(name);
            yabaweMonsters_back[i] = yabaweMonsters[i].transform.Find("back");
            //yabaweMonsters_yabawe[i] = yabaweMonsters[i].transform.FindChild("yabawe");
            //yabaweMonsters_reward[i] = yabaweMonsters[i].transform.FindChild("reward");
            //yabaweMonsters_reward_label[i] = yabaweMonsters_reward[i].transform.FindChild("amount");
            anim[i] = transform.Find("yabawe_monster_"+i).GetComponentInChildren<Animation>();
            anim[i].Stop();
            //yabaweMonsters[i].gameObject.SetActive(false);
        }

        yabawePos = new Transform[3];

        for (int i = 0; i < 3; ++i)
        {
            string name = "pos" + i.ToString();
            
            yabawePos[i] = transform.Find(name);
        }

        gotoMainBtn = transform.Find("btn_gotoMain");

        rewardInfo = new RewardInfo[3];

        rewardFocus = transform.Find("reward_focus");
        rewardFocus_click = transform.Find("reward_focus/btn_confirm/Label");
        rewardFocus_btn = transform.Find("reward_focus/btn_confirm");
        rewardFocus_item= transform.Find("reward_focus/item");
        rewardFocus_card = transform.Find("reward_focus/reward_focus_card");
        rewardFocus_card.gameObject.SetActive(false);

    }

    void Start()
    {
        selectedIndex = 0;
        selectedMonster = -1;
        dropMonster = -1;
        onGetReward(1);
        

        rewardFocus.gameObject.SetActive(false);
        for (int i = 0; i < 3; ++i)
        {
            yabaweMonsters[i].position = yabawePos[i].position;
        }
    }

    public IEnumerator showOtherReward(int index)
    {
        switch(index){
            case 0:
                anim[1].Play("eff_treasurebox_disappear_animation");
                anim[2].Play("eff_treasurebox_disappear_animation");
                break;
            case 1:
                anim[0].Play("eff_treasurebox_disappear_animation");
                anim[2].Play("eff_treasurebox_disappear_animation");
                break;
            case 2:
                anim[0].Play("eff_treasurebox_disappear_animation");
                anim[1].Play("eff_treasurebox_disappear_animation");
                break;
        }
        yield return new WaitForSeconds(0.33f);

    }

    public IEnumerator InitRewardCtrl( float actionTime )
    {
        /*
        for (int i = 0; i < 3; ++i)
        {
            yabaweMonsters[i].gameObject.SetActive(true);

            rewards[i].GetComponent<UISprite>().spriteName = RewardData.rewardItemPath[GlobalData.g_global.gachaInfo[i].type - 1];

            yabaweMonsters[i].position = yabawePos[i].position;
            yabaweMonsters[i].localScale = Vector3.one * 2f;
            iTween.ScaleTo(yabaweMonsters[i].gameObject, Vector3.one, actionTime / 3f);

            yabaweMonsters[i].audio.Play();

            yield return new WaitForSeconds(actionTime / 3f);

        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 3; i ++ )
        {
            yabaweMonsters_back[i].GetComponent<UISprite>().spriteName = "game_result_gift1";
            yabaweMonsters_yabawe[i].GetComponent<UISprite>().spriteName = "game_result_gift1";

            yabaweMonsters[i].GetComponent<UIButton>().normalSprite = "game_result_gift1";
            yabaweMonsters[i].GetComponent<UIButton>().hoverSprite = "game_result_gift1";
            yabaweMonsters[i].GetComponent<UIButton>().pressedSprite = "game_result_gift1";
            yabaweMonsters[i].GetComponent<UIButton>().disabledSprite = "game_result_gift1";
        }

        yield return new WaitForSeconds(1f);

        anim[0].enabled = true;
        anim[2].enabled = true;

        yield return new WaitForSeconds(2f);

        */
        setActiveCardButton(true);
        yield return null;
    }


    public void setActiveCardButton(bool active)
    {
        for (int i = 0; i < 3; ++i)
        {
            if (yabaweMonsters[i].gameObject)
            {
                yabaweMonsters[i].GetComponent<BoxCollider>().enabled = active;
            }
        }
    }

    public void onGetReward(int flag)
    {
        setActiveCardButton(false);
        onRequestGetReward();
    }

    private void onRequestGetReward()
    {

        for (int i = 0; i < 3; i++)
        {
            string spriteName = "";
            spriteName = RewardData.rewardItemPath[GlobalData.g_global.gachaInfo[i].type - 1];
        }
            StartCoroutine(InitRewardCtrl(1.0f));
    }

    public void dropSelectedMonster(int index)
    {
        isRetry = true;
        dropMonster = index;
        iTween.MoveBy(yabaweMonsters[dropMonster].gameObject, iTween.Hash("x", -4.0f, "y", -0.5f, "easeType", "easeOutQuad", "time", 0.8f));
        iTween.RotateBy(yabaweMonsters[dropMonster].gameObject, new Vector3(0f, 0f, 180f), 0.8f);
        iTween.ScaleTo(yabaweMonsters[dropMonster].gameObject, iTween.Hash("x", 0.001f, "y", 0.001f, "z", 0.001f, "easeType", "easeOutQuad", "time", 0.8f));
    }

    public IEnumerator setMonsterToRetry()
    {      
        if (gotoMainBtn.gameObject.activeSelf)
        {
            gotoMainBtn.GetComponent<BoxCollider>().enabled = false;
        }

        dropSelectedMonster(selectedMonster);

        yield return new WaitForSeconds(1.0f);

        // other cards move out
        int index = 0;
       
        
        yield return new WaitForSeconds(1.0f);

        // reposition cards
        
        yield return new WaitForSeconds(1.0f);
  

        bMustSelect = true;

    }

    public IEnumerator showFocusReward()
    {
        rewardFocus.gameObject.SetActive(true);

        GameObject effect = Instantiate(Resources.Load("effects/eff_gacha_finish_large")) as GameObject;
        Vector3 scale = effect.transform.localScale;

        effect.transform.parent = rewardFocus;
        effect.transform.localPosition = Vector3.zero;
        effect.transform.localScale = scale;

        rewardFocus.GetComponent<Popup_Effector>().activePopup();
        rewardFocus.GetComponent<UISprite>().MakePixelPerfect();

        //rewardFocus.audio.Play();

        string spriteName = "";
        if (rewardInfo[selectedIndex].rewardKind >= (int)RewardData.RewardItemInfo.REWARD_COIN)
        {
            rewardFocus_item.gameObject.SetActive(true);
            //rewardFocus_card.gameObject.SetActive(false);

            spriteName = RewardData.rewardItemPath[rewardInfo[selectedIndex].rewardKind];
            rewardFocus_item.GetComponent<UISprite>().spriteName = spriteName;

            string itemExplain = "";

         
            if (rewardInfo[selectedIndex].rewardKind == (int)RewardData.RewardItemInfo.REWARD_COIN)
            {
                itemExplain = "COIN \n";
            }
            else if (rewardInfo[selectedIndex].rewardKind == (int)RewardData.RewardItemInfo.REWARD_ITEM)
            {
                itemExplain = "ITEM \n";
            }
            if (rewardInfo[selectedIndex].rewardKind == (int)RewardData.RewardItemInfo.REWARD_BINGOTICKET)
            {
                itemExplain = " TICKET \n";
            }
            

            rewardFocus_item.GetComponentInChildren<UILabel>().text =  itemExplain + " + " + (rewardInfo[selectedIndex].amount).ToString()  ;


        }
        
        yield return new WaitForSeconds(1.2f);

        if (effect)
        {
            Destroy(effect);
        }
    }

    public void setRetry()
    {
        StartCoroutine( rewardFocus.GetComponent<Popup_Effector>().unactivePopup(0.3f) );

    }

    public IEnumerator showReward( int flag )
    {

        //selected monstercard
        string spriteName = "";
        
        spriteName = RewardData.rewardItemPath[GlobalData.g_global.gachaInfo[0].type - 1];
        rewardFocus.gameObject.SetActive(true);

        rewardFocus_click.gameObject.SetActive(false);
        rewardFocus_btn.transform.GetComponent<BoxCollider>().enabled = false;
        if (GlobalData.g_global.gachaInfo[0].type == 4)
        {
            rewardFocus_item.gameObject.SetActive(false);
        }
        else
        {
            rewardFocus_item.GetComponent<UISprite>().spriteName = spriteName;
        }
        
        GameObject effect = Instantiate(Resources.Load("effects/eff_gacha_finish_large")) as GameObject;
        Vector3 scale = effect.transform.localScale;

        effect.transform.parent = rewardFocus;
        effect.transform.localPosition = Vector3.zero;
        effect.transform.localScale = scale;

        //rewardFocus.audio.Play();

        yield return new WaitForSeconds(1.2f);

        if (effect)
        {
            Destroy(effect);
        }
        if (GlobalData.g_global.gachaInfo[0].type == 4)
        {
            GlobalData.g_global.cardCount++;
            rewardFocus_item.gameObject.SetActive(false);
            rewardFocus_card.gameObject.SetActive(true);

            GlobalData.g_global.gachaInfo[0].value = GlobalData.g_global.gachaInfo[0].value - 1;

            int at1 = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Ability1;
            int at2 = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Ability2;
            int at3 = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Ability3;
            
            float ab1v1 = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Lv1ability1value;
            float ab1v2 = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Lv1ability2value;
            float ab1v3 = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Lv1ability3value;
            float ab30v1 = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].AT1Add;
            float ab30v2 = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].AT2Add;
            float ab30v3 = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].AT3Add;
            
            string Name = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Name;
            int Rank = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Rank;
            int Rare = XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Rare;

            double atv1 = Math.Round(XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Lv1ability1value,1);
            Transform m_at1_label = rewardFocus_card.transform.Find("at1_label");
            Transform m_at2_label = rewardFocus_card.transform.Find("at2_label");
            Transform m_at3_label = rewardFocus_card.transform.Find("at3_label");

            Transform m_infoBar = rewardFocus_card.transform.Find("cardExpBar");
            Transform m_percent_label = rewardFocus_card.transform.Find("card_percent");
            Transform m_name = rewardFocus_card.transform.Find("name");
            Transform m_star = rewardFocus_card.transform.Find("star");

            m_name.GetComponent<UISprite>().spriteName = Name + "name";
            m_star.GetComponent<UISprite>().spriteName = "star" + Rank;

            if (at1 != 0)
            {
                if (at1 == 1)
                {
                    m_at1_label.GetComponent<UILabel>().text = "프리덮 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 2)
                {
                    m_at1_label.GetComponent<UILabel>().text = "더블덮 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 3)
                {
                    m_at1_label.GetComponent<UILabel>().text = "덮리워드 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 4)
                {
                    m_at1_label.GetComponent<UILabel>().text = "골드30 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 5)
                {
                    m_at1_label.GetComponent<UILabel>().text = "골드50 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 6)
                {
                    m_at1_label.GetComponent<UILabel>().text = "한방빙고 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 7)
                {
                    m_at1_label.GetComponent<UILabel>().text = "티켓 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 8)
                {
                    m_at1_label.GetComponent<UILabel>().text = "프리즈 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 9)
                {
                    m_at1_label.GetComponent<UILabel>().text = "블라인드 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 10)
                {
                    m_at1_label.GetComponent<UILabel>().text = "스왑시트 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 11)
                {
                    m_at1_label.GetComponent<UILabel>().text = "믹스시트 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 12)
                {
                    m_at1_label.GetComponent<UILabel>().text = "디펜스 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 13)
                {
                    m_at1_label.GetComponent<UILabel>().text = "골드티켓 아이템 생성 확률" + atv1 + "%증가";
                }
                else if (at1 == 14)
                {
                    m_at1_label.GetComponent<UILabel>().text = "랭크 보너스 점수의" + atv1 + "% 추가 점수";
                }
                else if (at1 == 15)
                {
                    m_at1_label.GetComponent<UILabel>().text = "게임 점수의" + atv1 + "% 추가 점수";
                }
                else if (at1 == 16)
                {
                    m_at1_label.GetComponent<UILabel>().text = atv1 / 2 + "~" + atv1 + "랜덤 추가 점수";
                }
                else if (at1 == 17)
                {
                    m_at1_label.GetComponent<UILabel>().text = atv1 / 10 + "~" + atv1 + "랜덤 추가 점수";
                }
                else if (at1 == 18)
                {
                    m_at1_label.GetComponent<UILabel>().text = "몬스터 보너스 점수" + atv1 + "%상승";
                }
                else if (at1 == 19)
                {
                    m_at1_label.GetComponent<UILabel>().text = "랭크 골드 보상의" + atv1 + "%추가 골드";
                }
                else if (at1 == 20)
                {
                    m_at1_label.GetComponent<UILabel>().text = "게임에서 얻은 골드의" + atv1 + "%추가 골드";
                }
                else if (at1 == 21)
                {
                    m_at1_label.GetComponent<UILabel>().text = atv1 / 2 + "~" + atv1 + "랜덤 추가 골드";
                }
                else if (at1 == 22)
                {
                    m_at1_label.GetComponent<UILabel>().text = atv1 / 10 + "~" + atv1 + "랜덤 추가 골드";
                }
                else
                {
                    m_at1_label.GetComponent<UILabel>().text = "몬스터 보너스 골드" + atv1 + "% 상승";
                }
            }
            else
            {
                m_at1_label.GetComponent<UILabel>().text = "";
            }
            if (at2 != 0)
            {
                double atv2 = Math.Round(XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Lv1ability2value,1);

                if (at2 == 1)
                {
                    m_at2_label.GetComponent<UILabel>().text = "프리덮 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 2)
                {
                    m_at2_label.GetComponent<UILabel>().text = "더블덮 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 3)
                {
                    m_at2_label.GetComponent<UILabel>().text = "덮리워드 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 4)
                {
                    m_at2_label.GetComponent<UILabel>().text = "골드30 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 5)
                {
                    m_at2_label.GetComponent<UILabel>().text = "골드50 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 6)
                {
                    m_at2_label.GetComponent<UILabel>().text = "한방빙고 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 7)
                {
                    m_at2_label.GetComponent<UILabel>().text = "티켓 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 8)
                {
                    m_at2_label.GetComponent<UILabel>().text = "프리즈 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 9)
                {
                    m_at2_label.GetComponent<UILabel>().text = "블라인드 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 10)
                {
                    m_at2_label.GetComponent<UILabel>().text = "스왑시트 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 11)
                {
                    m_at2_label.GetComponent<UILabel>().text = "믹스시트 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 12)
                {
                    m_at2_label.GetComponent<UILabel>().text = "디펜스 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 13)
                {
                    m_at2_label.GetComponent<UILabel>().text = "골드티켓 아이템 생성 확률" + atv2 + "%증가";
                }
                else if (at2 == 14)
                {
                    m_at2_label.GetComponent<UILabel>().text = "랭크 보너스 점수의" + atv2 + "% 추가 점수";
                }
                else if (at2 == 15)
                {
                    m_at2_label.GetComponent<UILabel>().text = "게임 점수의" + atv2 + "% 추가 점수";
                }
                else if (at2 == 16)
                {
                    m_at2_label.GetComponent<UILabel>().text = atv2 / 2 + "~" + atv2 + "랜덤 추가 점수";
                }
                else if (at2 == 17)
                {
                    m_at2_label.GetComponent<UILabel>().text = atv2 / 10 + "~" + atv2 + "랜덤 추가 점수";
                }
                else if (at2 == 18)
                {
                    m_at2_label.GetComponent<UILabel>().text = "몬스터 보너스 점수" + atv2 + "%상승";
                }
                else if (at2 == 19)
                {
                    m_at2_label.GetComponent<UILabel>().text = "랭크 골드 보상의" + atv2 + "%추가 골드";
                }
                else if (at2 == 20)
                {
                    m_at2_label.GetComponent<UILabel>().text = "게임에서 얻은 골드의" + atv2 + "%추가 골드";
                }
                else if (at2 == 21)
                {
                    m_at2_label.GetComponent<UILabel>().text = atv2 / 2 + "~" + atv2 + "랜덤 추가 골드";
                }
                else if (at2 == 22)
                {
                    m_at2_label.GetComponent<UILabel>().text = atv2 / 10 + "~" + atv2 + "랜덤 추가 골드";
                }
                else
                {
                    m_at2_label.GetComponent<UILabel>().text = "몬스터 보너스 골드" + atv2 + "% 상승";
                }
            }
            else
            {
                m_at2_label.GetComponent<UILabel>().text = "";
            }

            if (at3 != 0)
            {
                double atv3 = Math.Round(XmlCtrl.x_xml.MCxml[GlobalData.g_global.gachaInfo[0].value].Lv1ability3value,1);

                if (at3 == 1)
                {
                    m_at3_label.GetComponent<UILabel>().text = "프리덮 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 2)
                {
                    m_at3_label.GetComponent<UILabel>().text = "더블덮 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 3)
                {
                    m_at3_label.GetComponent<UILabel>().text = "덮리워드 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 4)
                {
                    m_at3_label.GetComponent<UILabel>().text = "골드30 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 5)
                {
                    m_at3_label.GetComponent<UILabel>().text = "골드50 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 6)
                {
                    m_at3_label.GetComponent<UILabel>().text = "한방빙고 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 7)
                {
                    m_at3_label.GetComponent<UILabel>().text = "티켓 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 8)
                {
                    m_at3_label.GetComponent<UILabel>().text = "프리즈 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 9)
                {
                    m_at3_label.GetComponent<UILabel>().text = "블라인드 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 10)
                {
                    m_at3_label.GetComponent<UILabel>().text = "스왑시트 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 11)
                {
                    m_at3_label.GetComponent<UILabel>().text = "믹스시트 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 12)
                {
                    m_at3_label.GetComponent<UILabel>().text = "디펜스 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 13)
                {
                    m_at3_label.GetComponent<UILabel>().text = "골드티켓 아이템 생성 확률" + atv3 + "%증가";
                }
                else if (at3 == 14)
                {
                    m_at3_label.GetComponent<UILabel>().text = "랭크 보너스 점수의" + atv3 + "% 추가 점수";
                }
                else if (at3 == 15)
                {
                    m_at3_label.GetComponent<UILabel>().text = "게임 점수의" + atv3 + "% 추가 점수";
                }
                else if (at3 == 16)
                {
                    m_at3_label.GetComponent<UILabel>().text = atv3 / 2 + "~" + atv3 + "랜덤 추가 점수";
                }
                else if (at3 == 17)
                {
                    m_at3_label.GetComponent<UILabel>().text = atv3 / 10 + "~" + atv3 + "랜덤 추가 점수";
                }
                else if (at3 == 18)
                {
                    m_at3_label.GetComponent<UILabel>().text = "몬스터 보너스 점수" + atv3 + "%상승";
                }
                else if (at3 == 19)
                {
                    m_at3_label.GetComponent<UILabel>().text = "랭크 골드 보상의" + atv3 + "%추가 골드";
                }
                else if (at3 == 20)
                {
                    m_at3_label.GetComponent<UILabel>().text = "게임에서 얻은 골드의" + atv3 + "%추가 골드";
                }
                else if (at3 == 21)
                {
                    m_at3_label.GetComponent<UILabel>().text = atv3 / 2 + "~" + atv3 + "랜덤 추가 골드";
                }
                else if (at3 == 22)
                {
                    m_at3_label.GetComponent<UILabel>().text = atv3 / 10 + "~" + atv3 + "랜덤 추가 골드";
                }
                else
                {
                    m_at3_label.GetComponent<UILabel>().text = "몬스터 보너스 골드" + atv3 + "% 상승";
                }
            }
            else
            {
                m_at3_label.GetComponent<UILabel>().text = "";
            }

            Transform star;
            Transform name;
            Transform monster;
            Transform rare;
            Transform level_label;
            Transform lvbk;

            star = rewardFocus_card.transform.Find("list_MonsterCard/star");
            name = rewardFocus_card.transform.Find("list_MonsterCard/name");
            monster = rewardFocus_card.transform.Find("list_MonsterCard/monster");
            rare = rewardFocus_card.transform.Find("list_MonsterCard/rare");
            level_label = rewardFocus_card.transform.Find("list_MonsterCard/Label");
            lvbk = rewardFocus_card.transform.Find("list_MonsterCard/lvbk");
            rare.GetComponent<UISprite>().spriteName = "card" + Rare;
            name.GetComponent<UISprite>().spriteName = Name + "name";

            star.GetComponent<UISprite>().spriteName = "star" + Rank;
            monster.GetComponent<UISprite>().spriteName = Name+"_"+Rare;

            level_label.GetComponent<UILabel>().text = "1";
            lvbk.GetComponent<UISprite>().spriteName = "lvbk_" + Rare;

        }
        else
        {
            rewardFocus_item.GetComponentInChildren<UILabel>().text = " + " +GlobalData.g_global.gachaInfo[0].value.ToString();
        }

        yield return new WaitForSeconds(1f);
        rewardFocus_btn.transform.GetComponent<BoxCollider>().enabled = true;
        rewardFocus_click.gameObject.SetActive(true);
        click_flag = 1;


    }

}

using UnityEngine;
using System.Collections;

public class gameRanking : MonoBehaviour {
    
    public GameObject resultPopupRoot;
    public Transform root;
    public Transform layer_black;
    private Transform card_Bigname;

    private Transform card_Smallname;

    private Transform card_monster;
    private Transform card_rare;
    private Transform card_star;

    public Transform ranking_eff_card;
    public Transform ranking_eff_firework;
    public Transform ranking_eff_flash;
    public Transform lose;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator initRanking()
    {

        root.Find("bg/gotoResult_btn").GetComponent<BoxCollider>().enabled = false;
        //root.FindChild("bg/gotoResult_btn").GetComponent<BoxCollider>().enabled = true;
        Transform cardroot = root.transform.Find("bg/input_MonsterCard");
        cardroot.gameObject.SetActive(false);

        layer_black.gameObject.SetActive(true);
        root.gameObject.SetActive(true);

        if(GlobalData.g_global.m_winnerList[0].nickname != GlobalData.g_global.myInfo.nickName){
            lose.gameObject.SetActive(true);
            GlobalData.g_global.win = 1;
        }

        GlobalData.g_global.myFriend = GlobalData.g_global.otherSheetInfo[0].nickname;

        Transform soundObj = transform.Find("sound2");
        soundObj.GetComponent<AudioSource>().PlayOneShot(soundObj.GetComponent<AudioSource>().clip);



        //Transform soundObj2 = transform.FindChild("sound3");
        
        iTween.MoveTo(root.gameObject, iTween.Hash("x", 0f, "easeType", "easeOutElastic", "time", 0.6f));
        card_Bigname = root.transform.Find("bg/card_name");
        card_Smallname = root.transform.Find("bg/input_MonsterCard/name");
        card_monster = root.transform.Find("bg/input_MonsterCard/monster");
        card_rare = root.transform.Find("bg/input_MonsterCard/rare");
        card_star = root.transform.Find("bg/input_MonsterCard/star");
        Transform sp1 = root.transform.Find("bg/Sprite2");
        Transform sp2 = root.transform.Find("bg/Sprite3");

        ranking_eff_card.gameObject.SetActive(true);
        ranking_eff_firework.gameObject.SetActive(true);
        ranking_eff_flash.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        sp1.gameObject.SetActive(true);
        sp2.gameObject.SetActive(true);

        cardroot.gameObject.SetActive(true);

        if (GlobalData.g_global.m_winnerList[0].monster_id == 0)
        {
            //card_Bigname.gameObject.SetActive(false);
            card_Bigname.GetComponent<UILabel>().text = GlobalData.g_global.m_winnerList[0].nickname;
            card_Smallname.gameObject.SetActive(false); 
            card_monster.gameObject.SetActive(false);
            card_rare.GetComponent<UISprite>().spriteName = "nocard";
            card_star.gameObject.SetActive(false);
        }
        else
        {
            card_Bigname.GetComponent<UILabel>().text = GlobalData.g_global.m_winnerList[0].nickname;
            card_Smallname.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.m_winnerList[0].monster_id - 1].Name + "name";
            card_monster.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.m_winnerList[0].monster_id - 1].Name + "_" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.m_winnerList[0].monster_id - 1].Rare;
            card_rare.GetComponent<UISprite>().spriteName = "card" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.m_winnerList[0].monster_id - 1].Rare;
            card_star.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.m_winnerList[0].monster_id - 1].Rank;
        }

        root.Find("bg/gotoResult_btn").GetComponent<BoxCollider>().enabled = true;
    }

    private IEnumerator ani_CtrlS(Transform ani_root)
    {
        GameObject daubItemObject = Instantiate(Resources.Load("effects/yun/eff_inner_ranking")) as GameObject;

        daubItemObject.transform.parent = ani_root;
        daubItemObject.transform.localPosition = Vector3.zero;
        daubItemObject.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(2);

    }

    private IEnumerator ani_CtrlF()
    {
        GameObject daubItemObject = Instantiate(Resources.Load("effects/yun/eff_out_ranking")) as GameObject;

        //daubItemObject.transform.parent = failRank[3];
        daubItemObject.transform.localPosition = Vector3.zero;
        daubItemObject.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(2);

        Destroy(daubItemObject);
    }
}

using UnityEngine;
using System.Collections;
using System.Xml;
public class myCardlist2 : MonoBehaviour
{
    Transform used;
    Transform star;
    Transform name;
    Transform monster;
    Transform rare;
    Transform level_label;
    Transform lvbk;
    CardListCtrl2 mycardinfo;
    private GameObject root;

    private monsterSceneUI2 monster_ui;


    // Use this for initialization
    void Awake()
    {
        used = transform.Find("used");
        star = transform.Find("star");
        name = transform.Find("name");
        monster = transform.Find("monster");
        rare = transform.Find("rare");
        level_label = transform.Find("Label");
        lvbk = transform.Find("lvbk");
    }


    void Start()
    {
        root = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
        mycardinfo = root.GetComponent<CardListCtrl2>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCardCtrl(MyCard Cardinfo)
    {
        name.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[Cardinfo.cardId - 1].Name + "name";

        star.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[Cardinfo.cardId - 1].Rank;
        monster.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[Cardinfo.cardId - 1].Name + "_" + XmlCtrl.x_xml.MCxml[Cardinfo.cardId - 1].Rare;
        rare.GetComponent<UISprite>().spriteName = "card" + XmlCtrl.x_xml.MCxml[Cardinfo.cardId - 1].Rare;
        level_label.GetComponent<UILabel>().text = Cardinfo.cardlevel.ToString();
        lvbk.GetComponent<UISprite>().spriteName = "lvbk_" + XmlCtrl.x_xml.MCxml[Cardinfo.cardId - 1].Rare;

        if (Cardinfo.cardused == 0)
        {
            used.GetComponent<UISprite>().spriteName = "";
        }
        else
        {
            used.GetComponent<UISprite>().spriteName = "use";
        }
    }
}

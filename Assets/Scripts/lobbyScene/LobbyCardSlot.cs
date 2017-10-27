using UnityEngine;
using System.Collections;

public class LobbyCardSlot : MonoBehaviour {

    private Transform[] slot;

	// Use this for initialization
	void Start () {
        
        defaultSet();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void defaultSet()
    {
        slot = new Transform[4];

        for (int i = 0; i < 4; i++)
        {
            slot[i] = transform.Find("list_MonsterCard" + (i + 1));
            slot[i].Find("Label").GetComponent<UILabel>().text = "";
            slot[i].Find("monster").GetComponent<UISprite>().spriteName = "";
            slot[i].Find("name").GetComponent<UISprite>().spriteName = "";
            slot[i].Find("lvbk").GetComponent<UISprite>().spriteName = "";
            slot[i].Find("rare").GetComponent<UISprite>().spriteName = "";
            slot[i].Find("star").GetComponent<UISprite>().spriteName = "";

        }

        for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++ )
        {
            if(GlobalData.g_global.myCardList[i].cardused <5 &&GlobalData.g_global.myCardList[i].cardused !=0){
//                GlobalData.g_global.myCardList[i].cardused
                slot[GlobalData.g_global.myCardList[i].cardused - 1].Find("Label").GetComponent<UILabel>().text 
                    = GlobalData.g_global.myCardList[i].cardlevel.ToString();
                slot[GlobalData.g_global.myCardList[i].cardused - 1].Find("monster").GetComponent<UISprite>().spriteName
                    = XmlCtrl.x_xml.MCxml[GlobalData.g_global.myCardList[i].cardId - 1].Name + "_" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.myCardList[i].cardId - 1].Rare;
                slot[GlobalData.g_global.myCardList[i].cardused - 1].Find("name").GetComponent<UISprite>().spriteName
                    = XmlCtrl.x_xml.MCxml[GlobalData.g_global.myCardList[i].cardId - 1].Name+"name";
                slot[GlobalData.g_global.myCardList[i].cardused - 1].Find("lvbk").GetComponent<UISprite>().spriteName
                    = "lvbk_" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.myCardList[i].cardId - 1].Rare;
                slot[GlobalData.g_global.myCardList[i].cardused - 1].Find("rare").GetComponent<UISprite>().spriteName
                    = "card" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.myCardList[i].cardId - 1].Rare;
                slot[GlobalData.g_global.myCardList[i].cardused - 1].Find("star").GetComponent<UISprite>().spriteName
                    = "star"+XmlCtrl.x_xml.MCxml[GlobalData.g_global.myCardList[i].cardId - 1].Rank;
            }
        }
    }

}

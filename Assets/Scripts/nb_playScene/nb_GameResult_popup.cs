using MarigoldModel.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nb_GameResult_popup : MonoBehaviour
{

    //1. 카드 결과(1~4)
    //2. 코인 습득 보상 - 없으면 스킵
    //3. 순위 보상 (코인, 크레딧, 경험치) - 없으면 스킵
    //4. 콜렉션 획득 - 없으면 스킵
    //5. 추가 보상 - 코인, 크레딧 x2 - 없으면 스킵
    //6. 추가 보상 - 경험치 x2 - 없으면 스킵
    //7. 추가 보상 - 상자(코인, 크레딧, 아이템, 콜렉션) - 없으면 스킵

    private GameObject[] Collection;

    void Awake()
    {
        Collection = new GameObject[12];
        for (int i = 1; i <= 12; ++i)
        {
            Collection[i - 1] = this.transform.Find("collection/item" + i).gameObject;
        }
    }

	void Start () 
    {
        //선택 카드 장수 세팅
        this.transform.Find("bg/i_select_card").GetComponent<UISprite>().spriteName = 
            "ui_reward_card" + nb_GlobalData.g_global.sheetInfo.activeSheetCount;
        this.transform.Find("bg/i_select_card").GetComponent<UISprite>().MakePixelPerfect();

		//콜렉션 세팅
        setCollection();

        //플레이어 경험치 세팅
        //todo:
	}
	
	void Update ()
    {
	
	}

    void setCollection()
    {
        for (int i = 1; i <= 12; ++i)
        {
            string name = "collection_" + nb_GlobalData.g_global.selectStageId + "_" + i;
            Collection col = nb_GlobalData.g_global.util.findCollection(name);

            Transform icon = Collection[i - 1].transform.Find("icon");
            Transform icon_off = Collection[i - 1].transform.Find("icon_off");
            UILabel count = Collection[i - 1].transform.Find("count").GetComponent<UILabel>();

            icon.GetComponent<UISprite>().atlas.name = col.AtlasName;
            icon.GetComponent<UISprite>().spriteName = col.SpriteName;
            icon.GetComponent<UISprite>().MakePixelPerfect();
            icon.localScale = new Vector3(0.45f, 0.45f);

            Texture texture = Resources.Load("nb_images/collection/" +
                col.AtlasName + "/" + col.SpriteName, typeof(Texture)) as Texture;
            icon_off.GetComponent<UITexture>().mainTexture = texture;
            icon_off.GetComponent<UITexture>().MakePixelPerfect();
            icon_off.GetComponent<UITexture>().color = new Color(0.22f, 0.3f, 0.47f);
            icon_off.GetComponent<UITexture>().alpha = 0.4f;
            icon_off.localScale = new Vector3(0.45f, 0.45f);

            int colCount = nb_GlobalData.g_global.util.getUserCollectionCount(col.Id);
            count.text = "x" + colCount;

            icon.gameObject.SetActive(colCount > 0);
            icon_off.gameObject.SetActive(colCount == 0);

            Collection[i - 1].GetComponent<UISprite>().spriteName =
                colCount > 0 ? "ui_collection_back1" : "ui_collection_back2";
        }
    }
}

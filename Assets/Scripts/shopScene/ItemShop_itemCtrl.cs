using UnityEngine;
using System.Collections;

public class ItemShop_itemCtrl : MonoBehaviour {


   
    public Item.ItemType itemType;

    private ItemShopCtrl itemshopCtrl;
    private UIScrollView sv;

    private Vector3 originPos;

    void Awake()
    {
        GameObject itemshopRoot = GameObject.Find("popup_item") as GameObject;
        itemshopCtrl = itemshopRoot.GetComponent<ItemShopCtrl>();
        sv = transform.GetComponent<UIDragScrollView>().scrollView;

        originPos = Vector3.zero;
        originPos.x = sv.transform.localPosition.x;
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        Vector3 pos = sv.transform.localPosition - originPos;

        Vector3 resultPos = transform.localPosition + pos;
        resultPos.y = 0f;

        itemshopCtrl.m_prevPos = sv.transform.localPosition.x;

        itemshopCtrl.showItemExplain(resultPos, itemType);
    }
}

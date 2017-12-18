using UnityEngine;
using System.Collections;

public class nb_shopBuy_btn : MonoBehaviour
{
    //public GameObject shopObj;
    public long productId;

    void OnClick()
    {
        if (nbHttp.state == nbHttp.nbHttpState.Wait)
        {
            nbHttp.http.BuyStoreProduct(
                nb_GlobalData.g_global.userSession.SessionKey, productId, "");
        }
    }
}

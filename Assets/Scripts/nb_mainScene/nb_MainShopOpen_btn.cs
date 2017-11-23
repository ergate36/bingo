using UnityEngine;
using System.Collections;

public class nb_MainShopOpen_btn : MonoBehaviour
{
    public GameObject shopObj;
    public GameObject mainLayer;
    public GameObject soundObj;

    public int shopTapIndex;

    void OnClick()
    {
        if (nb_GlobalData.g_global.MainShopActive == false)
        {
            nb_GlobalData.g_global.MainShopActive = true;
            //mainLayer.SetActive(false);
            shopObj.SetActive(true);

            shopObj.transform.Find("tap1").gameObject.SetActive(false);
            shopObj.transform.Find("tap2").gameObject.SetActive(false);
            shopObj.transform.Find("tap3").gameObject.SetActive(false);
            shopObj.transform.Find("tap4").gameObject.SetActive(false);
            shopObj.transform.Find("tap5").gameObject.SetActive(false);
            shopObj.transform.Find("tap" + shopTapIndex.ToString()).gameObject.SetActive(true);

            shopObj.transform.Find("tap_btn1").gameObject.SetActive(true);
            shopObj.transform.Find("tap_btn2").gameObject.SetActive(true);
            shopObj.transform.Find("tap_btn3").gameObject.SetActive(true);
            shopObj.transform.Find("tap_btn4").gameObject.SetActive(true);
            shopObj.transform.Find("tap_btn5").gameObject.SetActive(true);
            shopObj.transform.Find("tap_btn" + shopTapIndex.ToString()).gameObject.SetActive(false);

            var sound = soundObj.GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.Play();
            }
        }
    }
}

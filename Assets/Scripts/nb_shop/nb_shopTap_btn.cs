using UnityEngine;
using System.Collections;

public class nb_shopTap_btn : MonoBehaviour
{
    public GameObject shopObj;
    public GameObject selectSoundObj;

    public int shopTapIndex;

    void OnClick()
    {
        //shopObj.transform.Find("tap1").gameObject.SetActive(false);
        shopObj.transform.Find("tap2").gameObject.SetActive(false);
        shopObj.transform.Find("tap3").gameObject.SetActive(false);
        shopObj.transform.Find("tap4").gameObject.SetActive(false);
        shopObj.transform.Find("tap5").gameObject.SetActive(false);
        shopObj.transform.Find("tap" + shopTapIndex.ToString()).gameObject.SetActive(true);

        //shopObj.transform.Find("tap_btn1").gameObject.SetActive(true);
        shopObj.transform.Find("tap_btn2").gameObject.SetActive(true);
        shopObj.transform.Find("tap_btn3").gameObject.SetActive(true);
        shopObj.transform.Find("tap_btn4").gameObject.SetActive(true);
        shopObj.transform.Find("tap_btn5").gameObject.SetActive(true);
        shopObj.transform.Find("tap_btn" + shopTapIndex.ToString()).gameObject.SetActive(false);

        var sound = selectSoundObj.GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.Play();
        }
    }
}

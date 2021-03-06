﻿using UnityEngine;
using System.Collections;

public class nb_shopOpen_btn : MonoBehaviour
{
    private BoxCollider[] buttons;
    private MeshRenderer[] spineRenderers;

    public GameObject shopObj;
    public GameObject mainLayer;
    public GameObject selectSoundObj;

    public int shopTapIndex;

    void Awake()
    {
        buttons = mainLayer.GetComponentsInChildren<BoxCollider>();
        //for (int i = 0; i < buttons.Length; ++i)
        //{
        //    buttons[i].enabled = false;
        //}
    }

    void OnClick()
    {
        if (nb_GlobalData.g_global.ShopMenuActive == false)
        {
            nb_GlobalData.g_global.ShopMenuActive = true;

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }

            spineRenderers = mainLayer.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < spineRenderers.Length; ++i)
            {
                spineRenderers[i].enabled = false;
            }

            shopObj.SetActive(true);

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
}

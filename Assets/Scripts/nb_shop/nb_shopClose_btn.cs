using UnityEngine;
using System.Collections;

public class nb_shopClose_btn : MonoBehaviour
{
    private BoxCollider[] buttons;
    private MeshRenderer[] spineRenderers;

    public GameObject shopObj;
    public GameObject mainLayer;
    public GameObject closeSoundObj;

    void Awake()
    {
        Debug.Log("shop close btn : awake()");
        buttons = mainLayer.GetComponentsInChildren<BoxCollider>();
    }

    void OnClick()
    {
        if (nb_GlobalData.g_global.ShopMenuActive == true)
        {
            nb_GlobalData.g_global.ShopMenuActive = false;

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }

            spineRenderers = mainLayer.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < spineRenderers.Length; ++i)
            {
                spineRenderers[i].enabled = true;
            }

            shopObj.SetActive(false);

            var sound = closeSoundObj.GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.Play();
            }
        }
    }
}

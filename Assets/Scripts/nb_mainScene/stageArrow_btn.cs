using UnityEngine;
using System.Collections;
using Spine.Unity;

public class stageArrow_btn : MonoBehaviour
{
    public bool isLeft;

    public nb_MainScene main;

    private float delay = 0.2f;
    private float moveValue = 0;

    void Start()
    {
        moveValue = isLeft ? 0.01f : -0.01f;
        //StartCoroutine("ScaleWidth");
    }

    //IEnumerator ScaleWidth()
    //{
    //    //iTween.ScaleTo(this.gameObject, new Vector3(1.0f, 1.1f), delay);
    //    iTween.MoveBy(this.gameObject, new Vector3(moveValue, 0), delay);

    //    yield return new WaitForSeconds(delay);

    //    //iTween.ScaleTo(this.gameObject, Vector3.one, delay);
    //    iTween.MoveBy(this.gameObject, new Vector3(-moveValue, 0), delay);

    //    yield return new WaitForSeconds(delay);

    //    StartCoroutine("ScaleWidth");
    //}


	void OnClick()
    {
        if (nb_GlobalData.g_global.MainMenuActive == true)
        {
            //Debug.Log("MenuOn Return");
            return;
        }

        if (nb_GlobalData.g_global.MainShopActive == true)
        {
            //Debug.Log("ShopOn Return");
            return;
        }

        if (nb_GlobalData.g_global.MainStageMove == true)
        {
            //Debug.Log("MoveOn Return");
            return;
        }

        nb_GlobalData.g_global.MainStageMove = true;

        if (isLeft)
        {
            //Debug.Log("click Left");
            main.moveStagePrev();
        }
        else
        {
            //Debug.Log("click Right");
            main.moveStageNext();
        }
    }
}

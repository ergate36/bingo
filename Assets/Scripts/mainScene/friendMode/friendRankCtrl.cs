using UnityEngine;
using System.Collections;

public class friendRankCtrl : MonoBehaviour
{
    Transform name;
    Transform profile;
    Transform vs;
    Transform detail;
    Transform bg;

    void Start()
    {

    }

    void Update()
    {

    }

    public void setRankInfo(friendData fd)
    {
        bg = transform.Find("Background");
        name = transform.Find("info_front/nickname");
        vs = transform.Find("info_front/vs");
        profile = transform.Find("info_front/Texture");
        detail = transform.Find("info_front/detail");

        name.GetComponent<UILabel>().text = fd.FriendName;

        StartCoroutine(GetWebImage(fd.FriendKey));

        if (fd.flag == 1)
        {
            bg.GetComponent<UISprite>().spriteName = "friendlist1";
            vs.GetComponent<UISprite>().spriteName = "vs_1";
            
            detail.GetComponent<UISprite>().spriteName = "detail_1";
        }
        else
        {
            //detail.GetComponent<BoxCollider>().enabled = false;
            vs.GetComponent<BoxCollider>().enabled = false;

            bg.GetComponent<UISprite>().spriteName = "friendlist2";
            vs.GetComponent<UISprite>().spriteName = "vs_3";

            detail.GetComponent<UISprite>().spriteName = "detail_3";
        }
    }

    public IEnumerator GetWebImage(string friendKey)
    {
        WWW www = new WWW("https://graph.facebook.com/" + friendKey + "/picture");
        yield return www;
        if (www.error == null)
        {
            profile.GetComponent<UITexture>().mainTexture = www.texture;
        }
    }


}

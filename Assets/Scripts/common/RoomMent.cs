using UnityEngine;
using System.Collections;

public class RoomMent : MonoBehaviour {

    bool alphaIsZero;

    float firstTime;
	// Use this for initialization
	void Start () {
        alphaIsZero = false;
        firstTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        if (alphaIsZero == false)
        {
            Color color = GetComponent<UISprite>().color;
            color.a = 0f;
            GetComponent<UISprite>().color = color;

            firstTime += Time.deltaTime;

            if (firstTime > 0.5f)
            {
                alphaIsZero = true;
                firstTime = 0f;
            }
        }
        else {
            firstTime += Time.deltaTime;

            Color color = GetComponent<UISprite>().color;
            color.a = 1f;
            GetComponent<UISprite>().color = color;

            if (firstTime > 0.5f)
            {
                alphaIsZero = false;
                firstTime = 0;
            }
        }



	}
}

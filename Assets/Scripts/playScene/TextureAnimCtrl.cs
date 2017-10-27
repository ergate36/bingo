using UnityEngine;
using System.Collections;

public class TextureAnimCtrl : MonoBehaviour {

    public int      spriteAmount;
    

    float elapsedTime;
    int spriteIndex;
    string texname;

    private UITexture uiTex;

    private bool end = false;
    private bool start = false;

    private string path = "texAnim/attack/";

	// Use this for initialization
	void Start () {
        elapsedTime = 0f;

        texname = GetComponent<SpriteRenderer>().sprite.name;
        texname.Substring(0, texname.Length - 2);

        start = true;
        spriteIndex = 0;
	}
	


	// Update is called once per frame
	void Update () {

        if (start)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 0.033f)
            {
                if (spriteIndex < 10)
                {
                    GetComponent<SpriteRenderer>().sprite.name = texname + "0" + spriteIndex.ToString();
                }
                else
                {
                   
                    GetComponent<SpriteRenderer>().sprite.name = texname + spriteIndex.ToString();
                 
                }

                if (spriteIndex < spriteAmount - 1)
                    ++spriteIndex;
                else {
                    end = true;
                    start = false;

                    spriteIndex = 0;
                }

           
                elapsedTime = 0f;
            }
        }

        
        

	}
}

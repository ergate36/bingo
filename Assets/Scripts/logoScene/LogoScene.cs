using UnityEngine;
using System.Collections;

public class LogoScene : MonoBehaviour {

    public  float alpha;
    float StartTime;
    float fadeOutTimer;
    public Texture tex;
    public float fadeInOutSpeed;
    public float logoDisplayTime;
    
    bool fadeIn;
    bool fadeOut;
	// Use this for initialization
    void Awake()
    {
        Screen.SetResolution(1440, 1080, false);
        //Screen.SetResolution(960, 640, true);
        //  Screen.SetResolution(1280, 720, true);
     
    }
	void Start () {
        StartTime = Time.time;

        fadeIn = true;
        fadeOut = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (fadeIn) {
            alpha = Mathf.Lerp(1.0f, 0.0f, (Time.time - StartTime)*fadeInOutSpeed);
            if (alpha <= 0f) {
                fadeIn = false;

                //AudioClip clip = GetComponent<AudioSource>().clip; 
                //GetComponent<AudioSource>().PlayOneShot(clip);
            }
        }

        if (!fadeOut) {
            if (Time.time -  StartTime > logoDisplayTime) {
                fadeOut = true;
                fadeOutTimer = Time.time;
            }
            
        }
       
       

        if (fadeOut) {
            alpha = Mathf.Lerp(0.0f, 1.0f, (Time.time - fadeOutTimer) * fadeInOutSpeed);
            if (alpha >= 1f) {
                fadeOut = false;
                onNextLevel();
                
            }
        }


        
	}
    void onNextLevel()
    {
        Application.LoadLevel("nb_LoginScene");
    }

    void OnGUI()
    {
        if (tex)
        {
            Color color = Color.black;
            color.a = alpha;
            GUI.color = color;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);
           // Debug.Log("aaaa");
        }
        
    }


}

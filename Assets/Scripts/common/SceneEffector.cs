using UnityEngine;
using System.Collections;

public class SceneEffector : MonoBehaviour {

    Color color = Color.white;

    [HideInInspector]
    public float alpha = 1.0f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void fadeOut(float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", gameObject));
    }

    public void fadeIn(float time)
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", gameObject));
    }


    private void setalpha(float val)
    {
        alpha = val;
        color.a = alpha;
        Debug.Log(color);

        transform.GetComponent<SpriteRenderer>().color = color;
    }

}

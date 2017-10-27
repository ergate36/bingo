using UnityEngine;
using System.Collections;

public class Yabawe_Ctrl : MonoBehaviour {

    Animation anim;
	// Use this for initialization

    public GameObject root;
    [HideInInspector]
    public Transform yabawe;
    [HideInInspector]
    public Transform back;
    private Transform setPos;
    public int index;

    void Awake()
    {
        anim = transform.GetComponentInChildren<Animation>();
      //  yabawe = transform.FindChild("yabawe");
        back = transform.Find("back");
    }

    void Start()
    {
      //  yabawe.GetComponent<UISprite>().spriteName = "game_result_gift1";     
    }

    void OnClick()
    {
        Transform btnroot1 = root.transform.Find("yabawe_monster_0");
        Transform btnroot2 = root.transform.Find("yabawe_monster_1");
        Transform btnroot3 = root.transform.Find("yabawe_monster_2");

        btnroot1.GetComponent<BoxCollider>().enabled = false;
        btnroot2.GetComponent<BoxCollider>().enabled = false;
        btnroot3.GetComponent<BoxCollider>().enabled = false;

        transform.parent.GetComponent<Reward_Ctrl>().selectedMonster = index;

        int flag = 1;
        if (transform.parent.GetComponent<Reward_Ctrl>().isRetry)
            flag = 2;

        //transform.parent.GetComponent<Reward_Ctrl>().onGetReward(flag);

        StartCoroutine(plyaAybawe());
        StartCoroutine(transform.parent.GetComponent<Reward_Ctrl>().showOtherReward(index));
        
    }

    public void setButtonActive(bool active)
    {
        GetComponent<BoxCollider>().enabled = active;
    }

    public IEnumerator openMouse(float time)
    {
        yield return new WaitForSeconds(time);
        //yabawe.GetComponent<UISprite>().spriteName = "game_result_gift";
        //back.gameObject.SetActive(false);
    }

    private IEnumerator plyaAybawe()
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        // yabawe.GetComponent<UISprite>().spriteName = "game_result_gift2";

        // 141127

        //anim.SetBool("idle", false);
        //anim.SetBool("yabawe", true);
        
        GetComponent<AudioSource>().clip = GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.yabawe_loop];
        GetComponent<AudioSource>().Play();
        switch (index)
        {
            case 0:
                anim.Play("eff_treasurebox_left_animation");
                break;
            case 1:
                break;
            case 2:
                anim.Play("eff_treasurebox_right_animation");
                break;
        }

        yield return new WaitForSeconds(0.6f);

        anim.Play("eff_treasurebox_loop_animation");
        
        GameObject effect = Instantiate(Resources.Load("effects/yun/eff_treasurebox")) as GameObject;
        Vector3 scale = effect.transform.localScale;

        effect.transform.parent = transform.parent.GetComponent<Reward_Ctrl>().yabawePos[1];
        effect.transform.localPosition = transform.parent.GetComponent<Reward_Ctrl>().yabawePos[1].position;
        effect.transform.localScale = scale;

        yield return new WaitForSeconds(0.4f);

        anim.Play("eff_treasurebox_disappear_animation");
        
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.yabawe_end];
        GetComponent<AudioSource>().Play();

        if (effect)
        {
            Destroy(effect);
        }
        
        //yabawe.GetComponent<UISprite>().spriteName = "game_result_gift";

        //anim.SetBool("yabawe", false);
        //anim.SetBool("idle", true);

        transform.parent.GetComponent<Reward_Ctrl>().setActiveCardButton(false);

        int flag = 1;

        if (transform.parent.GetComponent<Reward_Ctrl>().isRetry)
            flag = 2;

        StartCoroutine(transform.parent.GetComponent<Reward_Ctrl>().showReward(flag));
    }
}

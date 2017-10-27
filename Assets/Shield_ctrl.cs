using UnityEngine;
using System.Collections;

public class Shield_ctrl : MonoBehaviour {

    private Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        anim.SetBool("shield_end", false);
    }

    public void unactiveShield()
    {
        anim.SetBool("shield_end", true);
    }
}

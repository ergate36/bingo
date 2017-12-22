using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nb_LogoTest : MonoBehaviour
{
    public float delay;

    void Start()
    {
        StartCoroutine("test");
	}

    IEnumerator test()
    {
        iTween.ScaleBy(this.gameObject, new Vector3(0.8f, 1.3f), delay);

        yield return new WaitForSeconds(delay);

        iTween.ScaleBy(this.gameObject, new Vector3(1.3f, 0.8f), delay);

        yield return new WaitForSeconds(delay);

        StartCoroutine("test");
    }
}

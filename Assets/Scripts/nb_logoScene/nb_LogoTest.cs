using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nb_LogoTest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("test");
	}

    IEnumerator test()
    {
        iTween.ScaleBy(this.gameObject, new Vector3(0.8f, 1.3f), 0.3f);

        yield return new WaitForSeconds(0.3f);

        iTween.ScaleBy(this.gameObject, new Vector3(1.3f, 0.8f), 0.3f);

        yield return new WaitForSeconds(0.3f);

        StartCoroutine("test");
    }
}

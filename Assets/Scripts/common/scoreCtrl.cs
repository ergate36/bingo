using UnityEngine;
using System.Collections;

public class scoreCtrl : MonoBehaviour {

    public IEnumerator setEffect( int score )
    {
        transform.GetComponent<UILabel>().text = "+" + score.ToString();

        iTween.MoveBy(gameObject, Vector3.up * 0.3f, 1.0f);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
    public IEnumerator setMinusEffect(int score)
    {
        transform.GetComponent<UILabel>().text = "-" + score.ToString();

        iTween.MoveBy(gameObject, Vector3.up * 0.3f, 1.0f);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}

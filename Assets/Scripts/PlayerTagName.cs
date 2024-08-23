using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTagName : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sr;
    public float duration;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeFrameColor());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startCouroutineColorChange()
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeFrameColor());
    }
    private IEnumerator ChangeFrameColor()
    {
        Color red = Color.cyan;
        Color orange = new Color(1f, 0.64f, 0f);
        Color yellow = Color.yellow;

        while (true)
        {

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                sr.color = Color.Lerp(red, orange, t);
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                sr.color = Color.Lerp(orange, yellow, t);
                yield return null;
            }

     
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                sr.color = Color.Lerp(yellow, orange, t);
                yield return null;
            }


            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                sr.color = Color.Lerp(orange, red, t);
                yield return null;
            }
        }
    }
}

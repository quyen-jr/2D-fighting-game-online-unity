using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FrameWhenChoosePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Image imageFrame;
    public float duration;
    void Start()
    {
        imageFrame = GetComponent<Image>();
        StartCoroutine(ChangeFrameColor());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startCouroutineColorChange()
    {
        if (imageFrame == null)
            imageFrame = GetComponent<Image>();
        StartCoroutine(ChangeFrameColor());
    }
    private IEnumerator ChangeFrameColor()
    {
        Color red = Color.cyan;
        Color orange = new Color(1f, 0.64f, 0f);
        Color yellow = Color.yellow;

        // float duration = 0.1f; // Thời gian chuyển màu từ một màu sang màu khác

        while (true)
        {
            // Chuyển từ đỏ sang cam
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                imageFrame.color = Color.Lerp(red, orange, t);
                yield return null;
            }

            // Chuyển từ cam sang vàng
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                imageFrame.color = Color.Lerp(orange, yellow, t);
                yield return null;
            }

            // Chuyển từ vàng về cam
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                imageFrame.color = Color.Lerp(yellow, orange, t);
                yield return null;
            }

            // Chuyển từ cam về đỏ
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                imageFrame.color = Color.Lerp(orange, red, t);
                yield return null;
            }
        }
    }
}

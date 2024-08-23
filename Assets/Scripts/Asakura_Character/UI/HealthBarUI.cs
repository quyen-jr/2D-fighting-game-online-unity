using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HeathBarUi : MonoBehaviour
{
    public Entity entity;
    public Slider slider;
    //public Slider sliderRight;
    PhotonView view;
    private void Start()
    {
        //   sliderLeft.value = 0;
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        view = GetComponent<PhotonView>();
        if (entity.photonView.IsMine)
        {
            Debug.Log("master");
        }
        else
        {
            Debug.Log("client");
        }
        StartCoroutine(ChangeSliderColor());
    }
    private void Update()
    {
        if (entity == null) return;
        if (!entity.photonView.IsMine) return;
        entity.photonView.RPC("RPC_UpdateHealthUI", RpcTarget.AllViaServer);
        // UpdateHealthUI();
    }
    public void UpdateHealthUI()
    {
        if (entity == null || slider == null) return;
        slider.maxValue = entity.GetMaxHealthValue();
        slider.value = entity.currentHealth;

    }
    private IEnumerator ChangeSliderColor()
    {
        Color red = Color.red;
        Color orange = new Color(1f, 0.64f, 0f);
        Color yellow = Color.yellow;

        float duration = 0.1f; // Thời gian chuyển màu từ một màu sang màu khác

        while (true)
        {
            // Chuyển từ đỏ sang cam
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                slider.fillRect.GetComponent<Image>().color = Color.Lerp(red, orange, t);
                yield return null;
            }

            // Chuyển từ cam sang vàng
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                slider.fillRect.GetComponent<Image>().color = Color.Lerp(orange, yellow, t);
                yield return null;
            }

            // Chuyển từ vàng về cam
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                slider.fillRect.GetComponent<Image>().color = Color.Lerp(yellow, orange, t);
                yield return null;
            }

            // Chuyển từ cam về đỏ
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                slider.fillRect.GetComponent<Image>().color = Color.Lerp(orange, red, t);
                yield return null;
            }
        }
    }




}

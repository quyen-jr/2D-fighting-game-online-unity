using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPosSkillEffect_Controller : MonoBehaviour
{
    private PhotonView view=>GetComponent<PhotonView>();
    private Animator anim => GetComponentInChildren<Animator>();
    [SerializeField] private Camera camera1;
    [SerializeField] private float y;
    private float timeLife=1f;
    private AudioSource audioEffect=>GetComponent<AudioSource>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject objectCamera = GameObject.FindWithTag("MainCamera");
        camera1 = objectCamera.GetComponent<Camera>();
        StartCoroutine(ActiveAudio());
    }
    public void Active()
    {
        anim.SetBool("Active", true);    
    }
    // Update is called once per frame
    void Update()
    {
        float scaleValue = camera1.orthographicSize / 9.2f;
        transform.localScale = new Vector2(scaleValue,scaleValue);
        transform.position = new Vector2( camera1.transform.position.x, camera1.transform.position.y);

        timeLife -= Time.deltaTime;
        if (timeLife < 0)
        {
            Destroy(gameObject);
            
        }
    }
    IEnumerator ActiveAudio()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("call Audio");
        if(audioEffect != null)
        {
            audioEffect.Play();
        }
    }
}

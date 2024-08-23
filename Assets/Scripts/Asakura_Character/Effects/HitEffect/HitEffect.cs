using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeLife=0.6f;
    private float faceDir;

    [Header("Audio")]
    [SerializeField] private AudioClip hitClip;
    private AudioSource hitSound = new AudioSource();
    private Animator anim => GetComponentInChildren<Animator>();

    private void Start()
    {
        hitSound = gameObject.AddComponent<AudioSource>();

        hitSound.clip = hitClip;
        hitSound.Play();
    }
    // Update is called once per frame
    public void SetUpEffect(float _faceDir)
    {
        faceDir = _faceDir;
        transform.localScale= new Vector2(_faceDir,transform.localScale.y);
        anim.SetBool("HitEffect", true);

    }
    void Update()
    {
        timeLife -= Time.deltaTime;
        if(timeLife < 0)
        {
            Destroy(gameObject);
        }
    }
}

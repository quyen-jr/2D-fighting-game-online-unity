using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ulti_Effect : MonoBehaviour
{
    private Animator anim => GetComponentInChildren<Animator>();
    [SerializeField] private float timeLife;
    private Vector2 newSize;
    public void SetUpEffect()
    {
        anim.SetBool("UltiEffectRun", true);
    }
    void Start()
    {
        newSize = transform.localScale * 3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, newSize, Time.deltaTime * 2);
        timeLife -= Time.deltaTime;
        if (timeLife < 0)
        {
            SeflDestroy();
        }
    }
    private void SeflDestroy()
    {
        Destroy(gameObject);
    }
}

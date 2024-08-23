using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGame : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeLife=3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLife-=Time.deltaTime;
        if(timeLife < 0)
        {
            Destroy(gameObject);
        }
    }
}

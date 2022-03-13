using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject self;
    private float lifeSpan = 5.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("bulletController");
        lifeSpan -= Time.deltaTime;
        //Debug.Log(lifeSpan);
        if (lifeSpan <= 0.00f)
        {
            Destroy(self);
        }
    }
}

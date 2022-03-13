using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diagnosticsManagment : MonoBehaviour
{

    public statsManager stmngr;


    private bool hasEnteredFixingZone = false;
    private bool hasImpacated = false;
    private bool hitable = true;


    void Start()
    {
        
    }
    void Update()
    {
        if (hitable && hasImpacated)
        {
            //heal random amount between 1% and 5%
            if(stmngr.health < 1f)
            {
                
                stmngr.updateHealth((Random.Range(0.01f, 0.05f)));
                GetComponent<AudioSource>().Play();
            }
            hitable = false;
        }
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "exitZone" )
        {
            
            hasEnteredFixingZone = true;
        }
        if (other.gameObject.name == "diagnosticsPanel")
        {
           
            hasImpacated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "exitZone")
        {
            hasEnteredFixingZone = false;
            hitable = true;
        }
        if (other.gameObject.name == "diagnosticsPanel")
        {
            hasImpacated = false;
        }
    }

}

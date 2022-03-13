using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelController : MonoBehaviour
{

    public List<GameObject> buttons;
    
    public void disablePanel()
    {
        foreach(GameObject g in buttons)
        {
            g.GetComponent<buttonController>().buttonEnabled = false;
        }
    }
    public void enablePanel()
    {
        foreach (GameObject g in buttons)
        {
            g.GetComponent<buttonController>().buttonEnabled = true;
        }
    }
}

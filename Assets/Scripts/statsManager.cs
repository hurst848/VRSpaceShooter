using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statsManager : MonoBehaviour
{
    public float health;
    public int asteroidsDestroyed;
    public healhBarScript healhBar;
    public scoreScript scoreDisplay;


    // Start is called before the first frame update
    void Start()
    {
        health = 1.0f;
        asteroidsDestroyed = 0;
        healhBar.setMaxHealth(100);
    }

    // Update is called once per frame
   
    public void updateHealth(float _x)
    {
        health += _x;
        int castedHealth = (int)(_x * 100);
        healhBar.updateHealth(castedHealth);
        //Debug.Log("health has been updated");
    }
    public void updateAsteroidsDestroyed()
    {
        asteroidsDestroyed++;
        scoreDisplay.updateScore(asteroidsDestroyed);
        //Debug.Log("asteroids destroyed has been updated");
    }
}

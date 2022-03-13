using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class pauseScript : MonoBehaviour
{
    public GameObject turret;
    public GameObject asteroids;
    public asteroidSpawnController asteroidcontrol;
    public List<GameObject> buttons;
    public GameObject mangagment;
    public bool passedTutorial= false;


    private List<Vector3> asteroidVectors;
    private bool pause = false;
    private bool hasPaused = false;

    public GameObject leftHand;
    public GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
        
        //asteroidVectors = new List<Vector3>(0);
    }

   
    private void LateUpdate()
    {
        if (pause && !hasPaused)
        {
            pauseTime();
            hasPaused = true;
        }
        else if (!pause && hasPaused)
        {
            resumeTime();
            hasPaused = false;
        }
    }

    public void pauseTime()
    {
        asteroidVectors = new List<Vector3>(0);

       
        for (int i = 0; i < asteroidcontrol.activeAsteroids.Count; i++)
        {
            
            asteroidVectors.Add(asteroidcontrol.activeAsteroids[i].GetComponent<Rigidbody>().velocity);
           
            asteroidcontrol.activeAsteroids[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            

        }
        if (passedTutorial)
        {
            mangagment.GetComponent<gameManagment>().isPaused = true;
        }
        

        asteroidcontrol.isPaused = true;

        rightHand.GetComponent<XRDirectInteractor>().allowSelect = false;
        leftHand.GetComponent<XRDirectInteractor>().allowSelect = false;

        for (int i =0; i< buttons.Count; i++)
        {

            buttons[i].GetComponent<buttonController>().buttonEnabled = false;
           
        }

        
    }

    public void resumeTime()
    {
        for (int i = 0; i < asteroidcontrol.activeAsteroids.Count; i++)
        {
            asteroidcontrol.activeAsteroids[i].GetComponent<Rigidbody>().velocity = asteroidVectors[i];
        }
        asteroidcontrol.isPaused = false;

        if (passedTutorial)
        {
            mangagment.GetComponent<gameManagment>().isPaused = false; 
        }

        rightHand.GetComponent<XRDirectInteractor>().allowSelect = true;
        leftHand.GetComponent<XRDirectInteractor>().allowSelect = true;

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].GetComponent<buttonController>().buttonEnabled = true;
        }


    }
    public void togglePause()
    {
        if (pause)
        {
            pause = false;
            resumeTime();
        }
        else
        {
            pause = true;
            pauseTime();
        }
    }

    public void enableHands()
    {
        rightHand.GetComponent<XRDirectInteractor>().allowSelect = true;
        leftHand.GetComponent<XRDirectInteractor>().allowSelect = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManagment : MonoBehaviour
{
    
    public AudioSource speaker;
    public AudioSource backgroundMusic;

    public GameObject turret;
    public GameObject cockpitPause;
    public GameObject cockpitShroud;
    public List<AudioClip> sounds;
   
    public GameObject tutorialPanel;
    public GameObject asteroidControl;
    public GameObject exitPanel;
    public GameObject pauseButton;

    public bool isPaused = false;

    private Vector3 tutMenuScale;
    private Vector3 shroudScale;
    public Vector3 exitMenuScale;

    private Vector3 turretUseableLocation;
    private Vector3 turretStorageLocation;


    private bool hasPaused = false;

    private int tutorial = 0; // 0 = unselected; 1 = yes, 2= no
   
    void Start()
    {
        turretStorageLocation = turret.transform.localPosition;
        turretUseableLocation = new Vector3(-0.096f,1.3f,1.39f);

        shroudScale = cockpitShroud.transform.localScale;

        cockpitPause.GetComponent<pauseScript>().pauseTime();
        
        tutMenuScale = tutorialPanel.transform.localScale;
        tutorialPanel.transform.localScale = Vector3.zero;

        exitMenuScale = exitPanel.transform.localScale;
        exitPanel.transform.localScale = Vector3.zero;
        

        tutorialPanel.GetComponent<panelController>().disablePanel();
        exitPanel.GetComponent<panelController>().disablePanel();

        StartCoroutine(startGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator startGame()
    {
        yield return new WaitForSeconds(1.5f);

        if (mainMenuManager.tutorialEnabled)
        {
            speaker.clip = sounds[0];
            speaker.Play();
            yield return new WaitForSeconds(sounds[0].length - 0.5f);
            StartCoroutine(growGameObject(tutorialPanel, tutMenuScale, 1));
            yield return new WaitForSeconds(1);
            tutorialPanel.GetComponent<panelController>().enablePanel();
            while (true)
            {
                if (tutorial != 0)
                {
                    break;
                }
                yield return new WaitForSeconds(0.001f);
            }
            StartCoroutine(shrinkGameObject(tutorialPanel, tutMenuScale, 1));
            tutorialPanel.GetComponent<panelController>().disablePanel();

            if (tutorial == 1)
            {
                speaker.clip = sounds[3];
                speaker.Play();
                yield return new WaitForSeconds(sounds[3].length + 2);
            }

        }
        speaker.clip = sounds[1];
        speaker.Play();
        backgroundMusic.Play();
        StartCoroutine(shrinkGameObject(cockpitShroud,shroudScale,1));
        StartCoroutine(moveTurretDown());
        yield return new WaitForSeconds(4.636f);
        cockpitPause.GetComponent<pauseScript>().resumeTime();
        cockpitPause.GetComponent<pauseScript>().passedTutorial = true;
        asteroidControl.GetComponent<asteroidSpawnController>().switchSpawningActive();
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            
            if (asteroidControl.GetComponent<asteroidSpawnController>().finished)
            {
                break;
            }
            if (isPaused && !hasPaused)
            {
                //Debug.Log("growingExitMenu");
                exitPanel.GetComponent<panelController>().enablePanel();
                StartCoroutine(growGameObject(exitPanel, exitMenuScale, 1));
                hasPaused = true;
            }
            else if (!isPaused && hasPaused)
            {
                //Debug.Log("shrinkingExitMenu");
                exitPanel.GetComponent<panelController>().disablePanel();
                StartCoroutine(shrinkGameObject(exitPanel, exitMenuScale, 1));
                hasPaused = false;
            }
            
            yield return new WaitForSeconds(0.001f);
        }
        backgroundMusic.Stop();
        speaker.clip = sounds[2];
        speaker.Play();
        cockpitPause.GetComponent<pauseScript>().pauseTime();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(growGameObject(cockpitShroud, shroudScale, 1));
        cockpitPause.GetComponent<pauseScript>().pauseTime();
        pauseButton.GetComponent<buttonController>().buttonEnabled = false;
        StartCoroutine(moveTurretUp());
        StartCoroutine(growGameObject(exitPanel, exitMenuScale, 1));
        exitPanel.GetComponent<panelController>().enablePanel();
        cockpitPause.GetComponent<pauseScript>().enableHands();
        yield return null;
    }

    public void tutorialStart()
    {
        tutorial = 1;
    }
    public void tutorialSkip()
    {
        tutorial = 2;
    }



    public IEnumerator growGameObject(GameObject _menu, Vector3 _scale, float speed)
    {
        _menu.SetActive(true);
        //Debug.Log(_scale.x + " " + _scale.y + " " + _scale.z + " ");
        float growthX = (_scale.x / 100) / speed;
        float growthY = (_scale.y / 100) / speed;
        float growthZ = (_scale.z / 100) / speed;
        Vector3 growth = new Vector3(growthX, growthY, growthZ);
        while (true)
        {
            _menu.transform.localScale += growth;

            yield return new WaitForSeconds(0.01f);
            if (_scale == _menu.transform.localScale)
            {
                break;
            }
        }
        
    }
    public IEnumerator shrinkGameObject(GameObject _menu, Vector3 _scale, float speed)
    {
        //Debug.Log(_scale.x + " " + _scale.y + " " + _scale.z + " ");
        float growthX = (_scale.x / 100) / speed;
        float growthY = (_scale.y / 100) / speed;
        float growthZ = (_scale.z / 100) / speed;
        Vector3 growth = new Vector3(growthX, growthY, growthZ);
        while (true)
        {
            _menu.transform.localScale -= growth;

            yield return new WaitForSeconds(0.01f);
            if (Vector3.zero == _menu.transform.localScale)
            {
                break;
            }
        }
        _menu.SetActive(false);
    }
    IEnumerator moveTurretDown()
    {
       // while (turret.transform.localPosition.y > 1.3)
       // {
            
        //    turret.transform.position = new Vector3(turret.transform.position.x, turret.transform.position.y - 0.0135f, turret.transform.position.z);
         //   yield return new WaitForSeconds(0.01f);
        //}
        for (float i = 0.00f; i < 1.0f; i += 0.01f)
        {
            turret.transform.localPosition = Vector3.Lerp(turretStorageLocation, turretUseableLocation, i);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator moveTurretUp()
    {
        //while (turret.transform.localPosition.y > 1.3)
        //{

        //   turret.transform.position = new Vector3(turret.transform.position.x, turret.transform.position.y - 0.0135f, turret.transform.position.z);
        //   yield return new WaitForSeconds(0.01f);
        //}
        Vector3 tmp = (turret.transform.localPosition);
        for (float i = 0.00f; i< 1.0f; i+=0.01f)
        {
            turret.transform.localPosition = Vector3.Lerp(tmp,turretStorageLocation,i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void reloadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class mainMenuManager : MonoBehaviour
{
    public static bool tutorialEnabled = true;

    public GameObject parentMenu;
    public GameObject exitMenu;
    public volumeBarScript volumeControl;
    
    public SpriteRenderer tutorialEnableButton;
    public List<Sprite> tutorialLabels = new List<Sprite>(2);

    public float transitionTime;

    private Vector3 parentMenuScale;
    private Vector3 exitMenuScale;
    



    // Start is called before the first frame update
    void Start()
    {
        volumeControl.setMaxVolume(100);
        InputTracking.Recenter();
        parentMenuScale = parentMenu.transform.localScale;
        exitMenuScale = exitMenu.transform.localScale;
        exitMenu.transform.localScale = Vector3.zero;
        exitMenu.SetActive(false);
    }

    

    public void loadMainGame()
    {
        SceneManager.LoadScene(1);
    }
    public void initQuit()
    {
        StopAllCoroutines();
        Application.Quit();
    }

    public void toMainMenu()
    {
        StartCoroutine(switchToMain());
    }
    public void toExitMenu()
    {
        StartCoroutine(switchToExit());
    }


    public IEnumerator switchToMain()
    {
        StartCoroutine(shrinkMenu(exitMenu, exitMenuScale));
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(growMenu(parentMenu, parentMenuScale));
    }
    public IEnumerator switchToExit()
    {
        Debug.Log("exitMenuCalled");
        StartCoroutine(shrinkMenu(parentMenu, parentMenuScale));
        yield return new WaitForSeconds(transitionTime);
        StartCoroutine(growMenu(exitMenu, exitMenuScale));
        
    }

    IEnumerator growMenu(GameObject _menu, Vector3 _scale)
    {
        _menu.SetActive(true);
        float growthX = (_scale.x / 100) / transitionTime;
        float growthY = (_scale.y / 100) / transitionTime;
        float growthZ = (_scale.z / 100) / transitionTime;
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
        //yield return null;
    }
    IEnumerator shrinkMenu(GameObject _menu, Vector3 _scale)
    {
        float growthX = (_scale.x / 100) / transitionTime;
        float growthY = (_scale.y / 100) / transitionTime;
        float growthZ = (_scale.z / 100) / transitionTime;
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
        //yield return null;
    }

    public void toggleTutorial()
    {
        if (tutorialEnabled)
        {
            tutorialEnabled = false;
            tutorialEnableButton.sprite = tutorialLabels[1];
        }
        else 
        {
            tutorialEnabled = true;
            tutorialEnableButton.sprite = tutorialLabels[0];
        }
    }
}
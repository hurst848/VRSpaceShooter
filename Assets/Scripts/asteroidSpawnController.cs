using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidSpawnController : MonoBehaviour
{

    public List<GameObject> spawners;
    public List<GameObject> targets;
    public List<GameObject> asteroidPrefabs;
    public GameObject statCntrl;
    public statsManager scoreInfo;
    public List<GameObject> activeAsteroids;
    public List<float> timeBetweenSpawns;

    public bool finished = false;

    public bool spawnRoutineActive = false;
    public bool isPaused = false;
    private int level = 0;
    private bool canPress = true;
    private bool spawnRoutineFinished = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isPaused)
        {
            if (spawnRoutineActive && spawnRoutineFinished)
            {
                StartCoroutine(asteroidSpawnerCoroutine());
                spawnRoutineFinished = false;
                canPress = false;
            }
            for (int i = 0; i < activeAsteroids.Count; i++)
            {
                activeAsteroids[i].GetComponent<asteroidController>().moveAsteroid();

                if (activeAsteroids[i].GetComponent<asteroidController>().isDestroyed == true)
                {
                    Destroy(activeAsteroids[i]);
                    activeAsteroids.RemoveAt(i);
                }
            } 
        }
        
    
    }

    IEnumerator asteroidSpawnerCoroutine()
    {
        while (scoreInfo.health > 0)
        {
            if (spawnRoutineActive)
            {
                //Debug.Log("reached spawning");
                if (scoreInfo.asteroidsDestroyed >= 10 && scoreInfo.asteroidsDestroyed < 25)
                {
                    level = 1;
                }
                else if (scoreInfo.asteroidsDestroyed >= 25 && scoreInfo.asteroidsDestroyed < 35)
                {
                    level = 2;
                }
                else if (scoreInfo.asteroidsDestroyed >= 35 && scoreInfo.asteroidsDestroyed < 50)
                {
                    level = 3;
                }
                else if (scoreInfo.asteroidsDestroyed >= 50 && scoreInfo.asteroidsDestroyed < 65)
                {
                    level = 4;
                }
                else if (scoreInfo.asteroidsDestroyed >= 65 && scoreInfo.asteroidsDestroyed < 75)
                {
                    level = 5;
                }
                else if (scoreInfo.asteroidsDestroyed >= 75 && scoreInfo.asteroidsDestroyed < 95)
                {
                    level = 6;
                }
                else if (scoreInfo.asteroidsDestroyed >= 95 && scoreInfo.asteroidsDestroyed < 115)
                {
                    level = 7;
                }
                else if (scoreInfo.asteroidsDestroyed >= 115)
                {
                    level = 7;
                }
                if (!isPaused)
                {
                    instantiateAsteroid();
                    yield return new WaitForSeconds(timeBetweenSpawns[level]);
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }
                
            } 
        }
        spawnRoutineFinished = true;
        finished = true;
    }

   

    public void instantiateAsteroid()
    {
        int spawner = Random.Range(0, spawners.Count-1);
        int target = Random.Range(0, targets.Count - 1);
        int asteroid = Random.Range(0, asteroidPrefabs.Count - 1);

        Quaternion rot = new Quaternion();
        rot.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        GameObject newAsteroid = Instantiate(asteroidPrefabs[asteroid],spawners[spawner].transform.position, rot);
        newAsteroid.GetComponent<asteroidController>().setTarget(targets[target]);
        newAsteroid.GetComponent<asteroidController>().statController = statCntrl;
        newAsteroid.name = "asteroid";
        activeAsteroids.Add(newAsteroid);
    }

    public void switchSpawningActive()
    {
        if (spawnRoutineActive && canPress)
        {
            spawnRoutineActive = false;
        }
        else if (!spawnRoutineActive && canPress)
        {
            spawnRoutineActive = true;
        }
    }
}

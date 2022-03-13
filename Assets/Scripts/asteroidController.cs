using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidController : MonoBehaviour
{
    public GameObject statController;
    public GameObject self;
    public ParticleSystem explosion;
    
    public float asteroidCoefficant = 1;
    public bool isDestroyed;
    

    private GameObject target;
    private Collider asteroidCollider;
    private Rigidbody asteroidRidgidbody;

    private bool hitShip;
    private bool hitBullet;
    

    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        asteroidCollider = self.GetComponent<Collider>();
        asteroidRidgidbody = self.GetComponent<Rigidbody>();
        hitShip = false;
        hitBullet = false;
    }

   


    public void moveAsteroid()
    {
        asteroidRidgidbody.AddForce(asteroidCoefficant * Vector3.Normalize(target.transform.position - asteroidRidgidbody.transform.position));
        if (hitShip)
        {
            self.GetComponent<AudioSource>().Play();
            ParticleSystem exp = Instantiate(explosion, self.transform.position, Quaternion.identity);
            exp.Play();
            statController.GetComponent<statsManager>().updateHealth(-(Random.Range(0.01f, 0.15f)));
            isDestroyed = true;
            

        }
        else if (hitBullet)
        {
            self.GetComponent<AudioSource>().Play();
            ParticleSystem exp = Instantiate(explosion, self.transform.position, Quaternion.identity);
            exp.Play();
            statController.GetComponent<statsManager>().updateAsteroidsDestroyed();
            isDestroyed = true;
            

        }
    }


    private void LateUpdate()
    {
        //if (isDestroyed)
        //{
        //    Destroy(gameObject);
        //}
    }

    public void setTarget(GameObject t)
    {
        target = t;
    }

   private void OnTriggerEnter(Collider other)
   {
        //Debug.Log("collisionDetection");
        if (other.gameObject.name == "shipCollider")
        {
            hitShip = true;
            //Debug.Log("hitShip");
        }
        else if (other.gameObject.name == "bullet")
        {
            //Debug.Log("hitbullet");
            hitBullet = true;
            Destroy(other.gameObject);
        }
        else
        {
            // do nothing
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
        
    //}
    


}

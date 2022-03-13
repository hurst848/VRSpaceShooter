using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;



[System.Serializable]
public class turretController : MonoBehaviour
{

    //public float lockPosition = 0;
    public GameObject bullet;
    public GameObject bulletspawn;
    public int bulletSpeed = 100;



    private InputDevice targetDevice;
    public bool shootActive = false;
    // Start is called before the first frame update
    void Start()
    {

        
        List<InputDevice> devices = new List<InputDevice>();
        //InputDevices.GetDevices(devices);

        InputDeviceCharacteristics rightControllerCharateritics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharateritics, devices);
       

        foreach (var item in devices)
        {
            Debug.Log(item.name + item);
        }
        if(devices.Count>0)
        {
            targetDevice = devices[0];
        }
    }

    
    public void activateShoot()
    {
        shootActive = true;
    }
    public void deactivateShoot()
    {
        shootActive = false;
    }
    public void shoot()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f && shootActive)
        {
            GameObject firedBullet = Instantiate(bullet, bulletspawn.transform.position, bulletspawn.transform.rotation) as GameObject;
            firedBullet.name = "bullet";
            firedBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        }
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class handPresenceScript : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharateristics;
    public InputDeviceCharacteristics headsetCharateristics;
    public GameObject handModelPrefab;


    private InputDevice targetDevice;
    private GameObject spawnedHandModel;
    private bool handsActive = true;
    private Animator handAnimator;
   
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        
        InputDevices.GetDevicesWithCharacteristics(controllerCharateristics, devices);

        //InputDevices.GetDevices(devices);
        InputTracking.Recenter();

        foreach (var item in devices)
        {
            //nothing for now
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            spawnedHandModel = Instantiate(handModelPrefab, transform);
        }
        handAnimator = spawnedHandModel.GetComponent<Animator>();
        
    }

    void UpdatehandAnimations()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger,out float triggerout))
        {
            handAnimator.SetFloat("Trigger", triggerout);
            //Debug.Log("Trigger");
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripout))
        {
            handAnimator.SetFloat("Grip", gripout);
            //Debug.Log("Grip");
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    public void ActivateHand()
    {
        spawnedHandModel.SetActive(true);
    }
    public void DeactivateHand()
    {
        spawnedHandModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("handPresenceScript");
        if(handsActive)
        {
            spawnedHandModel.SetActive(true);
            UpdatehandAnimations();
        }
        else
        {
            spawnedHandModel.SetActive(false);
        }

    }
}

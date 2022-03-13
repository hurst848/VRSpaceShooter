using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class buttonController : XRBaseInteractable
{

    public UnityEvent onPress = null;
    public bool buttonEnabled = true;
    
    private bool prevPress = false;

  
    private float min = 0.00f;
    private float max = 0.00f;
    private float prevHandHeight;
    private XRBaseInteractor hoverInteractor = null;


    protected override void Awake()
    {
        base.Awake();
        onHoverEnter.AddListener(startPress);
        onHoverExit.AddListener(endPress);
    }
    private void OnDestroy()
    {
        onHoverEnter.RemoveListener(startPress);
        onHoverExit.RemoveListener(endPress);
    }

    private void startPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        prevHandHeight = getLocalYPos(hoverInteractor.transform.position);
    }

    private void endPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        prevHandHeight = 0.00f;

        prevPress = false;
        setYPos(max);
    }

    private void Start()
    {
        setMinMax();
    }

    private void setMinMax()
    {
        Collider clidr = GetComponent<Collider>();
        min = transform.localPosition.y - (clidr.bounds.size.y * 0.5f ) + 0.025f;
        max = transform.localPosition.y;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (hoverInteractor)
        {
            float handHeight = getLocalYPos(hoverInteractor.transform.position);
            float handDiff = prevHandHeight - handHeight;
            prevHandHeight = handHeight;

            float newPos = transform.localPosition.y - handDiff;
            setYPos(newPos);

            checkPress();
        }
       
    }

    private float getLocalYPos(Vector3 pos)
    {
        Vector3 lclPos = transform.root.InverseTransformPoint(pos);
        return lclPos.y;
    }

    private void setYPos(float pos)
    {
        Vector3 newPos = transform.localPosition;
        newPos.y = Mathf.Clamp(pos, min, max);
        transform.localPosition = newPos;
    }
    
    private void checkPress()
    {
        bool inPos = inPosition();
        if (inPos && inPos!=prevPress && buttonEnabled)
        {
            onPress.Invoke();
        }
        prevPress = inPos;

    }

    private bool inPosition()
    {
        float inrange = Mathf.Clamp(transform.localPosition.y, min, min + 0.01f);
        return transform.localPosition.y == inrange;
    }

  
}

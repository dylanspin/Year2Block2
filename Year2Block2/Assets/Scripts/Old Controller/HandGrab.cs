using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrab : MonoBehaviour
{   
    [Header("Testing")]

    [Tooltip("Pc input")]
    [SerializeField] private int mouseInput;

    [Header("Settings")]

    [Tooltip("The controller input for grabbing objects")]
    [SerializeField] private OVRInput.Button grabInput;

    [Tooltip("The position that the hand grabs")]
    [SerializeField] private Transform grabPoint;

    [Header("Private data")]
    private List<Transform> inTriggerObjects = new List<Transform>();//all the transforms that are currently in the list
    private Transform grabbedObject;//the current grabbed object
    private Vector3 oldPos;//was going to be used to add force to the object when let go but still needed some extra add ons

    /// <summary>
    /// if a grabbable object enters the trigger zone of the hand
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        Transform enteredTransform = other.transform.root;
        if(enteredTransform.tag == "Grabbable")
        {
            if(enteredTransform.GetComponent<GrabbableCustom>())
            {
                if(!inTriggerObjects.Contains(enteredTransform))//if isnt already in the list
                {
                    inTriggerObjects.Add(enteredTransform);
                    oldPos = transform.position;
                }
            }
        }
    }
 
    /// <summary>
    /// if a grabbable object leaves the trigger zone of the hand
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        Transform exitTransform = other.transform.root;
        if(exitTransform.tag == "Grabbable")
        {
            if(exitTransform.GetComponent<GrabbableCustom>())
            {
                inTriggerObjects.Remove(exitTransform);
            }
        }
    }

    /// <summary>
    /// Runs all the inputs either from pc or from the controller
    /// </summary>
    private void Update()
    {
        testingInputs();

        //uses Occulus input
        if(OVRInput.GetDown(grabInput))
        {
            grab(true);
        }

        if(OVRInput.GetUp(grabInput))
        {
            grab(false);
        }
    }   

    /// <summary>
    /// Used for testing the grabbing on pc in the editor
    /// </summary>
    private void testingInputs()
    {

        if(Input.GetMouseButtonDown(mouseInput))
        {
            grab(true);
        }

        if(Input.GetMouseButtonDown(2))
        {
            grab(false);
        }
    }

    /// <summary>
    /// Function for grabbing checks if it can grab or if it should let go 
    /// </summary>
    private void grab(bool active)
    {
        if(active)
        {
            if(!grabbedObject)
            {
                Transform closedTriggered = getClosed();
                if(closedTriggered != null)
                {
                    GrabbableCustom grabScript = closedTriggered.GetComponent<GrabbableCustom>();
                    closedTriggered.GetComponent<GrabbableCustom>().grabThis(true,grabPoint);
                    grabbedObject = closedTriggered;
                }
            }
        }
        else
        {
            if(grabbedObject)
            {
                letgoForce();
                grabbedObject.GetComponent<GrabbableCustom>().grabThis(false,grabPoint);
                grabbedObject = null;
            }
        }
    }

    /// <summary>
    /// Adds force to the object when let go
    /// </summary>
    private void letgoForce()
    {
        Vector3 velocityCal = transform.position - oldPos;
        oldPos = transform.position;
    }

    /// <summary>
    /// Function returns the closesed transform in the list
    /// </summary>
    private Transform getClosed()
    {
        Transform closedPos = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = grabPoint.position;
        foreach (Transform pos in inTriggerObjects)
        {
            float dist = Vector3.Distance(pos.position, currentPos);
            // GrabbableCustom grabScript = pos.GetComponent<GrabbableCustom>();

            if(dist < minDist)
            {
                closedPos = pos;
                minDist = dist;
            }
        }
        
        return closedPos;
    }
}
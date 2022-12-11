/*
    @author Dylan spin,
*/

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
    [SerializeField] private List<Transform> inTriggerObjects = new List<Transform>();//all the transforms that are currently in the list
    [SerializeField] private Transform grabbedObject;//the current grabbed object
    private Vector3 oldPos;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
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
    /// OnTriggerExit is called when the Collider other exits the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this exit.</param>
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
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        testingInputs();

        if(OVRInput.GetDown(grabInput))
        {
            grab(true);
        }

        if(OVRInput.GetUp(grabInput))
        {
            grab(false);
        }
    }   

    private void testingInputs()
    {
        // letgoForce();

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
                grabbedObject.GetComponent<GrabbableCustom>().grabThis(false,grabPoint);
                grabbedObject = null;
            }
        }
    }

    private void letgoForce()
    {
        Vector3 velocityCal = transform.position - oldPos;
        oldPos = transform.position;

        Debug.Log(velocityCal);
    }

    /// <summary>
    /// Function returns the most close transform in the list
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
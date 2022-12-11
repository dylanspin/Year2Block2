/*
    @author Dylan spin,
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableCustom : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform[] grabPoints;
    [SerializeField] private bool[] grabbed = new bool[0];
    [SerializeField] private Rigidbody objectRb;

    [Header("Private values")]
    private List<Transform> handPositions = new List<Transform>();
    private int handsOn = 0;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        grabbed = new bool[grabPoints.Length];
    }
  
    // <summary>
    // Sets if the object is grabbed or not
    // </summary>
    public void grabThis(bool active,Transform newPos)
    {
        if(active)
        {
            handPositions.Add(newPos);
        }
        else
        {
            handPositions.Remove(newPos);
        }
        
        handsOn += active ? 1 : -1;
        objectRb.constraints = active ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
       
        this.enabled = active;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(handsOn > 0)
        {
            Vector3 test = transform.position - grabPoints[0].transform.position;
        
            transform.position = handPositions[0].position + test;
            if(handPositions.Count > 1)
            {
                setHandsRot();
            }
            else
            {
                transform.rotation = handPositions[0].rotation;
            }
        }
    }

    private void setHandsRot()
    {
        Vector3 handPos1 = handPositions[0].transform.position;
        Vector3 handPos2 = handPositions[1].transform.position;
        handPos1.x = transform.position.x;
        handPos2.x = transform.position.x;

        Vector3 direction = handPos1 - handPos2;
        
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, 1, 0.0f);
        Quaternion rot = Quaternion.LookRotation(newDirection);

        transform.rotation = rot;
    }

    private Transform getCloseGrab(Transform handPos)
    {
        Transform closedPos = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = handPos.position;
        int i=0;

        foreach (Transform pos in grabPoints)
        {
            float dist = Vector3.Distance(pos.position, currentPos);

            if(dist < minDist && !grabbed[i])
            {
                closedPos = pos;
                minDist = dist;
            }
            i++;
        }
        
        return closedPos;
    }
}

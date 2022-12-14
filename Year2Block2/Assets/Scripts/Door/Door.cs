/*
    @author Dylan spin,
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Hinge Settings")]
    
    [Tooltip("Rigidbody for the hinge connection point")]
    [SerializeField] private Rigidbody connectionPoint;

    [Tooltip("Hinge rotation position offset")]
    [SerializeField] private Vector3 anchorOffset = new Vector3(0,1,2);

    [Tooltip("Hinge rotation axis directiong")]
    [SerializeField] private Vector3 rotationAxis = new Vector3(0,90,0);

    [Tooltip("Hinge rotation Limits")]
    [SerializeField] private Vector2 rotationLimits = new Vector2(-90,90);

    [Header("Settings")]

    [Tooltip("The min velocity needed to break the door")]
    [SerializeField] private float breakingVel = 1.5f;

    [SerializeField] private float fallingForce = 100;

    [Tooltip("The new layer of the broken of piece after being hit")]
    [SerializeField] private string colliderLayer = "NonColliding";

    [Header("Components")]

    [SerializeField] private DoorPiece[] hingePoints;
    [SerializeField] private DoorPiece[] allPoints;

    [Header("Private data")]
    
    private int hingeCount;
    private Rigidbody rb;
    private HingeJoint hinge;

    /// <summary>
    /// On start sets the settings for the door pieces and runs a check to prevent bugs 
    /// </summary>
    private void Start()
    {
        hingeCount = hingePoints.Length;

        for(int i=0; i<allPoints.Length; i++)
        {
            allPoints[i].StartCheck(breakingVel);
        }
    }

    /// <summary>
    /// Runs checks when the lock part of the door is broken 
    /// </summary>
    public void removeLock(Transform player)
    {
        if(!rb)//if hinges arentBroken
        {
            rb = gameObject.AddComponent<Rigidbody>();
            setHinge();//adds the hinge and its settings

            // hinge.
            rb.AddForce(player.transform.forward * Random.Range(fallingForce/2,fallingForce), ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Adds physics hinge and its settings to the door
    /// </summary>
    private void setHinge()
    {
        hinge = gameObject.AddComponent<HingeJoint>();

        hinge.connectedBody = connectionPoint;
        hinge.anchor = anchorOffset;
        hinge.axis = rotationAxis;

        JointLimits limits = hinge.limits;
        limits.min = rotationLimits.x;
        limits.bounciness = 0;
        limits.bounceMinVelocity = 0;
        limits.max = rotationLimits.y;
        hinge.limits = limits;
        hinge.useLimits = true;
    }

    /// <summary>
    /// Removes a hinge and checks if there are any left other wise the door is lose and falls over
    /// </summary>
    public void removeHinge(Transform player)
    {
        if(hingeCount > 1)
        {
            hingeCount --;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer(colliderLayer);
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer(colliderLayer);
            }

            if(!hinge)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
            else
            {
                Destroy(hinge);
            }
            rb.AddForce(player.transform.forward * Random.Range(fallingForce/2,fallingForce), ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Checks all pieces still in the list if they still have another connection
    /// </summary>
    public void checkAll()
    {
        for(int i=0; i<allPoints.Length; i++)
        {
            if(allPoints[i])//if the piece still excist
            {
                allPoints[i].checkEmpty();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AxeCollider : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("Name of the break tag")]
    [SerializeField] private string breakTag = "Door";

    [Header("Components")]

    [Tooltip("The holding script of the axe")]
    [SerializeField] private TwoHanded holdScript;

    [Tooltip("The second holding point script")]
    [SerializeField] private XRSimpleInteractable secondGrab;

    [Tooltip("Transform of the head of the axe to measure the velocity")]
    [SerializeField] private Transform axeHead;

    [Tooltip("RigidBody of the axe")]
    [SerializeField] private Rigidbody axeRb;

    [Header("Private data")]

    private Vector3 previous;
    private float velocity;

    /// <summary>
    /// Sets the start function listeners 
    /// </summary>
    private void Start()
    {
        previous = axeHead.position;
    }

    /// <summary>
    /// Calculates the felocity of the axe
    /// </summary>
    private void Update()
    {
        velocity = ((axeHead.position - previous).magnitude) / Time.deltaTime;
        previous = axeHead.position;
    }
    
    /// <summary>
    /// When the axe collides check if it can break collided object
    /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.gameObject.tag == breakTag)
        {
            DoorPiece pieceScript = other.collider.gameObject.GetComponent<DoorPiece>();
            float sendVeloc = (holdScript.isTwoHanded() ? 2f : 1) * velocity;//if is grabbed with two hands double the hit force

            pieceScript.hitPiece(sendVeloc,transform.root.transform,this);
            // ActivateHaptic();//Would have added shake to the controller when something is broken.
        }
    }
}

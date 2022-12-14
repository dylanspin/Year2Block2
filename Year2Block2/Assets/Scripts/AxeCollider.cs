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
    private List<XRController> controller; 

    /// <summary>
    /// Sets the start function listeners 
    /// </summary>
    private void Start()
    {
        previous = axeHead.position;

        holdScript.onSelectEntered.AddListener(onHandGrab);
        holdScript.onSelectExited.AddListener(onHandRelease);

        secondGrab.onSelectEntered.AddListener(onHandGrab);
        secondGrab.onSelectExited.AddListener(onHandRelease);
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
            pieceScript.hitPiece(velocity,transform.root.transform,this);
        }
    }

    /// <summary>
    /// When the controller grabs the axe adds to the list for shaking
    /// </summary>
    public void onHandGrab(XRBaseInteractor interactor)
    {
        XRController newController = interactor.gameObject.GetComponent<XRController>();
        if(!controller.Contains(newController))
        {
            controller.Add(newController);
        }
    }

    /// <summary>
    /// When the controller lets go remove from list
    /// </summary>
    public void onHandRelease(XRBaseInteractor interactor)
    {
        XRController newController = interactor.gameObject.GetComponent<XRController>();
        controller.Remove(newController);
    }

    /// <summary>
    /// Shakes the controller when the axe collided with breakable piece
    /// </summary>
    public void ActivateHaptic()
    {
        for(int i=0; i<controller.Count; i++)
        {
            if(controller[i])
            {
                controller[i].SendHapticImpulse(0.7f, 2f);
            }
        }
    }
}

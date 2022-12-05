using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollider : MonoBehaviour
{
    [Header("Components")]

    [Tooltip("Transform of the head of the axe to measure the velocity")]
    [SerializeField] private Transform axeHead;

    [Tooltip("Name of the break tag")]
    [SerializeField] private string breakTag = "Door";

    [Header("Private data")]

    private Vector3 previous;
    private float velocity;
    private Rigidbody axeRb;
    
    private void Start()
    {
        axeRb = GetComponent<Rigidbody>();
        previous = transform.position;
    }

    private void Update()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == breakTag)
        {
            DoorPiece pieceScript = other.gameObject.GetComponent<DoorPiece>();
            pieceScript.hitPiece(velocity,transform.root.transform);
        }
    }
}

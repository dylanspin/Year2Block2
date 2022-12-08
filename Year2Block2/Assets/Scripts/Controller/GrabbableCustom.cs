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
    [SerializeField] private Rigidbody objectRb;

    [Header("Private values")]
    private bool grabbed = false;
    private Transform handPosition;

    // <summary>
    // Sets if the object is grabbed or not
    // </summary>
    public void grabThis(bool active,Transform newPos)
    {
        grabbed = active;
        handPosition = active ? newPos : null;
        objectRb.constraints = active ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
       
        this.enabled = active;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(grabbed)
        {
            Vector3 test = transform.position - grabPoints[0].transform.position;
            Debug.Log(test);
            transform.position = handPosition.position + test;
            transform.rotation = handPosition.rotation;
        }
    }

    /// <summary>
    /// Getter that returns if is already grabbed
    /// </summary>
    public bool getGrabbed()
    {
        return grabbed;
    }
}

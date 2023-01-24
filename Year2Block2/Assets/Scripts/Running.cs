using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : MonoBehaviour
{
    [Header("Scripts")]

    [Tooltip("The movement script used for setting the movementSpeed")]
    [SerializeField] private RBMovement movementScript;

    [Tooltip("The look script used for pc controlls")]
    [SerializeField] private Look lookScript;

    [Header("Components")]

    [Tooltip("The transform of the right hand used for tracking its position")]
    [SerializeField] private Transform RightHand;

    [Tooltip("The transform of the left hand used for tracking its position")]
    [SerializeField] private Transform leftHand;

    [Tooltip("The transform of the camera used for tracking its position")]
    [SerializeField] private Transform cameraPoint;

    [Header("Private data")]
    private float offSet = 0.0f;

    private void Start()
    {
        if(Application.isEditor)//if on pc use the testing height
        {
            offSet = lookScript.getSetHeight();
        }
        else
        {
            offSet = 0;
        }
    }

    /// <summary>
    /// Check if both hands are below half of the camera height if so set running else walking
    /// </summary>
    void Update()
    {
        float currentCameraHeight = (cameraPoint.transform.localPosition.y + offSet) * 0.6f;
        float rHeight = RightHand.transform.localPosition.y;
        float lHeight = leftHand.transform.localPosition.y;

        bool running = rHeight < currentCameraHeight && lHeight < currentCameraHeight;//if both hands are below half of the cameraHeight

        movementScript.setRunning(running);
    }
}

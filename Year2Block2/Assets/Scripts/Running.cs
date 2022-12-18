using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private RBMovement movementScript;
    [SerializeField] private Look lookScript;

    [Header("Components")]
    [SerializeField] private Transform RightHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform cameraPoint;

    [Header("Private data")]
    private float offSet = 0.5f;

    private void Start()
    {
        if(Application.isEditor)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    /*
        Look script for on pc 
    */

    [Header("Settings")]

    [Tooltip("The height of the camera in the scene view")]
    [SerializeField] private float setHeadHeight = 0.5f;//to set the camera at the same height as it would be in VR

    [Tooltip("Sensitivy value for looking around")]
    [SerializeField] private float sensitivity = 1.5f;//sens for looking around

    [Tooltip("Min Downwards looking angle")]
    [SerializeField] private float minAngle = -65;//the min down angle

    [Tooltip("Max Downwards looking angle")]
    [SerializeField] private float maxAngel = 90;//the max up angle

    [Header("Components")]

    [Tooltip("Main object of the player for rotating horizontally")]
    [SerializeField] private Transform playerBody;//the horizontal rotation object

    [Header("Private data")]
    private float xRotation = 0.0f;//the current x angle Rotation
    private float yRotation = 0.0f;//the current Y angle Rotation
    
    private void Start()
    {
        Vector3 currentPos = transform.localPosition;
        transform.localPosition = new Vector3(currentPos.x,setHeadHeight,currentPos.z);
    }

    void Update ()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;//the X angle mouse input
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;//the Y angle mouse input

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minAngle, maxAngel);//clamps up and down angle

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);//rotates this object for looking up and down
        playerBody.Rotate(Vector3.up * mouseX);//rotates the horizontal player object
    }
}

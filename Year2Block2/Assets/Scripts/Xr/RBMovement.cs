using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class RBMovement : MonoBehaviour 
{
    [Header("Inputs")]
    
    [Tooltip("Input source for moving around")]
    [SerializeField] private XRNode inputSource;

    [Header("Settings Values")]

    [Tooltip("The general speed multiplier")]
    [SerializeField] private float speedMultiplier = 100;

    [Tooltip("The walking speed")]
    [SerializeField] private float walkingSpeed = 2.0f;

    [Tooltip("The running speed")]
    [SerializeField] private float runningSpeed = 4.0f;

    [Tooltip ("The extra height above the playershead for the collider")]
    [SerializeField]  private float extraHeight = 0.2f;

    [Header("Components")]

    [Tooltip("The collider of the player")]
    [SerializeField] private CapsuleCollider playerCol;

    [Tooltip("The character controller component of the player")]
    [SerializeField] private Rigidbody rbPlayer;

    [Tooltip("The head tranform for moving in the direction of the head")]
    [SerializeField] private XROrigin playerHead;

    [Header("private Data")]

    private Vector2 inputAxis;
    private float currentSpeed;//current movement speed for if i want to make something that changes the speed
    private Vector3 movementPlayer;//movement velocity

    /// <summary>
    /// Sets the start settings
    /// </summary>
    public void Start()
    {
        currentSpeed = walkingSpeed;
    }

    /// <summary>
    /// gets the input from the controller
    /// </summary>
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
       
        if(Application.isEditor)
        {
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");
        }
    }
    
    /// <summary>
    /// set the velocity of the rigid body with the input from the set controller
    /// </summary>
    void FixedUpdate()
    {
        followHeadset();
        
        Quaternion headDir = Quaternion.Euler(0,playerHead.Camera.transform.eulerAngles.y,0);
        movementPlayer = headDir * new Vector3(inputAxis.x, 0f, inputAxis.y);

        movementPlayer = movementPlayer * currentSpeed * speedMultiplier * Time.deltaTime;

        rbPlayer.velocity = new Vector3(movementPlayer.x ,rbPlayer.velocity.y, movementPlayer.z);
    }
    
    /// <summary>
    /// The collider follows the player
    /// </summary>
    private void followHeadset()
    {
        playerCol.height = playerHead.CameraInOriginSpaceHeight + extraHeight;
        Vector3 centerCollider = transform.InverseTransformPoint(playerHead.Camera.transform.position);
        playerCol.center = new Vector3(centerCollider.x, playerCol.height/2,centerCollider.z);
    }

    public void setRunning(bool active)
    {
        currentSpeed = active? runningSpeed : walkingSpeed;
    }
}

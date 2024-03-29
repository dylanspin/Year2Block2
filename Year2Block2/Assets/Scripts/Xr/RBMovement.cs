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

    [Tooltip("Input source for jumping")]
    [SerializeField] private XRNode jumpingSource;

    [Header("Settings Values")]

    [Tooltip("The general speed multiplier")]
    [SerializeField] private float speedMultiplier = 100;

    [Tooltip("The walking speed")]
    [SerializeField] private float walkingSpeed = 2.0f;

    [Tooltip("The running speed")]
    [SerializeField] private float runningSpeed = 4.0f;

    [Tooltip("The jumping Height")]
    [SerializeField] private float jumpHeight = 4.0f;

    [Tooltip ("The extra height above the playershead for the collider")]
    [SerializeField]  private float extraHeight = 0.2f;

    [Header("Ground checks")]
    
    [Tooltip("the ground layer")]
    [SerializeField] private LayerMask groundMask;

    [Tooltip("the position the check gets done from")]
    [SerializeField] Transform grounCheck;

    [Tooltip("the distance the sphere checks for if there is ground")]
    [SerializeField] float GroundDistance = 0.4f;

    [Header("Components")]

    [Tooltip("The collider of the player")]
    [SerializeField] private CapsuleCollider playerCol;

    [Tooltip("The character controller component of the player")]
    [SerializeField] private Rigidbody rbPlayer;

    [Tooltip("The head tranform for moving in the direction of the head")]
    [SerializeField] private XROrigin playerHead;

    [Header("private Data")]

    private bool isGrounded = false;//if the player is on the ground 
    private Vector2 inputAxis;//inputs from the player converted in a vector2
    private float currentSpeed;//current movement speed for if i want to make something that changes the speed
    private Vector3 movementPlayer;//movement velocity
    private bool aButtonDown;//if the jump button is held down

    /// <summary>
    /// Sets the start settings
    /// </summary>
    private void Start()
    {
        currentSpeed = walkingSpeed;
    }

    /// <summary>
    /// gets the input from the controller
    /// </summary>
    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
       
        if(Application.isEditor)
        {
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");
        }

        checkJump();
    }

    /// <summary>
    /// Checks jump input and if the player is grounded
    /// </summary>
    private void checkJump()
    {
        isGrounded = Physics.OverlapSphere(transform.position, GroundDistance,groundMask).Length > 0;//cast sphere cast

        if(isGrounded)
        {
            bool aButton;
        
            InputDevice device = InputDevices.GetDeviceAtXRNode(jumpingSource);
            
            if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out aButton) && aButton || Input.GetKey(KeyCode.Space))
            {
                if(!aButtonDown)
                {
                    aButtonDown = true;
                    BoostUp(jumpHeight);
                }
            }
            else
            {
                aButtonDown = false; 
            }
        }
    }
    
    /// <summary>
    /// set the velocity of the rigid body with the input from the set controller
    /// </summary>
    private void FixedUpdate()
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

    /// <summary>
    /// Boost up the player using addforce
    /// </summary>
    public void setRunning(bool active)
    {
        currentSpeed = active? runningSpeed : walkingSpeed;
    }

    /// <summary>
    /// Boost up the player using addforce
    /// </summary>
    public void BoostUp(float Height)
    {
        rbPlayer.AddForce(transform.up * jumpHeight,ForceMode.Impulse);
        // movementPlayer.y = Mathf.Sqrt(Height * -2);
    }

    /// <summary>
    /// Draws a sphere for debugging
    /// </summary>
    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(grounCheck.position, GroundDistance);
    // }
}

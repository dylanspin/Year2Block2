using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(CharacterController))]
public class XRMovement : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("Input source for moving around")]
    [SerializeField] private XRNode inputSource;

    [Tooltip ("The gravity strength on the player")]
    [SerializeField]  private float Gravity = -12f;

    [Tooltip("The walking speed")]
    [SerializeField] private float walkingSpeed = 2.0f;

    [Tooltip("The running speed")]
    [SerializeField] private float runningSpeed = 4.0f;

    [Tooltip ("The extra height above the playershead for the collider")]
    [SerializeField]  private float extraHeight = 0.2f;
    
    [Header("Components")]

    [Tooltip("The character controller component of the player")]
    [SerializeField] private CharacterController characterController;

    [Tooltip("The head tranform for moving in the direction of the head")]
    [SerializeField] private XROrigin playerHead;

    [Header("Private values")]

    private Vector2 inputAxis;
    private float currentSpeed;
    private Vector3 velocity;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        currentSpeed = walkingSpeed;
    }

    /// <summary>
    /// gets the input from the inputsource
    /// </summary>
    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        if(characterController.isGrounded)
        {
            velocity.y = -2;
        }

        velocity.y += Gravity * Time.deltaTime;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        followHeadset();
        
        Quaternion headDir = Quaternion.Euler(0,playerHead.Camera.transform.eulerAngles.y,0);
        Vector3 dir = headDir * new Vector3(inputAxis.x,0,inputAxis.y);

        characterController.Move(dir * Time.fixedDeltaTime * currentSpeed);
        
        characterController.Move(velocity * Time.deltaTime);//gravity
    }

    private void followHeadset()
    {
        // characterController.height = playerHead.transform.localPosition.y + extraHeight;
        characterController.height = playerHead.CameraInOriginSpaceHeight + extraHeight;
        Vector3 centerCollider = transform.InverseTransformPoint(playerHead.transform.position);
        characterController.center = new Vector3(centerCollider.x, characterController.height/2 + characterController.skinWidth,centerCollider.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField] [Tooltip ("The walking speed of the player")]
    private float WalkSpeed = 2.0f;

    [SerializeField] [Tooltip ("The running speed of the player")]
    private float RunSpeed = 4.0f;

    [SerializeField] [Tooltip ("The speed of the player rotation")]
    private float RotationSmoothTime = 0.2f;

    [SerializeField] [Tooltip ("The gravity strength on the player")]
    private float Gravity = -12f;

    [SerializeField] [Tooltip ("The time it takes for the player to get a boost so the jump animation works")]
    private float jumpDelay = 0.25f;

    [SerializeField] [Tooltip ("The height that the character can jump")]
    private float JumpHeight = 1.0f;

    [SerializeField] [Tooltip ("Speed of transition speed between movement animations")]
    private float SpeedSmoothTime = 0.1f;

    [Header("Set Components")]

    [SerializeField] [Tooltip ("character controller component for the movement")]
    private CharacterController characterController;

    [Header("Private values")]

    private Vector3 velocity;
    private float rotationVelocityTime;
    private float speedSmoothVelocity;
    private float currentSpeed;
    private bool running = false;
    private bool crouched = false;
  
    private void Update()
    {   
        running = Input.GetKey(KeyCode.LeftShift);//gets the running input
        crouched = Input.GetKey(KeyCode.LeftControl);//gets the crouch input
        
        var hInput = Input.GetAxis("Horizontal");//gets horizontal input
        var vInput = Input.GetAxis("Vertical");//gets verticle input

        if(hInput != 0 || vInput != 0)
        {
            currentSpeed = running ? RunSpeed : WalkSpeed;
        }
        else
        {
            currentSpeed = 0;
        }

        if(characterController.isGrounded)
        {
            velocity.y = -2;
        }

        Vector3 move = transform.right * hInput + transform.forward * vInput;
        characterController.Move(move * currentSpeed * Time.deltaTime);//moves the player with the character controller

        velocity.y += Gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);//gravity
    }
}

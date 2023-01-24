using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAxeSwing : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("Rotation speed of axe when holding right click")]
    [SerializeField] private float rotateSpeed = 360;

    [Header("Components")]

    [Tooltip("Rotation object for when holding right click")]
    [SerializeField] private Transform rotatePoint;

    [Tooltip("Animator for extending Axe forward")]
    [SerializeField] private Animator anim;

    /// <summary>
    /// turns this off if its not in the editor so on the quest its turned off
    /// </summary>
    private void Start()
    {   
        this.enabled = Application.isEditor;
    }
    
    /// <summary>
    /// Calls the pcAxePush function 
    /// </summary>
    private void Update()
    {
        pcAxePush();
    }

    /// <summary>
    /// Checks inputs for moving the axe
    /// </summary>
    private void pcAxePush()
    {
        if(Input.GetMouseButtonDown(0))//push the axe forward so it can be used for testing
        {
            anim.SetBool("Push",true);
        }

        if(Input.GetMouseButtonUp(0))//returns the axe to the normal position when the mouse button is let go
        {
            anim.SetBool("Push",false);
        }
        
        if(Input.GetMouseButton(1))//rotate the axe for better testing
        {
            rotatePoint.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
        }
    }
}

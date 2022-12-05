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

    void Start()
    {   
        this.enabled = Application.isEditor;
    }
    
    void Update()
    {
        pcAxePush();
    }

    /// <summary>
    /// Checks inputs for moving the axe
    /// </summary>
    private void pcAxePush()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Push",true);
        }

        if(Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Push",false);
        }
        
        if(Input.GetMouseButton(1))
        {
            rotatePoint.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
        }
    }
}

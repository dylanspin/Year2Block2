using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [Header("Components")]

    [Tooltip("The fire script of this cell")]
    [SerializeField] private Fire fireScript;

    [Header("Options")]

    [SerializeField] private string playerTag = "Player";

    [Header("Private data")]

    private FireController controllerScript;
    private bool intrigger = false;

    private void Start()
    {
        controllerScript = transform.root.GetComponent<FireController>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.tag == playerTag)
        {
            if(fireScript.isOnFire())
            {
                intrigger = true;
                controllerScript.setIntrigger(true);
            }
        }   
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.root.tag == playerTag)
        {
            if(intrigger)
            {
                controllerScript.setIntrigger(false);
            }
        }
    }
}

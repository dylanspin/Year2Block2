using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [Header("Options")]

    [SerializeField] private string playerTag = "Player";

    [Header("Private data")]

    private FireController controllerScript;

    private void Start()
    {
        controllerScript = transform.root.GetComponent<FireController>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        controllerScript.setIntrigger(true);
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        controllerScript.setIntrigger(false);
    }
}

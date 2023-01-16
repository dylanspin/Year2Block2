using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class CheckHelm : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private endEffect endScript;

    [Header("Trigger settings")]

    [Tooltip("Layer of helmet")]
    [SerializeField] private LayerMask checkLayers;//the trigger sphere layers 

    [Tooltip("Distance checked for the bus stops")]
    [SerializeField] private float triggerDistance = 2;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        checkhelmet();
    }

    /// <summary>
    /// Check if the helmet is in the trigger zone
    /// </summary>
    private void checkhelmet()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, triggerDistance,checkLayers);//cast sphere cast
        if(hitColliders.Length > 0)//if it collided with something that means the stop is in the cast
        {
            for(int i=0; i<hitColliders.Length; i++)
            {
                if(hitColliders[0].transform.parent.gameObject.tag == "Helmet")
                {
                    bool grabbed = hitColliders[0].transform.parent.GetComponent<ItemHold>().getIsHeld();
                    if(grabbed)
                    {
                        endScript.loadGameTransition();
                    }
                }
            }
        }
    }

    /*Draws sphere around the player for visualizing the range of the trigger*/
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, triggerDistance);
    }
}

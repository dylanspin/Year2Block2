using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class extinguisher : MonoBehaviour
{   
    [Header("Settings")]

    [Tooltip("The amount of damage it does to the fire each second")]
    [SerializeField] private float damageToFire = 2.5f;

    [Tooltip("The range of the extinguisher")]
    [SerializeField] private float range = 5.0f;

    [Tooltip("Input source for the buttons")]
    [SerializeField] private XRNode inputSource;

    [Header("Scripts")]

    [Tooltip("The grabbing script to check if its grabbed or nto")]
    [SerializeField] private ItemHold holdScript;

    [Header("Components")]

    [Tooltip("The layers that the extinguisher can hit")]
    [SerializeField] private LayerMask viewLayers;

    [Tooltip("The particle system for the extinguisher")]
    [SerializeField] private ParticleSystem particle;

    [Tooltip("The sound effect for the extinguisher")]
    [SerializeField] private AudioSource audioEffect;

    [Header("Private data")]

    private bool activeShooting = false;

    /// <summary>
    /// checks if active if so shoot and check if hitting fire 
    /// </summary>
    private void Update()
    {
        if(activeShooting || Input.GetMouseButton(0))
        {
            if(!particle.isPlaying)
            {
                particle.Play();
            }

            if(!audioEffect.isPlaying)
            {
                audioEffect.Play();
            }

            RaycastHit hit;
            Ray ray = new Ray(particle.transform.position, particle.transform.forward);
            if(Physics.Raycast(ray, out hit, range, viewLayers))
            {
                if(hit.collider.tag == "Fire")
                {
                    hit.transform.parent.GetComponent<Fire>().takeFireHealth(damageToFire);
                }
            }
        }
        else
        {
            particle.Stop();
            audioEffect.Stop();
        }

        drawRay();
    }

    /// <summary>
    /// Draws the ray for debugging
    /// </summary>
    private void drawRay()
    {
        Vector3 forward = particle.transform.TransformDirection(Vector3.forward) * range;
        Debug.DrawRay(particle.transform.position, forward, Color.green);
    }


    /// <summary>
    /// Turns on or off via the xr grab interactable script
    /// </summary>
    public void shootExtinguisher(bool active)
    {
        activeShooting = active;
    }
}

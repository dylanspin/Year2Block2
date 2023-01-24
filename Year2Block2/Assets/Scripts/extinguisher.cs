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

    [Header("Scripts")]

    [Header("Components")]

    [Tooltip("The layers that the extinguisher can hit")]
    [SerializeField] private LayerMask viewLayers;

    [Tooltip("The particle system for the extinguisher")]
    [SerializeField] private ParticleSystem particle;

    [Tooltip("The sound effect for the extinguisher")]
    [SerializeField] private AudioSource audioEffect;

    [Tooltip("The sound clips for the extinguisher")]
    [SerializeField] private AudioClip[] clips = new AudioClip[2];


    [Header("Private data")]

    private bool activeShooting = false;//if the extinguisher is active

    /// <summary>
    /// checks if active if so shoot and check if hitting fire 
    /// </summary>
    private void Update()
    {
        if(activeShooting)
        {
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

        //for pc testing
        if(Input.GetMouseButtonDown(0))
        {
            shootExtinguisher(true);
        }

        if(Input.GetMouseButtonUp(0))
        {
            shootExtinguisher(false);
        }

        // drawRay();//for debugging the range of the extinguisher
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

        if(active)
        {
            audioEffect.clip = clips[0];//sets it to the looping clip
            audioEffect.Play();
            particle.Play();
        }
        else
        {
            particle.Stop();
            audioEffect.Stop();
            audioEffect.PlayOneShot(clips[1], 0.5f);//plays the cutting of sound effect
        }
    }
}

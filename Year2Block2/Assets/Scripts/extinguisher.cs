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


    private void Update()
    {
        bool mainTrigger;
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);

        if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out mainTrigger) || Input.GetKey(KeyCode.P))
        {
            if(!particle.isPlaying)
            {
                particle.Play();
            }

            RaycastHit hit;
            Ray ray = new Ray(particle.transform.position, particle.transform.forward);
            if(Physics.Raycast(ray, out hit, range, viewLayers))
            {
                if(hit.collider.tag == "Fire")
                {
                    hit.transform.GetComponent<Fire>().takeFireHealth(damageToFire);
                }
            }
        }
        else
        {
            particle.Stop();
        }
    }
}

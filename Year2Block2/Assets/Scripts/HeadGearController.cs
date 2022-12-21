using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class HeadGearController : MonoBehaviour
{
    [Header("Components")]

    [Tooltip("Input source for moving around")]
    [SerializeField] private XRNode inputSource;
    
    [Tooltip("Post processing volume")]
    [SerializeField] private Volume postVolume;

    [Tooltip("Camera of the player for changing lighting Settings")]
    [SerializeField] private Camera playerCamera;

    [Header("Vision profiles")]

    [Tooltip("Night vision postprocession profile")]
    [SerializeField] private VolumeProfile nightVision;

    [Tooltip("Thermal vision postprocession profile")]
    [SerializeField] private VolumeProfile thermalVision;

    [Tooltip("Normal vision postprocession profile")]
    [SerializeField] private VolumeProfile normalVision;

    [Header("Vision lighting settings")]

    [Tooltip("Thermal vision lighting level")]
    [SerializeField] private float thermalLight = 0.9f;

    [Tooltip("Night vision lighting level")]
    [SerializeField] private float nightLight = 0.9f;

    [Header("Private data")]
    private bool aButtonDown = false;
    private bool bButtonDown = false;
    private float defaultLight = 0.2f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        defaultLight = RenderSettings.ambientIntensity;
        setSkyBox(0);
    }

   /// <summary>
   /// Update is called every frame, if the MonoBehaviour is enabled.
   /// </summary>
    private void Update()
    {
        bool aButton;
        bool bButton;
    
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        
        if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out aButton) && aButton || Input.GetKey(KeyCode.N))
        {
            if(!aButtonDown)
            {
                aButtonDown = true;
                checkProfile(nightVision);
                setSkyBox(nightLight);
            }
            // if start pressing, trigger event
        }
        else
        {
            aButtonDown = false; 
        }

        if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bButton) && bButton || Input.GetKey(KeyCode.M))
        {
            if(!bButtonDown)
            {
                bButtonDown = true;
                checkProfile(thermalVision);
                setSkyBox(thermalLight);
            }
            // if start pressing, trigger event
        }
        else
        {
            bButtonDown = false;
        }
    }

    private void checkProfile(VolumeProfile newProfile)
    {
        if(postVolume.profile == newProfile)
        {
            postVolume.profile = normalVision;
        }
        else
        {
            postVolume.profile = newProfile;
        }
    } 

    private void setSkyBox(float newLighting)
    {
        if(RenderSettings.ambientIntensity == newLighting)
        {
            RenderSettings.ambientIntensity = defaultLight;
        }
        else
        {
            RenderSettings.ambientIntensity = newLighting;
        }
    }
}

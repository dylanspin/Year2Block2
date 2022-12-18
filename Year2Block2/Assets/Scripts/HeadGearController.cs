using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class HeadGearController : MonoBehaviour
{
    [Tooltip("Input source for moving around")]
    [SerializeField] private XRNode inputSource;
    
    [SerializeField] private Volume postVolume;
    [SerializeField] private VolumeProfile nightVision;
    [SerializeField] private VolumeProfile thermalVision;
    [SerializeField] private VolumeProfile normalVision;

    private bool aButtonDown = false;
    private bool bButtonDown = false;

   /// <summary>
   /// Update is called every frame, if the MonoBehaviour is enabled.
   /// </summary>
    private void Update()
    {
        bool aButton;
        bool bButton;
    
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        
        if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out aButton) && aButton)
        {
            if(!aButtonDown)
            {
                aButtonDown = true;
                checkProfile(nightVision);
            }
            // if start pressing, trigger event
        }
        else
        {
            aButtonDown = false; 
        }

        if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bButton) && bButton)
        {
            if(!bButtonDown)
            {
                bButtonDown = true;
                checkProfile(thermalVision);
            }
            // if start pressing, trigger event
        }
        else
        {
            bButtonDown = false;
        }
      

        // if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out aButton))
        // {
        //     checkProfile(nightVision);
        // }

        // if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bButton))
        // {
        //     checkProfile(thermalVision);
        // }
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
 
}

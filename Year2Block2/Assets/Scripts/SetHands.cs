using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetHands : MonoBehaviour
{   
    [Header("Hand settings")]

    [Tooltip("The id of the hand where this script is on")]
    [SerializeField] private int handId = 0;

    [Header("Private Static data")]

    private static Quaternion[] startRot = new Quaternion[2];//the start rotation of the two hands attachTransform
    private static XRBaseInteractor[] HandsScripts = new XRBaseInteractor[2];//the two interactor scripts of the hands


    /// <summary>
    /// Sets the start data 
    /// </summary>
    private void Start()
    {
        HandsScripts[handId] = GetComponent<XRBaseInteractor>();
        startRot[handId] = HandsScripts[handId].attachTransform.localRotation;
    }

    /// <summary>
    /// Resets the attach transform when not holding anything cald from the item hold script
    /// </summary>
    public static void resetHand()
    {
        for(int i=0; i<HandsScripts.Length; i++)
        {
            if(!HandsScripts[i].isSelectActive)
            {
                HandsScripts[i].attachTransform.localRotation = startRot[i];
            }
        }
    }
    
    /// <summary>
    /// Returns the hand id used for checking if left or right hand
    /// </summary>
    public int getHandId()
    {
        return handId;
    }
}

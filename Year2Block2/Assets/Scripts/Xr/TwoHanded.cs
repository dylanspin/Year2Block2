using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHanded : XRGrabInteractable
{
    [Header("Settings")]

    [Tooltip("The start rotation for the twohanded object")]
    [SerializeField] Vector3 defaultRot = new Vector3(-90,-90,0);

    [Tooltip("The second grab point of the object using XrSimpleInteractable component")]
    [SerializeField] private List<XRSimpleInteractable> secondGrabPoints = new List<XRSimpleInteractable>();

    [Tooltip("The 3 different types of holding the object/ none : use no rotation of the two points - First : use the rotation of this object for the end rotation - Second use the second grab point rotation for the end rotation")]
    [SerializeField] private enum TwoHandRotationType {none,First,Second};

    [Tooltip("The Selected type of rotation type for this object")]
    [SerializeField] private TwoHandRotationType twoHandRotationType;

    [Header("Private data")]
    private XRBaseInteractor secondInteractor;//the second controller 


    /// <summary>
    /// Sets the second grab points listeners for interacting with two hands
    /// </summary>
    private void Start()
    {
        foreach(var point in secondGrabPoints)
        {
            point.onSelectEntered.AddListener(onSecondHandGrab);
            point.onSelectExited.AddListener(onSecondHandRelease);
        }
    }

    /// <summary>
    /// Processes the interactable and sets the right angle / scale for the direction of the axe head
    /// </summary>
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(isTwoHanded())//if is held with two hands
        {
            setFlipped(selectingInteractor.GetComponent<SetHands>().getHandId() == 0);
            selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
        }
        else 
        {
        
        }

        base.ProcessInteractable(updatePhase);
    }

    /// <summary>
    /// Flips the axe when held with other hand
    /// </summary>
    private void setFlipped(bool active)
    {
        transform.localScale = new Vector3(active? -1 : 1, 1,1);
    }

    /// <summary>
    /// Gets the rotation between the two hands
    /// </summary>
    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;
        if(twoHandRotationType == TwoHandRotationType.none)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
        }
        else if(twoHandRotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.transform.up);//, selectingInteractor.transform.up
        }
        else
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.transform.up);//, secondInteractor.attachTransform.up
        }

        return targetRotation;
    }

    /// <summary>
    /// When the second point is held set the secondinteractor
    /// </summary>
    public void onSecondHandGrab(XRBaseInteractor interactor)
    {
        secondInteractor = interactor;
    }

    /// <summary>
    /// When the second point is letgo set the secondinteractor to null
    /// </summary>
    public void onSecondHandRelease(XRBaseInteractor interactor)
    {
        secondInteractor = null;
    }

    /// <summary>
    /// Sets if the axe can be grabbed or not
    /// </summary>
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);

        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }

    /// <summary>
    /// Returns if is held with two hands is used for the axe adds extra velocity power when held with two hands
    /// </summary>
    public bool isTwoHanded()
    {
        return secondInteractor && isSelected;
    }

    /// <summary>
    /// Sets the start rotation of the attachtransform when first grabbed
    /// </summary>
    public void setStartRot()
    {
        selectingInteractor.attachTransform.localEulerAngles = defaultRot;
    }
}

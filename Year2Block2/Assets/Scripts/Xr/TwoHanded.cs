using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHanded : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondGrabPoints = new List<XRSimpleInteractable>();
    private XRBaseInteractor secondInteractor;
    private Quaternion startRotation;

    public enum TwoHandRotationType {none,First,Second};
    public TwoHandRotationType twoHandRotationType;

    // protected override void Awake()
    // {
    //     base.Awake();
    //     selectMode = InteractableSelectMode.Multiple;
    // }

    private void Start()
    {
        foreach(var point in secondGrabPoints)
        {
            point.onSelectEntered.AddListener(onSecondHandGrab);
            point.onSelectExited.AddListener(onSecondHandRelease);
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(secondInteractor && isSelected)
        {
            selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
        }

        base.ProcessInteractable(updatePhase);
    }

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

    public void onSecondHandGrab(XRBaseInteractor interactor)
    {
        secondInteractor = interactor;
        startRotation = selectingInteractor.transform.localRotation;
    }

    public void onSecondHandRelease(XRBaseInteractor interactor)
    {
        secondInteractor = null;
        selectingInteractor.transform.localRotation = startRotation;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);
        startRotation = selectingInteractor.transform.localRotation;
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);
        selectingInteractor.transform.localRotation = startRotation;
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);

        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}

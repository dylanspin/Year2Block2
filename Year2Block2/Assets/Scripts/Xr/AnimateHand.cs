using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class AnimateHand : MonoBehaviour
{
    [Tooltip("Input source for moving around")]
    [SerializeField] private XRNode inputSource;
    [SerializeField] private const string ANIM_PARAM_NAME_FLEX = "Flex";
    [SerializeField] private Animator m_animator = null;

    private int m_animParamIndexFlex = -1;

    void Start()
    {
        m_animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
    }

    void Update()
    {
        UpdateAnimStates();
    }

    private void UpdateAnimStates()
    {
        float flex;
        float pinch;

        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.trigger, out pinch);
        device.TryGetFeatureValue(CommonUsages.grip, out flex);
        
        m_animator.SetFloat(m_animParamIndexFlex, flex);
        m_animator.SetFloat("Pinch", pinch);
    }
}

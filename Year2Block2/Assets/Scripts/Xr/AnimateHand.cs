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


    [Tooltip("Animation information")]
    [SerializeField] private const string ANIM_LAYER_NAME_POINT = "Point Layer";
    [SerializeField] private const string ANIM_LAYER_NAME_THUMB = "Thumb Layer";
    [SerializeField] private const string ANIM_PARAM_NAME_FLEX = "Flex";
    [SerializeField] private const string ANIM_PARAM_NAME_POSE = "Pose";

    [Tooltip("Components")]
    [SerializeField] private Animator m_animator = null;
    [SerializeField] private HandPose defaultHand = null;
    
    [Tooltip("Private data")]
    private int m_animParamIndexFlex = -1;
    private int m_animLayerIndexThumb = -1;
    private int m_animLayerIndexPoint = -1;
    private int m_animParamIndexPose = -1;

    private bool m_isPointing = false;
    private bool m_isGivingThumbsUp = false;
    private float m_pointBlend = 0.0f;
    private float m_thumbsUpBlend = 0.0f;

    private void Start()
    {
        m_animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
        m_animLayerIndexPoint = m_animator.GetLayerIndex(ANIM_LAYER_NAME_POINT);
        m_animLayerIndexThumb = m_animator.GetLayerIndex(ANIM_LAYER_NAME_THUMB);
        m_animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
        m_animParamIndexPose = Animator.StringToHash(ANIM_PARAM_NAME_POSE);
    }

    private void Update()
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

        bool grabbing = false;
        HandPose grabPose = defaultHand;
        
        HandPoseId handPoseId = grabPose.PoseId;
        m_animator.SetInteger(m_animParamIndexPose, (int)handPoseId);

        m_animator.SetFloat(m_animParamIndexFlex, flex);

        // Point
        bool canPoint = !grabbing || grabPose.AllowPointing;
        float point = canPoint ? m_pointBlend : 0.0f;
        m_animator.SetLayerWeight(m_animLayerIndexPoint, point);

        // Thumbs up
        bool canThumbsUp = !grabbing || grabPose.AllowThumbsUp;
        float thumbsUp = canThumbsUp ? m_thumbsUpBlend : 0.0f;
        m_animator.SetLayerWeight(m_animLayerIndexThumb, thumbsUp);
    }
}

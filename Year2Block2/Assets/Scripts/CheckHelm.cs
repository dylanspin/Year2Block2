using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class CheckHelm : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private endEffect endScript;

    public void grabbed()
    {
        endScript.loadGameTransition();
    }
}

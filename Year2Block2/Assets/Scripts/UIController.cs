using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Components")]

    [Tooltip("The oxygen/health slider")]
    [SerializeField] Slider healthSlider;

    /// <summary>
    /// Sets the start settings if on pc
    /// </summary>
    private void Start()
    {
        if(Application.isEditor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    /// <summary>
    /// Sets the max health of the player called from the health script
    /// </summary>
    public void setMaxHealth(float newMax)
    {
        healthSlider.maxValue = newMax;
        healthSlider.value = newMax;
    }

    /// <summary>
    /// Sets the current health given as Value called from the health script
    /// </summary>
    public void setHealth(float value)
    {
        healthSlider.value = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Slider healthSlider;

    private void Start()
    {
        if(Application.isEditor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }



    public void setMaxHealth(float newMax)
    {
        healthSlider.maxValue = newMax;
        healthSlider.value = newMax;
    }

    public void setHealth(float value)
    {
        healthSlider.value = value;
    }
}

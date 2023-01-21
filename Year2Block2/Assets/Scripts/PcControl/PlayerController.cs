using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]

    [Tooltip("Script that controlls the transition effects")]
    [SerializeField] private endEffect transitionController;

    [Tooltip("Script that controlls the transition effects")]
    [SerializeField] private HeadGearController visionController;

    [Header("Scripts")]

    [Tooltip("The script that controlls the health of the player")]
    [SerializeField] private Health healthScript;

    [Tooltip("The script that controlls the UI on the main interface of the helmet")]
    [SerializeField] private UIController uiScript;

    [Tooltip("The script that allows looking around with pc controlls")]
    [SerializeField] private Look lookScript;

    [Tooltip("The script that controlls the whole scene")]
    [SerializeField] private GameController controllerScript;

    /// <summary>
    /// Sets the start information of scritps
    /// </summary>  
    private void Start()
    {
        if(controllerScript)
        {
            controllerScript.setStart(this,transitionController);
        }
        
        transitionController.gameObject.SetActive(true);

        setStart();
    }

    public void setScripts(GameEffectController effectController)
    {
        visionController.setStart(effectController);
    }

    /// <summary>
    /// Turns on or off components/objects based up on if its playing in the editor or on the quest
    /// </summary>    
    private void setStart()
    {
        bool isEditor = Application.isEditor;

        if(lookScript)
        {
            lookScript.enabled = isEditor;
        }
        
        if(healthScript)
        {
            healthScript.setStart(this,uiScript);
        }
    }

    /// <summary>
    /// When the player lost all their health
    /// </summary>  
    public void lostGame()
    {
        controllerScript.setEnd(false);
    }

    /// <summary>
    /// When the player loses health
    /// </summary>  
    public void dealDamage(float amount)
    {
        healthScript.loseHealth(amount);
    }
}

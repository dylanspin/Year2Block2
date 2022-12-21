using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]

    [Tooltip("Animator that controlls the transition effects")]
    [SerializeField] private Animator transitionAnim;

    [Header("Scripts")]

    [Tooltip("The script that controlls the health of the player")]
    [SerializeField] private Health healthScript;

    [Tooltip("The script that allows looking around with pc controlls")]
    [SerializeField] private Look lookScript;

    [Tooltip("The script that controlls the whole scene")]
    [SerializeField] private GameController controllerScript;

    /// <summary>
    /// Sets the start information of scritps
    /// </summary>  
    private void Start()
    {
        controllerScript.setStart(this);
        transitionAnim.gameObject.SetActive(true);
        setStart();
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
            healthScript.setStart(this);
        }
    }

    /// <summary>
    /// When the player lost all their health
    /// </summary>  
    public void lostGame()
    {
        controllerScript.lostGame();
    }
}

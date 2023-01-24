using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{   
    [Header("Scipts")]

    [Tooltip("The end point for the dolls script")]
    [SerializeField] private EndPoint pointScript;

    [Tooltip("The main vision effects script")]
    [SerializeField] private GameEffectController effectController;

    [Tooltip("The main fire controller of the scene")]
    [SerializeField] private FireController fireScript;

    [Header("Private data")]
    private PlayerController controllerScript;//the player controller script which is the centeralized script of the player object
    private endEffect endController;//the script that controlls the end screen and the transition between scenes

    /// <summary>
    /// Sets the start data using the player controller and sets the data on the player controller
    /// </summary>
    public void setStart(PlayerController newController,endEffect newEnd)
    {
        controllerScript = newController;
        endController = newEnd;
        pointScript.setStartData(effectController.getPeople());
        fireScript.setStart(newController);
        setPlayerScripts();
    }

    /// <summary>
    /// Sets all the scripts of the player controller 
    /// </summary>
    private void setPlayerScripts()
    {
        controllerScript.setScripts(effectController);
    }

    /// <summary>
    /// If the game is ended use
    /// </summary>
    public void setEnd(bool won)
    {
        effectController.setVision(0);
        endController.setState(won);
    }
}

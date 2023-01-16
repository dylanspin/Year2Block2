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
    private PlayerController controllerScript;
    private endEffect endController;

    public void setStart(PlayerController newController,endEffect newEnd)
    {
        controllerScript = newController;
        endController = newEnd;
        pointScript.setStartData(effectController.getPeople());
        fireScript.setStart(newController);
        setPlayerScripts();
    }

    private void setPlayerScripts()
    {
        controllerScript.setScripts(effectController);
    }

    public void setEnd(bool active)
    {
        endController.setState(active);
    }
}

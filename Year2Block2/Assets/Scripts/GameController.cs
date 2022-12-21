using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{   
    [Header("Private data")]
    private PlayerController controllerScript;
    private Animator endAnim;

    public void setStart(PlayerController newController)
    {
        controllerScript = newController;
    }

    public void wonGame()
    {
        
    }
    public void lostGame()
    {

    }

    /// <summary>
    /// After the game is lost animation is shown then the scene is reloaded
    /// </summary>
    public void reloadScene()
    {

    }
}

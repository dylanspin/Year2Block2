using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{   
    [Header("Private data")]
    private PlayerController controllerScript;
    private endEffect endController;

    public void setStart(PlayerController newController,endEffect newEnd)
    {
        controllerScript = newController;
        endController = newEnd;
    }

    public void setEnd(bool active)
    {
        endController.setState(active);
    }
}

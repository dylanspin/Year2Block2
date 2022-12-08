using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Testing Objects")]
    [SerializeField] private GameObject[] testingObject;

    [Header("Scripts")]
    [SerializeField] private Movement moveScript;
    [SerializeField] private Look lookScript;

    private void Start()
    {
        setStart();
    }

    /// <summary>
    /// Turns on or off components/objects based up on if its playing in the editor or on the quest
    /// </summary>    
    private void setStart()
    {
        bool isEditor = Application.isEditor;

        if(testingObject.Length > 0)
        {
            for(int i=0; i<testingObject.Length; i++)
            {
                testingObject[i].SetActive(isEditor);
            }
        }

        moveScript.enabled = isEditor;
        lookScript.enabled = isEditor;
    }
}

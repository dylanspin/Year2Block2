using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("Tag of the root object of the rescue people")]
    [SerializeField] private string triggerTag = "Doll";

    [Header("Scripts")]

    [Tooltip("Controller of the scene")]
    [SerializeField] private GameController controllerScript;
    
    [Header("UI")]

    [Tooltip("The text that displayes how many people have been Rescued")]
    [SerializeField] private TMPro.TextMeshProUGUI count;

    [Header("Private Data")]
    private List<Doll> inMap = new List<Doll>();
    private List<Doll> people = new List<Doll>();

    /// <summary>
    /// Sets the start count of how many dolls there are in the scene
    /// </summary>
    public void setStartData(List<Doll> setPeople)
    {
        inMap = setPeople;//sets the amount of dolls in the scene
        setText();//sets the UI of the end point
    }

    /// <summary>
    /// when gameobject enters the trigger check if its a doll if so add to the count 
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        GameObject rootObj = other.transform.root.gameObject;

        if(rootObj.tag == triggerTag)//if it is a people/doll
        {
            Doll dollScript = rootObj.GetComponent<Doll>();

            if(!people.Contains(dollScript))//if not already resqued
            {
                dollScript.stopSound();//stops the "help help" sound of the doll
                people.Add(dollScript);//ads to the found list
                setText();//Updates the UI

                if(people.Count >= inMap.Count)//if all people are resqued 
                {
                    controllerScript.setEnd(true);
                }
            }
        }
    }

    /// <summary>
    /// Sets the UI count
    /// </summary>
    private void setText()
    {
        count.text = people.Count + " Out Of " + inMap.Count + " Rescued";
    }
}

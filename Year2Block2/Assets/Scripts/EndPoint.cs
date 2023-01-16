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
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    public void setStartData(List<Doll> setPeople)
    {
        inMap = setPeople;
        setText();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        GameObject rootObj = other.transform.root.gameObject;

        if(rootObj.tag == triggerTag)//if it is a people/doll
        {
            Doll dollScript = rootObj.GetComponent<Doll>();

            if(!people.Contains(dollScript))//if not already resqued
            {
                people.Add(dollScript);
                setText();

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

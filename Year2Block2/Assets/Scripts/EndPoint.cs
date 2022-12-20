using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI count;
    [SerializeField] private string triggerTag = "Doll";

    [SerializeField] private List<GameObject> inMapHostages = new List<GameObject>();

    private List<GameObject> hostages = new List<GameObject>();

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        setText();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        GameObject rootObj = other.transform.root.gameObject;

        if(rootObj.tag == triggerTag)
        {
            if(!hostages.Contains(rootObj))
            {
                hostages.Add(rootObj);
                setText();
            }
        }
    }

    private void setText()
    {
        count.text = hostages.Count + " Out Of " + inMapHostages.Count + " Rescued";
    }
}

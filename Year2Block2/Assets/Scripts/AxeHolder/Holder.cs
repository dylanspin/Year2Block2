using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{   
    [Header("Settings")]

    [Tooltip("The tag of the objects that can be held")]
    [SerializeField] private string holdingTag = "Grabbable";

    [Tooltip("If the object should follow the angle/position of the holder object")]
    [SerializeField] private bool followSetPos = false;

    [Header("Components")]

    [Header("Optional")] [Tooltip("The gameobject that will be shown when in trigger")]
    [SerializeField] private GameObject ghostIndicator;

    [Header("Optional")] [Tooltip("The transform position where the item will be held")]
    [SerializeField] private Transform holdingPoint;

    [Header("Optional")] [Tooltip("Item that it should hold from the start")]
    [SerializeField] private ItemHold startHold;

    [Header("Private data")]
    private ItemHold currentHolding;

    ///still show a see trough indicator that the axe can be held maybe in a axe shape when in trigger

    /// <summary>
    /// Sets the start hold items
    /// </summary>
    private void Start()
    {
        if(startHold)
        {
            startHold.transform.parent = null;
            startHold.setStartGrab(this);
        }
    }

    private void Update()
    {
        if(followSetPos)
        {
            if(getHold())
            {
                Debug.Log("Test");
                currentHolding.setToHoldPos();
            }
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        checkTrigger(other,true);
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        checkTrigger(other,false);
    }

    /// <summary>
    /// Checks if the trigger object is a grabable object and the sets the intrigger on that item to true or false
    /// </summary>
    private void checkTrigger(Collider other,bool active)
    {
        GameObject rootObject = other.transform.root.gameObject;
        if(rootObject.tag == "Grabbable")
        {
            if(rootObject.GetComponent<ItemHold>())
            {
                ItemHold itemScript = rootObject.GetComponent<ItemHold>();
                itemScript.setIntrigger(this,active);
                showIndicator(active);
            }
        }
    }

    /// <summary>
    /// Shows or hides indicator object when in or out of the trigger 
    /// </summary>
    private void showIndicator(bool active)
    {
        if(ghostIndicator)
        {
            ghostIndicator.SetActive(active && !getHold());
        }
    }

    /// <summary>
    /// if item is set to be hold 
    /// </summary>
    public void setHold(ItemHold holdScript)
    {
        currentHolding = holdScript;
        if(holdScript != null)
        {
            showIndicator(false);
        }
    }

    /// <summary>
    /// returns if this holder is holding something
    /// </summary>
    public bool getHold()
    {
        return currentHolding != null;
    }

    /// <summary>
    /// returns custom grab transform pos or the current transform when the custom is not set
    /// </summary>
    public Transform getSetTrans()
    {
        return holdingPoint ? holdingPoint : transform;
    }
}

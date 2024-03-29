using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ItemHold : MonoBehaviour
{

    [Header("Scripts")]

    [Tooltip("The script for transitioning used for grabbing the helmet")]
    [SerializeField] endEffect endEffect;

    [Tooltip("The script for controlling the extinguisher")]
    [SerializeField] extinguisher extinguisherScript;

    [Tooltip("The script for controlling a twohanded item")]
    [SerializeField] TwoHanded twoScript;
 
    [Header("Testing")]

    [Tooltip("THe meshrenderer for visual testing of grabbing the object")]
    [SerializeField] private MeshRenderer testMesh;

    [Tooltip("Materials for visual testing of grabbing the object")]
    [SerializeField] private Material[] testMat;

    [Header("Components")]

    [Tooltip("This object main rigidbody")]
    [SerializeField] private Rigidbody objectRb;

    [Tooltip("The animator for controlling the grab collider")]
    [SerializeField] private Animator grabPointAnim;

    [Tooltip("The gameobject of the second grab point")]
    [SerializeField] private GameObject secondGrabPoint;

    [Header("Private data")]
    private List<Holder> inTriggerList = new List<Holder>();//all the holder scripts of where this object is in their trigger
    private Holder holderScript = null;//the script of the holder if this item is being held by a holder
    private bool grabbed = false;//if this object is currently grabbed or not

    /// <summary>
    /// Sets if the object should be held as starting holding object
    /// </summary>
    public void setStartGrab(Holder startScript)
    {
        holderScript = startScript;
        holderScript.setHold(this);
        setGrabCollider(false);
        setHold(true);
    }

    /// <summary>
    /// When the item is grabbed or let go by the interactor
    /// </summary>
    public void setGrab(bool active)
    {
        grabbed = active;

        // testingGrab(grabbed);//for testing
        setGrabCollider(active);
        checkScripts(active);

        if(active)//when grabbed
        {
            if(holderScript)
            {
                holderScript.setHold(null);
                setHold(false);
            }
        }
        else//when let go
        {
            SetHands.resetHand();

            Holder closedPos = getClosed();

            if(closedPos != null)
            {
                holderScript = closedPos;
                holderScript.setHold(this);
                setHold(true);
            }
        }
    }

    /// <summary>
    /// Changes the size of the grab collider of the axe so it can be grabbed from any point
    /// </summary>
    private void setGrabCollider(bool active)
    {
        if(grabPointAnim)
        {
            if(secondGrabPoint)
            {
                secondGrabPoint.SetActive(active);
            }
            grabPointAnim.SetBool("Grabbed",active);
        }
    }

    /// <summary>
    /// Changes the materials to indicate if its grabbed or not
    /// </summary>
    private void testingGrab(bool active)
    {
        testMesh.material = active ? testMat[0] : testMat[1];
    }

    /// <summary>
    /// Sets if the object should be held by the holder
    /// </summary>
    private void setHold(bool active)
    {
        if(active)
        {
            objectRb.constraints = RigidbodyConstraints.FreezeAll;
            setToHoldPos();
        }
        else
        {
            objectRb.constraints = RigidbodyConstraints.None;
        }
    }

    /// <summary>
    /// Set the position of held object
    /// </summary>
    public void setToHoldPos()
    {
        Transform holdTrans = holderScript.getSetTrans();
        transform.position = holdTrans.position;
        transform.eulerAngles = holdTrans.eulerAngles;
    }


    /// <summary>
    /// Function returns the most close transform in the list
    /// </summary>
    private Holder getClosed()
    {
        Holder closedPos = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Holder pos in inTriggerList)
        {
            float dist = Vector3.Distance(pos.transform.position, currentPos);

            if(dist < minDist && !pos.getHold())
            {
                closedPos = pos;
                minDist = dist;
            }
        }
        
        return closedPos;
    }

    private void checkScripts(bool active)
    {
        if(active)
        {
            if(endEffect)
            {
                endEffect.loadGameTransition();
            }

            if(twoScript)
            {
                twoScript.setStartRot();
            }
        }
        else
        {
            if(extinguisherScript)
            {
                extinguisherScript.shootExtinguisher(false);
            }
        }
    }

    /// <summary>
    /// Adds or removes holder to the script depending on if this object is in the trigger
    /// </summary>
    public void setIntrigger(Holder newAdd,bool add)
    {
        if(add)
        {
            if(!inTriggerList.Contains(newAdd))
            {
                inTriggerList.Add(newAdd);
            }
        }
        else
        {
            if(inTriggerList.Contains(newAdd))
            {
                inTriggerList.Remove(newAdd);
            }
        }
    }

    /// <summary>
    /// Function returns if the item is held or not
    /// </summary>
    public bool isGrabbed()
    {
        return grabbed;
    }
}

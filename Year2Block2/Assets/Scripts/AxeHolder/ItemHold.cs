using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHold : MonoBehaviour
{
    [Header("Scripts")]

    [SerializeField] endEffect endEffect;
 
    [Header("Components")]

    [SerializeField] private MeshRenderer testMesh;
    [SerializeField] private Material[] testMat;
    [SerializeField] private Rigidbody objectRb;
    [SerializeField] private Animator grabPointAnim;
    [SerializeField] private GameObject secondGrabPoint;

    [Header("Private data")]
    private List<Holder> inTriggerList = new List<Holder>();
    private Holder holderScript = null;
    private bool grabbed = false;

    /// <summary>
    /// Sets if the object should be held as starting holding object
    /// </summary>
    public void setStartGrab(Holder startScript)
    {
        holderScript = startScript;
        holderScript.setHold(true);
        setGrabCollider(false);
        setHold(true);
    }

    /// <summary>
    /// When the item is grabbed or let go by the interactor
    /// </summary>
    public void setGrab(bool active)
    {
        grabbed = active;
        // testingGrab(grabbed);
        setGrabCollider(active);

        if(active)//when grabbed
        {
            checkScripts();
            
            if(holderScript)
            {
                holderScript.setHold(false);
                setHold(false);
            }
        }
        else//when let go
        {
            Holder closedPos = getClosed();

            if(closedPos != null)
            {
                holderScript = closedPos;
                holderScript.setHold(true);
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
            Transform holdTrans = holderScript.getSetTrans();
            transform.position = holdTrans.position;
            transform.eulerAngles = holdTrans.eulerAngles;
        }
        else
        {
            objectRb.constraints = RigidbodyConstraints.None;
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

    private void checkScripts()
    {
        if(endEffect)
        {
            endEffect.loadGameTransition();
        }
    }
}

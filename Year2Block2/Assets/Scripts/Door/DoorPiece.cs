/*
    @author Dylan spin,
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPiece : MonoBehaviour
{
    [Header("Settings")]    

    [Tooltip("if all pieces break then this enables break parts when hit")]
    [SerializeField] private bool placeHolder = false;

    [Tooltip("if hinge breaking point of the door")]
    [SerializeField] private bool hingePoint = false;

    [Tooltip("if its the side of the door on the hinge side")]
    [SerializeField] private bool isSide = false;

    [Tooltip("if its the lock of the door")]
    [SerializeField] private bool isLock = false;
    
    [Header("Components")]

    [Tooltip("Main root door script")]
    [SerializeField] private Door doorScript;

    [Tooltip("The connected parts of the door for checking if a big part needs to break of or not")]
    [SerializeField] private List<DoorPiece> connectedParts;

    [Tooltip("The new layer of the broken of piece after being hit")]
    [SerializeField] private string colliderLayer = "NonColliding";

    [Header("Private values")]
    private bool broken = false;//if current piece is broken off
    private float breakingVel = 1.5f;

    /// <summary>
    /// Start function checks if the object it self is in its own list if so removes it this is used to prevent bugs
    /// </summary>
    public void StartCheck(float breakForce)
    {
        breakingVel = isLock? 2 : 1 * breakForce;//if is lock double the required force

        for(int i=0; i<connectedParts.Count; i++)
        {
            if(connectedParts[i] == this || connectedParts[i] == null)
            {
                connectedParts.RemoveAt(i);
            }
        }
    }
    
    /// <summary>
    /// Called from axe collision when this piece is hit 
    /// </summary>
    public void hitPiece(float velocity,Transform player,AxeCollider axeScript)
    {
        if(!broken)
        {
            if(velocity >= breakingVel)
            {
                breakPiece(player);

                startCheckHasConnection(player);
            }
        }
    }

    /// <summary>
    /// Breaks of piece of the door
    /// </summary>
    public void breakPiece(Transform player)
    {
        broken = true;

        if(placeHolder)//if place holder collider
        {
            gameObject.SetActive(false);
        }
        else
        {
            if(hingePoint)
            {
                doorScript.removeHinge(player,gameObject);
            }

            if(isLock)
            {
                doorScript.removeLock(player);
            }
            
            gameObject.layer = LayerMask.NameToLayer(colliderLayer);
            gameObject.AddComponent<Rigidbody>();
            Destroy(gameObject,2);
        }
    }


    /// <summary>
    /// Start of check if lose pieces should fall off or not
    /// </summary>
    private void startCheckHasConnection(Transform player)
    {
        List<DoorPiece> checkAll = new List<DoorPiece>();

        for(int i=0; i<connectedParts.Count; i++)
        {
            if(connectedParts[i])
            {
                connectedParts[i].removeFromCheck(this,player);
            }
        }

        doorScript.checkAll(player,placeHolder);

        // checkConnection(checkAll,0,this);
    }   

    /// <summary>
    /// Checks connected parts for if they are connected to the door in the end
    /// </summary>
    public void checkConnection(List<DoorPiece> checkList,int count,DoorPiece firsPiece)
    {
        if(count < 10)
        {
            checkList.Add(this);
            for(int i=0; i<connectedParts.Count; i++)
            {
                if(connectedParts[i])
                {
                    if(!checkList.Contains(connectedParts[i]))
                    {
                        connectedParts[i].checkConnection(checkList,count+1,firsPiece);
                    }
                    else
                    {
                        Debug.Log("Is in list");
                        //is already in list
                    }
                }
            }
        }
    }

    /// <summary>
    /// Removes the initial broken piece from all the connected parts their list
    /// </summary>
    public void removeFromCheck(DoorPiece removeScript,Transform player)
    {
        if(!broken)
        {
            if(connectedParts.Count <= 1)
            {
                breakPiece(player);
            }

            connectedParts.Remove(removeScript);
        }
    }

    /// <summary>
    /// Double check if there is a empty one in list
    /// </summary>
    public void checkEmpty(Transform player)
    {
        if(!broken)
        {
            for(int i=0; i<connectedParts.Count; i++)
            {
                if(connectedParts[i] == null)
                {
                    connectedParts.RemoveAt(i);
                }
            }
            if(connectedParts.Count <= 1)
            {
                breakPiece(player);
            }
        }
    }

    //go trough all connected make new list and add the current script to it each time its called 
    //then in the function check if its side position is attached to it
    //dont call the function again on one of the objects already in the list
}

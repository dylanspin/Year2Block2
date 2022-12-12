/*
    @author Dylan spin,
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Testing materials")]

    [SerializeField] private Material[] checkmaterials;
    [SerializeField] private Material defaultMat;

    [Header("Settings")]

    [Tooltip("The min velocity needed to break the door")]
    [SerializeField] private float breakingVel = 1.5f;

    [SerializeField] private float fallingForce = 100;

    [Tooltip("The new layer of the broken of piece after being hit")]
    [SerializeField] private string colliderLayer = "NonColliding";

    [Header("Components")]

    [SerializeField] private DoorPiece[] hingePoints;
    [SerializeField] private DoorPiece[] allPoints;

    [Header("Private data")]
    
    private int hingeCount;
    private Rigidbody rb;
  
    private void Start()
    {
        hingeCount = hingePoints.Length;

        for(int i=0; i<allPoints.Length; i++)
        {
            allPoints[i].StartCheck(breakingVel);
        }
    }

    public void removeHinge(Transform player)
    {
        if(hingeCount > 1)
        {
            hingeCount --;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer(colliderLayer);
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer(colliderLayer);
            }

            rb = gameObject.AddComponent<Rigidbody>();
            rb.AddForce(player.transform.forward * Random.Range(fallingForce/2,fallingForce), ForceMode.Impulse);
        }
    }

    /////////////testing

    public Material getMaterial(int count)
    {
        if(count >= checkmaterials.Length)
        {
            return checkmaterials[checkmaterials.Length-1];
        }  
        else
        {
            return checkmaterials[count];
        }
    }

    public void checkAll()
    {
        for(int i=0; i<allPoints.Length; i++)
        {
            if(allPoints[i])
            {
                allPoints[i].checkEmpty();
            }
        }
    }
}

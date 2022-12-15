using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Doll : MonoBehaviour
{   
    [SerializeField] private Material[] testmat;
    [SerializeField] private MeshRenderer[] testRender;

    [Header("Settings")]

    [Tooltip("The layer when the doll is grabbed")]
    [SerializeField] private string noCollisionLayer = "Doll";

    [Tooltip("The layer when the doll is not grabbed")]
    [SerializeField] private string collisionLayer = "Default";

    [Header("Private values")]
    private int grabbed = 0;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            grabDoll();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            letGoDoll();
        }
    }

    /// <summary>
    /// Function sets the new layers
    /// </summary>
    private void setAllLayer(bool active)
    {
        string newLayer = active ? noCollisionLayer : collisionLayer;

        setChilderen(gameObject,LayerMask.NameToLayer(newLayer));
    }

    /// <summary>
    /// Function sets childeren layers
    /// </summary>
    private void setChilderen(GameObject setObject, int newLayer)
    {
        setObject.layer = newLayer;

        foreach (Transform child in setObject.transform)
        {
            child.gameObject.layer = newLayer;

            Transform _HasChildren = child.GetComponentInChildren<Transform>();
            if (_HasChildren != null)
            {
                setChilderen(child.gameObject, newLayer);
            }
        }
    }

    /// <summary>
    /// Function for grabbing the doll
    /// </summary>
    public void grabDoll()
    {
        if(grabbed <= 0)
        {
            setAllLayer(true);
            testing(true);
        }

        grabbed ++;
    }

    /// <summary>
    /// Function for letting go of the doll
    /// </summary>
    public void letGoDoll()
    {
        if(grabbed <= 1)
        {
            setAllLayer(false);
            testing(false);
        }

        grabbed --;    
    }
    
    private void testing(bool active)
    {
        Material testMat = active? testmat[0] : testmat[1];
        for(int i=0; i<testRender.Length; i++)
        {
            testRender[i].material = testMat;
        }
    }
}

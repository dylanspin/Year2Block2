using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Doll : MonoBehaviour
{   
    [SerializeField] private Material[] visionMats;
    [SerializeField] private MeshRenderer[] meshRenders;

    public void setMat(int setMat)
    {
        Material mat = visionMats[setMat];
        for(int i=0; i<meshRenders.Length; i++)
        {
            meshRenders[i].material = mat;
        }
    }
}

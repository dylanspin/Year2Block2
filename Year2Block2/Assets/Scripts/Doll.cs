using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Doll : MonoBehaviour
{   

    [Header("Visuals")]

    [Tooltip("0 = normal vision material 1 = thermal vision material")]
    [SerializeField] private Material[] visionMats;

    [Header("All the mesh renderes of the doll for switching the materials")]
    [SerializeField] private MeshRenderer[] meshRenders;

    [Header("Adio components")]

    [Tooltip("The audio source that indicates where the doll is")]
    [SerializeField] private AudioSource helpSoundEffect;

    /// <summary>
    /// Changes the materials based up on the different types of visions
    /// </summary>
    public void setMat(int setMat)
    {
        Material mat = visionMats[setMat];
        for(int i=0; i<meshRenders.Length; i++)
        {
            meshRenders[i].material = mat;
        }
    }

    /// <summary>
    /// Stops the indicator sound when the doll is in the resque zone
    /// </summary>
    public void stopSound()
    {
        helpSoundEffect.Stop();
    }
}

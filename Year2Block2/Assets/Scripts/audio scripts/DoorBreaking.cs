using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreaking : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    [Header("Private data")]
    private bool hasPlayed = false;

    public void playSoundEffect()
    {
        if (!hasPlayed)
        {
            source.PlayOneShot(clip);
            hasPlayed = true;
        }
        else
        {
            hasPlayed= false;   
        }
    }
}

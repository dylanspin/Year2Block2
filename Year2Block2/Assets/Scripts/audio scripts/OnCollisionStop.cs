using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionStop : MonoBehaviour
{
    /*Made by Sergiu*/
    [SerializeField] public AudioSource source;
    [SerializeField] public AudioClip clip;

    private bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        source.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Grabbable")
        {

            if (!hasPlayed)
            {
                source.Play();
                hasPlayed = true;
            }
        }
        else
            source.Stop();

        Debug.Log("Success!");
        }
    }


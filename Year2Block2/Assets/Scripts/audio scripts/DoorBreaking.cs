using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBreaking : MonoBehaviour
{
    [SerializeField] public AudioSource source;
    [SerializeField] public AudioClip clip;

    private bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {

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
                source.PlayOneShot(clip);
                hasPlayed = true;
            }
            else
            {
                hasPlayed= false;   
            }


            Debug.Log("Success!");
         }
     }
}

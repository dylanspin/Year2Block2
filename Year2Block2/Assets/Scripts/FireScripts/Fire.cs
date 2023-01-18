using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    
    [Header("Components")]

    [Tooltip("The fire particle system")]
    [SerializeField] private ParticleSystem fireParticle;
    
    [Tooltip("The fire particle system")]
    [SerializeField] private GameObject triggerObject;

    [Tooltip("The fire sound effect emiter")]
    [SerializeField] private AudioSource fireSound;

    [Tooltip("The fires this fire can spread to")]
    [SerializeField] private List<Fire> spreadOptions = new List<Fire>();

    [Header("Private data")]
    private int spreadCount = 0;
    private int spreadNeed = 100;

    /// <summary>
    /// Sets the connecting fires
    /// </summary>
    public void addFireOption(Fire added)
    {
        spreadOptions.Add(added);
    }

    /// <summary>
    /// Sets start data and checks the fire spread options list to prevent bugs
    /// </summary>
    public void setStart(int required)
    {
        spreadNeed = required;
    }

    /// <summary>
    /// Adds to the spread count 
    /// </summary>
    public void spread()
    {
        if(isOnFire())
        {
            if(spreadCount + 1 < spreadNeed)
            {
                spreadCount ++;
            }
            else
            {
                ligtFire();
            }
        }
    }

    /// <summary>
    /// Turns on a connected fire
    /// </summary>
    private void ligtFire()
    {
        int spreadAmount = spreadOptions.Count;

        if(spreadAmount > 0)
        {
            spreadCount = 1;//resets the count
            int randomFire = Random.Range(0,spreadOptions.Count);

            if(!spreadOptions[randomFire].isOnFire())
            {
                spreadOptions[randomFire].setFire();
            }
        }
    }

    /// <summary>
    /// Takes of health from the fire when hit something that turns it off
    /// </summary>
    public void takeFireHealth()
    {
        if(spreadCount > 1)
        {
            spreadCount --;
        }
        else
        {
            setParticleSystem(false);
        }
    }

    /// <summary>
    /// Starts the fire
    /// </summary>
    public void setFire()
    {
        spreadCount = 1;
        setParticleSystem(true);
    }

    /// <summary>
    /// Turns on or off the particle system with a boolean 
    /// </summary>
    private void setParticleSystem(bool active)
    {
        if(active)
        {
            CancelInvoke("disableFireParticle");
            triggerObject.SetActive(true);
            fireParticle.gameObject.SetActive(true);
            fireSound.Play();
            fireParticle.Play();
        }
        else
        {
            Invoke("disableFireParticle",1);
            CancelInvoke("disableFireParticle");
            triggerObject.SetActive(false);
            fireSound.Stop();
            fireParticle.Stop();
        }
    }

    /// <summary>
    /// Turns of the particle system object for performance after 1 sec so the particle system can die out
    /// </summary>
    private void disableFireParticle()
    {
        fireParticle.gameObject.SetActive(false);
    }

    public void setParticle(float newAmount)
    {
        var emission = fireParticle.emission;
        emission.rateOverTime = newAmount;
    }

    //get value functions

    /// <summary>
    /// Check if the current fire is on fire
    /// </summary>
    public bool isOnFire()
    {
        return spreadCount > 0;
    }
}

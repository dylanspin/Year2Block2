using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("Damage per second")]
    [SerializeField] private float damagePerSec = 15;

    [Tooltip("The amount of time for the fire to update")]
    [SerializeField] private float timeNextupdate = 2.5f;

    [Tooltip("The amount required to spread to another spot")]
    [SerializeField] private int spreadRequired = 10;

    [Tooltip("The amount of fires that start at the start of the scene")]
    [SerializeField] private int startSpread = 20;

    [Header("Private data")]
    private PlayerController player;//the main player controller script for taking damage if the player is in any trigger
    private int inTrigger = 0;//the intrigger count this counts in how many triggers the player is currently
    private List<Fire> allFires = new List<Fire>();//list of al the fires in the scene
    private List<Fire> randomFires = new List<Fire>();//list used for setting on random fires at the start of the game so there wont be any double lightings of fire

    /// <summary>
    /// The start function of this script called from the game controller. Sets the main player script for taking health
    /// </summary>
    public void setStart(PlayerController setPlayer)
    {
        player = setPlayer;
        setAllFires();
        setFires();
        setStartFires();
        InvokeRepeating("addSpread",timeNextupdate,timeNextupdate);
    }

    /// <summary>
    /// Sets base data for all the individual fires
    /// </summary>
    private void setFires()
    {
        for(int i=0; i<allFires.Count; i++)
        {
            allFires[i].setStart(spreadRequired);
        }
    }

    /// <summary>
    /// Sets the start fires using the value startSpread
    /// </summary>
    private void setStartFires()
    {
        for(int i=0; i<startSpread; i++)
        {
            int randomFire = Random.Range(0,randomFires.Count);
            randomFires[randomFire].setFire();
            randomFires.RemoveAt(randomFire);
        }
    }

    /// <summary>
    /// Update is called every frame, checks the dealing damage function
    /// </summary>
    private void Update()
    {
        checkDamage();
    }

    /// <summary>
    /// Check if the player should take damage
    /// </summary>
    private void checkDamage()
    {
        if(inTrigger > 0)
        {
            float amount = damagePerSec * Time.deltaTime;
            player.dealDamage(amount);
        }
    }

    /// <summary>
    /// runs the spread function on each fire grid
    /// </summary>
    private void addSpread()
    {
        for(int i=0; i<allFires.Count; i++)
        {
            allFires[i].spread();
        }
    }

    /// <summary>
    /// Adds to the count of in how many triggers the player is
    /// </summary>
    public void setIntrigger(bool active)
    {
        inTrigger += active ? 1 : -1;
    }

    /// <summary>
    /// Adds to fireList
    /// </summary>
    public void setAllFires()
    {
        int colums = transform.childCount;

        for(int i=0; i<colums; i++)
        {
            transform.GetChild(i).GetComponent<ColumnManager>().setStart(this);
        }
    }

    /// <summary>
    /// Add the fire cell to the list at the start of the scene so all the fires are in one list
    /// </summary>
    public void addFireToList(Fire added)
    {
        if(!allFires.Contains(added))
        {
            randomFires.Add(added);
            allFires.Add(added);
        }
    }   

    /// <summary>
    /// set the particle systems emition when switching between different types of views
    /// </summary>
    public void downFire(float emitionAmount)
    {
        for(int i=0; i<allFires.Count; i++)
        {
            allFires[i].setParticle(emitionAmount);
        }
    }
}

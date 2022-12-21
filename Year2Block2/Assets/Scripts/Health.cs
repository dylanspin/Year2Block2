using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("The max amount of health that the player can have")]
    [SerializeField] private float maxHealth = 100;

    [Tooltip("The amount of health that gets added every second")]
    [SerializeField] private float healthRegen = 5;

    [Header("Privat Data")]
    private bool dead = false;
    private float currentHealth = 100;
    private PlayerController controllerScript;//the main controller of the player thats linked to the game controller

    /// <summary>
    /// Sets the start settings
    /// </summary>
    public void setStart(PlayerController newController)
    {
        controllerScript = newController;
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(!dead)
        {
            regenHealth();
        }
    }

    /// <summary>
    /// checks if the health is below the max health and then adds the health regen amount 
    /// </summary>
    private void regenHealth()
    {
        if(currentHealth < maxHealth)
        {
            if(currentHealth + healthRegen < maxHealth)
            {
                currentHealth += healthRegen;
            }
            else
            {
                currentHealth = maxHealth;
            }
        }
    }

    /// <summary>
    /// Function that takes of a amount of health for damage and checks if the player still has enough health left
    /// </summary>  
    public void loseHealth(float amount)
    {
        if(!dead)
        {
            if(currentHealth - amount > 0)
            {
                currentHealth -= amount;
            }
            else//if the played doesnt have enough health
            {
                dead = true;
                currentHealth = 0;
                controllerScript.lostGame();
            }
        }
    }
}

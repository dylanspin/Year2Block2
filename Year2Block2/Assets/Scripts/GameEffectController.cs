using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffectController : MonoBehaviour
{
    [Header("Fire settings")]

    [Tooltip("Script that controlls all the fire in the scene")]
    [SerializeField] private FireController fireScript;

    [Tooltip("The amount of particles for the particle systems / 0 = normal vision - 1 = thermal vision")]
    [SerializeField] private float[] emesionAmounts = {10,2};

    [Header("Rescue dolls settings")]

    [Tooltip("All the fire particle systems in the scene used for changing the settings when the thermal vision is on")]
    [SerializeField] private List<Doll> dols = new List<Doll>();

    /// <summary>
    /// Sets the particle systems and dolls in the scene for the current vision
    /// </summary>
    public void setVision(int visionId)
    {
        for(int i=0; i<dols.Count; i++)
        {
            dols[i].setMat(visionId);
        }

        fireScript.downFire(emesionAmounts[visionId]);
    }

    /// <summary>
    /// Returns the list of all the dolls in the scene
    /// </summary>
    public List<Doll> getPeople()
    {
        return dols;
    }
}

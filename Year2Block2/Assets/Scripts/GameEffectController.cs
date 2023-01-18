using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffectController : MonoBehaviour
{
    [Header("Fire settings")]

    [Tooltip("Script that controlls all the fire in the scene")]
    [SerializeField] private FireController fireScript;

    [SerializeField] private float[] emesionAmounts = {15,2};

    [Header("Rescue dolls settings")]

    [Tooltip("All the fire particle systems in the scene used for changing the settings when the thermal vision is on")]
    [SerializeField] private List<Doll> dols = new List<Doll>();

    public void setVision(int visionId)
    {
        for(int i=0; i<dols.Count; i++)
        {
            dols[i].setMat(visionId);
        }

        fireScript.downFire(emesionAmounts[visionId]);
    }


    public List<Doll> getPeople()
    {
        return dols;
    }
}

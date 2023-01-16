using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEffectController : MonoBehaviour
{
    [Header("Fire settings")]

    [Tooltip("All the fire particle systems in the scene used for changing the settings when the thermal vision is on")]
    [SerializeField] private ParticleSystem[] fires;

    [Tooltip("All the fire particle systems in the scene used for changing the settings when the thermal vision is on")]
    [SerializeField] private List<Doll> dols = new List<Doll>();

    public void setVision(int visionId)
    {
        for(int i=0; i<dols.Count; i++)
        {
            dols[i].setMat(visionId);
        }
    }


    public List<Doll> getPeople()
    {
        return dols;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extinguisher : MonoBehaviour
{   
    [Header("Settings")]

    [Tooltip("The range of the extinguisher")]
    [SerializeField] private float range = 5.0f;

    [Header("Components")]

    [Tooltip("The position of where the raycast gets shot out of")]
    [SerializeField] private Transform shootPoint;

    private void Update()
    {
        
    }
}

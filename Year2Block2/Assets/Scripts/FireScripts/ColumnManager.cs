using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnManager : MonoBehaviour
{
    [SerializeField] private List<ColumnManager> connectingColums = new List<ColumnManager>();

    [Header("Private data")]
    private FireController controllerScript;

    public void setStart(FireController setControllerScript)
    {
        controllerScript = setControllerScript;
        setConnection();
    }

    private void setConnection()
    {
        int childCount = transform.childCount;
        for(int i=0; i<childCount; i++)
        {
            if(i == 0)//first
            {
                addNext(i,childCount);
            }
            else if(i == childCount-1)//last
            {
                addBefore(i);
            }
            else//in between
            {
                addNext(i,childCount);
                addBefore(i);
            }
        }
    }

    private void addNext(int i, int childCount)
    {
        if(i + 1 <= childCount)
        {
            Fire cellFire = transform.GetChild(i).GetComponent<Fire>();
            controllerScript.addFireToList(cellFire);
            if(connectingColums.Count > 0)
            {
                for(int b=0; b<connectingColums.Count; b++)
                {
                    if(connectingColums[b].transform.childCount >= i + 1)
                    {
                        Fire nextColum = connectingColums[b].transform.GetChild(i + 1).GetComponent<Fire>();
                        Fire sideColum = connectingColums[b].transform.GetChild(i).GetComponent<Fire>();
                        cellFire.addFireOption(nextColum);
                    }
                }
            }
        
            Fire next = transform.GetChild(i + 1).GetComponent<Fire>();
            cellFire.addFireOption(next);
        }
    }

    private void addBefore(int i)
    {
        if(i - 1 >= 0)
        {
            Fire cellFire = transform.GetChild(i).GetComponent<Fire>();
            controllerScript.addFireToList(cellFire);
            if(connectingColums.Count > 0)
            {
                for(int b=0; b<connectingColums.Count; b++)
                {
                    Fire nextColum = connectingColums[b].transform.GetChild(i - 1).GetComponent<Fire>();
                    Fire sideColum = connectingColums[b].transform.GetChild(i).GetComponent<Fire>();
                    cellFire.addFireOption(nextColum);
                }
            }

            Fire next = transform.GetChild(i - 1).GetComponent<Fire>();
            cellFire.addFireOption(next);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnManager : MonoBehaviour
{
    [Header("Componenets")]

    [Tooltip("Other columns connected to this one")]
    [SerializeField] private List<ColumnManager> connectingColums = new List<ColumnManager>();

    [Header("Private data")]
    private FireController controllerScript;

    /// <summary>
    /// Sets the starting data called from the firecontroller
    /// </summary>
    public void setStart(FireController setControllerScript)
    {
        controllerScript = setControllerScript;
        setConnection();
    }

    /// <summary>
    /// Sets all the cell connections in this column 
    /// </summary>
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

    /// <summary>
    /// Adds next cell in child order
    /// </summary>
    private void addNext(int i, int childCount)
    {
        if(i + 1 <= childCount)
        {
            check(i,true);
        }
    }

    /// <summary>
    /// Adds the cell before this one in the child order
    /// </summary>
    private void addBefore(int i)
    {
        if(i - 1 >= 0)
        {
            check(i,false);
        }
    }

    /// <summary>
    /// Check if the selected cell should be checked if so run the connection checks
    /// </summary>
    private void check(int i, bool up)
    {
        Fire cellFire = transform.GetChild(i).GetComponent<Fire>();
        int addAmount = (up ? 1 : -1);
        if(cellFire.gameObject.activeSelf)
        {
            controllerScript.addFireToList(cellFire);
            if(connectingColums.Count > 0)
            {
                for(int b=0; b<connectingColums.Count; b++)
                {
                    addSide(b,i + addAmount,cellFire);
                    addSide(b,i,cellFire);
                }
            }

            addSelf(i + addAmount,cellFire);
        }
    }

    /// <summary>
    /// Adds the cell from this column
    /// </summary>
    private void addSelf(int i,Fire cellFire)
    {
        Fire next = transform.GetChild(i).GetComponent<Fire>();
        if(next.gameObject.activeSelf)
        {
            cellFire.addFireOption(next);
        }
    }   

    /// <summary>
    /// Adds the cell from connecting columns
    /// </summary>
    private void addSide(int b,int i,Fire cellFire)
    {
        int childCount = connectingColums[b].transform.childCount;
        if(i <= childCount)
        {
            Fire nextColum = connectingColums[b].transform.GetChild(i).GetComponent<Fire>();
            if(nextColum.gameObject.activeSelf)
            {
                cellFire.addFireOption(nextColum);
            }
        }
    }
}

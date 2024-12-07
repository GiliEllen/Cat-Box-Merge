using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] catsPrefabs;
    public Cat[] catRow1 ;
    public Cat[] catRow2 ;
    public Cat[] catRow3 ;
    public Cat[] catRow4 ;

     public List<List<Cat>> catMatrix;
    void Start()
    {
        catMatrix = new List<List<Cat>>()
        {
            new List<Cat>(catRow1),
            new List<Cat>(catRow2),
            new List<Cat>(catRow3),
            new List<Cat>(catRow4)
        };
        initializeCatStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 void initializeCatStatus() 
{
    for (int i = 0; i < catMatrix.Count; i++) 
    {
        for (int j = 0; j < catMatrix[i].Count; j++) 
        {
            // Mark cats in the last row as clickable
            if (i == catMatrix.Count - 1) 
            {
                if (catMatrix[i][j] != null) 
                {
                    catMatrix[i][j].isClickable = true;
                }
            }

            // Check if the next row exists and is within bounds
            if (i + 1 < catMatrix.Count && j < catMatrix[i + 1].Count) 
            {
                if (catMatrix[i+1][j] == null) 
                {
                    if (catMatrix[i][j] != null) 
                    {
                        catMatrix[i][j].isClickable = true;
                    }
                }
            }

            // Check the left and right neighbors for valid indices
            if ((j - 1 >= 0 && catMatrix[i][j-1] == null) || (j + 1 < catMatrix[i].Count && catMatrix[i][j+1] == null)) 
            {
                if (catMatrix[i][j] != null) 
                {
                    catMatrix[i][j].isClickable = true;
                }
            }
        }
    }
}
}

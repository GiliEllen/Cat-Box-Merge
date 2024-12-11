using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // public GameObject[] catsPrefabs;
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
        InitializeCatStatus();
    }

public void RemoveCatFromList(int catId) {
    for (int row = 0; row < catMatrix.Count; row++) {
        for (int col = 0; col < catMatrix[row].Count; col++) {
            Cat currentCat = catMatrix[row][col];
            if (currentCat != null && currentCat.catId == catId) {
                catMatrix[row][col] = null;
                Debug.Log("cat is removed");
            }
        }
    }
    InitializeCatStatus(); 
}

    void InitializeCatStatus() {
        for (int i = 0; i < catMatrix.Count; i++) 
        {
            for (int j = 0; j < catMatrix[i].Count; j++) 
            {
                Cat currentCat = catMatrix[i][j];

                if (currentCat == null)
                {
                    continue;
                } else {
                    Debug.Log(currentCat);
                }

                bool hasNextInRow = (j + 1 < catMatrix[i].Count) && (catMatrix[i][j + 1] != null);
                bool hasPrevInRow = (j - 1 >= 0) && (catMatrix[i][j - 1] != null);
                bool hasNextRow = (i + 1 < catMatrix.Count) && (j < catMatrix[i + 1].Count) && (catMatrix[i + 1][j] != null);

                if (hasNextInRow && hasPrevInRow && hasNextRow) {
                    currentCat.setClickableFalse(CatStatus.Loafing);
                } else {
                    currentCat.setClickableTrue();
                }
            }
        }
    }
}

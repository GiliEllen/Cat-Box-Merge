using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // public GameObject[] catsPrefabs;
    public Cat[] catRow1 ;
    public Cat[] catRow2 ;
    public Cat[] catRow3 ;
    public Cat[] catRow4 ;
    public Cat[] catRow5 ;
    public Cat[] catRow6 ;

    public List<List<Cat>> catMatrix;
    public BoxAreaManager boxAreaManager;

    public int levelIndex;
    public GameObject nextSubLevel;
    public GameObject levelIndicator;
    public GameObject nextLevelIndicator;
    public GameObject levelMap;
    public GameObject nextLevelMap;
    public GameObject levelInstruction;
    public GameObject nextLevelInstruction;
    void Start()
    {
        catMatrix = new List<List<Cat>>()
        {
            new List<Cat>(catRow1),
            new List<Cat>(catRow2),
            new List<Cat>(catRow3),
            new List<Cat>(catRow4),
            new List<Cat>(catRow5),
            new List<Cat>(catRow6)
        };
        InitializeCatStatus();
    }

public void RemoveCatFromList(int catId) {
    for (int row = 0; row < catMatrix.Count; row++) {
        for (int col = 0; col < catMatrix[row].Count; col++) {
            Cat currentCat = catMatrix[row][col];
            if (currentCat != null && currentCat.catId == catId) {
                catMatrix[row][col] = null;
                // Debug.Log("cat is removed");
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
                } else if (currentCat.catId == 0) {
                    continue;
                } {
                    Debug.Log(currentCat);
                }

                bool hasNextInRow = (j + 1 < catMatrix[i].Count) && (catMatrix[i][j + 1] != null);
                bool hasPrevInRow = (j - 1 >= 0) && (catMatrix[i][j - 1] != null);
                bool hasNextRow = (i + 1 < catMatrix.Count) && (j < catMatrix[i + 1].Count) && (catMatrix[i + 1][j] != null);
                bool hasPrevRow = (i - 1 >= 0) && (j < catMatrix[i - 1].Count) && (catMatrix[i - 1][j] != null); 


                if (hasNextInRow && hasPrevInRow && hasNextRow && hasPrevRow) {
                    currentCat.setClickableFalse(CatStatus.Loafing);
                } else if (j == 0) {
                    if (hasNextInRow && hasNextRow) {
                        currentCat.setClickableFalse(CatStatus.Loafing);
                    } else  {
                         currentCat.setClickableTrue();
                    }
                } else if (j == catMatrix[i].Count -1) {
                        if (hasPrevInRow && hasNextRow) {
                        currentCat.setClickableFalse(CatStatus.Loafing);
                    } else  {
                         currentCat.setClickableTrue();
                    }
                } else if (i == 0 && hasNextInRow && hasPrevInRow && hasNextRow){
                    currentCat.setClickableFalse(CatStatus.Loafing);
                } else {
                    currentCat.setClickableTrue();
                }
            }
        }
    }



    public bool CheckIfGameWin()
    {
        foreach (var catList in catMatrix)
        {
            foreach (var cat in catList)
            {
                if (cat != null && cat.catId != 0)
                {
                    Debug.Log("A cat is still active in the matrix.");
                    return false;
                }
            }
        }

        foreach (var cat in boxAreaManager.cats)
        {
            if (cat != null && cat.catId != 0)
            {
                Debug.Log("A cat is still active in the box area.");
                return false;
            }
        }

        return true;
    }

    public void CompleteLevel() {
        if (nextSubLevel != null) {
            nextSubLevel.SetActive(true);
            if (levelIndicator != null) {
                Image levelIndicatorRenderer = levelIndicator.GetComponent<Image>();
                if (levelIndicatorRenderer != null) {
                    Color color = levelIndicatorRenderer.color;
                    color.a = 0.5f;
                    levelIndicatorRenderer.color = color;
                }
            }

            if (nextLevelIndicator != null) {
                Image nextLevelIndicatorRenderer = nextLevelIndicator.GetComponent<Image>();
                if (nextLevelIndicatorRenderer != null) {
                    Color color = nextLevelIndicatorRenderer.color;
                    color.a = 1f;
                    nextLevelIndicatorRenderer.color = color;
                }
            }

            if (nextLevelMap != null) {
                nextLevelMap.SetActive(true);
            }
            if (levelMap != null) {
                levelMap.SetActive(false);
            }
            if (nextLevelInstruction != null) {
                nextLevelInstruction.SetActive(true);
            }
            if (levelInstruction != null) {
                levelInstruction.SetActive(false);
            }
            this.gameObject.SetActive(false);
        } else {
            GameManager gameManager = GameManager.Instance;
            gameManager.CompleteLevel(levelIndex);
        }
    }
}

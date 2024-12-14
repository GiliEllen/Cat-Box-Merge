using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoxAreaManager : MonoBehaviour
{
    public List<Cat> cats;
    public int maxCatsInRow  = 7;  
    public float spacing = 0.0f;

    public int columns = 7;            
    public GameObject[] boxes;
    public LevelManager levelManager; 

    public int GetCatsCount()
    {
        return cats.Count; 
    }

    void Start()
    {
        cats = new List<Cat>(); 
    }

    public void AddCat(Cat newCat)
    {
        cats.Add(newCat);
        // Debug.Log("Cat added.");
        PositionCats(); 
        CheckAndRemoveCatsOfSameColor(newCat.color);
        // Debug.Log(newCat.catId);
        levelManager.RemoveCatFromList(newCat.catId);

        bool isGameOver = IsGameOver();
        // Debug.Log("isGameOver: " + isGameOver);
        bool isGameWin = levelManager.CheckIfGameWin();
        if (isGameWin) {
            levelManager.CompleteLevel();
        }
    }

private void PositionCats()
{
    for (int i = 0; i < cats.Count; i++)
    {
        Vector3 newPosition = boxes[i].transform.position;
        newPosition.y += 0.25f;
        cats[i].transform.position = newPosition;
    }
}

    private void CheckAndRemoveCatsOfSameColor(string color)
    {
        var catsOfColor = cats.Where(cat => cat.color == color).ToList();

        if (catsOfColor.Count == 3)
        {
            foreach (var cat in catsOfColor)
            {
                cat.gameObject.SetActive(false);
            }
            cats.RemoveAll(cat => cat.color == color);
            // Debug.Log($"Removed all cats of color: {color}");
        }

        if (IsGameOver()) {
            Debug.Log("game is over");
        }
    }

    public bool IsGameOver() {
        if (cats.Count == 7) {
            //TODO: game over logic
            // Debug.Log("Game over");
            return true;
        } else {
            return false;
        }
    }
}
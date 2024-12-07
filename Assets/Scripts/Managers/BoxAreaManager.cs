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
    private float cellWidth = 0.5f;         
    private float cellHeight = 0.5f; 
     public GameObject[] boxes;

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
        Debug.Log("Cat added.");
        // MoveCatToBoxArea(newCat);
        PositionCats(); 
        CheckAndRemoveCatsOfSameColor(newCat.color);
    }

   private void PositionCats()
    {

        for (int i = 0; i < cats.Count; i++)
        {
            cats[i].transform.position = boxes[i].transform.position;
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
            Debug.Log($"Removed all cats of color: {color}");
        }

        if (IsGameOver()) {
            Debug.Log("game is over");
        }
    }

    public bool IsGameOver() {
        if (cats.Count == 7) {
            //TODO: game over logic
            Debug.Log("Game over");
            return true;
        } else {
            return false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoxAreaManager : MonoBehaviour
{
    public List<Cat> cats;
    public int maxCatsInRow = 7;  
    public float spacing = 0.0f;

    public int columns = 7;            
    public GameObject[] boxes;
    public LevelManager levelManager; 

    public Modal gameOverModal;
    public AudioManager audioManager;

    private Dictionary<Cat, Vector3> catTargetPositions = new Dictionary<Cat, Vector3>(); 
    private Dictionary<Cat, float> lerpStartTimes = new Dictionary<Cat, float>(); 
    public float lerpDuration = 1f; 

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
        bool isGameOver = IsGameOver();
        if (isGameOver) {
            gameOverModal.gameObject.SetActive(true);
            levelManager.gameObject.SetActive(false);
            return;
        }
        cats.Add(newCat);
        // Debug.Log("Cat added.");
        PositionCats(); 
        CheckAndRemoveCatsOfSameColor(newCat.color);
        // Debug.Log(newCat.catId);
        levelManager.RemoveCatFromList(newCat.catId);

        isGameOver = IsGameOver();
        // Debug.Log("isGameOver: " + isGameOver);
        if (isGameOver) {
            gameOverModal.gameObject.SetActive(true);
            levelManager.gameObject.SetActive(false);
            return;
        }
        bool isGameWin = levelManager.CheckIfGameWin();
        if (isGameWin) {
            levelManager.CompleteLevel();
        }
    }

    private void PositionCats()
    {
        for (int i = 0; i < cats.Count; i++)
        {
            Vector3 targetPosition = boxes[i].transform.position;
            targetPosition.y += 0.25f; 
            
            catTargetPositions[cats[i]] = targetPosition;
            lerpStartTimes[cats[i]] = Time.time; 
        }
    }

    void Update()
    {
        // Move all cats smoothly using Lerp
        foreach (var cat in cats)
        {
            if (catTargetPositions.ContainsKey(cat))
            {
                Vector3 startPosition = cat.transform.position;
                Vector3 targetPosition = catTargetPositions[cat];

                float lerpTime = (Time.time - lerpStartTimes[cat]) / lerpDuration; 
                lerpTime = Mathf.Clamp01(lerpTime);  

                cat.transform.position = Vector3.Lerp(startPosition, targetPosition, lerpTime); 

                if (lerpTime >= 1f)
                {
                    catTargetPositions.Remove(cat);
                    lerpStartTimes.Remove(cat);
                }
            }
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
            audioManager.PlayRandomMeow();
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

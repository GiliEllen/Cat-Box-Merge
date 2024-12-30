using System.Collections.Generic;
using UnityEngine;

public class EndlessModeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> catPrefabs; // Prefabs for the 7 types of cats (order: Black, White, Orange, Calico, Sphinx, Siamese, Purple, Pink).
    private List<List<GameObject>> catMatrix;

    public void Start()
    {
        int difficulty = 3; // Example difficulty value, change as needed.
        InitializeEndlessMode(difficulty);
    }

    private void InitializeEndlessMode(int difficulty)
    {

        List<GameObject> randomizedCats = GenerateRandomizedCatList(difficulty);

        // Create the matrix of lists, each containing 5 cats.
        catMatrix = new List<List<GameObject>>();

        while (randomizedCats.Count > 0)
        {
            List<GameObject> catRow = new List<GameObject>();

            for (int i = 0; i < 5 && randomizedCats.Count > 0; i++)
            {
                catRow.Add(randomizedCats[0]);
                randomizedCats.RemoveAt(0);
            }

            catMatrix.Add(catRow);
        }

        // Instantiate the cats from the matrix.
        SpawnCatsFromMatrix();
    }

    private List<GameObject> GenerateRandomizedCatList(int difficulty)
    {
        List<GameObject> catList = new List<GameObject>();

        // Randomly pick `difficulty` types of cats and create triplets.
        List<int> chosenIndices = new List<int>();

        for (int i = 0; i < difficulty; i++)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, catPrefabs.Count);
            } while (chosenIndices.Contains(randomIndex));

            chosenIndices.Add(randomIndex);

            // Add triplets of the chosen cat to the list.
            for (int j = 0; j < 3; j++)
            {
                catList.Add(catPrefabs[randomIndex]);
            }
        }

        // Shuffle the list.
        for (int i = 0; i < catList.Count; i++)
        {
            int randomIndex = Random.Range(0, catList.Count);
            (catList[i], catList[randomIndex]) = (catList[randomIndex], catList[i]);
        }

        return catList;
    }

    private void SpawnCatsFromMatrix()
    {
        for (int i = 0; i < catMatrix.Count; i++)
        {
            for (int j = 0; j < catMatrix[i].Count; j++)
            {
                // Instantiate the cat game object.
                GameObject cat = Instantiate(catMatrix[i][j]);

                // Optionally set position or parent.
                cat.transform.position = new Vector3(i * 2, j * 2, 0); // Example grid-based positioning.
            }
        }
    }
}

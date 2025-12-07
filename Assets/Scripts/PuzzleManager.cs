using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Prefabs for this subject")]
    public GameObject[] puzzlePrefabs; 

    [Header("Spawn Point")]
    public Transform puzzleSpawnPoint; 

    void Start()
    {
        SpawnRandomPuzzle();
    }

    void SpawnRandomPuzzle()
    {
        if (puzzlePrefabs.Length == 0)
        {
            Debug.LogWarning("No puzzle prefabs assigned!");
            return;
        }

        int index = Random.Range(0, puzzlePrefabs.Length);
        GameObject puzzle = Instantiate(puzzlePrefabs[index], puzzleSpawnPoint.position, Quaternion.identity);
        puzzle.transform.parent = puzzleSpawnPoint; 
    }
}


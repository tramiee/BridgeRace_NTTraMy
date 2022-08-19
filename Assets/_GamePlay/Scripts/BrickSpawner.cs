using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameObject[] brickPrefabs; 
    public int numx = 8;
    public int numz = 8;
    public float spacex = 0.25f;
    public float spacez = 0.25f;

    public List<Transform> startPoses;

    private int[] numOfBrick = { 25, 25, 25, 25 };
    private int[,] sampleMap = new int[10, 10];
    private List<int[,]> mapStages = new List<int[, ]>();
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < startPoses.Count; i++)
        {
            mapStages.Add(InitMapStage(sampleMap));
        }

        for (int i = 0; i < brickPrefabs.Length; i++)
        {
            SpawnerBrick(i, 0);
        }
    }

    private int[,] InitMapStage(int[,] mapStage)
    {
        for (int i = 0; i < numx; i++)
        {
            for (int j = 0; j < numz; j++)
            {
                while (true)
                {
                    int randomBricks = Random.Range(0, brickPrefabs.Length);
                    if (numOfBrick[randomBricks] > 0)
                    {
                        mapStage[i, j] = randomBricks;
                        numOfBrick[randomBricks]--;
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < numOfBrick.Length; i++)
        {
            numOfBrick[i] = 25;
        }
        return mapStage;

    }

    // 0 is blue, 1 is green, 2 is red, 3 is yellow 
    public void SpawnerBrick(int brickType, int stageIndex)
    {
        for (int i = 0; i < sampleMap.GetLength(0); i++)
        {
            for (int j = 0; j < sampleMap.GetLength(1); j++)
            {
                if (mapStages[stageIndex][i, j] == brickType)
                {
                    SimplePool.Spawn(brickPrefabs[brickType], startPoses[stageIndex].position + new Vector3(i * spacex, 0.015f, j * spacez), startPoses[stageIndex].rotation);
                }
            }
        }
    }
}

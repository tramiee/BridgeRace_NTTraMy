using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public GameObject[] brick; 
    public int numx = 8;
    public int numz = 8;
    public float spacex = 0.25f;
    public float spacez = 0.25f;
    public Transform startPos;
    public Transform startPos2;
    public GameObject brickPrefab;

    private int[] numOfBrick = { 25, 25, 25, 25 };
    private int[,] mapStage2;
    // Start is called before the first frame update
    void Start()
    {
        mapStage2 = new int[10, 10];
        Invoke("SpawnerBrick1",0f);
        InitMapStage2();
    }

    public void SpawnerBrick1()
    {
        for (int i = 0; i < numx; i++)
        {
            for (int j = 0; j < numz; j++)
            {
                while (true)
                {
                    int randomBricks = Random.Range(0, brick.Length);
                    if (numOfBrick[randomBricks] > 0)
                    {
                        SimplePool.Spawn(brick[randomBricks], startPos.position + new Vector3(i * spacex, 0.015f, j * spacez), startPos.rotation);
                        numOfBrick[randomBricks]--;
                        break;
                    }
                }
            }
        }
    }
    
    private void InitMapStage2()
    {
        int[] numOfBrickStage2 = { 25, 25, 25, 25 };

        for (int i = 0; i < numx; i++)
        {
            for (int j = 0; j < numz; j++)
            {
                while (true)
                {
                    int randomBricks = Random.Range(0, brick.Length);
                    if (numOfBrickStage2[randomBricks] > 0)
                    {
                        mapStage2[i, j] = randomBricks;
                        numOfBrickStage2[randomBricks]--;
                        break;
                    }
                }
            }
        }
    }
    
    // 0 is blue, 1 is green, 2 is red, 3 is yellow 
    public void SpawnerBrick2(int brickType)
    {
        for (int i = 0; i < mapStage2.GetLength(0); i++)
        {
            for (int j = 0; j < mapStage2.GetLength(1); j++)
            {
                if (mapStage2[i, j] == brickType)
                {
                    SimplePool.Spawn(brick[brickType], startPos2.position + new Vector3(i * spacex, 0.015f, j * spacez), startPos2.rotation);
                }
            }
        }
    }
}

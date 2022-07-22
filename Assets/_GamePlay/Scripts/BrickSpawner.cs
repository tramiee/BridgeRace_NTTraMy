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
    public GameObject BickBlue;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnerBrick",0f);
        BickBlue = brick[0];
    }

    public void SpawnerBrick()
    {
        for (int i = 0; i < numx; i++)
        {
            for (int j = 0; j < numz; j++)
            {
                int randomBricks = Random.Range(0, brick.Length);
                GameObject newBrick = Instantiate(brick[randomBricks], startPos);
                newBrick.transform.position = startPos.position + new Vector3(i * spacex, 0.015f, j * spacez);
            }
        }
    }
}

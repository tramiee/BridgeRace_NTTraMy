using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public GameObject[] brick;
    public Transform brickHolder;

    public List<Transform> stackHolder;
    public List<GameObject> stack;

    public Transform stepHolder;
    public GameObject stepPrefab;
    public int numOfBricksPerBridge;
    public int numOfBridge;

    private void Awake()
    {
        for (int i = 0; i < brick.Length; i++)
        {
            SimplePool.Preload(brick[i], 25, brickHolder);
        }
        
        for (int i = 0; i < stack.Count; i++)
        {
            SimplePool.Preload(stack[i], 50, stackHolder[i]);
        }

        SimplePool.Preload(stepPrefab, numOfBricksPerBridge * numOfBridge, stepHolder);
    }
}

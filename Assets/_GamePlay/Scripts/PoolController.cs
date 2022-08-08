using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    public GameObject[] brick;
    public Transform brickHolder;

    public List<Transform> stackHolder;
    public List<GameObject> stack;

    //public List<GameObject> step;
    //public List<Transform> stepHolder;
    //public Dictionary<GameObject, Renderer> objRenderer = new Dictionary<GameObject, Renderer>();
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

        /*for(int i = 0; i < step.Count; i++)
        {
            SimplePool.Preload(step[i], 22, stepHolder[i]);
        }*/
    }

    private void Start()
    {
        //InitStep();
    }

    /*public void InitStep()
    {
        GameObject newStep;
        Renderer newRenderer;
        for (int i = 0; i < stepHolder.Count; i++)
        {
            newStep = SimplePool.Spawn(step[i], stepHolder[i].position, stepHolder[i].rotation);
            newRenderer = newStep.GetComponent<Renderer>();
            objRenderer.Add(newStep, newRenderer);
        }
    }*/
}

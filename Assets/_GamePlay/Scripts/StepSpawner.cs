using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSpawner : MonoBehaviour
{
    public List<Transform> stepHolders;
    public GameObject stepPrefab;
    public int maxBridge = 22;
    public int bridgeIndex;
    // Start is called before the first frame update
    void Start()
    {
        while(bridgeIndex < maxBridge)
        {
            for(int i = 0; i < stepHolders.Count; i++)
            {
                SimplePool.Spawn(stepPrefab, stepHolders[i].position + new Vector3(0, 0.05f, 0.15f) * bridgeIndex, stepHolders[i].rotation);
            }
            bridgeIndex++;
        }
    }
}

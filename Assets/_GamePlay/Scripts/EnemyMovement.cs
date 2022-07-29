using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject stackHolder;
    public GameObject stackPrefab;
    public GameObject stepHolder;
    public GameObject stepHolder1;
    public GameObject stepHolder2;
    public GameObject bridgePrefab;
    public int maxBridge = 22;
    private int bridgeIndex = 0;
    private int numOfStack = 0;
    public enum BrickTag { Yellow, Red, Blue, Green}
    public BrickTag brickTag;

    public enum BridgeTag { BridgeYellow, BridgeRed, BridgeBlue, BridgeGreen }
    public BridgeTag bridgeTag;

    public Material bridgeMaterial;

    public GameObject brickPrefab;
    public Vector3 targetPos;
    public Animator enemyAnimator;

    public Transform stagePoint;
    public Transform stagePoint1;

    public BrickSpawner brickSpawner;

    private bool isWin = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPos += transform.position + new Vector3(0, 0, 0.3f);
        agent.SetDestination(targetPos);

        while (bridgeIndex < maxBridge)
        {
            GameObject newBridge = Instantiate(bridgePrefab) as GameObject;
            newBridge.transform.SetParent(stepHolder.transform);
            newBridge.transform.position = stepHolder.transform.position + new Vector3(0, 0.05f, 0.15f) * bridgeIndex;
            newBridge.gameObject.GetComponent<Renderer>().enabled = false;

            GameObject newBridge1 = Instantiate(bridgePrefab) as GameObject;
            newBridge1.transform.SetParent(stepHolder.transform);
            newBridge1.transform.position = stepHolder.transform.position + new Vector3(0, 0.05f, 0.15f) * bridgeIndex;
            newBridge1.gameObject.GetComponent<Renderer>().enabled = false;

            GameObject newBridge2 = Instantiate(bridgePrefab) as GameObject;
            newBridge2.transform.SetParent(stepHolder.transform);
            newBridge2.transform.position = stepHolder.transform.position + new Vector3(0, 0.05f, 0.15f) * bridgeIndex;
            newBridge2.gameObject.GetComponent<Renderer>().enabled = false;

            bridgeIndex++;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin) return;
        enemyAnimator.SetBool("isRun", true);
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            targetPos = SimplePool.GetPositionBrick(brickPrefab);
            agent.SetDestination(targetPos);
        }

        if (Vector3.Distance(transform.position, stagePoint.position) < 0.1f)
        {
            SimplePool.Collect(brickPrefab);
            brickSpawner.SpawnerBrick2(2);
            stagePoint = stagePoint1;
            targetPos = SimplePool.GetPositionBrick(brickPrefab);
            agent.SetDestination(targetPos);
        }



        int layermask = LayerMask.GetMask("Bridge");
        if (Physics.Raycast(transform.position + Vector3.forward * 0.1f + Vector3.up * 10f, Vector3.down, out RaycastHit hit, Mathf.Infinity, layermask))
        {
            if (hit.collider.CompareTag(bridgeTag.ToString()))
            {
                return;
            }
            else
            {
                if (numOfStack == 0)
                {
                    agent.SetDestination(targetPos);
                }
                else
                {
                    hit.collider.gameObject.GetComponent<Renderer>().enabled = true;
                    hit.collider.gameObject.GetComponent<Renderer>().material = bridgeMaterial;
                    hit.collider.tag = bridgeTag.ToString();
                    RemoveStack();
                    SimplePool.Respawn(brickPrefab);
                }
            }
        }
    }

    public void AddStack()
    {
        SimplePool.Spawn(stackPrefab, stackHolder.transform.position + stackHolder.transform.up * numOfStack * 0.05f, stackHolder.transform.rotation);
        numOfStack += 1;
        if (numOfStack >= 5)
        {
            agent.SetDestination(stagePoint.position);
        }
    }

    public void RemoveStack()
    {
        SimplePool.DespawnNewest(stackPrefab);
        numOfStack -= 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(brickTag.ToString()))
        {
            AddStack();
            SimplePool.Despawn(other.gameObject);
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            isWin = true;
            enemyAnimator.Play("Win");
            foreach(Transform stack in stackHolder.transform)
            {
                Destroy(stack.gameObject);
            }
        }
    }
}

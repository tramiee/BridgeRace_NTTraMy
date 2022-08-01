using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public FixedJoystick joystick;
    public float speed;
    public Animator playerAnimator;
    private bool isMoving = false;

    public Transform stackHolder;
    public GameObject stackPrefab;
    private int numOfStacks = 0;

    public GameObject stepHolder;
    public GameObject stepHolder1;
    public GameObject stepHolder2;
    public GameObject bridgePrefab;
    public Material bridgeMaterial;
    private int bridgeIndex = 0;

    public Constant.BrickTags brickTags;

    public Constant.BridgeTag bridgeTags;

    public int maxBridge = 22;
    public GameObject brickPrefab;
    public BrickSpawner brickSpawner;
    public Transform stagePoint;
    public Transform stagePoint1;

    private void Start()
    {
        while (bridgeIndex < maxBridge)
        {
            //SimplePool.Spawn(brickPrefab, stepHolder.transform.position + new Vector3(0, 0.05f, 0.15f) * bridgeIndex, Quaternion.identity);
            GameObject newBridge = Instantiate(bridgePrefab) as GameObject;
            newBridge.transform.SetParent(stepHolder.transform);
            newBridge.transform.position = stepHolder.transform.position + new Vector3(0, 0.05f, 0.15f) * bridgeIndex;
            newBridge.gameObject.GetComponent<Renderer>().enabled = false;

            GameObject newBridge1 = Instantiate(bridgePrefab) as GameObject;
            newBridge1.transform.SetParent(stepHolder1.transform);
            newBridge1.transform.position = stepHolder1.transform.position + new Vector3(0, 0.05f, 0.15f) * bridgeIndex;
            newBridge1.gameObject.GetComponent<Renderer>().enabled = false;

            GameObject newBridge2 = Instantiate(bridgePrefab) as GameObject;
            newBridge2.transform.SetParent(stepHolder2.transform);
            newBridge2.transform.position = stepHolder2.transform.position + new Vector3(0, 0.05f, 0.15f) * bridgeIndex;
            newBridge2.gameObject.GetComponent<Renderer>().enabled = false;

            bridgeIndex++;
        }
    }

    private void Update()
    {
        int layermask = LayerMask.GetMask(Constant.LAYER_BRIDGE);
        if (Physics.Raycast(transform.position + Vector3.forward * 0.1f + Vector3.up * 10f , Vector3.down, out RaycastHit hit, Mathf.Infinity, layermask))
        {
            if (hit.collider.CompareTag(bridgeTags.ToString()))
            {
                isMoving = true;
            }
            else
            {
                if (numOfStacks == 0)
                {
                    isMoving = false;
                }
                else
                {
                    isMoving = true;
                    hit.collider.gameObject.GetComponent<Renderer>().enabled = true;
                    hit.collider.gameObject.GetComponent<Renderer>().material = bridgeMaterial;
                    hit.collider.tag = bridgeTags.ToString();
                    RemoveStack();
                    SimplePool.Respawn(brickPrefab);
                }
            }
        } 
        else
        {
            isMoving = true;
        }
        
        if(Vector3.Distance(transform.position, stagePoint.position) < 0.2f || Vector3.Distance(transform.position, stagePoint1.position) < 0.2f)
        {
            SimplePool.Collect(brickPrefab);
            brickSpawner.SpawnerBrick2(3);
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector3 movement = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);
        if (!isMoving && movement.z > 0)
        {
            movement = Vector3.zero;
        }
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            rb.velocity = movement;
            rb.rotation = Quaternion.LookRotation(rb.velocity);
            playerAnimator.SetBool(Constant.ANIM_ISRUN, true);
        }
        else
        {
            playerAnimator.SetBool(Constant.ANIM_ISRUN, false);
        }
    }

    public void AddStack()
    {
        SimplePool.Spawn(stackPrefab, stackHolder.position + stackHolder.up * numOfStacks * 0.05f, stackHolder.rotation);
        numOfStacks += 1;
    }

    public void RemoveStack()
    {
        SimplePool.DespawnNewest(stackPrefab);
        numOfStacks -= 1;
    }

    public void Fall()
    {
        playerAnimator.SetBool(Constant.ANIM_ISFALL, true);
    }

    public void Win()
    {
        playerAnimator.Play(Constant.ANIM_WIN);
        SimplePool.Collect(stackPrefab);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(brickTags.ToString()))
        {
            SimplePool.Despawn(other.gameObject);
            AddStack();
        }

        if (other.gameObject.CompareTag(Constant.TAG_FINISH))
        {
            Win();
        }
    }

}

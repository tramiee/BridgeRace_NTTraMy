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
    public GameObject stackHolder;
    public GameObject stepHolder;
    public GameObject stepHolder1;
    public GameObject stepHolder2;
    public GameObject stackPrefab;
    public GameObject bridgePrefab;
    private int numOfStacks = 0;

    public Material bridgeMaterial;
    public enum BrickTags { Yellow, Red, Blue, Green};
    public BrickTags bricktag; 
    public int maxBridge = 22;
    private int bridgeIndex = 0;

    private bool isMoving = false;

    public GameObject brickPrefab;

    public Transform stagePoint;
    public Transform stagePoint1;

    public BrickSpawner brickSpawner;
    private void Start()
    {
        while (bridgeIndex < maxBridge)
        {
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
        int layermask = LayerMask.GetMask("Bridge");
        if (Physics.Raycast(transform.position + Vector3.forward * 0.1f + Vector3.up * 10f , Vector3.down, out RaycastHit hit, Mathf.Infinity, layermask))
        {
            if (hit.collider.CompareTag("BridgeYellow"))
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
                    hit.collider.tag = "BridgeYellow";
                    RemoveStack();
                    SimplePool.Respawn(brickPrefab);
                }
            }
        } 
        else
        {
            isMoving = true;
        }
        
        if(Vector3.Distance(transform.position, stagePoint.position) < 0.1f || Vector3.Distance(transform.position, stagePoint1.position) < 0.15f)
        {
            SimplePool.Collect(brickPrefab);
            brickSpawner.SpawnerBrick2(3);
        }
    }
    private void FixedUpdate()
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
            playerAnimator.SetBool("isRun", true);
        }
        else
        {
            playerAnimator.SetBool("isRun", false);
        }
    }

    public void AddStack()
    {
        GameObject newStack = Instantiate(stackPrefab) as GameObject;
        newStack.transform.SetParent(stackHolder.transform);
        newStack.transform.position = stackHolder.transform.position + stackHolder.transform.up * numOfStacks * 0.05f;
        newStack.transform.rotation = stackHolder.transform.rotation;
        numOfStacks += 1;
    }

    public void RemoveStack()
    {
        Destroy(stackHolder.transform.GetChild(stackHolder.transform.childCount - 1).gameObject);
        numOfStacks -= 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(bricktag.ToString()))
        {
            SimplePool.Despawn(other.gameObject);
            AddStack();
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            playerAnimator.Play("Win");
        }
    }

}

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
    private bool isStop = false;
    public Vector3 movement;

    public Transform stackHolder;
    public GameObject stackPrefab;
    private int numOfStacks = 0;

    public Material bridgeMaterial;

    public Constant.BrickTags brickTags;
    public Constant.BridgeTag bridgeTags;
    public Constant.BrickType brickType;

    public GameObject brickPrefab;
    public BrickSpawner brickSpawner;

    public List<Transform> stage1Point;
    public List<Transform> stage2Point;

    public PlayerMovement thisPlayer;
    public List<EnemyMovement> enemies = new List<EnemyMovement>();

    public Transform posFinish;
    public bool isWin = false;

    
    private void Start()
    {
    }
  
    private void Update()
    {
        BuildBridge();

        SpawnBrick();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void SpawnBrick()
    {
        for(int i = 0; i < stage1Point.Count; i++)
        {
            if(Vector3.Distance(transform.position, stage1Point[i].position) < 0.2f)
            {
                SimplePool.Collect(brickPrefab);
                brickSpawner.SpawnerBrick((int)brickType, 1);
            }
        }

        for (int i = 0; i < stage2Point.Count; i++)
        {
            if (Vector3.Distance(transform.position, stage2Point[i].position) < 0.2f)
            {
                SimplePool.Collect(brickPrefab);
                brickSpawner.SpawnerBrick((int)brickType, 2);
            }
        }

    }

    public void BuildBridge()
    {
        int layermask = LayerMask.GetMask(Constant.LAYER_BRIDGE);
        if (Physics.Raycast(transform.position + Vector3.forward * 0.1f + Vector3.up * 10f, Vector3.down, out RaycastHit hit, Mathf.Infinity, layermask))
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
    }

    public void Move()
    {
        //movement = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);
        Vector2 dir = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        movement = new Vector3(dir.x * speed * Time.fixedDeltaTime, rb.velocity.y, dir.y * speed * Time.fixedDeltaTime);
        if (!isMoving && movement.z > 0)
        {
            movement = Vector3.zero;
        }
        if (isWin)
        {
            movement = Vector3.zero;
        }
        if (isStop)
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

    public int GetNumOfStack()
    {
        return numOfStacks;
    }

    public void ColliderEnemy()
    {
        foreach(EnemyMovement enemy in enemies)
        {
            if (thisPlayer.GetNumOfStack() < enemy.GetNumOfStacks())
            {
                Fall();
                return;
            }
        }
    }

    public void Fall()
    {
        StartCoroutine(NotFall());
    }

    IEnumerator NotFall()
    {
        playerAnimator.SetBool(Constant.ANIM_ISFALL, true);
        while(numOfStacks > 0)
        {
            
            SimplePool.DespawnNewest(stackPrefab);
            numOfStacks--;
        }
        isStop = true;
        yield return new WaitForSeconds(4f);
        playerAnimator.SetBool(Constant.ANIM_ISFALL, false);
        isStop = false;
    }

    public void Win()
    {
        transform.position = posFinish.position;
        isWin = true;
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

    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.CompareTag(Constant.TAG_ENEMY))
        {
            ColliderEnemy();
        }
    }
}

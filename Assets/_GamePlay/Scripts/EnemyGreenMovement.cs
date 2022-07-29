using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreenMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector3.right * speed * Time.deltaTime;
            rb.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector3.left * speed * Time.deltaTime;
            rb.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector3.forward * speed * Time.deltaTime;
            rb.rotation = Quaternion.LookRotation(rb.velocity);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.rotation = Quaternion.LookRotation(rb.velocity);
            direction = Vector3.back * speed * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction;
    }
}

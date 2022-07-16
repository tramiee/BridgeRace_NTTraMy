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
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
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

    }

    public void RemoveStack()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}

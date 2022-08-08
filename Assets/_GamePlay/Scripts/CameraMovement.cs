using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float speedCamera = 10f;

    private void LateUpdate()
    {
        CameraFollowPlayer();
    }

    public void CameraFollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, speedCamera * Time.deltaTime);
    }
}

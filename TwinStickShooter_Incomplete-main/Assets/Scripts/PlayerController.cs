using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float speedRotation;
    public Transform target;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 mov = new Vector3(horizontal, 0, vertical) * speed;
        Vector3 vel = transform.TransformDirection(mov);

        rb.velocity = new Vector3(mov.x, rb.velocity.y, mov.z);
    }
}
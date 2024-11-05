using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    public float speedRotation;
    public float stoppingDistance;
    private float current_anim_speed;

    [SerializeField] private float speed;

    private Animator anim;
    private Rigidbody rb;

    [SerializeField] private Transform player;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        PlayerRotation.instance.AddEnemy(this);
    }
    private void Update()
    {
        Vector3 dir = player.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir, transform.up);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rot, speedRotation * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, player.position);
        Debug.Log(distance);
        if (distance > stoppingDistance)
        {
            if (current_anim_speed < speed)
                current_anim_speed += Time.deltaTime * stoppingDistance;
        }
        else
            if (current_anim_speed > 0)
                current_anim_speed -= Time.deltaTime * stoppingDistance;

        anim.SetFloat("Velocity", current_anim_speed);
        rb.velocity = new Vector3(transform.forward.x * speed * Time.deltaTime, rb.velocity.y, transform.forward.z * speed * Time.deltaTime);
    }

    public void Kill()
    {
        rb.isKinematic = true;
        rb.useGravity = false;

        anim.enabled = false;

        Rigidbody[] child_rb;
        child_rb = gameObject.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in child_rb)
        {
            rb.isKinematic = false;
        }
    }
}
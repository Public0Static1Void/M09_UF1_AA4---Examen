using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float speedRotation;
    public float stoppingDistance;

    [SerializeField] private Transform player;
    private void Start()
    {

    }
    private void Update()
    {
        Vector3 dir = player.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir, transform.up);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rot, speedRotation * Time.deltaTime);
    }

    public void Kill()
    {

    }
}
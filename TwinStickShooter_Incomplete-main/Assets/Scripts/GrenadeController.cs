using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeController : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask mask;
    public float launchForce;
    public float timer;
    public float radius;
    public float explosionForce;
    public GameObject particles;

    public Transform player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce((player.forward + player.up) * launchForce, ForceMode.Impulse);
        StartCoroutine(GrenadeCooldown());
    }
    
    IEnumerator GrenadeCooldown()
    {
        yield return new WaitForSeconds(timer);
        Instantiate(particles, transform.position, transform.rotation);

        Collider[] hit_colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider coll in hit_colliders)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                coll.gameObject.GetComponent<EnemyController>().Kill();
            }

            Rigidbody rb = coll.gameObject.GetComponent<Rigidbody>();
            rb?.AddExplosionForce(explosionForce, transform.position, radius);
        }

        Destroy(this.gameObject);
    }
}
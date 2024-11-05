using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{
    public LineRenderer line;
    public float lineFadeSpeed;
    public LayerMask mask;
    public LayerMask enemy_mask;
    public float knockbackForce = 10;
    public string enemy_tag;

    [SerializeField] private float range;

    void Update()
    {
        line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, line.startColor.a - Time.deltaTime * lineFadeSpeed);
        line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, line.endColor.a - Time.deltaTime * lineFadeSpeed);

        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if (hit.transform.gameObject.tag == enemy_tag)
                {
                    EnemyController ec = hit.transform.gameObject.GetComponent<EnemyController>();
                    ec?.Kill();
                    Debug.Log("Enemy killed");
                }
                else
                {
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    rb?.AddForceAtPosition(transform.forward * knockbackForce, hit.point);
                }

                line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, 1);
                line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, 1);

                line.SetPosition(0, transform.position);
                line.SetPosition(1, transform.position + transform.forward * hit.distance);
            }
            else
            {
                line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, 1);
                line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, 1);

                line.SetPosition(0, transform.position);
                line.SetPosition(1, transform.position + transform.forward * range);
            }
        }
    }
}

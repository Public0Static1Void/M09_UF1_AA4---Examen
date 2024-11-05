using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public static PlayerRotation instance { get; private set; }

    [SerializeField] private float rotation_speed;

    [SerializeField] private Transform mirilla;

    private List<EnemyController> enemyControllers;

    [SerializeField] private EnemyController current_target;

    private float enemies_per_round = 0;

    private int count = 0;

    public List<Transform> spawn_zones;

    public GameObject enemy;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        enemyControllers = new List<EnemyController>();
    }

    private void Update()
    {
        if (count == 0)
        {
            StartRound();
        }
        RefreshTargetLocation();
    }

    void StartRound()
    {
        for (int i = 0; i < enemies_per_round; i++)
        {
            int rand = Random.Range(0, spawn_zones.Count);
            GameObject ob = Instantiate(enemy, spawn_zones[i].transform.position, Quaternion.identity);
            AddEnemy(ob.GetComponent<EnemyController>());
        }

        enemies_per_round *= 1.2f;
    }
    private void RefreshTargetLocation()
    {
        if (count > 0)
        {
            float distance = Vector3.Distance(enemyControllers[0].transform.position, transform.position);
            for (int i = 0; i < count; i++)
            {
                if (Vector3.Distance(transform.position, enemyControllers[i].transform.position) <= distance)
                {
                    distance = Vector3.Distance(transform.position, enemyControllers[i].transform.position);
                    current_target = enemyControllers[i];
                }
            }

            mirilla.position = new Vector3(current_target.transform.position.x, 0, current_target.transform.position.z);

            Vector3 dir = current_target.transform.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(dir, transform.up);
            transform.localRotation = Quaternion.Slerp(transform.rotation, rot, rotation_speed * Time.deltaTime);
        }
        else
            mirilla.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    public void AddEnemy(EnemyController controller)
    {
        enemyControllers.Add(controller);
        count++;
    }

    public void RemoveEnemy(EnemyController controller)
    {
        enemyControllers.Remove(controller);
        if (count > 0)
            count--;
        RefreshTargetLocation();
    }
}
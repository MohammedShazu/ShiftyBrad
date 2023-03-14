using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    public EnemyBehave enemy;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    // Update is called once per frame
    private void Update()
    {
        if (enemy.isPlayerDetected)
        {
            Seek(target.transform.position);
        }
    }
}

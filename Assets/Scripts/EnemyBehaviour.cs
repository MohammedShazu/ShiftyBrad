using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    public bool GetIsPlayerDetected()
    {
        return isPlayerDetected;
    }
    public float detectionRadius = 10.0f;
    public Material detectedMaterial;
    public bool isPlayerDetected = false;
    private Material originalMaterial;
    private GameObject player;
    public float moveSpeed = 2.0f;
    public float captureRadius = 2.0f;
    public Transform[] waypoints;
    public float waitTime = 2.0f;
    private Renderer sensorRenderer;
    private int currentWaypoint = 0;
    private float waitTimer = 0.0f;
    private Animator anim;
    private Color defaultSensorColor;
    private NavMeshAgent navAgent;

    public enum GuardState
    {
        Patrol,
        Chase,
        Capture
    }

    public GuardState currentState;

    void Start()
    {
        sensorRenderer = GetComponent<Renderer>();
        // Get the sensor material
        var sensorMaterial = transform.Find("Sensor").GetComponent<Renderer>().material;

        // Set the default color
        sensorMaterial.color = defaultSensorColor;

        originalMaterial = GetComponentInChildren<MeshRenderer>().material;

        currentState = GuardState.Patrol;

        anim = GetComponent<Animator>();
        sensorRenderer = transform.Find("Sensor").GetComponent<Renderer>();

        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        switch (currentState)
        {
            case GuardState.Patrol:
                Patrol();
                break;
            case GuardState.Chase:
                if (isPlayerDetected)
                {
                    Chase();
                }
                else
                {
                    currentState = GuardState.Patrol;
                }
                break;
            case GuardState.Capture:
                Capture();
                break;
        }
    }


    void Patrol()
    {
        anim.SetBool("isWalking", true);
        if (waitTimer > waitTime)
        {
            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 1.1f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            }
            navAgent.SetDestination(waypoints[currentWaypoint].position);
            transform.LookAt(waypoints[currentWaypoint]);
        }
        else
        {
            waitTimer += Time.deltaTime;
        }

        // Detect Thief
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<PlayerMovement>().isHidden == false)
            {
                isPlayerDetected = true;
                currentState = GuardState.Chase;
                player = col.gameObject;
                sensorRenderer.material.color = Color.red;
                break;
            }
            if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<PlayerMovement>().isHidden == true)
            {
                isPlayerDetected = true;
                currentState = GuardState.Patrol;
                player = col.gameObject;
                sensorRenderer.material.color = Color.red;
                break;
            }
            else
            {
                if (col.gameObject.CompareTag("Player") && col.gameObject.GetComponent<PlayerMovement>().isHidden == false)
                {
                    currentState = GuardState.Capture;
                }
            }
        }

    }
    void Chase()
    {
        // If player is within capture radius, capture them
        if (Vector3.Distance(transform.position, player.transform.position) < captureRadius)
        {
            currentState = GuardState.Capture;
            return;
        }

        // Only chase if player is within detection radius
        if (Vector3.Distance(transform.position, player.transform.position) < detectionRadius)
        {
            // Move towards player
            navAgent.SetDestination(player.transform.position);

            // Check if player is hidden
            if (player.GetComponent<PlayerMovement>().isHidden)
            {
                // If player is hidden, go back to patrol state
                currentState = GuardState.Patrol;
                isPlayerDetected = false;
                sensorRenderer.material.color = defaultSensorColor;
            }
        }
        else
        {
            // If player is out of range, go back to patrol state
            currentState = GuardState.Patrol;
            isPlayerDetected = false;
            sensorRenderer.material.color = defaultSensorColor;
        }
    }

    void Capture()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isCapturing", true);

        navAgent.isStopped = true;

        // Capture the player
        if (currentState == GuardState.Capture) // check if currentState is Capture
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("EndGame");
        }
        //Destroy(player);
        currentState = GuardState.Patrol;
    }
}
using UnityEngine;
using System.Collections;

public class EnemyBehave : MonoBehaviour
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
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, moveSpeed * Time.deltaTime);
            transform.LookAt(waypoints[currentWaypoint]);
        }
        else
        {
            waitTimer += Time.deltaTime;
        }

        // Detect thief
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
        anim.SetBool("isWalking", true);
        if (Vector3.Distance(transform.position, player.transform.position) > captureRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player.transform);
        }
        else
        {
            currentState = GuardState.Capture;
        }
    }

    void Capture()
    {
        anim.SetBool("isWalking", false);
        anim.SetTrigger("capture");
        if (currentState == GuardState.Capture) // check if currentState is Capture
        {
            Debug.Log("Game Over");
        }
        //Destroy(player);
        currentState = GuardState.Patrol;
    }
}
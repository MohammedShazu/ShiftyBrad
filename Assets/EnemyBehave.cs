using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehave : MonoBehaviour
{

    public enum EnemyEnumState { guarding, chasing, captured } //declaring all states that a guard can do
    public EnemyEnumState myState = EnemyEnumState.guarding;

    //can't detect thief normally, so captured and detects thief is defaulted a false 
    public Transform Target;
    public bool detectsthief = false;
    public bool captured = false;
    public GameObject Player;
    public float speed = 1;
    //call detection script
    public Detection script;
    bool chasePlayer;
    private Detection TriggeredCheck; //referring to the Detection script in order for me to be able to access the boolean
    public GameObject boolTracker;
    public Detection volumeToMonitor; //added on for enemy chasing part

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        volumeToMonitor = GetComponent<Detection>();

        // myScript = GetComponent<Detection>();
        TriggeredCheck = boolTracker.GetComponent<Detection>();
    }
    private void Update()
    {
        switch (myState)
        {
            case EnemyEnumState.guarding:
                //guarding
                {
                    Debug.Log("Triggered bool read");//added this line a few more times to figure out where I was supposed to put this

                    if (detectsthief == true && captured == false)
                    {
                        if (TriggeredCheck.Triggered) 
                        {
                           
                            myState = EnemyEnumState.chasing;
                            // trigger bool needs to go here
                        }
                    }
                    //case 2 code goes here
                    myState = EnemyEnumState.chasing;
                    break;
                }
            case EnemyEnumState.chasing:
                {
                    //chasing
                    Debug.Log("Is chasing");
                    if (detectsthief == false && captured == false)
                    {
                        myState = EnemyEnumState.guarding;
                    }
                    if (detectsthief == true && captured == false)
                    {
                        //set eyes on the player
                        transform.LookAt(Player.transform.position);
                        //start chasing the player
                        transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    }
                    //checking to see if enemy is close enough to get captured
                    if (Vector3.Distance(Player.transform.position, transform.position) > 2.0f)
                    {
                        myState = EnemyEnumState.captured;
                    }
                     
                      //checking to see if player is in enemy's radar, if not then it'll go back to guarding
                      if (volumeToMonitor.Triggered == false)
                    {
                        myState = EnemyEnumState.guarding;
                    }
                    break;
                }
            case EnemyEnumState.captured:
                //captured
                {
                    Debug.Log("Game Over!!!!");
                    break;

                }
            //always need to add default at the end of every case
            default:
                {
                    break;
                }
        }

    }
}

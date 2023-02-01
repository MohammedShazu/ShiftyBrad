using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybehave : MonoBehaviour
{

    public enum EnemyEnumState { guarding, chasing, captured }
    public EnemyEnumState myState = EnemyEnumState.guarding;

    //can't detect thief normally
    public Transform Target;
    public bool detectsthief = false;
    public bool captured = false;
    public GameObject player;
    public float speed = 1;


    private void Update()
    {
        switch (myState)
        {
            case EnemyEnumState.guarding:
                //guarding
                {
                    if (detectsthief == true && captured == false)
                    {
                        myState = EnemyEnumState.chasing;
                    }
                    //case 1 code goes here
                    break;
                }
            case EnemyEnumState.chasing:
                {
                    //chasing
                    if (detectsthief == false && captured == false)
                    {
                        
                        Vector3 targetDirection = player.transform.position - transform.position;
                        var step = speed * Time.deltaTime; // calculate distance to move
                        transform.position = Vector3.MoveTowards(transform.position, targetDirection, step);
                        myState = EnemyEnumState.guarding;
                    }
                    //case 1 code goes here
                    if (detectsthief == true && captured == true)
                    {
                        myState = EnemyEnumState.captured;
                    }
                    break;
                }
            case EnemyEnumState.captured:
                //caprured
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
    // Start is called before the first frame update
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    bool thiefInRadar = false;
    public bool Triggered = false;
    
    //public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        //if the player falls into the radar zone then it will print out trigger making "Triggered" = true
        if (other.tag == "Player")
        {
            Debug.Log("TRIGGER");
            Triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //It will print out false if player leaves that radar zone making "Triggered" false
        if (other.tag == "Player")
        {
            Debug.Log("exit");
            Triggered = false;
        }
    }

    private void Start()
    {
        //if (thiefInRadar == true)
        //Debug.Log("Enemy has spotted thief");
    }
    //else
    //if (thiefInRadar == false)
    //Debug.Log("Enemy doesn't see thief");
    //break;
    void thiefDetected()
    {
        // If the thief is in Enemy's radar then enemy goes after thief
        if (thiefInRadar == true)
        {
            // go after thief
            Debug.Log("Enemy sees thief");
            transform.position = Vector3.zero;
        }
        // If it isn't in sight, enemy carries on guarding
        else if (thiefInRadar == false)
        {
            // carry on guarding
            Debug.Log("No thief in sight");
        }
        //need to add make an if statement involving other and turning it into player
    }
}

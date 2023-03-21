using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public int cashNeeded = 100; // The amount of cash needed to destroy the door

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.instance.GetScore() >= cashNeeded)
            {
                // The player has collected enough cash to destroy the door
                Destroy(gameObject);
            }
            else
            {
                // The player has not collected enough cash yet
                Debug.Log("You need more cash to destroy the door!");
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPickup : MonoBehaviour
{
    public int cashValue = 10000; // The amount of cash the player will receive

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add the cash value to the player's score or currency
        }
    }
}
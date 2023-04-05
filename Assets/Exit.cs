using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] CashPick CashPickInstance;
    public int requiredCashAmount = 170000;
    public GameObject exitDoor;
    private int collectedCashAmount = 0;
    public int Cash;

    private void Start()
    {
        CashPickInstance = FindObjectOfType<CashPick>();
        Debug.Log("Starting Cash Amount: " + CashPickInstance.Cash);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called");

        if (other.CompareTag("Cash"))
        {
            collectedCashAmount++;
            Destroy(other.gameObject);

            Debug.Log("Collected cash amount: " + collectedCashAmount);
            Debug.Log("Required cash amount: " + requiredCashAmount);

            if (collectedCashAmount == requiredCashAmount)
            {
                Debug.Log("Destroying exit door");
                Destroy(exitDoor);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] CashPick Cash;
    public int requiredCashAmount;
    public GameObject exitDoor;
    private int collectedCashAmount;
    private CashPick CashPickInstance;

    private void Start()
    {
        CashPick cashPick = Cash;
        CashPickInstance = FindObjectOfType<CashPick>();
        CashPickInstance.Cash = 10000;
        Debug.Log("CashPick.cash");
    }
    void OnTriggerEnter(Collider other)
    {
        requiredCashAmount = 160000;
        if (other.CompareTag("Cash"))
        {
            collectedCashAmount++;
            Destroy(other.gameObject);

            if (collectedCashAmount == requiredCashAmount)
            {
                Destroy(exitDoor);
            }
        }
    }
}

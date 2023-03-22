using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPick : MonoBehaviour
{
    //storing collecting cash
    public int Cash;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Cash")
        {
            Debug.Log("Cash Picked Up");
            Cash = Cash + 10000;
            //col.gameObject.SetActive(false);
            Destroy(col.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

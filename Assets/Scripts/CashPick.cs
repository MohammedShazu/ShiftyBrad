using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPick : MonoBehaviour
{
    //storing collecting cash
    public int Cash;
    public GameObject doorToDestroy;
    public GameObject doorToDestroy2;
    public GameObject doorToDestroy3;
    public GameObject doorToDestroy4;
    public AudioClip pickupSound;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Cash")
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Debug.Log("Cash Picked Up");
            Cash = Cash + 10000;
            //col.gameObject.SetActive(false);
            Destroy(col.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Cash >= 160000)
        {
            Destroy(doorToDestroy);
            Destroy(doorToDestroy2);
        }
    }
}

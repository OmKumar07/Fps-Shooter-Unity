using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public GameObject AssaultRifle;
    public GameObject Ar;
    public GameObject otherGun1;
    public GameObject otherGun2;
    public GameObject otherGun3;
    public GameObject otherGun4;
    public GameObject PickedUp;
    
    private void Start()
    {

        

    }
    public void FixedUpdate()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "First Person Player")
        {

            StartCoroutine(Pickup());


        }


    }

    IEnumerator Pickup()
    {
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(b());
        PickedUp.SetActive(true);
        AssaultRifle.SetActive(true);
        Ar.SetActive(true);
        if(otherGun1 != null)
        {
            otherGun1.SetActive(false);
        }
        if(otherGun2 != null)
        {
            otherGun2.SetActive(false);
        }
        if(otherGun3 != null)
        {
            otherGun3.SetActive(false);
        }
        if(otherGun4 != null)
        {
            otherGun4.SetActive(false);
        }
        

    }
    IEnumerator b()
    {
        yield return new WaitForSeconds(1f);
       
        Destroy(gameObject);
        PickedUp.SetActive(false);
        
    }
}

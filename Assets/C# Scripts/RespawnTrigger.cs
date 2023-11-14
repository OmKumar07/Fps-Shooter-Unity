using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public Respawn respawn;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TDMfriend")
        {
            other.transform.position = respawn.place.position;
            print("Player");
            
        }
    }
}

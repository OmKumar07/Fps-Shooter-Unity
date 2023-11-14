using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public PlayerDeath Player;
    public Transform[] place;
    private Transform ResPawnPlaces;
    
    void Start()
    {
        ResPawnPlaces = place[Random.Range(0, place.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.Health <= 100)
        {
            transform.position = ResPawnPlaces.transform.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loose : MonoBehaviour
{
    public PlayerDeath player;
    public GameObject LooseCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.dead == true)
        {
            LooseCanvas.SetActive(true);
        }
    }
}

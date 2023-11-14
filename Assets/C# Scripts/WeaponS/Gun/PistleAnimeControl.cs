using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistleAnimeControl : MonoBehaviour
{
    public Animator animator;
    //public PlayerMovementScript move;
    private bool moving = true;
  
    void Start()
    {
        animator.GetComponent<Animator>();
        //move.GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {

       
    }
}

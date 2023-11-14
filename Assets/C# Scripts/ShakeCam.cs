using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCam : MonoBehaviour
{
    public Dynamite D;
    public Animator CamAnim;
    public TurretDestroy turret;
    private void Start()
    {
        D.GetComponent<Dynamite>();
        CamAnim.GetComponent<Animator>();
    }
    private void Update()
    {
        if(D.blasted == false)
        {
            CamAnim.SetBool("Blast", false);
        }
    }
}

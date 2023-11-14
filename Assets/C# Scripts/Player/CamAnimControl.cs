using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CamAnimControl : MonoBehaviour
{
    public DanamiteBlast Blast;
    public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        if (Blast != null)
        {
            Blast.GetComponent<DanamiteBlast>();
        }
        else if(Blast == null)
        {
            return;
        }
        
        Anim.GetComponent<Animator>();
    }
    IEnumerator stopBlast()
    {
        
        if(Anim.GetBool("Blast") == true)
        {
            yield return new WaitForSeconds(1f);
            Anim.SetBool("Blast", false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Blast == null)
        {
            return;
        }
        StartCoroutine(stopBlast());
    }
}

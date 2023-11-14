using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShotDisplay : MonoBehaviour
{
    public Target Head;
    public Target target2;
    public GameObject HeadshotImg;
    

    void Start()
    {
        Head.GetComponent<Target>();
        target2.GetComponent<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Head.Headshot1 == true )
        {
            if (Head.dead == false && target2.dead == false)
            {
                HeadshotImg.SetActive(true);
                StartCoroutine(Headshot());
            }
            else
                return;
        }
            
    }
    IEnumerator Headshot()
    {
        yield return new WaitForSeconds(0.5f);
        HeadshotImg.SetActive(false);
    }
}

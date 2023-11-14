using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
    public GameObject Player;
    private int ChildCount;
    private int LiveChildCount;
    public Transform[] places;
    [HideInInspector]
    public Transform place;
    public bool player = false;
    

        
    void Start()
    {
        ChildCount = transform.childCount;
        LiveChildCount = ChildCount;
        
    }

    // Update is called once per frame
    void Update()
    {
        place = places[Random.Range(0, places.Length)];
        ResPawn();
        
        
    }
    public void ResPawn()
    {
        LiveChildCount = transform.childCount;
        if (LiveChildCount < ChildCount)
        {

            StartCoroutine(Loop());
        }
        else if (LiveChildCount >= ChildCount)
        {
            StopAllCoroutines();
        }
    }
   
    public IEnumerator Loop()
    {
        
        while(true)
        {
            if (LiveChildCount < ChildCount)
            {
                

                yield return new WaitForSeconds(player ? 10 : Random.Range(3, 8));
                
                GameObject Spawned =  Instantiate(Player, place.position, place.rotation, transform);
                Spawned.SetActive(true);
                yield return new WaitForSeconds(player ? 10 : Random.Range(3, 8));
               
            }
            else
                break;
            
        }
        

    }
}

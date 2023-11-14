using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TdmAfterPlayerDeath : MonoBehaviour
{
    public GameObject SpectatorCamera;
    public Vector3 Offset;
    public Transform TdmPlayer;
    public GameObject Canvas;
    public GameObject spectating;
    
    public bool SpectateOnly = false;
    

    
    

    private void FixedUpdate()
    {
        if (SpectateOnly == true)
        {
            TdmPlayer.gameObject.SetActive(false);
            Canvas.SetActive(false);
        }
    }
    void Update()
    {
        if(SpectateOnly == false)
        {
            if (TdmPlayer.childCount == 0)
            {
                FindClosestsEnemy();
                SpectatorCamera.SetActive(true);
                Canvas.SetActive(false);


                spectating.SetActive(true);
            }
            else
            {
                Canvas.SetActive(true);
                SpectatorCamera.SetActive(false);
               spectating.SetActive(false);
            }
        }
        else if (SpectateOnly == true)
        {
            
            FindClosestsEnemy();
        }






    }
    void FindClosestsEnemy()
    {
        float shortestdistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        GameObject[] Allenemies = GameObject.FindGameObjectsWithTag("TDMfriend");

        foreach (GameObject currentEnemy in Allenemies)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, currentEnemy.transform.position);
            if (distanceToPlayer < shortestdistance)
            {


                shortestdistance = distanceToPlayer;
                closestEnemy = currentEnemy;




            }
            SpectatorCamera.transform.position = closestEnemy.transform.position + Offset;
            SpectatorCamera.transform.LookAt(closestEnemy.transform.position + closestEnemy.transform.forward);
        }
        

    }
 
}

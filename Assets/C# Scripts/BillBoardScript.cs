using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardScript : MonoBehaviour
{
   
    

    // Update is called once per frame
    void LateUpdate()
    {
        GameObject[] Allenemies = GameObject.FindGameObjectsWithTag("MainCamera");
        foreach (GameObject currentEnemy in Allenemies)
        {
            if(currentEnemy == null)
            {
                return;
            }else
                transform.LookAt(transform.position + currentEnemy.transform.forward);
        }
        
    }
}

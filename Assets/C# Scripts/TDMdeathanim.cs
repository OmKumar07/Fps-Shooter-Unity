using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDMdeathanim : MonoBehaviour
{
    public GameObject Playerbody;
    public PlayerDeath playerdeath;
    public GameObject DeathCam;
    // Start is called before the first frame update
    
    private void FixedUpdate()
    {
        if(playerdeath.dead == true)
        {
            Playerbody.SetActive(true);
            DeathCam.SetActive(true);
        }
    }


}

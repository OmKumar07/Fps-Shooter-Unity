using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlow : MonoBehaviour
{
    PlayerMovementScript basicPlayerMovementScript;
    public LaserTurret LaserTurret;
    public float SprintSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        basicPlayerMovementScript = GetComponent<PlayerMovementScript>();
        LaserTurret.GetComponent<LaserTurret>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LaserTurret.isHitted == true);
        {
            basicPlayerMovementScript.Speed = SprintSpeed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsDropped : MonoBehaviour
{
    public Target Enemy;
    public GameObject PickupGun;
    // Start is called before the first frame update
    void Start()
    {
        Enemy.GetComponent<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemy.dead == true)
        {
            Instantiate(PickupGun, transform.position, transform.rotation);
        }
    }
}

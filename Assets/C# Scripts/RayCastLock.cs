using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastLock : MonoBehaviour
{
    public Transform target;
    public DanamiteBlast player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, -direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 5f);
    }
}

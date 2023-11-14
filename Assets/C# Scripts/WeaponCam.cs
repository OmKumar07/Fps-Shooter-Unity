using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCam : MonoBehaviour
{
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float swaySmoothValue = 4.0f;
    private Vector3 initialSwayPosition;
    public GameObject MainCamera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
            float movementX = -Input.GetAxis("Mouse X") * swayAmount;
            float movementY = -Input.GetAxis("Mouse Y") * swayAmount;
            //Clamp movement to min and max values
            movementX = Mathf.Clamp
                (movementX, -maxSwayAmount, maxSwayAmount);
            movementY = Mathf.Clamp
                (movementY, -maxSwayAmount, maxSwayAmount);
            //Lerp local pos
            Vector3 finalSwayPosition = new Vector3
                (movementX, movementY, 0);
            transform.localPosition = Vector3.Lerp
                (transform.localPosition, finalSwayPosition +
                initialSwayPosition, Time.deltaTime * swaySmoothValue);
        
    }

    
}

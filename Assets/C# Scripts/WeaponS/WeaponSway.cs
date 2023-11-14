using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Position Sway")]
    public float Amount;
    public float MaxAmount;
    public float SmoothAmount;

    [Space(10)]

    [Header("Rotational Sway")]
    public float rotationAmount = 4f;
    public float maxRotation = 5f;
    public float smoothRotation = 12f;
    [Space()]
    public bool rotationX = true;
    public bool rotationY = true;
    public bool rotationZ = true;

    public bool Sway = true;

    private Vector3 InitialPosition;
    private Quaternion InitialRotation;


    public void Start()
    {
        InitialPosition = transform.localPosition;
        InitialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Sway == true)
        {
            //use joystick by replacing This
            float movementX = -Input.GetAxis("Mouse X") * Amount;
            float movementY = -Input.GetAxis("Mouse Y") * Amount;


            movementX = Mathf.Clamp(movementX, -MaxAmount, MaxAmount);
            movementY = Mathf.Clamp(movementY, -MaxAmount, MaxAmount);


            Vector3 FinalPosition = new Vector3(movementX, movementY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, FinalPosition + InitialPosition, Time.deltaTime * SmoothAmount);
        }
        TiltSway();
        
    }
    private void TiltSway()
    {
        float movementX = -Input.GetAxis("Mouse X") * Amount;
        float movementY = -Input.GetAxis("Mouse Y") * Amount;


        movementX = Mathf.Clamp(movementX * rotationAmount, -maxRotation, maxRotation );
        movementY = Mathf.Clamp(movementY * rotationAmount, -maxRotation, maxRotation);


        Quaternion FinalRotation = Quaternion.Euler(new Vector3(rotationX ? -movementX : 0, rotationY ? movementY : 0, rotationZ ? movementY : 0));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, FinalRotation * InitialRotation, Time.deltaTime * smoothRotation);
    }
}

using UnityEngine;

public class Mouselook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    public Joystick joystick;

    public bool CursorLock;


    float xRotation = 0f;
    public PlayerDeath Dead;



    


    // Start is called before the first frame update
    public void Start()
    {if(CursorLock == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            
        }
    if(Dead == null)
        {
            return;
        }
    Dead.GetComponent<PlayerDeath>();
        
    }

    // Update is called once per frame
   public void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
       if (Dead.dead == false)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
       if(Dead == null)
        {
            return;
        }
       

    }
   
}

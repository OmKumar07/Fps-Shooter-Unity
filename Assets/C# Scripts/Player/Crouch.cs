using UnityEngine;

public class Crouch : MonoBehaviour
{
    public CharacterController  player;
    float originalHeight;
    public float crouchHeight;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<CapsuleCollider>();
        originalHeight = player.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            crouch ();
        else if (Input.GetKeyUp(KeyCode.C))
            Normal ();
    }

    public void crouch ()
    {
        player.height = crouchHeight;
    }

    public void Normal()
    {
        player.height = originalHeight;
    }

}

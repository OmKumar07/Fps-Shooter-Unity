using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{

    PlayerMovementScript basicPlayerMovementScript;
    public float SprintSpeed = 5f;
    [HideInInspector]
    public bool SprinTing = false;

    [Space(20)]
    [Header("For Pistle")]
    [Space(20)]

    public Animator anim;

    [Space(20)]
    [Header("For AR")]
    [Space(20)]

    public Animator ArAnim;

    [Space(20)]
    [Header("For SNIPER")]
    [Space(20)]

    public Animator SniperAnim;

    [Space(20)]
    [Header("For SHORTGUN")]
    [Space(20)]

    public Animator Shortgun;

    [Space(20)]
    [Header("For SMG")]
    [Space(20)]

    public Animator SMGanim;

    private float MovementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        basicPlayerMovementScript = GetComponent<PlayerMovementScript>();
        anim.GetComponent<Animator>();
        anim.GetComponent<Animator>();
        SniperAnim.GetComponent<Animator>();
        Shortgun.GetComponent<Animator>();
        SMGanim.GetComponent<Animator>();
        MovementSpeed = basicPlayerMovementScript.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && basicPlayerMovementScript.isGrounded == true)
        {
            SprinTing = true;
            basicPlayerMovementScript.Speed = SprintSpeed;
            anim.SetBool("Run", true);
            ArAnim.SetBool("Run", true);
            SniperAnim.SetBool("Run", true);
            Shortgun.SetBool("Run", true);
            SMGanim.SetBool("Run", true);
            
        }
           
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SprinTing = false;
            basicPlayerMovementScript.Speed = MovementSpeed;
            anim.SetBool("Run", false);
            ArAnim.SetBool("Run", false);
            SniperAnim.SetBool("Run", false);
            Shortgun.SetBool("Run", false);
            SMGanim.SetBool("Run", false);
            
        }
            
    }
}

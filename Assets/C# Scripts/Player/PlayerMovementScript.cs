using JetBrains.Annotations;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public CharacterController controller;

    public float Speed = 12f;
    public float Gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float JumpHight = 3f;

    public Joystick joystick;

    [HideInInspector]
    public bool ismoving = true;

    Vector3 velocity;
    [HideInInspector]
    public bool isGrounded;

    public PlayerDeath PlayerDead;

    //for Pistle
    [Space(20)]
    [Header("For Pistle")]

    public Animator animator;
    
    // For AR
    [Space(20)]
    [Header("For Ar")]
    public Animator ArAnim;

    [Space(20)]
    [Header("For Sniper")]
    public Animator sniperAnim;

    [Space(20)]
    [Header("For Shortgun")]
    public Animator ShortGAnim;

    [Space(20)]
    [Header("For SMG")]
    public Animator SMGAnim;

    [Space (20)]
    [Header("For Camera")]
    public Animator CamAnim;




    public Rigidbody rigidbody;
    public AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip runningSound;
    public Sprint sprint;
    private float OriginalPitch;
    public float WalkingPitch = 0.8f;
    public float RunningPitch = 1.3f;
    public float SmoothSound = 0.35f;






    public void Start()
    {
        animator.GetComponent<Animator>();
        ArAnim.GetComponent<Animator>();
        sniperAnim.GetComponent<Animator>();
        ShortGAnim.GetComponent<Animator>();
        SMGAnim.GetComponent<Animator>();
        CamAnim.GetComponent<Animator>();
        PlayerDead.GetComponent<PlayerDeath>();
        OriginalPitch = audioSource.pitch;
        
    }
    private void FixedUpdate()
    {

            Footstep();
        if(sprint.SprinTing == true)
        {
            audioSource.pitch = RunningPitch;
        }
        else if (sprint.SprinTing == false)
        {
            audioSource.pitch = WalkingPitch;        }
        
       
    }

    private void Footstep()
    {
        if (isGrounded && rigidbody.velocity.sqrMagnitude > 1.5f)
        {
            CamAnim.SetBool("Walk", true);

            audioSource.clip = sprint.SprinTing ? runningSound : walkingSound;
            if (!audioSource.isPlaying)
            {
                
                audioSource.Play();
                if(sprint.SprinTing == false && isGrounded)
                {
                    animator.SetBool("Moving", true);

                    ismoving = true;
                    SMGAnim.SetBool("Moving", true);

                    ArAnim.SetBool("Moving", true);

                    sniperAnim.SetBool("Moving", true);

                    ShortGAnim.SetBool("Moving", true);
                }
                else if (rigidbody.velocity.sqrMagnitude < 1.5f)
                {
                    return;
                }
                
                
            }
        }
        else
        {
            ismoving = false;
            if (audioSource.isPlaying)
            {
                StartCoroutine(FootSoundStop());
                animator.SetBool("Moving", false);
                
                SMGAnim.SetBool("Moving", false);
                
                ArAnim.SetBool("Moving", false);
                
                sniperAnim.SetBool("Moving", false);
                
                ShortGAnim.SetBool("Moving", false);
                
            }
        }
    }
    private IEnumerator FootSoundStop()
    {
        yield return new WaitForSeconds(SmoothSound);
        
        audioSource.Pause();
        CamAnim.SetBool("Walk", false);

        
    
    }
    public void Update()
    {

        if(PlayerDead.dead == false)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
                        
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(JumpHight * -2f * Gravity);
                CamAnim.SetBool("Jump", true);
                animator.SetBool("Moving", false);
                animator.SetBool("Run", false);
                SMGAnim.SetBool("Moving", false);
                SMGAnim.SetBool("Run", false);
                ArAnim.SetBool("Moving", false);
                ArAnim.SetBool("Run", false);
                sniperAnim.SetBool("Moving", false);
                sniperAnim.SetBool("Run", false);
                ShortGAnim.SetBool("Moving", false);
                ShortGAnim.SetBool("Run", false);
            }
            else
                CamAnim.SetBool("Jump", false);
            
                moving();
            
            
            
        }
        
       
    }
    
    public void moving()      
    {       
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");

        velocity.y += Gravity * Time.deltaTime;

        Vector3 move = transform.right * X + transform.forward * Z;

        controller.Move(move * Speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}

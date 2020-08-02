using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCController : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    SpriteRenderer sprite;

    [SerializeField]
    Canvas gameoverMenu = null;

    public enum MovementState { normalWalk, moonWalk, spinWalk, bounceWalk, crouchWalk}
    [SerializeField]
    public MovementState state = MovementState.normalWalk;

    [SerializeField]
    float maxSpeed = 0.25f;
    [SerializeField]
    float acceleration = 0.25f;
    [SerializeField]
    float deceleration = 1f;
    [SerializeField]
    float jumpSpeed = 0.5f;
    [SerializeField]
    float gravity = 1f;

    [SerializeField]
    Vector3 desiredVel = Vector3.zero;

    [SerializeField]
    bool dead = false;
    [SerializeField]
    float deathTime = 2f;
    float currentTime;

    bool jump = false;

    public void Kill()
    {
        if (dead != true)
        {
            currentTime = 0;
            animator.SetTrigger("Killed");
            dead = true;
        }
    }
    public void Jump()
    {
        animator.SetTrigger("Jumped");
        jump = true;
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>(); 
        animator = GetComponent<Animator>(); 
        sprite = GetComponentInChildren<SpriteRenderer>();
        gameoverMenu.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!controller.isGrounded){
            desiredVel.y -= gravity * Time.deltaTime;
        }
        else
        {
            desiredVel.y = -0.05f;
        }
        if (!dead)
        {
            if(transform.position.y < -5)
            {
                Kill();
            }
            else
            {
                UpdateInputs();
            }
        }
        else
        {
            currentTime += Time.deltaTime;
            if(currentTime > deathTime)
            {
                gameoverMenu.gameObject.SetActive(true);
            }
            if (controller.isGrounded)
            {
                desiredVel.x *= 0.95f;
            }
        }
        if (jump)
        {
            jump = false;
            desiredVel.y = jumpSpeed;
        }
        animator.SetBool("IsDead", dead);
        animator.SetBool("IsGrounded", controller.isGrounded);
        animator.SetBool("FacingRight", controller.velocity.x >= 0);
        animator.SetFloat("State", (float)state);
        animator.SetFloat("SpeedH", Mathf.Abs(desiredVel.x));
        controller.Move(desiredVel);
        animator.SetFloat("VelocityV", controller.velocity.y);
    }
    void UpdateInputs()
    {
        //float h = Input.GetAxis("Horizontal");
        float h = 0;
        if (Input.GetKey(KeyCode.A))
        {
            h = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            h = 1;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Kill();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            state = MovementState.normalWalk;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            state = MovementState.moonWalk;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            state = MovementState.spinWalk;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            state = MovementState.bounceWalk;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            state = MovementState.crouchWalk;
        }

        if(h==0) 
        {
            if(desiredVel.x > deceleration * Time.deltaTime)
            {
                desiredVel.x -= deceleration * Time.deltaTime;
            }
            else if(desiredVel.x < -deceleration * Time.deltaTime)
            {
                desiredVel.x += deceleration * Time.deltaTime;
            }
            else
            {
                desiredVel.x = 0;
            }
        }
        else if((h<0 && desiredVel.x > 0) || (h>0 && desiredVel.x < 0))
        {
            if(h < 0)
            {
                desiredVel.x -= deceleration * Time.deltaTime;
            }
            else if(h>0)
            {
                desiredVel.x += deceleration * Time.deltaTime;
            }
        }
        else if(h>0 && desiredVel.x < maxSpeed)
        {
            desiredVel.x += acceleration * Time.deltaTime;
        }
        else if(h<0 && desiredVel.x > -maxSpeed)
        {
            desiredVel.x -= acceleration * Time.deltaTime;
        }
        desiredVel.x = Mathf.Clamp(desiredVel.x, -maxSpeed, maxSpeed);
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }
}

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

    public void Kill()
    {
        dead = true;
        currentTime = 0;
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
        if (!dead)
        {
            UpdateInputs();
            if(transform.position.y < -5)
            {
                Kill();
            }
        }
        else
        {
            currentTime += Time.deltaTime;
            if(currentTime > deathTime)
            {
                gameoverMenu.gameObject.SetActive(true);
            }
            desiredVel.y = -0.05f;
            if (controller.isGrounded)
            {
                desiredVel.x *= 0.95f;
            }
        }
        if (!controller.isGrounded){
            desiredVel.y -= gravity * Time.deltaTime;
        }
        animator.SetBool("IsDead", dead);
        animator.SetBool("IsGrounded", controller.isGrounded);
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
        if (h != 0)
        {
            transform.right = Vector3.right * Mathf.Sign(h);
        }
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                desiredVel.y = jumpSpeed;
                animator.SetTrigger("Jumped");
            }
            else
            {
                desiredVel.y = -0.05f;
            }
        }
    }
}

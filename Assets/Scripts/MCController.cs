using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCController : MonoBehaviour
{	

	public AudioSource source;
	public float audioTime;  // for testing

    CharacterController controller;
    Animator animator;
    SpriteRenderer sprite;

    [SerializeField]
    Canvas gameoverMenu = null;
    [SerializeField]
    Canvas winMenu = null;

    public enum MovementState { normalWalk, moonWalk, lightningWalk}
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

    //Trigger flags
    bool jump = false;
    bool bounce = false;
    bool shoot = false;

    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    GameObject lightningPrefab;

    public void SetState(MovementState newState)
    {
        state = newState;
    }

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
    public void Bounce()
    {
        animator.SetTrigger("Jumped");
        bounce = true;
    }
    public void Shoot()
    {
        animator.SetTrigger("Shoot");
        shoot = true;
    }
    public void FireProjectile()
    {
        GameObject newProj = Instantiate(projectilePrefab);
        newProj.transform.position = transform.position;
    }
    public void FireStrike()
    {
        GameObject lightning = Instantiate(lightningPrefab);
        lightning.transform.position = transform.position + Vector3.right * Random.Range(-0f, 5f);
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>(); 
        animator = GetComponent<Animator>(); 
        sprite = GetComponentInChildren<SpriteRenderer>();
        gameoverMenu.gameObject.SetActive(false);
        winMenu.gameObject.SetActive(false);
    }
    private void Update()
    {
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
    }
    private void FixedUpdate()
    {
        if(transform.position.x > 250)
        {
            winMenu.gameObject.SetActive(true);
            return;
        }
        if (!controller.isGrounded){
            desiredVel.y -= gravity * Time.fixedDeltaTime;
        }
        else
        {
            desiredVel.y = -0.01f;
        }
        if(dead)
        {
            currentTime += Time.fixedDeltaTime;
            if(currentTime > deathTime)
            {
                gameoverMenu.gameObject.SetActive(true);
            }
            if (controller.isGrounded)
            {
                desiredVel.x *= 0.95f * Time.fixedDeltaTime;
            }
        }
        if (jump)
        {
            jump = false;
            desiredVel.y = jumpSpeed;
        }
        else if (bounce)
        {
            bounce = false;
            desiredVel.y = 0.25f * jumpSpeed;
        }
        else if (shoot)
        {
            shoot = false;
            desiredVel.x = 0;
        }
        animator.SetBool("IsDead", dead);
        animator.SetBool("IsGrounded", controller.isGrounded);
        animator.SetBool("FacingRight", controller.velocity.x >= 0);
        animator.SetInteger("State", (int)state);
        animator.SetFloat("SpeedH", Mathf.Abs(desiredVel.x));
        controller.Move(desiredVel);
        animator.SetFloat("VelocityV", controller.velocity.y);
    }
    void UpdateInputs()
    {
		audioTime = source.time;
		
		if (49.5 <= audioTime && audioTime <= 50.5 ) {
			Shoot();
		} else if (130 <= audioTime && audioTime <= 175) {
			state = MovementState.moonWalk;
		} else if (175 <= audioTime && audioTime <= 200) {
			state = MovementState.lightningWalk;
		} else {
			state = MovementState.normalWalk;
		}
		
        float h = 1;
        //if (Input.GetKey(KeyCode.A))
        //{
        //    h = -1;
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    h = 1;
        //}
        // if (Input.GetMouseButtonDown(0))
        // {
            // Shoot();
        // }
        // if (Input.GetMouseButtonDown(1))
        // {
            // Kill();
        // }
        // if (Input.GetKey(KeyCode.Alpha1))
        // {
            // state = MovementState.normalWalk;
        // }
        // if (Input.GetKey(KeyCode.Alpha2))
        // {
            // state = MovementState.moonWalk;
        // }
        // if (Input.GetKey(KeyCode.Alpha3))
        // {
            // state = MovementState.lightningWalk;
        // }

        if(h==0) 
        {
            if(desiredVel.x > deceleration * Time.fixedDeltaTime)
            {
                desiredVel.x -= deceleration * Time.fixedDeltaTime;
            }
            else if(desiredVel.x < -deceleration * Time.fixedDeltaTime)
            {
                desiredVel.x += deceleration * Time.fixedDeltaTime;
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
                desiredVel.x -= deceleration * Time.fixedDeltaTime;
            }
            else if(h>0)
            {
                desiredVel.x += deceleration * Time.fixedDeltaTime;
            }
        }
        else if(h>0 && desiredVel.x < maxSpeed)
        {
            desiredVel.x += acceleration * Time.fixedDeltaTime;
        }
        else if(h<0 && desiredVel.x > -maxSpeed)
        {
            desiredVel.x -= acceleration * Time.fixedDeltaTime;
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

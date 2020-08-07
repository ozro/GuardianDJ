using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseAttack : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    [SerializeField]
    MCController.MovementState safeState = MCController.MovementState.normalWalk;
    [SerializeField]
    public GameObject deathRattle;
    [SerializeField]
    Camera mainCam;
    [SerializeField]
    float activationDist;
    bool active = false;

    float actualSpeed;

    private void Start()
    {
        actualSpeed = speed + Random.Range(-1f, 1f) * speed * 0.75f;
        mainCam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if(!active && Mathf.Abs((transform.position-mainCam.transform.position).x) < activationDist)
        {
            active = true;
        }
        if (active)
        {
            transform.Translate(Vector3.left * actualSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        MCController MC = other.GetComponent<MCController>();
        if(MC != null)
        {
            if(MC.state != safeState)
            {
                MC.Kill();
            }
        }
    }

    private void OnDestroy()
    {
    }
}

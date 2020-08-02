using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseAttack : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    [SerializeField]
    MCController.MovementState safeState = MCController.MovementState.normalWalk;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
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
}

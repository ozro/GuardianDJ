using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField]
    CharacterController target = null;
    [SerializeField]
    float trackDelay;

    Vector3 trackVel;

    float separation = 0;

    private void Start()
    {
        separation = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = target.transform.position;
        pos.z = separation;
        transform.position = pos;
    }
}

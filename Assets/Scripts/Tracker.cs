using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField]
    CharacterController target = null;
    [SerializeField]
    float trackDelay = 0.5f;
    [SerializeField]
    float verticalOffset = 0;
    [SerializeField]
    float windowTop = 3;
    [SerializeField]
    float windowBot = 0;
    [SerializeField]
    float windowLeft = 1;
    [SerializeField]
    float windowRight = 0;

    Vector3 trackVel;

    float separation = 0;

    private void Start()
    {
		separation = transform.position.z;
		Rect camRect = Camera.main.rect;
		camRect.yMin = 0.4f; // 60% of viewport
		Camera.main.rect = camRect;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position+ new Vector3((windowRight-windowLeft)/2, (windowTop - windowBot)/2-verticalOffset, 0), new Vector3(windowLeft+windowRight, windowBot + windowTop, 10));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 offset = target.transform.position - (transform.position - Vector3.up*verticalOffset);
        if(offset.x > windowRight)
        {
            offset.x -= windowRight;
        }
        else if (offset.x < -windowLeft)
        {
            offset.x += windowLeft;
        }
        else
        {
            offset.x = 0;
        }
        if(offset.y > windowTop)
        {
            offset.y -= windowTop;
        }
        else if (offset.y < -windowBot)
        {
            offset.y += windowBot;
        }
        else
        {
            offset.y = 0;
        }
        Vector3 pos = transform.position + offset;
        pos.z = separation;
        transform.position = pos;
    }
}

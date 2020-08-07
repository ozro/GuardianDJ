using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recordCamera : MonoBehaviour
{
	public Camera recordCam;
    // Start is called before the first frame update
    void Start()
    {
		Rect camRect = recordCam.rect;
		camRect.yMax = 0.4f; // 60% of viewport
		recordCam.rect = camRect;
    }
}

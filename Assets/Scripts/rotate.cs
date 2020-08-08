using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{	
	// camera
	public Camera recordCam;

	// audio
	private AudioSource source;
	private float audioLength = 359.549f;
	
	// auto rotation
	public float rotZ;
	public float rotationSpeed;
	private bool mouseUp = true;
	
	// mouse drag rotation
	private float baseAngle = 0.0f;
	
	public float currentAngle = 0.0f;
	
	void Start() {
		source = GetComponent<AudioSource>();
		source.Play();
		
		rotationSpeed = audioLength / 360;
	}
	
	// void Update() {
	// continue to rotate as long as mouse isn't clicked on object
	// if (mouseUp) {
		// rotZ = Time.deltaTime * rotationSpeed;
		// transform.Rotate(0, 0, -rotZ, Space.Self);
		// Quaternion previous = transform.rotation;
		// transform.rotation = Quaternion.AngleAxis(rotZ, rotAxis) * previous;
	// }
	
	// currentAngle = 360 - transform.eulerAngles.z;
	
	// grab the new rotation (if mouse rotated it)
	// rotZ = currentAngle;
	// }

	void Update() {
		// continue to rotate as long as mouse isn't clicked on object
		if (mouseUp) {
			rotZ += -Time.deltaTime * rotationSpeed;
			transform.rotation = Quaternion.Euler(45, 0, rotZ);
		}
		// if we rotate past 360 we reset back towards 0 to keep angles within 360
		if (rotZ <= -360) {
			rotZ += 360;
		}
		// grab the new rotation (if mouse rotated it)
		rotZ = -360 + transform.eulerAngles.z;
		
		currentAngle = 360 - transform.eulerAngles.z;
	}

    void OnMouseDown() {
		mouseUp = false;
		Vector3 pos = recordCam.WorldToScreenPoint(transform.position);
		pos = Input.mousePosition - pos;
		baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
		baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) *Mathf.Rad2Deg;
		
		source.Pause();
    }

    void OnMouseDrag() {
        Vector3 pos = recordCam.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - baseAngle;
		transform.rotation = Quaternion.Euler(45, 0, angle);
    }
	
	void OnMouseUp() {
		mouseUp = true;
		
		// calc new audio position
		source.time = ((360 - transform.eulerAngles.z) / 360) * audioLength;
		
		source.Play();
	}
}

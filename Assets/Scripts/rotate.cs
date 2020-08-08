using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{	
	// camera
	public Camera recordCam;
	public GameObject cursor;

	// backing track
	private AudioSource backingTrack;
	private float backingTrackLength = 60.0f;
	
	// sound effects
	public AudioSource slowMotionEffect;
	public AudioSource scratchEffect;
	
	// auto rotation
	public float rotZ;
	public float rotationSpeed;
	private bool mouseUp = true;
	
	// mouse drag rotation
	private float baseAngle = 0.0f;
	public float currentAngle = 0.0f;
	
	// offeset to align with head
	public float offset;
	
	void Start() {
		backingTrack = GetComponent<AudioSource>();
		backingTrack.Play();
		
		rotationSpeed = 360 / backingTrackLength;

        Cursor.visible = false;
	}

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

        cursor.GetComponent<RectTransform>().position = Input.mousePosition;
	}

    void OnMouseDown() {
		backingTrack.Pause();
		slowMotionEffect.Play();
		scratchEffect.Play();
		
		Vector3 pos = recordCam.WorldToScreenPoint(transform.position);
		pos = Input.mousePosition - pos;
		baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
		baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
		
		mouseUp = false;
    }

    void OnMouseDrag() {
	
        Vector3 pos = recordCam.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - baseAngle;
		transform.rotation = Quaternion.Euler(45, 0, angle);
    }
	
	void OnMouseUp() {
		slowMotionEffect.Stop();
		scratchEffect.Stop();
		mouseUp = true;
		
		// calc new audio position
		backingTrack.time = ((360 - transform.eulerAngles.z) / 360) * backingTrackLength;
		
		backingTrack.Play();
	}
}

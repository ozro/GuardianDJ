using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCursor : MonoBehaviour
{
	public Vector2 cursorPos;
	
	void Start() {
		Cursor.visible = false;
	}
	
	void Update() {
		Cursor.visible = false;

		cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 5));
		transform.position = cursorPos;
		
	}
}
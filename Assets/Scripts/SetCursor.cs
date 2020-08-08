using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{	
	public Texture2D hand;
	
    void Start()
    {
        Vector2 cursorOffset = new Vector2(hand.width * 0.35f, hand.height * 0.35f);
		Cursor.SetCursor(hand, cursorOffset, CursorMode.ForceSoftware);
    }
}

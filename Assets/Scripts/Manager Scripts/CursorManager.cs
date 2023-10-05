using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    // THIS SCRIPT IS USED TO CHANGE THE GRAPHICS OF THE MOUSE CURSOR.

    public Texture2D aimCursor;
    private Vector2 cursorHotspot;

    void Start()
    {
        CursorAim();
    }

    void CursorAim()
    {
        // SETS THE SPRITE SO THAT THE SPRITE'S CENTER IS ALSO THE CURSOR'S CENTER
        cursorHotspot = new Vector2(aimCursor.width / 2, aimCursor.height / 2);
        Cursor.SetCursor(aimCursor, cursorHotspot, CursorMode.Auto);
    }
}

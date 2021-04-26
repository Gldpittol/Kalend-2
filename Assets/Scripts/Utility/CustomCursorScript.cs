using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorScript : MonoBehaviour
{
    [SerializeField] private float offsetX, offsetY;
    [SerializeField] private Texture2D _customCursor;

    private Vector2 offset;
    private void Awake()
    {
        offset = new Vector2(offsetX, offsetY);
        Cursor.SetCursor(_customCursor, offset, CursorMode.Auto);
    }
}

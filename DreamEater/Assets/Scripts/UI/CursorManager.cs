using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private bool showCursor;

    private void Start()
    {
        Cursor.visible = showCursor;
    }

    public void ShowCursor(bool _showCursor)
    {
        Cursor.visible = _showCursor;
    }
}

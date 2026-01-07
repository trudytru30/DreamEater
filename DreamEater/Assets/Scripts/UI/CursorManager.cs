using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private bool showCursor;
    public static CursorManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Cursor.visible = showCursor;
    }

    public void ShowCursor(bool _showCursor)
    {
        Cursor.visible = _showCursor;
    }
}

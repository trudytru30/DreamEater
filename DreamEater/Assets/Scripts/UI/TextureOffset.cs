using UnityEngine;

public class TextureOffset : MonoBehaviour
{
    public float speedX = 0.1f;
    public float speedY = 0.1f;

    private Renderer _renderer;
    private Vector2 _offset;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _offset = _renderer.material.mainTextureOffset;
    }

    private void Update()
    {
        _offset.x += speedX * Time.deltaTime;
        _offset.y += speedY * Time.deltaTime;

        _renderer.material.mainTextureOffset = _offset;
    }
}
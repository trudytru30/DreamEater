using UnityEngine;

public class TextureOffset : MonoBehaviour
{
    public float speedX = 0.1f;
    public float speedY = 0.1f;

    private Renderer _rend;
    private Vector2 _offset;

    void Start()
    {
        _rend = GetComponent<Renderer>();
        _offset = _rend.material.mainTextureOffset;
    }

    void Update()
    {
        _offset.x += speedX * Time.deltaTime;
        _offset.y += speedY * Time.deltaTime;

        _rend.material.mainTextureOffset = _offset;
    }
}

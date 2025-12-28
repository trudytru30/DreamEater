using UnityEngine;

public class TextureOffset : MonoBehaviour
{
    public float speedX = 0.1f;
    public float speedY = 0.1f;

    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        offset = rend.material.mainTextureOffset;
    }

    void Update()
    {
        offset.x += speedX * Time.deltaTime;
        offset.y += speedY * Time.deltaTime;

        rend.material.mainTextureOffset = offset;
    }
}

using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    public float rotationOffset = 0f; 

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
    }
}

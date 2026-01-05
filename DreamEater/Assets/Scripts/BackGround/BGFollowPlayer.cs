using UnityEngine;

public class BGFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float verticalOffset;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.position.y+verticalOffset, transform.position.z);
    }
}

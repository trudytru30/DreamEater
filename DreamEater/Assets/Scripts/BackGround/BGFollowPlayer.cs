using UnityEngine;

public class BGFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
    }
}

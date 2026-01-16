using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class DialogueSpriteActivo : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Image active;
    [SerializeField] private Image inactive;

    private void Awake()
    {
        active.enabled = false;
        inactive.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        active.enabled = true;
        inactive.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        active.enabled = false;
        inactive.enabled = false;
    }
}
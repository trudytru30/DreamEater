using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class DialogueSpriteActivo : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Image _activo;
    [SerializeField] private Image _desactivado;

    void Awake()
    {
        _activo.enabled = false;
        _desactivado.enabled = false;
    }
        

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _activo.enabled = true;
            _desactivado.enabled = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _activo.enabled = false;
            _desactivado.enabled = false;
        }
    }
}

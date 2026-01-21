/*
 Este es el codigo de cada platforma inidivual
 No esta gestionado el cambio de plataforma para eso ver ShockingPlatformController
*/

using UnityEngine;

public class ShockingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject particleSystem;
    private bool _canShock; //inidica si es la plataforma de shock activa
    private PlayerController2 _playerOnTop;


    //configuracion por defecto de variables
    private void Awake()
    {
        _canShock = false;
        particleSystem.SetActive(false);
    }

    //registra al player colisionando con esa plataforma
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _playerOnTop = collision.gameObject.GetComponent<PlayerController2>();
    }

    //desregistra al player colisionando con esa plataforma
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _playerOnTop = null;
    }

    //si el player esta registrado y la plataforma activa lo mata
    private void FixedUpdate()
    {
        //activa y desactiva particulas
        if (!_canShock)
        {
            particleSystem.SetActive(false);
        }
        else if (_canShock)
        {
            particleSystem.SetActive(true);
        }
        
        //intenta matar al player
        if (_canShock && _playerOnTop != null)
        {
            _playerOnTop.Die();
        }
    }
 
    //getters y setters
    public void setCanShock(bool canShock)
    {
        _canShock = canShock;
    }

    public bool getCanShock()
    {
        return _canShock;
    }

    public void setParticle(GameObject _particleSystem)
    {
        particleSystem = _particleSystem;
    }
    
    public GameObject getParticle()
    {
        return particleSystem;
    }
}

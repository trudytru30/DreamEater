/*
 Este es el codigo de cada platforma inidivual
 No esta gestionado el cambio de plataforma para eso ver ShockingPlatformController
*/

using UnityEngine;

public class ShockingPlatform : MonoBehaviour
{
    [SerializeField] GameObject _particleSystem;
    private bool _canShock = false; //inidica si es la plataforma de shock activa
    private PlayerController2 _playerOnTop;



    private void Start()
    {
        _particleSystem.SetActive(false);
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
        if (_canShock && _playerOnTop != null)
            _playerOnTop.Die();
        if (_canShock == false)
        {
            _particleSystem.SetActive(false);
        }
        else if (_canShock)
        {
            _particleSystem.SetActive(true);
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


    public GameObject getParticle()
    {

    return _particleSystem; }
}

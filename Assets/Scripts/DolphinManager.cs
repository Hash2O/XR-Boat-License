using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinManager : MonoBehaviour
{
    [SerializeField] float _distanceMax;
    [SerializeField] float _distancePlayer;
    [SerializeField] GameObject _player;

    [SerializeField] float _vitesse;
    [SerializeField] float _vitesseTranquille;
    [SerializeField] float _vitesseRapide;

    [SerializeField] float _distanceMaxPoint;
    [SerializeField] float _distancePlay;

    [SerializeField] Animator _animator;

    //bool _coroutine;
    Vector3 _newPosition;
    bool _coroutinePlay;


    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _distancePlayer = Vector3.Distance(_player.transform.position, gameObject.transform.position);
        _newPosition = CreatePoint();
        _vitesse = _vitesseTranquille;
    }


    void Update()
    {
        /*
        _distancePlayer = Vector3.Distance(_player.transform.position, transform.position);

        if (_distancePlayer < _distanceMax)
        {

            MoveToObject(_player.transform.position); // je vais vers le player

            //_animator.SetBool("SpeedDown", false); // je joue l'anim rapiement
            _vitesse = _vitesseRapide; // vitesse rapide

            // l'ennemy attack**************
            if (_distancePlayer < _distancePlay)
            {
                _animator.SetBool("Play", true);
            }
            else
            {
                _animator.SetBool("Play", false);
            }

        }
        else
        */
        {
            float _distancePoint = Vector3.Distance(_newPosition, transform.position);
            if (_distancePoint < 1f)
            {
                _newPosition = CreatePoint();
            }
            MoveToObject(_newPosition); // 
        }
        
    }

    Vector3 CreatePoint()
    {

        Vector3 _pointPosition = new Vector3(Random.Range(-_distanceMaxPoint, _distanceMaxPoint) + gameObject.transform.position.x, gameObject.transform.position.y, Random.Range(-_distanceMaxPoint, _distanceMaxPoint) + gameObject.transform.position.z);
        return _pointPosition;

    }


    void MoveToObject(Vector3 _movTo)
    {

        float _distanceMoveTo = Vector3.Distance(_movTo, gameObject.transform.position);
        gameObject.transform.LookAt(_movTo);
        Vector3 _vectorMove = _movTo - gameObject.transform.position;
        gameObject.transform.position += _vectorMove.normalized * _vitesse;

    }

    /*
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _newPosition = CreatePoint();
        }

        if (other.gameObject.CompareTag("Player") && !_coroutinePlay)
        {
            StartCoroutine(PlayWithPlayerCoroutine());
        }
    }

    IEnumerator PlayWithPlayerCoroutine()
    {
        _coroutinePlay = true;
        //Code à venir
        yield return new WaitForSeconds(2f);
        _coroutinePlay = false;
    }
    */

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Dolphins !");
            WindSailingBoatManager _player = other.gameObject.GetComponent<WindSailingBoatManager>();
            _player.meteo = "Nous avons de la compagnie. Un groupe de dauphins croise notre route. Gardez le cap !";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[3]);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WindSailingBoatManager _player = other.gameObject.GetComponent<WindSailingBoatManager>();
            _player.meteo = "Félicitations, continuez comme ça !";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[5]);
        }
    }
}

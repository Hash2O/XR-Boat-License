using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseManager : MonoBehaviour
{
    //private float _speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Base de code pour animer la lumi�re du phare. A cibler sur un child du Lighthouse proprement dit
        //transform.Rotate(Vector3.up, _speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Approaching Lighthouse !");
            SailingWindSimulationManager _player = other.gameObject.GetComponent<SailingWindSimulationManager>();
            _player.meteo = "Nous approchons du phare. Veillez � bien rester en dehors de la zone d�limit�e par les bou�es, pour �viter les �tocs.";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[7]);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SailingWindSimulationManager _player = other.gameObject.GetComponent<SailingWindSimulationManager>();
            _player.meteo = "F�licitations, continuez comme �a !";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[5]);
        }
    }
}

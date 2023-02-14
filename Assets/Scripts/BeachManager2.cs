using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachManager2 : MonoBehaviour
{
    [SerializeField] float windBoost;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SailingWindSimulationManager _player = other.gameObject.GetComponent<SailingWindSimulationManager>();
            _player.windPower *= windBoost;
            _player.meteo = "Vous approchez d'une plage. Respectez la zone délimitée par les bouées et réduisez votre vitesse.";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[4]);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SailingWindSimulationManager _player = other.gameObject.GetComponent<SailingWindSimulationManager>();
            _player.windPower = _player.initialWindPower;
            _player.meteo = "Félicitations, continuez comme ça.";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[5]);
        }
    }
}

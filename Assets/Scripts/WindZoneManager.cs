using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneManager : MonoBehaviour
{
    [SerializeField] float windBoost;
    // Start is called before the first frame update
    void Start()
    {
        if(windBoost == 0f)
        {
            windBoost = 1f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("Go with the wind !");
            WindSailingBoatManager _player = other.gameObject.GetComponent<WindSailingBoatManager>();
            _player.windPower *= windBoost;
            _player.meteo = "Zone de grand vent, prudence";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[0]);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Entering dead calm zone.");
            WindSailingBoatManager _player = other.gameObject.GetComponent<WindSailingBoatManager>();
            _player.windPower = _player.initialWindPower;
            _player.meteo = "Zone de calme, restez vigilant";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[1]);
        }
    }

}

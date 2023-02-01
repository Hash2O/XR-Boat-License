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

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("Go with the wind !");
            WindSailingBoatManager _player = other.gameObject.GetComponent<WindSailingBoatManager>();
            _player.windPower *= windBoost;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Entering dead calm zone.");
            WindSailingBoatManager _player = other.gameObject.GetComponent<WindSailingBoatManager>();
            _player.windPower = _player.initialWindPower;
        }
    }

}

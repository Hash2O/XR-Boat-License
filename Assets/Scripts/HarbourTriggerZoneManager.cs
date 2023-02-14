using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarbourTriggerZoneManager : MonoBehaviour
{
    [SerializeField] SailingWindSimulationManager player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Quand le navire rentre au port, il utilise son moteur
        if(other.gameObject.tag == "Player")
        {
            if(player.isWindSailing == true)
            {
                player.isWindSailing = false;
                player.meteoText.SetText("Zone de la marina, affalez les voiles et manoeuvrez au moteur.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Quand le navire sort du port, il utilise ses voiles
        if (other.gameObject.tag == "Player")
        {
            if (player.isWindSailing == false)
            {
                player.isWindSailing = true;
                player.meteoText.SetText("Vous sortez de la marina. Si vous désirez faire le parcours, la première bouée est en direction du sud. Bon vent !");
            }
        }
    }
}

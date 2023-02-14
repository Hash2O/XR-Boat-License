using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoZoneManager : MonoBehaviour
{
    [SerializeField] GameObject arrowInfoZone;
    [SerializeField] Transform player;
    [SerializeField] SailingWindSimulationManager boat;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 50)
        {
            print("Point de passage atteint. Cap sur le suivant !");
            if(gameObject.name == "InfoZone")
            {
                boat.meteoText.SetText("F�licitations ! Prochaine bou�e, cap au Sud-Est");
            }
            else if (gameObject.name == "InfoZone 2")
            {
                boat.meteoText.SetText("F�licitations ! Prochaine bou�e, cap Est-Nord-Est");
            }
            else if (gameObject.name == "InfoZone 3")
            {
                boat.meteoText.SetText("F�licitations ! Prochaine bou�e, cap au Nord");
            }
            else if (gameObject.name == "InfoZone 4")
            {
                boat.meteoText.SetText("F�licitations ! Prochaine bou�e, cap Ouest-Nord-Ouest");
            }
            else if (gameObject.name == "InfoZone 5")
            {
                boat.meteoText.SetText("F�licitations ! Prochaine bou�e, cap au Sud-Ouest");
            }
            else if (gameObject.name == "InfoZone 6")
            {
                boat.meteoText.SetText("F�licitations, c'est la derni�re bou�e du parcours. La marina n'est plus tr�s loin !");
            }
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("Entering Info Zone");
            arrowInfoZone.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    
     //Si l'option setactive(false) n'est pas retenue, cette fonction permet de faire disparaitre la fleche
    //d�s que le bateau sort du trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Exiting Info Zone");
            arrowInfoZone.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    
}

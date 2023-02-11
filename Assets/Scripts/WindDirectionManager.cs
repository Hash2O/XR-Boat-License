using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindDirectionManager : MonoBehaviour
{
    Vector3 windDestination;
    Vector3 windOrigin;
    // Start is called before the first frame update
    void Start()
    {
        //windOrigin = Déterminer par la winddirection récupéré par APIManager (float compris entre 0.0f et 360.0f)

        //Centre de la map, et destination de l'addforce qui servira à simuler le vent
        windDestination = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

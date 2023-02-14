using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoZoneArrowManager : MonoBehaviour
{
    Vector3 hautBas;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y >= 47f)
        {
            hautBas = Vector3.forward;
        }
        else if (transform.position.y < 32f) 
        {
            hautBas = Vector3.back;
        }
        transform.Translate(hautBas * Time.deltaTime * speed);
    }

}

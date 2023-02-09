using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleEventManager : MonoBehaviour
{
    [SerializeField] ParticleSystem eventParticles;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("whaleEventEmission", 2f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void whaleEventEmission()
    {
        Instantiate(eventParticles, transform.position, transform.rotation);
        //print("Event in action");
    }
}

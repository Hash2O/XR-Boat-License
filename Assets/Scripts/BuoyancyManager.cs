using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyancyManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            Rigidbody objectRb = gameObject.GetComponent<Rigidbody>();
            objectRb.velocity = new Vector3(0,0,0);
        }
    }
}

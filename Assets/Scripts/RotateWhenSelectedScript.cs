using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWhenSelectedScript : MonoBehaviour
{
    public bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected == true)
        {
            StartCoroutine("OnRotate", 2f);
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        print("Trigger entered");
    }

    public void OnTriggerStay(Collider other)
    {
        transform.Rotate(Vector3.up, 5f * Time.deltaTime);
    }

    public void OnTriggerExit(Collider other)
    {
        print("Trigger exited");
    }

    public void OnRotate()
    {
        transform.Rotate(Vector3.up, 5f * Time.deltaTime);
    }

}

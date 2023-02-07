using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    [SerializeField] 
    private float _floatingSpeed;

    [SerializeField]
    private float _movingSpeed;

    [SerializeField]
    private float _turnSpeed;

    [SerializeField]
    private float _roulis;

    //private float horizontalInput;
    //private float verticalInput;

    private Vector3 hautBas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MonterDescendre();

        /*
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * _movingSpeed * verticalInput);
        transform.Rotate(Vector3.up, _turnSpeed * horizontalInput * Time.deltaTime);
        */
    }

    void MonterDescendre()
    {
        transform.Translate(hautBas * Time.deltaTime);
        if (transform.position.y <= -3.15f)
        {
            hautBas = new Vector3(0, _floatingSpeed, 0);
        }
        else if (transform.position.y > -2.9f)
        {
            hautBas = new Vector3(0, -_floatingSpeed, 0);
        }
    }

    IEnumerator Roulis()
    {
        yield return new WaitForSeconds(2.0f);
        print("gauche");
        transform.Rotate(Vector3.forward, _roulis * Time.deltaTime);
        yield return new WaitForSeconds(2.0f);
        print("droite");
        transform.Rotate(Vector3.forward, - _roulis * Time.deltaTime);
    }
}

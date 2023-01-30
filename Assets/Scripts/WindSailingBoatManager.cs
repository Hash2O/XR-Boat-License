using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindSailingBoatManager : MonoBehaviour
{
    [SerializeField]
    private float _floatingSpeed;

    //[SerializeField]
    //private float _movingSpeed;

    [SerializeField]
    private float _turnSpeed = 15;

    [SerializeField]
    private float _roulis;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 hautBas;

    private Rigidbody boatRb;
    [SerializeField] private float windPower = 0;

    [SerializeField] TextMeshProUGUI speedometerText;
    private float speed;

    [SerializeField] GameObject centerOfMass;
    

    // Start is called before the first frame update
    void Start()
    {
        boatRb = GetComponent<Rigidbody>();
        boatRb.centerOfMass = centerOfMass.transform.position;
    }

    private void Update()
    {
        //Speedometer
        speed = Mathf.Round(boatRb.velocity.magnitude * 3.6f); //2.237 for mph
        speedometerText.SetText("Vitesse : " + speed + " km/h");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MonterDescendre();

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.forward * Time.deltaTime * _movingSpeed * verticalInput);
        boatRb.AddRelativeForce(Vector3.forward * windPower * verticalInput);
        transform.Rotate(Vector3.up, _turnSpeed * horizontalInput * Time.deltaTime);
    }

    void MonterDescendre()
    {
        transform.Translate(hautBas * Time.deltaTime);
        if (transform.position.y <= -3.75f)
        {
            hautBas = new Vector3(0, _floatingSpeed, 0);
        }
        else if (transform.position.y > -3.35f)
        {
            hautBas = new Vector3(0, -_floatingSpeed, 0);
        }
    }
}

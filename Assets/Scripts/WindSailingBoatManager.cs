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


    [SerializeField]
    APIManager apiManager;
    float windSpeed;
    
    private float horizontalInput;
    private float verticalInput;

    private Vector3 hautBas;

    private Rigidbody boatRb;
    public float windPower;

    public float initialWindPower = 15000f;

    [SerializeField] public TextMeshProUGUI meteoText;
    public string meteo;

    [SerializeField] TextMeshProUGUI speedometerText;
    private float speed;

    [SerializeField] GameObject centerOfMass;

    [SerializeField] public AudioSource audioSource;
    [SerializeField] public List<AudioClip> recommandations;
    

    // Start is called before the first frame update
    void Start()
    {
        windPower = initialWindPower;
        boatRb = GetComponent<Rigidbody>();
        boatRb.centerOfMass = centerOfMass.transform.position;

        //_flottaison = 0.2f;

        meteo = "Rien à signaler";
        meteoText.SetText(meteo);

        if(apiManager != null)
        {
            windSpeed = apiManager.affichageVitesseVent;
            print("Vitesse du vent récupérée via l'API meteo : " + windSpeed);
        }
        else
        {
            print("pas de données météo disponibles");
        }
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
        if (transform.position.y <= - 3.8f)//transform.position.y - _flottaison)
        {
            hautBas = new Vector3(0, _floatingSpeed, 0);
        }
        else if (transform.position.y > - 3.4f) //transform.position.y + _flottaison)
        {
            hautBas = new Vector3(0, -_floatingSpeed, 0);
        }
    }
}

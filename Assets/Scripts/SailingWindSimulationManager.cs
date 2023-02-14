using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SailingWindSimulationManager : MonoBehaviour
{
    [SerializeField]
    private float floatingSpeed;   //Vitesse pour monter descendre pour simuler l'effet de la houle

    private Vector3 hautBas;    //Axe lelong duquel le navire monte et descend

    [SerializeField]
    private float _roulis;  //Angle et vitesse de roulis du navire quand il tourne

    //Inputs pour gérer le déplacement du bateau
    [SerializeField]
    private float turnSpeed = 15;  //Pour faire virer le bateau à une allure 
    private float horizontalInput;
    private float forwardInput;

    [SerializeField]
    OpenMeteoAPIManager apiManager; //Pour récupérer les infos de l'API météto

    //float windSpeed;

    //Pour l'affichage en temps réel, sur le bateau, des données importantes :
    //Provenance du vent, orientation du navire par rapport à elle et allure adoptée
    [SerializeField] TextMeshProUGUI affichageDirection;
    [SerializeField] TextMeshProUGUI affichageVitesse;

    //Variable qui stocke la puissance fournie par le vent pendant la navigation
    public float windPower;

    //Variable qui va de 0 à 360. Stocke la valeur donnée par l'API via winddirection
    //Sinon fixée à 0f (ce qui donne un vent venant du nord, par défaut)
    public float windDirection;

    //Permet de déterminer si le bateau avance à la voile ou pas. 
    //Influence le type de comportement adopté par le bateau pendant la navigation
    public bool isWindSailing;

    //Variable essentielle qui représente la direction prise par le bateau
    //son calcul actualisé permet de déterminer comment se situe le bateau par rapport au vent
    public float direction;

    //Permet l'affichage des données fournies par l'API météo depuis le format JSON
    public TextMeshProUGUI affichageMeteo;
 
    //Puissance de l'impulsion permettant de déplacer le navire autour de 25 km/h;
    public float initialWindPower = 15000f;

    //Variable mal nommée. Affiche en texte (FR) les indications et conseils de la monitrice
    [SerializeField] public TextMeshProUGUI meteoText;
    public string meteo;

    //Gérer l'affichage de la vitesse actuelle du bateau
    [SerializeField] TextMeshProUGUI speedometerText;
    private float speed;

    //physique du jeu. Permet d'équilibrer le bateau dans les virages notamment
    [SerializeField] GameObject centerOfMass;
    private Rigidbody boatRb;

    //Les conseils et remarques prodiguées par la navigatrice/monitrice
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public List<AudioClip> recommandations;

    // Start is called before the first frame update
    void Start()
    {
        //Ici, on tire partie du moteur physique du jeu, qui permet de donner une masse
        //et de l'inertie au bateau, pour accroitre le "réalisme" de la simulation
        windPower = initialWindPower;
        boatRb = GetComponent<Rigidbody>();
        boatRb.centerOfMass = centerOfMass.transform.position;

        meteo = "Rien à signaler";
        meteoText.SetText(meteo);

        //Par défaut, si aucune donnée n'est reçue de l'API, on fixe la provenance du vent au Nord
        windDirection = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Speedometer
        speed = Mathf.Round(boatRb.velocity.magnitude * 3.6f); //2.237 for mph
        speedometerText.SetText("Vitesse : " + speed + " km/h");

        //Utilisation d'un booleen pour des/activer la navigation à la voile
        //La valeur fournie ici permet de simuler un déplacement du navire avec son moteur
        if (isWindSailing == false)
        {
            //Simulation de la vitesse fournie par le moteur du navire (sans vent donc)
            windPower = initialWindPower;
        }
        else
        {
            //Ici, on prend en compte la présence, et donc l'orientation du vent
            //Cette variable va se modifier en fonction de la donnée winddirection de l'API
            float modificateurDirection;

            //Calcul de la variable qui détermine la provenance du vent
            if (0 <= transform.localEulerAngles.y && transform.localEulerAngles.y < windDirection)
            {
                modificateurDirection = 360 - windDirection;
                direction = transform.localEulerAngles.y + modificateurDirection;
                affichageDirection.SetText("Direction du navire : " + direction);
            }
            else
            {
                modificateurDirection = windDirection;
                direction = transform.localEulerAngles.y - modificateurDirection;
                affichageDirection.SetText("Direction du navire : " + direction);
            }


            //Tester l'orientation du navire pour déterminer sa vitesse, selon qu'il soit plus ou moins face au vent
            if (0 < direction && direction < 15f)
            {
                windPower = 0f;
                affichageVitesse.SetText("Face au vent. \n Vitesse : " + speed);
            }
            else if (15f <= direction && direction <= 90f)
            {
                windPower = initialWindPower * 0.9f;
                affichageVitesse.SetText("Au près. Vitesse : " + speed);
            }
            else if (90f < direction && direction <= 135f)
            {
                windPower = initialWindPower;
                affichageVitesse.SetText("Vent de travers. Vitesse : " + speed);
            }
            else if (135f < direction && direction < 165f)
            {
                windPower = initialWindPower * 1.5f;
                affichageVitesse.SetText("Grand largue. Vitesse : " + speed);
            }
            else if (165f <= direction && direction <= 195f)
            {
                windPower = initialWindPower * 2f;
                affichageVitesse.SetText("Vent arrière. Vitesse : " + speed);
            }
            else if (195f < direction && direction <= 235f)
            {
                windPower = initialWindPower * 1.5f;
                affichageVitesse.SetText("Grand largue. Vitesse : " + speed);
            }
            else if (235f < direction && direction <= 270f)
            {
                windPower = initialWindPower;
                affichageVitesse.SetText("Vent de travers. Vitesse : " + speed);
            }
            else if (270f < direction && direction <= 345f)
            {
                windPower = initialWindPower * 0.9f;
                affichageVitesse.SetText("Au près. Vitesse : " + speed);
            }
            else if (direction > 345f && direction <= 0)
            {
                windPower = 0f;
                affichageVitesse.SetText("Face au vent. \n Vitesse : " + speed);
            }
        }

    }

    void FixedUpdate()
    {
        //Essentiel pour que le navire reste à flot pendant la simulation
        MonterDescendre();

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //Pas de marche arrière en voilier
        if(forwardInput < 0)
        {
            print("pas de marche arrière");
            forwardInput = 0;
        }

        boatRb.AddRelativeForce(Vector3.forward * windPower * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

    }

    //Méthodes pour activer/désactiver le mode voile ou moteur pour faire avancer le bateau
    public void OnWindSailing()
    {
        isWindSailing = true;
    }

    public void OnMotorBoat()
    {
        isWindSailing = false;
    }

    
    void MonterDescendre()
    {
        transform.Translate(hautBas * Time.deltaTime);
        if (transform.position.y <= -3.8f)//transform.position.y - _flottaison)
        {
            hautBas = new Vector3(0, floatingSpeed, 0);
        }
        else if (transform.position.y > -3.4f) //transform.position.y + _flottaison)
        {
            hautBas = new Vector3(0, -floatingSpeed, 0);
        }
    }
    
}

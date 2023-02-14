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

    //Inputs pour g�rer le d�placement du bateau
    [SerializeField]
    private float turnSpeed = 15;  //Pour faire virer le bateau � une allure 
    private float horizontalInput;
    private float forwardInput;

    [SerializeField]
    OpenMeteoAPIManager apiManager; //Pour r�cup�rer les infos de l'API m�t�to

    //float windSpeed;

    //Pour l'affichage en temps r�el, sur le bateau, des donn�es importantes :
    //Provenance du vent, orientation du navire par rapport � elle et allure adopt�e
    [SerializeField] TextMeshProUGUI affichageDirection;
    [SerializeField] TextMeshProUGUI affichageVitesse;

    //Variable qui stocke la puissance fournie par le vent pendant la navigation
    public float windPower;

    //Variable qui va de 0 � 360. Stocke la valeur donn�e par l'API via winddirection
    //Sinon fix�e � 0f (ce qui donne un vent venant du nord, par d�faut)
    public float windDirection;

    //Permet de d�terminer si le bateau avance � la voile ou pas. 
    //Influence le type de comportement adopt� par le bateau pendant la navigation
    public bool isWindSailing;

    //Variable essentielle qui repr�sente la direction prise par le bateau
    //son calcul actualis� permet de d�terminer comment se situe le bateau par rapport au vent
    public float direction;

    //Permet l'affichage des donn�es fournies par l'API m�t�o depuis le format JSON
    public TextMeshProUGUI affichageMeteo;
 
    //Puissance de l'impulsion permettant de d�placer le navire autour de 25 km/h;
    public float initialWindPower = 15000f;

    //Variable mal nomm�e. Affiche en texte (FR) les indications et conseils de la monitrice
    [SerializeField] public TextMeshProUGUI meteoText;
    public string meteo;

    //G�rer l'affichage de la vitesse actuelle du bateau
    [SerializeField] TextMeshProUGUI speedometerText;
    private float speed;

    //physique du jeu. Permet d'�quilibrer le bateau dans les virages notamment
    [SerializeField] GameObject centerOfMass;
    private Rigidbody boatRb;

    //Les conseils et remarques prodigu�es par la navigatrice/monitrice
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public List<AudioClip> recommandations;

    // Start is called before the first frame update
    void Start()
    {
        //Ici, on tire partie du moteur physique du jeu, qui permet de donner une masse
        //et de l'inertie au bateau, pour accroitre le "r�alisme" de la simulation
        windPower = initialWindPower;
        boatRb = GetComponent<Rigidbody>();
        boatRb.centerOfMass = centerOfMass.transform.position;

        meteo = "Rien � signaler";
        meteoText.SetText(meteo);

        //Par d�faut, si aucune donn�e n'est re�ue de l'API, on fixe la provenance du vent au Nord
        windDirection = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Speedometer
        speed = Mathf.Round(boatRb.velocity.magnitude * 3.6f); //2.237 for mph
        speedometerText.SetText("Vitesse : " + speed + " km/h");

        //Utilisation d'un booleen pour des/activer la navigation � la voile
        //La valeur fournie ici permet de simuler un d�placement du navire avec son moteur
        if (isWindSailing == false)
        {
            //Simulation de la vitesse fournie par le moteur du navire (sans vent donc)
            windPower = initialWindPower;
        }
        else
        {
            //Ici, on prend en compte la pr�sence, et donc l'orientation du vent
            //Cette variable va se modifier en fonction de la donn�e winddirection de l'API
            float modificateurDirection;

            //Calcul de la variable qui d�termine la provenance du vent
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


            //Tester l'orientation du navire pour d�terminer sa vitesse, selon qu'il soit plus ou moins face au vent
            if (0 < direction && direction < 15f)
            {
                windPower = 0f;
                affichageVitesse.SetText("Face au vent. \n Vitesse : " + speed);
            }
            else if (15f <= direction && direction <= 90f)
            {
                windPower = initialWindPower * 0.9f;
                affichageVitesse.SetText("Au pr�s. Vitesse : " + speed);
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
                affichageVitesse.SetText("Vent arri�re. Vitesse : " + speed);
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
                affichageVitesse.SetText("Au pr�s. Vitesse : " + speed);
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
        //Essentiel pour que le navire reste � flot pendant la simulation
        MonterDescendre();

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //Pas de marche arri�re en voilier
        if(forwardInput < 0)
        {
            print("pas de marche arri�re");
            forwardInput = 0;
        }

        boatRb.AddRelativeForce(Vector3.forward * windPower * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

    }

    //M�thodes pour activer/d�sactiver le mode voile ou moteur pour faire avancer le bateau
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;


public class APIManager : MonoBehaviour
{
    //[SerializeField] 
    string requestURL;
    string queryString;
    //Parametres pour URL
    //[SerializeField] string parameterName;
    //[SerializeField] string parameterValue;

    [SerializeField] List<string> parameterNames;
    [SerializeField] List<string> parameterValues;

    [SerializeField] Button weatherReportButton;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] TextMeshProUGUI marinaM�t�oText;

    public float affichageTemp�rature;
    public float affichageVitesseVent;
    public float DirectionVent;
    string affichageDirectionVent;

    [SerializeField] WindSailingBoatManager infoMeteo;

    // Start is called before the first frame update
    private void Start()
    {
        //Par d�faut, la m�t�o sera celle de La Rochelle, sauf choix diff�rent dans le jeu
        //CallURL("https://api.open-meteo.com/v1/forecast?latitude=46.16&longitude=-1.15&current_weather=true");
        
    }


    public void CallURL(string requestURL)
    {
        //StartCoroutine(GetRequest(requestURL);

        //Version simplifi�e
        //queryString = parameterName + "=" + parameterValue;

        //Version avec list
        //NB : v�rifier ici qu'il y autant de noms que de valeurs, sinon �a plante
        for(int i = 0; i < parameterNames.Count; i++)
        {
            queryString += parameterNames[i] + "=" + parameterValues[i] + "&";
        }

        StartCoroutine(GetRequest(requestURL + "?" + queryString)); //Format de la demande avec queryString
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Envoi de la requ�te
            yield return webRequest.SendWebRequest();

            // V�rification des erreurs
            if (webRequest.isNetworkError)  //Obsolete. Remplacer par UnityWebRequest.result == UnityWebRequest.Result.ConnectionError mais .result g�n�re une autre erreur
            {
                Debug.Log("Error: " + webRequest.error);
                //Changer l'affichage sur le bouton
                buttonText.SetText("Error: " + webRequest.error);
            }
            else
            {
                // Affichage du r�sultat
                HandleResponse(webRequest);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleResponse(UnityWebRequest webRequest)
    {
        Debug.Log("Received: " + webRequest.downloadHandler.text);
        //Changer l'affichage sur le bouton
        //buttonText.SetText("Received: " + webRequest.downloadHandler.text);

        //Traitement de donn�es au format JSON
        //NB : Le parsing est ton ami
        //On r�cup�re les donn�es au format JSON et on les met au format de la classe voulue,
        ////ici APIResponse. On le stocke dans une variable
        var apiResponse = JsonUtility.FromJson<APIResponse>(webRequest.downloadHandler.text);
        Debug.Log("APIResponse, temp�rature demand�e : " + apiResponse.current_weather.temperature);
        affichageTemp�rature = apiResponse.current_weather.temperature;
        affichageVitesseVent = apiResponse.current_weather.windspeed;
        DirectionVent = apiResponse.current_weather.winddirection;

        //Gestion de l'affichage de la direction du vent
        if (0 < DirectionVent && DirectionVent < 22.5f)
        {
            affichageDirectionVent = "Nord";
        }
        else if ( 22.6f < DirectionVent && DirectionVent < 45f)
        {
            affichageDirectionVent = "Nord Nord Est - Aliz�";
        }
        else if (45.1f < DirectionVent && DirectionVent < 67.5f)
        {
            affichageDirectionVent = "Nord Est - Aliz�";
        }
        else if (67.6f < DirectionVent && DirectionVent < 90f)
        {
            affichageDirectionVent = "Est - Aliz�";
        }
        else if (90.1f < DirectionVent && DirectionVent < 112.5f)
        {
            affichageDirectionVent = "Est Sud Est";
        }
        else if (112.6f < DirectionVent && DirectionVent < 135f)
        {
            affichageDirectionVent = "Sud Est";
        }
        else if (135.1f < DirectionVent && DirectionVent < 157.5f)
        {
            affichageDirectionVent = "Sud Sud Est";
        }
        else if (157.6f < DirectionVent && DirectionVent < 180f)
        {
            affichageDirectionVent = "Sud";
        }
        else if (180.1f < DirectionVent && DirectionVent < 202.5f)
        {
            affichageDirectionVent = "Sud Sud Ouest";
        }
        else if (202.6f < DirectionVent && DirectionVent < 225f)
        {
            affichageDirectionVent = "Sud Ouest";
        }
        else if (225.1f < DirectionVent && DirectionVent < 247.5f)
        {
            affichageDirectionVent = "Ouest Sud Ouest";
        }
        else if (247.6f < DirectionVent && DirectionVent < 270f)
        {
            affichageDirectionVent = "Ouest";
        }
        else if (270.1f < DirectionVent && DirectionVent < 292.5f)
        {
            affichageDirectionVent = "Nord Ouest";
        }
        else if (292.6f < DirectionVent && DirectionVent < 315f)
        {
            affichageDirectionVent = "Nord Nord Ouest";
        }
        else if (315.1f < DirectionVent && DirectionVent < 337.5f)
        {
            affichageDirectionVent = "Nord Nord Ouest";
        }
        else if (337.6f < DirectionVent && DirectionVent < 360f)
        {
            affichageDirectionVent = "Nord";
        }

        //Affichage des donn�es dans la marina
        marinaM�t�oText.SetText("Temp�rature actuelle : " + affichageTemp�rature.ToString() +
                        "�C \nVitesse du vent : " + affichageVitesseVent.ToString() +
                        "km/h \n Direction du vent : " + affichageDirectionVent);

        //Affichage sur le navire
        infoMeteo.meteoText.SetText("Direction du vent: " + affichageDirectionVent);

        //R�cup�ration de la vitesse du vent pour g�n�rer un modificateur de vitesse du navire
        float windPowerModifier = affichageVitesseVent / 20f; //Cr�er une variable s�rialis�e pour g�rer le bonus d�sir�

        //Affichage sur l'�cran du navire du modificateur en cours
        infoMeteo.meteoText.SetText("Wind Power Modifier : " + windPowerModifier);

        //Application du modif sur la vitesse de base.
        //NB : chaque appel cr�e une nouvelle valeur qui s'ajoute � la pr�c�dente...
        //Rem�dier � ca : 
        infoMeteo.windPower = infoMeteo.windPower * windPowerModifier;

    }
}

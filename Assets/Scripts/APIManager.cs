using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;


public class APIManager : MonoBehaviour
{
    [SerializeField] string requestURL;
    string queryString;
    //Parametres pour URL
    //[SerializeField] string parameterName;
    //[SerializeField] string parameterValue;

    [SerializeField] List<string> parameterNames;
    [SerializeField] List<string> parameterValues;

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI buttonText;
    // Start is called before the first frame update
    private void Start()
    {
        CallURL();
    }


    public void CallURL()
    {
        //StartCoroutine(GetRequest(requestURL);

        //Version simplifiée
        //queryString = parameterName + "=" + parameterValue;

        //Version avec list
        //NB : vérifier ici qu'il y autant de noms que de valeurs, sinon ça plante
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
            // Envoi de la requête
            yield return webRequest.SendWebRequest();

            // Vérification des erreurs
            if (webRequest.isNetworkError)  //Obsolete. Remplacer par UnityWebRequest.result == UnityWebRequest.Result.ConnectionError mais .result génère une autre erreur
            {
                Debug.Log("Error: " + webRequest.error);
                //Changer l'affichage sur le bouton
                buttonText.SetText("Error: " + webRequest.error);
            }
            else
            {
                // Affichage du résultat
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

        /*
        //Changer la couleur de l'image (ne fonctionne pas)
        image = buttonText.transform.parent.GetComponent<Image>();
        Color couleur;
        ColorUtility.TryParseHtmlString(webRequest.downloadHandler.text, out couleur);
        image.color = couleur;
        */

        //Traitement de données au format JSON
        //NB : Le parsing est ton ami
        //On récupère les données au format JSON et on les met au format de la classe voulue,
        ////ici APIResponse. On le stocke dans une variable
        var apiResponse = JsonUtility.FromJson<APIResponse>(webRequest.downloadHandler.text);
        Debug.Log("APIResponse, température demandée : " + apiResponse.current_weather.temperature);
        var affichage = apiResponse.current_weather.temperature;
        buttonText.SetText(affichage.ToString());

    }
}

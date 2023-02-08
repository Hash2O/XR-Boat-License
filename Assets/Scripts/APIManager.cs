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

        /*
        //Changer la couleur de l'image (ne fonctionne pas)
        image = buttonText.transform.parent.GetComponent<Image>();
        Color couleur;
        ColorUtility.TryParseHtmlString(webRequest.downloadHandler.text, out couleur);
        image.color = couleur;
        */

        //Traitement de donn�es au format JSON
        //NB : Le parsing est ton ami
        //On r�cup�re les donn�es au format JSON et on les met au format de la classe voulue,
        ////ici APIResponse. On le stocke dans une variable
        var apiResponse = JsonUtility.FromJson<APIResponse>(webRequest.downloadHandler.text);
        Debug.Log("APIResponse, temp�rature demand�e : " + apiResponse.current_weather.temperature);
        var affichage = apiResponse.current_weather.temperature;
        buttonText.SetText(affichage.ToString());

    }
}

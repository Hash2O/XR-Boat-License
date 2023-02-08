using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BasicAPIManager : MonoBehaviour
{
    [SerializeField] string requestURL;
    string queryString;
    //Parametres pour URL
    [SerializeField] string parameterName;
    [SerializeField] string parameterValue;

    [SerializeField] List<string> parameterNames;
    [SerializeField] List<string> parameterValues;
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
        for (int i = 0; i < parameterNames.Count; i++)
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
            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                // Affichage du résultat
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

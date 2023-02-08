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

        //Version simplifi�e
        //queryString = parameterName + "=" + parameterValue;

        //Version avec list
        //NB : v�rifier ici qu'il y autant de noms que de valeurs, sinon �a plante
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
            // Envoi de la requ�te
            yield return webRequest.SendWebRequest();

            // V�rification des erreurs
            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                // Affichage du r�sultat
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

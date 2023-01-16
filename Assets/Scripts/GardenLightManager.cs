using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenLightManager : MonoBehaviour
{
    private Transform[] _children;
    // Start is called before the first frame update
    void Start()
    {
        //On récupère les enfants du GO
        _children = transform.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleLights()
    {
        //On parcourt le tableau des enfants du GO
        //et on applique la condition à chaque fois
        foreach(var child in _children)
        {
            GameObject gm = child.gameObject;

            if (gm.activeSelf)
            {
                gm.SetActive(false);
            }
            else
            {
                gm.SetActive(true);
            }
        }

        
    }
}

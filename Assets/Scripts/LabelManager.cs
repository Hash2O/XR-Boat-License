using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text.text = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

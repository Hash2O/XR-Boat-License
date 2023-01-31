using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Compass : MonoBehaviour
{

    //URL Source : https://makeyourgame.fun/blog/unity/creer-une-boussole-horizontale-a-la-battle-royale

    [SerializeField] RawImage compass;
    [SerializeField] Transform sailboat;

    void Update()
    {
        compass.uvRect = new Rect(sailboat.localEulerAngles.y / 360f, 0, 1, 1);
    }

}

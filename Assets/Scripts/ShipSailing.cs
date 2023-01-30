using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSailing : MonoBehaviour
{
    public float waterLevel;
    public float floatHeight;
    public float bounceDamp;
    public Vector3 buoyancyCentreOffset;

    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 uplift;

    void Update()
    {
        actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
        forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);

        if (forceFactor > 0f)
        {
            uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
            GetComponent<Rigidbody>().AddForceAtPosition(uplift, actionPoint);
        }
    }
}

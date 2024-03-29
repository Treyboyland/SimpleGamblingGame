using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField]
    float degreesPerSecond;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, degreesPerSecond * Time.deltaTime);
    }
}

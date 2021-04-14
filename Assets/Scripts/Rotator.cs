using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField] float xAngle = 0;
    [SerializeField] float yAngle = 0.5f;
    [SerializeField] float zAngle = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xAngle, yAngle, zAngle);
    }
}

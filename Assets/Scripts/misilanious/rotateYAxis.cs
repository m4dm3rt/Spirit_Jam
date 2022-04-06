using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateYAxis : MonoBehaviour
{
    public float rotateSpeed = 90f;

    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * rotateSpeed);     
    }
}

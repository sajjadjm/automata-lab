using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    public GameObject text;
    public GameObject text2;
    
    void Update()
    {
        text.transform.eulerAngles = Vector3.forward * 0.0f;
        text2.transform.eulerAngles = Vector3.forward * 0.0f;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    public static FixRotation Instance;
    public List<GameObject> Texts = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        foreach (var text in Texts)
        {
            text.transform.eulerAngles = Vector3.forward * 0.0f;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNumber : MonoBehaviour
{
    public static StateNumber Instance;
    public GameObject [] number;
    public Transform [] field;

    private GameObject[] activeObject;
    public int numbers = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        activeObject = new GameObject[field.Length];
        SetValue(numbers);
    }
    
    public void Inc(int currentStateNumber)
    {
        Clear();
        if (numbers < 99)
        {
            numbers++;
        }
        SetValue(numbers + currentStateNumber);
    }
    
    public void Dec()
    {
        Clear();
        if (numbers > 0)
        {
            numbers--;
        }
        SetValue(numbers);
    }
 
    private void Clear()
    {
        for (int i = 0; i < field.Length; i++)
        {
            Destroy(activeObject[i]);
        }
    }

    private void SetValue(int numbers)
    {
        int convert = 1;
        for (int i = 0; i < field.Length; i++)
        {
            int numberConvert = (numbers / convert) % 10;
            Print(i, numberConvert, i);
            convert *= 10;
        }
    }

    private void Print(int activeObj, int numbers, int field)
    {
        this.activeObject [activeObj] =
            Instantiate(this.number[numbers], this.field[field].position, this.field[field].rotation);
        this.activeObject [activeObj].name = this.field[field].name;
        this.activeObject[activeObj].transform.parent = this.field[field];
    }
}

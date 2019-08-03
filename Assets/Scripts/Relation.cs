using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Relation
{
    public State startState;
    public State endState;
    public GameObject Arrow;
    public LineRenderer Line;
    public Vector3 startPoint;
    public Vector3 endPoint;
    public bool isRecursive;
    public bool isReturn;
    public bool isAdded;
    public char value;
    public int textPosMultiplier = 1;
    public int textIndex = 3;

    public Relation(State startS, State endS, GameObject arrow, LineRenderer line)
    {
        startState = startS;
        endState = endS;
        Arrow = arrow;
        Line = line;
    }

    public Relation(State startS, State endS)
    {
        startState = startS;
        endState = endS;
    }
}
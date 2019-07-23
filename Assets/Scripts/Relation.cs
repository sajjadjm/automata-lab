using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public char value;

    public Relation(State startS, State endS, GameObject arrow, LineRenderer line, Vector3 startP, Vector3 endP)
    {
        startState = startS;
        endState = endS;
        Arrow = arrow;
        Line = line;
        startPoint = startP;
        endPoint = endP;
    }

    public Relation(State startS, State endS, Vector3 startP, Vector3 endP)
    {
        startState = startS;
        endState = endS;
        startPoint = startP;
        endPoint = endP;
    }
}
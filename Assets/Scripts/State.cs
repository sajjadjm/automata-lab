using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State
{
    public string Name;
    public bool isStart;
    public bool isEnd;
    public int textPosMultiplier = 1;
    public GameObject stateGameObject;
    public Vector3 statePosition;
    public List<Relation> relatedInLines = new List<Relation>();
    public List<Relation> relatedOutLines = new List<Relation>();
    public List<TuringRelation> turRelatedInLines = new List<TuringRelation>();
    public List<TuringRelation> turRelatedOutLines = new List<TuringRelation>();

    public State(GameObject stateObj , string name)
    {
        stateGameObject = stateObj;
        Name = name;
    }
}

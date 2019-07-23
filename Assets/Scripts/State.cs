﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public string Name;
    public bool isStart;
    public bool isEnd;
    public GameObject stateGameObject;
    public List<Relation> relatedInLines = new List<Relation>();
    public List<Relation> relatedOutLines = new List<Relation>();
    public List<TuringRelation> turRelatedInLines = new List<TuringRelation>();
    public List<TuringRelation> turRelatedOutLines = new List<TuringRelation>();

    public State(GameObject stateObj, string name)
    {
        stateGameObject = stateObj;
        Name = name;
    }
}
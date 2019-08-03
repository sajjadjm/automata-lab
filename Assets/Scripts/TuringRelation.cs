using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TuringRelation : Relation
{
    public char valueToChange;
    public bool isRight = false;
    
    public TuringRelation(State startS, State endS, GameObject arrow, LineRenderer line)
        : base(startS, endS, arrow, line) {}
    
    public TuringRelation(State startS, State endS)
        : base(startS, endS) {}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuringRelation : Relation
{
    public char valueToChange;
    public bool isRight = false;
    
    public TuringRelation(State startS, State endS, GameObject arrow, LineRenderer line, Vector3 startP, Vector3 endP)
        : base(startS, endS, arrow, line, startP, endP) {}
    
    public TuringRelation(State startS, State endS, Vector3 startP, Vector3 endP)
        : base(startS, endS, startP, endP) {}
}
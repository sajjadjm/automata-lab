using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TuringBrain : MonoBehaviour
{
    public static TuringBrain Instance;
    public InputField input;
    public bool Accepted = true;
    public List<State> Steps = new List<State>();
    public List<TuringRelation> Rels = new List<TuringRelation>();
    public string inputValue = "";

    private void Awake()
    {
        Instance = this;
    }

    public void Solve()
    { 
        State startState = null;
        Tape.Instance.counter = 0;
        TuringRelation rel = null;
        GameObject startSector = Tape.Instance.startSector;
        
        foreach (var s in Steps)
        {
            s.stateGameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        
        foreach (var s in Tape.Instance.Sectors)
        {
            s.transform.Find("Text").GetComponent<TextMeshPro>().text = "#";
        }
        
        Steps.Clear();
        Rels.Clear();
        inputValue = input.text;

        for (int i = 0; i < inputValue.Length; i++)
        {
            Tape.Instance.Sectors[7 + i].transform.Find("Text").GetComponent<TextMeshPro>().text = inputValue[i].ToString();
        }
        
        foreach (var s in DrawState.Instance.States)
        {
            if (s.Name == "StartState")
            {
                startState = s;
            }
        }

        for (int i = 0 ;; i++)
        {
            Steps.Add(startState);
            

            if (startState.relatedOutLines.Count == 0)
            {
                if (!startState.isEnd)
                {
                    Accepted = false;
                }

                break;
            }

            foreach (var r in startState.relatedOutLines)
            {
                Debug.Log(r.value);
                Debug.Log(startSector.transform.Find("Text").GetComponent<TextMeshPro>().text[0]);
                if (r.value == startSector.transform.Find("Text").GetComponent<TextMeshPro>().text[0])
                {
                    rel = (TuringRelation)r;
                    startState = rel.endState;
                    Rels.Add(rel);
                }
            }

            if (rel == null)
            {
                if (!rel.startState.isEnd)
                {
                    Accepted = false;
                }
                
                break;
            }
            
            if (rel.isRight)
            {
                startSector = Tape.Instance.Sectors[Tape.Instance.Sectors.IndexOf(startSector) + 1];
                startSector.transform.Find("Text").GetComponent<TextMeshPro>().text = rel.valueToChange.ToString();
            }
            
            else
            {
                startSector = Tape.Instance.Sectors[Tape.Instance.Sectors.IndexOf(startSector) - 1];
                startSector.transform.Find("Text").GetComponent<TextMeshPro>().text = rel.valueToChange.ToString();
            }
        }

        Debug.Log(Accepted);
        Accepted = true;
        Tape.Instance.counter = 0;
    }
}

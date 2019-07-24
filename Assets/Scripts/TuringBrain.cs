using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TuringBrain : MonoBehaviour
{
    public static TuringBrain Instance;
    public InputField input;
    public bool Accepted = true;
    public List<State> Steps = new List<State>();
    public List<bool> CurserMoves = new List<bool>();
    public List<TuringRelation> Rels = new List<TuringRelation>();
    public string inputValue = "";
    private State startState;

    private void Awake()
    {
        Instance = this;
    }

    public void Solve()
    {
        Tape.Instance.counter = 0;
        TuringRelation rel = null;
        
        foreach (var s in Steps)
        {
            s.stateGameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        
        foreach (var s in Tape.Instance.Sectors)
        {
            s.transform.Find("#").gameObject.SetActive(true);
            s.transform.Find("1").gameObject.SetActive(false);
            s.transform.Find("0").gameObject.SetActive(false);
        }
        
        Steps.Clear();
        Rels.Clear();
        inputValue = input.text;

        for (int i = 0; i < inputValue.Length; i++)
        {
            if (TuringBrain.Instance.inputValue[i] == '1')
            {
                Tape.Instance.Sectors[7 + i].transform.Find("1").gameObject.SetActive(true);
                Tape.Instance.Sectors[7 + i].transform.Find("0").gameObject.SetActive(false);
                Tape.Instance.Sectors[7 + i].transform.Find("#").gameObject.SetActive(false);
            }
            
            else if (TuringBrain.Instance.inputValue[i] == '0')
            {
                Tape.Instance.Sectors[7 + i].transform.Find("0").gameObject.SetActive(true);
                Tape.Instance.Sectors[7 + i].transform.Find("1").gameObject.SetActive(false);
                Tape.Instance.Sectors[7 + i].transform.Find("#").gameObject.SetActive(false);
            }
            
            else if (TuringBrain.Instance.inputValue[i] == '#')
            {
                Tape.Instance.Sectors[7 + i].transform.Find("#").gameObject.SetActive(true);
                Tape.Instance.Sectors[7 + i].transform.Find("1").gameObject.SetActive(false);
                Tape.Instance.Sectors[7 + i].transform.Find("0").gameObject.SetActive(false);
            }
        }
        
        foreach (var s in DrawState.Instance.States)
        {
            if (s.Name == "StartState")
            {
                startState = s;
            }
        }

        for (int i = 0; i < inputValue.Length; i++)
        {
            Steps.Add(startState);

            if (startState.relatedOutLines.Count == 0)
            {
                Accepted = false;
                break;
            }

            foreach (var r in startState.relatedOutLines)
            {
                if (r.value == inputValue[i])
                {
                    rel = (TuringRelation)r;
                    Rels.Add(rel);
                    CurserMoves.Add(rel.isRight);
                }
            }

            if (rel == null)
            {
                Accepted = false;
                break;
            }
            
            if ((i == inputValue.Length - 1 || (inputValue.Length == 1 && i == 0)) && rel != null && !rel.endState.isEnd)
            {
                Accepted = false;
            }

            if (i == inputValue.Length - 1 && rel != null)
            {
                Steps.Add(rel.endState);
            }

            if (rel != null)
            {
                startState = rel.endState;
            }
        }

        Debug.Log(Accepted);

        input.text = "";
        Accepted = true;
    }
}

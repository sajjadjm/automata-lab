﻿using System;
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
    public Text acceptedText;
    public Text rejectedText;

    private int truthCounter = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInputOnTape()
    {
        Tape.Instance.canResetTape = true;
        Tape.Instance.counter = 0;
        
        foreach (var s in Steps)
        {
            if (s != null)
            {
                s.stateGameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
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
    }

    public void Solve()
    {
        Steps.Clear();
        State startState = null;
        TuringRelation rel = null;
        GameObject startSector = Tape.Instance.startSector;
        
        foreach (var s in DrawState.Instance.States)
        {
            if (s.Name == "StartState")
            {
                startState = s;
            }
        }

        for (;;)
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
                if (r.value == startSector.transform.Find("Text").GetComponent<TextMeshPro>().text[0])
                {
                    rel = (TuringRelation) r;
                    startState = rel.endState;
                    Rels.Add(rel);
                    truthCounter++;
                }
            }

            if (truthCounter == 0)
            {
                if (!startState.isEnd)
                {
                    Accepted = false;
                }
                break;
            }

            if (rel == null)
            {
                if (!startState.isEnd)
                {
                    Accepted = false;
                }
                break;
            }
            
            startSector.transform.Find("Text").GetComponent<TextMeshPro>().text = rel.valueToChange.ToString();

            if (rel.isRight)
            {
                startSector = Tape.Instance.Sectors[Tape.Instance.Sectors.IndexOf(startSector) + 1];
            }

            else
            {
                startSector = Tape.Instance.Sectors[Tape.Instance.Sectors.IndexOf(startSector) - 1];
            }

            truthCounter = 0;
        }
        Debug.Log(Accepted);
        
        if (Accepted)
        {
            rejectedText.gameObject.SetActive(false);
            acceptedText.gameObject.SetActive(true);
        }

        else
        {
            rejectedText.gameObject.SetActive(true);
            acceptedText.gameObject.SetActive(false);
        }
        
        Accepted = true;
        Tape.Instance.counter = 0;
    }
}

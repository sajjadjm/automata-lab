using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DFABrain : MonoBehaviour
{
    public static DFABrain Instance;
    public InputField input;
    public bool Accepted = true;
    public List<State> Steps = new List<State>();

    private string inputValue = "";
    private State startState;
    private State state;

    private void Awake()
    {
        Instance = this;
    }

    public void Solve()
    {
        Relation rel = null;
        Steps.Clear();
        inputValue = input.text;
        
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
                    rel = r;
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

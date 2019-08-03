using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiInputs : MonoBehaviour
{
    public static MultiInputs Instance;
    public GameObject InputFields;

    private void Awake()
    {
        Instance = this;
    }

    public void setInputFieldsActive()
    {
        InputFields.SetActive(true);
    }

    public void closeInputFields()
    {
        InputFields.SetActive(false);
    }

    public void multiSolve()
    {
        for (int i = 0; i <= 14; i++)
        {
            InputFields.transform.Find("Input" + i.ToString()).transform.Find("AcceptedText").gameObject.SetActive(false);
            InputFields.transform.Find("Input" + i.ToString()).transform.Find("RejectedText").gameObject.SetActive(false);
        }
        
        InputField input;
        bool accepted = true;
        for (int i = 0; i <= 14; i++)
        {
            input = InputFields.transform.Find("Input" + i.ToString()).transform.Find("InputField").GetComponent<InputField>();
            if (input.text != "")
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    accepted = Solve(input.text);   
                }
                
                else if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    accepted = TuringSolve(input.text);
                }

                if (accepted)
                {
                   InputFields.transform.Find("Input" + i.ToString()).transform.Find("AcceptedText").gameObject.SetActive(true);
                   InputFields.transform.Find("Input" + i.ToString()).transform.Find("RejectedText").gameObject.SetActive(false);
                }

                else
                {
                   InputFields.transform.Find("Input" + i.ToString()).transform.Find("AcceptedText").gameObject.SetActive(false);
                   InputFields.transform.Find("Input" + i.ToString()).transform.Find("RejectedText").gameObject.SetActive(true);
                }
            }
        }
    }
    
    bool Solve(string inputValue)
    {
        Relation rel = null;
        State startState = null;
        bool Accepted = true;

        foreach (var s in DrawState.Instance.States)
        {
            if (s.Name == "StartState")
            {
                startState = s;
            }
        }

        for (int i = 0; i < inputValue.Length; i++)
        {
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

            if (rel != null)
            {
                startState = rel.endState;
            }
        }
        
        return Accepted;
    }

    bool TuringSolve(string inputValue)
    {
        State startState = null;
        TuringRelation rel = null;
        GameObject startSector = Tape.Instance.startSector;
        bool Accepted = true;
        int truthCounter = 0;
        
        foreach (var s in DrawState.Instance.States)
        {
            if (s.Name == "StartState")
            {
                startState = s;
            }
        }
        
        for (int i = 0; i < inputValue.Length; i++)
        {
            Tape.Instance.Sectors[7 + i].transform.Find("Text").GetComponent<TextMeshPro>().text = inputValue[i].ToString();
        }

        for (;;)
        {
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

        return Accepted;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiInputs : MonoBehaviour
{
    public GameObject InputFields;
    
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
               accepted = Solve(input.text);
               
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSteps : MonoBehaviour
{
    private int counter = 0;
    
    public void ShowStep()
    {
        if (counter == DFABrain.Instance.Steps.Count - 1)
        {
            DFABrain.Instance.Steps[counter].stateGameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        else
        {
            DFABrain.Instance.Steps[counter].stateGameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        
        if (counter > 0 && DFABrain.Instance.Steps[counter - 1].stateGameObject.name != DFABrain.Instance.Steps[counter].stateGameObject.name)
        {
            DFABrain.Instance.Steps[counter - 1].stateGameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        counter++;
    }
}

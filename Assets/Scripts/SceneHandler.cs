using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void DFAButton()
    {
        SceneManager.LoadScene(1);
    }
    
    public void TuringButton()
    {
        if (WebRequest.Instance.validateFlag == 1)
        {
            SceneManager.LoadScene(2);
        }

        else
        {
            WebRequest.Instance.inputSerial.SetActive(true);
        }
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void closeValidation()
    {
        WebRequest.Instance.inputSerial.SetActive(false);
    }
}
